using GameLogic.Actions.ActionTemplates;
using GameLogic.Model;
using Net.Contexts.Night.ActionInfo;
using Net.Extensions;
using Net.Servers;

namespace Net.Manager.Night
{
    public class NightDetailFacade
    {
        private LANServer server;

        public NightDetailFacade(LANServer server)
        {
            this.server = server;
        }

        public void ResolveType(BaseDetail det)
        {
            switch(det.Type)
            {
                case ActionType.WITCH_CONTROL:
                {
                    SendWitch(det);
                    break;
                }
                case ActionType.DRIVER_SWAP:
                {
                    SendDriver(det);
                    break;
                }
                case ActionType.ESCORT_BLOCK:
                {
                    SendEscort(det);
                    break;
                }
                case ActionType.CULTUS_LEADER_RECRUIT:
                {
                    SendCultusRecruit(det);
                    break;
                }
                case ActionType.GODFATHER_RECRUIT:
                {
                    SendGodfatherRecruit(det);
                    break;
                }
                case ActionType.MAFIA_KILL:
                {
                    SendMafiaKill(det);
                    break;
                }
                case ActionType.VIGILANTE_KILL:
                {
                    SendVigilangeKill(det);
                    break;
                }
                case ActionType.SERIAL_KILLER_KILL:
                {
                    SendSerialKiller(det);
                    break;
                }
                case ActionType.HEAL:
                {
                    SendHealer(det);
                    break;
                }
                case ActionType.INVESTIGATE:
                {
                    SendInvestigate(det);
                    break;
                }
                case ActionType.DETECT:
                {
                    SendDetect(det);
                    break;
                }
                case ActionType.TERRORIST_BLOW:
                {
                    SendBlow(det);
                    break;
                }
                case ActionType.RESSURECT:
                {
                    SendRessurect(det);
                    break;
                }
            }
        }

        private void SendWitch(BaseDetail det)
        {
            //Witch has base detail
            //Show info only for executor and primary target

            var exmsg = new EWitchInfoContext();
            server.SendSessionMessage(exmsg, det.Executor.Id);

            var prmsg = new WitchInfoContext();
            server.SendSessionMessage(prmsg, det.PrimaryTarget.Id);
        }

        private void SendDriver(BaseDetail det)
        {
            //Driver has double detail
            //Show info only for targets
            DoubleDetail ddet = (DoubleDetail)det;

            var tmsg = new DriverInfoContext();
            server.SendSessionMessage(tmsg, ddet.PrimaryTarget.Id);
            server.SendSessionMessage(tmsg, ddet.SecondaryTarget.Id);
        }

        private void SendEscort(BaseDetail det)
        {
            //Escort has block detail
            //Show info only for target
            BlockDetail bdet = (BlockDetail)det;

            var tmsg = new BlockInfoContext(bdet.Info);
            server.SendSessionMessage(tmsg, bdet.PrimaryTarget.Id);
        }

        private void SendCultusRecruit(BaseDetail det)
        {
            //Cultus has recruit detail
            //Show info for executor and for target only if success
            RecruitDetail rdet = (RecruitDetail)det;

            var exmsg = new ECultusRecruitInfoContext(rdet.Info);
            server.SendSessionMessage(exmsg, det.Executor.Id);

            if(rdet.Info == RecruitInfo.ADD_NEW_MEMBER)
            {
                //Has success
                var prmsg = new CultusRecruitInfoContext();
                server.SendSessionMessage(prmsg, rdet.PrimaryTarget.Id);
            }
        }

        private void SendGodfatherRecruit(BaseDetail det)
        {
            //Godfather has recruit detail
            //Show info for executor and for target only if success
            RecruitDetail rdet = (RecruitDetail)det;

            var exmsg = new EGodfatherRecruitInfoContext(rdet.Info);
            server.SendSessionMessage(exmsg, rdet.Executor.Id);

            if(rdet.Info == RecruitInfo.ADD_NEW_MEMBER)
            {
                //Has success
                var prmsg = new GodfatherRecruitInfoContext();
                server.SendSessionMessage(prmsg, rdet.PrimaryTarget.Id);
            }
        }

