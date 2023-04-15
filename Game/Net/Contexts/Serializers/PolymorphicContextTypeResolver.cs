using Net.Contexts.Chat;
using Net.Contexts.Connection;
using Net.Contexts.Day;
using Net.Contexts.Game;
using Net.Contexts.Intro;
using Net.Contexts.Lobby;
using Net.Contexts.Lynch;
using Net.Contexts.Morning;
using Net.Contexts.Night;
using Net.Contexts.Night.ActionInfo;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Net.Contexts.Serializers
{
    public class PolymorphicContextTypeResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

            Type baseContextType = typeof(Context);
            if(jsonTypeInfo.Type == baseContextType)
            {
                jsonTypeInfo.PolymorphismOptions = new JsonPolymorphismOptions()
                {
                    TypeDiscriminatorPropertyName = "$context-type",
                    IgnoreUnrecognizedTypeDiscriminators = true,
                    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
                    DerivedTypes =
                    {
                        new JsonDerivedType(typeof(AuthorizationContext), "Authorization"),

                        new JsonDerivedType(typeof(MessageContext), "Message"),
                        new JsonDerivedType(typeof(ScopedMessageContext), "ScopedMessage"),

                        new JsonDerivedType(typeof(ConnectPlayerContext), "ConnectPlayer"),
                        new JsonDerivedType(typeof(DisconnectPlayerContext), "DisconnectPlayer"),
                        new JsonDerivedType(typeof(SessionIdContext), "SessionId"),
                        new JsonDerivedType(typeof(UsernameContext), "Username"),

                        new JsonDerivedType(typeof(DayContext), "Day"),
                        new JsonDerivedType(typeof(DayPlayerStateContext), "DayPlayerState"),
                        new JsonDerivedType(typeof(ElectionResultContext), "ElectionResult"),
                        new JsonDerivedType(typeof(ReceiveVoteContext), "ReceiveVote"),
                        new JsonDerivedType(typeof(SendVoteContext), "SendVote"),
                        new JsonDerivedType(typeof(WarningVoteContext), "WarningVote"),

                        new JsonDerivedType(typeof(CommonPlayerStateContext), "CommonPlayerState"),
                        new JsonDerivedType(typeof(EndGameContext), "EndGame"),
                        new JsonDerivedType(typeof(ScreenContext), "Screen"),
                        new JsonDerivedType(typeof(TimerContext), "Timer"),

                        new JsonDerivedType(typeof(IntroContext), "Intro"),
                        new JsonDerivedType(typeof(IntroPlayerContext), "IntroPlayer"),
                        new JsonDerivedType(typeof(IntroRunGameContext), "IntroRunGame"),
                        new JsonDerivedType(typeof(NicknameContext), "Nickname"),

                        new JsonDerivedType(typeof(LobbyInitialDataContext), "LobbyInitialData"),
                        new JsonDerivedType(typeof(LobbyMaxPlayerContext), "LobbyMaxPlayer"),
                        new JsonDerivedType(typeof(LobbyReadyContext), "LobbyReady"),
                        new JsonDerivedType(typeof(LobbyRoleContext), "LobbyRole"),
                        new JsonDerivedType(typeof(LobbyRunIntroContext), "LobbyRunIntro"),

                        new JsonDerivedType(typeof(LynchContext), "Lynch"),
                        new JsonDerivedType(typeof(LynchPlayerStateContext), "LynchPlayerState"),
                        new JsonDerivedType(typeof(ReceiveLastMessageContext), "ReceiveLastMessage"),
                        new JsonDerivedType(typeof(SendLastMessageContext), "SendLastMessage"),

                        new JsonDerivedType(typeof(MorningContext), "Morning"),
                        new JsonDerivedType(typeof(VictimContext), "Victim"),

                        new JsonDerivedType(typeof(BlockInfoContext), "BlockInfo"),
                        new JsonDerivedType(typeof(BlowInfoContext), "BlowInfo"),
                        new JsonDerivedType(typeof(CultusRecruitInfoContext), "CultusRecruitInfo"),
                        new JsonDerivedType(typeof(DriverInfoContext), "DriverInfo"),
                        new JsonDerivedType(typeof(ECultusRecruitInfoContext), "ECultusRecruitInfo"),
                        new JsonDerivedType(typeof(EDetectInfoContext), "EDetectInfo"),
                        new JsonDerivedType(typeof(EGodfatherRecruitInfoContext), "EGodfatherRecruitInfo"),
                        new JsonDerivedType(typeof(EInvestigationInfoContext), "EInvestigationInfo"),
                        new JsonDerivedType(typeof(EWitchInfoContext), "EWitchInfo"),
                        new JsonDerivedType(typeof(GodfatherRecruitInfoContext), "GodfatherRecruitInfo"),
                        new JsonDerivedType(typeof(HealInfoContext), "HealInfo"),
                        new JsonDerivedType(typeof(MafiaInfoContext), "MafiaInfo"),
                        new JsonDerivedType(typeof(RessurectInfoContext), "RessurectInfo"),
                        new JsonDerivedType(typeof(SerialKillerInfoContext), "SerialKillerInfo"),
                        new JsonDerivedType(typeof(VigilanteInfoContext), "VigilanteInfo"),
                        new JsonDerivedType(typeof(WitchInfoContext), "WitchInfo"),

                        new JsonDerivedType(typeof(ItemsContext), "Items"),
                        new JsonDerivedType(typeof(NightContext), "Night"),
                        new JsonDerivedType(typeof(NightPlayerStateContext), "NightPlayerState"),
                        new JsonDerivedType(typeof(SendActionContext), "SendAction"),
                        new JsonDerivedType(typeof(SendDActionContext), "SendDAction"),
                    }
                };
            }

            return jsonTypeInfo;
        }
    }
}