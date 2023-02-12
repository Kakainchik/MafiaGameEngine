using GameLogic.Actions.ActionTemplates;
using Net.Contexts.Night.ActionInfo;
using System.Windows.Documents;
using System.Windows.Media;
using WPFApplication.Extensions;
using WPFApplication.Model;
using WPFApplication.Resources.GameStoryText;

namespace WPFApplication.ViewModel.Game.Screen.Night
{
    public class ActionStoryFacade
    {
        private Paragraph story;

        public ActionStoryFacade(Paragraph paragraph)
        {
            story = paragraph;
        }

        public void ShowStory(NightInfoContext context)
        {
            //if(story.Inlines.Count > 3) story.Inlines.Clear();
            switch(context)
            {
                case EWitchInfoContext _:
                {
                    AddRun(NightResources.EWitchAct);
                    break;
                }
                case WitchInfoContext _:
                {
                    AddRun(NightResources.TWitchAct);
                    break;
                }
                case DriverInfoContext _:
                {
                    AddRun(NightResources.TDriverAct);
                    break;
                }
                case BlockInfoContext con:
                {
                    HandleBlock(con);
                    break;
                }
                case ECultusRecruitInfoContext con:
                {
                    HandleECultus(con);
                    break;
                }
                case CultusRecruitInfoContext _:
                {
                    AddRun(NightResources.TCultusLeaderAct);
                    break;
                }
                case EGodfatherRecruitInfoContext con:
                {
                    HandleEGodfather(con);
                    break;
                }
                case GodfatherRecruitInfoContext _:
                {
                    AddRun(NightResources.TGodfatherAct);
                    break;
                }
                case KillInfoContext con:
                {
                    HandleKill(con);
                    break;
                }
                case HealInfoContext con:
                {
                    HandleHeal(con);
                    break;
                }
                case EInvestigationInfoContext con:
                {
                    HandleEInvestigate(con);
                    break;
                }
                case EDetectInfoContext con:
                {
                    HandleEDetect(con);
                    break;
                }
                case BlowInfoContext _:
                {
                    AddRun(NightResources.TerroristAct);
                    break;
                }
                case RessurectInfoContext con:
                {
                    HandleRessurect(con);
                    break;
                }
            }
            story.Inlines.Add(new LineBreak());
        }

        private void HandleBlock(BlockInfoContext context)
        {
            switch(context.Info)
            {
                case BlockInfo.BLOCK_SOMEONE:
                {
                    AddRun(NightResources.TEscortAct);
                    break;
                }
                case BlockInfo.BLOCK_SELF:
                {
                    AddRun(NightResources.TEscortActSELF);
                    break;
                }
                case BlockInfo.BLOCKED_BY_OTHER:
                {
                    AddRun(NightResources.TEscortActOTHER);
                    break;
                }
            }
        }

        private void HandleECultus(ECultusRecruitInfoContext context)
        {
            switch(context.Info)
            {
                case RecruitInfo.ADD_NEW_MEMBER:
                {
                    AddRun(NightResources.ECultusLeaderAct, RoleVisual.CULTIST.GetColor());
                    break;
                }
                case RecruitInfo.NON_RECRUITABLE:
                {
                    AddRun(NightResources.ECultusLeaderActF);
                    break;
                }
            }
        }

        private void HandleEGodfather(EGodfatherRecruitInfoContext context)
        {
            switch(context.Info)
            {
                case RecruitInfo.ADD_NEW_MEMBER:
                {
                    AddRun(NightResources.EGodfatherAct, new SolidColorBrush(Colors.Yellow));
                    break;
                }
                case RecruitInfo.NON_RECRUITABLE:
                {
                    AddRun(NightResources.EGodfatherActF);
                    break;
                }
            }
        }

        private void HandleKill(KillInfoContext context)
        {
            switch(context.Info)
            {
                case KillInfo.TARGET_IMMUNE:
                {
                    AddRun(NightResources.EKillImmune);
                    break;
                }
                case KillInfo.KILL:
                {
                    if(context.ToTarget) AddRun(NightResources.TKillAct, RoleVisual.MAFIA.GetColor());
                    else switch(context)
                        {
                            case MafiaInfoContext _:
                            {
                                AddRun(NightResources.MafiaActKILL);
                                break;
                            }
                            case VigilanteInfoContext _:
                            {
                                AddRun(NightResources.VigilanteActKILL);
                                break;
                            }
                            case SerialKillerInfoContext _:
                            {
                                AddRun(NightResources.SerialKillerActKILL);
                                break;
                            }
                        }
                    break;
                }
                case KillInfo.SUICIDE:
                {
                    if(context.ToTarget) AddRun(
                        NightResources.TKillActSUICIDE,
                        RoleVisual.MAFIA.GetColor());
                    else AddRun(NightResources.KillActSUICIDE);
                    break;
                }
                case KillInfo.DRIVER_ACCIDENT:
                {
                    //If ToTarget - play sound
                    AddRun(NightResources.KillActDRIVER_ACCIDENT);
                    break;
                }
            }
        }

        private void HandleHeal(HealInfoContext context)
        {
            if(context.ToTarget) AddRun(NightResources.THealAct, RoleVisual.DOCTOR.GetColor());
            //else play sound
        }

        private void HandleEInvestigate(EInvestigationInfoContext context)
        {
            string line = string.Format(NightResources.EInvestigateAct,
                context.Info.MapRole().GetLocalizedName());
            AddRun(line);
        }

        private void HandleEDetect(EDetectInfoContext context)
        {
            switch(context.Info)
            {
                case DetectInfo.PEACEFUL:
                {
                    AddRun(NightResources.EDetectActPEACEFUL);
                    break;
                }
                case DetectInfo.DANGEROUS:
                {
                    AddRun(NightResources.EDetectActDANGEROUS);
                    break;
                }
            }
        }

        private void HandleRessurect(RessurectInfoContext context)
        {
            if(context.ToTarget) AddRun(NightResources.TRessurectAct);
            else AddRun(NightResources.RessurectAct);
        }

        private void AddRun(string line) => story.Inlines.Add(new Run(line));

        private void AddRun(string line, Brush color) =>
            story.Inlines.Add(new Run(line)
            {
                Foreground = color
            });
    }
}