        private void SendMafiaKill(BaseDetail det)
        {
            //Kill has kill detail
            //Show info to all
            KillDetail kdet = (KillDetail)det;

            var msg = new MafiaInfoContext(kdet.Info, false);
            if(kdet.Info == KillInfo.TARGET_IMMUNE)
                server.SendSessionMessage(msg, det.Executor.Id);
            else
            {
                server.BroadcastSessionMessage(msg, det.PrimaryTarget.Id);

                //Show the player that he was a target
                var tmsg = new MafiaInfoContext(kdet.Info, true);
                server.SendSessionMessage(tmsg, det.PrimaryTarget.Id);
            }
        }

        private void SendVigilangeKill(BaseDetail det)
        {
            //Kill has kill detail
            //Show info to all
            KillDetail kdet = (KillDetail)det;

            var msg = new VigilanteInfoContext(kdet.Info, false);
            if(kdet.Info == KillInfo.TARGET_IMMUNE)
                server.SendSessionMessage(msg, det.Executor.Id);
            else
            {
                server.BroadcastSessionMessage(msg, det.PrimaryTarget.Id);

                //Show the player that he was a target
                var tmsg = new VigilanteInfoContext(kdet.Info, true);
                server.SendSessionMessage(tmsg, det.PrimaryTarget.Id);
            }
        }

        private void SendSerialKiller(BaseDetail det)
        {
            //Kill has kill detail
            //Show info to all
            KillDetail kdet = (KillDetail)det;

            var msg = new SerialKillerInfoContext(kdet.Info, false);
            if(kdet.Info == KillInfo.TARGET_IMMUNE)
                server.SendSessionMessage(msg, det.Executor.Id);
            else
            {
                server.BroadcastSessionMessage(msg, det.PrimaryTarget.Id);

                //Show the player that he was a target
                var tmsg = new SerialKillerInfoContext(kdet.Info, true);
                server.SendSessionMessage(tmsg, det.PrimaryTarget.Id);
            }
        }

        private void SendHealer(BaseDetail det)
        {
            //Heal has base detail
            //Show info only for target

            var msg = new HealInfoContext(false);
            server.BroadcastSessionMessage(msg, det.PrimaryTarget.Id);

            //Show the player that he was a target
            var tmsg = new HealInfoContext(true);
            server.SendSessionMessage(tmsg, det.PrimaryTarget.Id);
        }

        private void SendInvestigate(BaseDetail det)
        {
            //Investigate has investigate detail
            //Show info only to executor
            InvestigateDetail idet = (InvestigateDetail)det;

            var exmsg = new EInvestigationInfoContext(idet.Info.IntoSignature());
            server.SendSessionMessage(exmsg, idet.Executor.Id);
        }

        private void SendDetect(BaseDetail det)
        {
            //Detect has detect detail
            //Show info only to executor
            DetectDetail ddet = (DetectDetail)det;

            var exmsg = new EDetectInfoContext(ddet.Info);
            server.SendSessionMessage(exmsg, ddet.Executor.Id);
        }

        private void SendBlow(BaseDetail det)
        {
            //Blow has blow detail
            //Show info to all
            //TODO: Probably to show info to victims

            var msg = new BlowInfoContext();
            server.BroadcastSessionMessage(msg);
        }

        private void SendRessurect(BaseDetail det)
        {
            //Ressurect has base detail
            //Show info to all

            var msg = new RessurectInfoContext(false);
            server.BroadcastSessionMessage(msg, det.PrimaryTarget.Id);

            //Show the player that he was a target
            var tmsg = new RessurectInfoContext(true);
            server.SendSessionMessage(tmsg, det.PrimaryTarget.Id);
        }
    }
}