using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Actions.ActionTemplates
{
    public class KillAndHealTemplate : ActionTemplate
    {
        public KillAndHealTemplate(IEnumerable<ActionLog> logs) : base(logs)
        {

        }

        public override IEnumerable<BaseDetail> Convert()
        {
            //Find all killers and healers
            var killheal = logs.Where(s =>
                {
                    if((s.Executor is MafiaRole
                    || s.Executor is VigilanteRole
                    || s.Executor is SerialKillerRole)
                    && !s.IsBlocked)
                        return true;
                    else if((s.Executor is DoctorRole
                    || s.Executor is SurgeonRole)
                    && s.Success)
                        return true;
                    else return false;
                });

            foreach(var action in killheal)
            {
                BaseDetail cursedCase = null;

                Player target = action.PrimaryTarget.Owner;
                Player executor = action.Executor.Owner;
                ActionType type = FindOutType(action.Executor);

                if(type == ActionType.HEAL)
                {
                    yield return new BaseDetail(type, executor, target);
                }
                else
                {
                    KillInfo info;
                    if(action.Success)
                    {
                        if(action.PrimaryTarget == action.Executor)
                        {
                            //Cause of killing self
                            info = FindReasonOfKillingSelf(action);
                        }
                        else
                        {
                            //Target was killed successfully
                            info = KillInfo.KILL;

                            //If victim was cursed
                            if(action.PrimaryTarget is CursedRole)
                            {
                                //add ressurect to order
                                cursedCase = new BaseDetail(ActionType.RESSURECT, target, target);
                            }
                        }
                    }
                    else
                    {
                        //Kill was not successful, probably it was immune target
                        info = KillInfo.TARGET_IMMUNE;
                    }

                    yield return new KillDetail(type, executor, target, info);

                    if(cursedCase != null)
                        yield return cursedCase;
                }
            }
        }

        private ActionType FindOutType(Role role)
        {
            switch(role)
            {
                case MafiaRole _:
                    return ActionType.MAFIA_KILL;
                case VigilanteRole _:
                    return ActionType.VIGILANTE_KILL;
                case SerialKillerRole _:
                    return ActionType.SERIAL_KILLER_KILL;
                case DoctorRole _:
                    return ActionType.HEAL;
                case SurgeonRole _:
                    return ActionType.HEAL;
                default:
                    throw new Exception("There is not a killer in the kill action.");
            }
        }

        private KillInfo FindReasonOfKillingSelf(ActionLog log)
        {
            if(logs.Any(s =>
            {
                if(s.Executor is WitchRole)
                {
                    //Witch always has double action
                    //and non-blockable
                    var witch = s as DoubleActionLog;
                    return witch.PrimaryTarget == witch.SecondaryTarget;
                }
                else return false;
            }))
            {
                return KillInfo.SUICIDE;
            }
            else if(logs.Any(s =>
            {
                if(s.Executor is DriverRole)
                {
                    //Driver always has double action
                    //and non-blockable
                    var driver = s as DoubleActionLog;
                    return driver.PrimaryTarget == log.Executor
                           || driver.SecondaryTarget == log.Executor;
                }
                else return false;
            }))
            {
                return KillInfo.DRIVER_ACCIDENT;
            }
            else return KillInfo.SUICIDE;
        }
    }

    public enum KillInfo
    {
        TARGET_IMMUNE,
        KILL,
        SUICIDE,
        DRIVER_ACCIDENT
    }
}