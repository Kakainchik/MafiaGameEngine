using GameLogic.Attributes;
using GameLogic.Model;
using GameLogic.Roles;
using Net.Attributes;
using Net.Models;
using System.Reflection;

namespace Net.Extensions
{
    public static class RoleExtension
    {
        private const string ROLE_VALID_ERROR = "Such role is not valid.";

        public static Type GetRoleType(this RoleSignature signature)
        {
            FieldInfo? fi = signature.GetType().GetField(signature.ToString());
            RoleTypeAttribute? attr = fi?.GetCustomAttribute<RoleTypeAttribute>();

            return attr?.RoleType ??
                throw new CustomAttributeFormatException(
                    "The signature is not associated to any role.");
        }

        /// <summary>
        /// Convert App role enumerator into Logic Game Role instance.
        /// </summary>
        /// <returns>Logic role instance.</returns>
        public static Role MakeGameRole(this RoleSignature signature)
        {
            Type roleType = signature.GetRoleType();
            ConstructorInfo? ci = roleType.GetConstructor(Type.EmptyTypes);

            return (Role?)ci?.Invoke(null) ??
                throw new ArgumentException(ROLE_VALID_ERROR, nameof(signature));
        }

        /// <summary>
        /// Represent Role class as App enumerator of roles.
        /// </summary>
        /// <returns>Role signature value.</returns>
        public static RoleSignature IntoSignature(this Role role)
        {
            return role switch
            {
                CitizenRole _ => RoleSignature.CITIZEN,
                DoctorRole _ => RoleSignature.DOCTOR,
                PolicemanRole _ => RoleSignature.POLICEMAN,
                MafiaRole _ => RoleSignature.MAFIA,
                CounselorRole _ => RoleSignature.COUNSELOR,
                CultistRole _ => RoleSignature.CULTIST,
                CultusLeaderRole _ => RoleSignature.CULTUS_LEADER,
                CursedRole _ => RoleSignature.CURSED,
                DetectiveRole _ => RoleSignature.DETECTIVE,
                DriverRole _ => RoleSignature.DRIVER,
                GodfatherRole _ => RoleSignature.GODFATHER,
                MasonRole _ => RoleSignature.MASON,
                ProstituteRole _ => RoleSignature.PROSTITUTE,
                PsychicRole _ => RoleSignature.PSYCHIC,
                RecruitRole _ => RoleSignature.RECRUIT,
                SerialKillerRole _ => RoleSignature.SERIAL_KILLER,
                SurgeonRole _ => RoleSignature.SURGEON,
                TerroristRole _ => RoleSignature.TERRORIST,
                VigilanteRole _ => RoleSignature.VIGILANTE,
                WhoreRole _ => RoleSignature.WHORE,
                WitchRole _ => RoleSignature.WITCH,
                ZombieRole _ => RoleSignature.ZOMBIE,
                _ => throw new ArgumentException(ROLE_VALID_ERROR, nameof(role))
            };
        }

        public static Team GetTeam(this RoleSignature signature)
        {
            Type roleType = signature.GetRoleType();
            TeamAttribute? teamAttr = roleType.GetCustomAttribute<TeamAttribute>();

            return teamAttr?.ItsTeam ?? default;
        }

        public static (ChatScope scope, bool canWrite)[] GetChatScopes(this RoleSignature signature)
        {
            Type roleType = signature.GetRoleType();
            var attrs = (ChatScopeAttribute[])roleType.GetCustomAttributes(
                typeof(ChatScopeAttribute), false);

            var result = new (ChatScope scope, bool canWrite)[attrs.Length];
            for(int i = 0; i < attrs.Length; i++)
            {
                result[i] = (attrs[i].Scope, attrs[i].CanWrite);
            }
            return result;
        }

        public static ExecutorType GetExecutorType(this RoleSignature signature)
        {
            Type roleType = signature.GetRoleType();
            ExecutorAttribute? attr = roleType.GetCustomAttribute<ExecutorAttribute>();

            return attr?.EType ?? default;
        }
    }
}