using GameLogic.Roles;
using Net.Attributes;

namespace Net.Models
{
    public enum RoleSignature : byte
    {
        [RoleType(typeof(CitizenRole))]
        CITIZEN = 0,

        [RoleType(typeof(CursedRole))]
        CURSED = 1,

        [RoleType(typeof(DetectiveRole))]
        DETECTIVE = 2,

        [RoleType(typeof(DoctorRole))]
        DOCTOR = 3,

        [RoleType(typeof(DriverRole))]
        DRIVER = 4,

        [RoleType(typeof(MasonRole))]
        MASON = 5,

        [RoleType(typeof(PolicemanRole))]
        POLICEMAN = 6,

        [RoleType(typeof(ProstituteRole))]
        PROSTITUTE = 7,

        [RoleType(typeof(RecruitRole))]
        RECRUIT = 8,

        [RoleType(typeof(VigilanteRole))]
        VIGILANTE = 9,

        [RoleType(typeof(CounselorRole))]
        COUNSELOR = 10,

        [RoleType(typeof(GodfatherRole))]
        GODFATHER = 11,

        [RoleType(typeof(MafiaRole))]
        MAFIA = 12,

        [RoleType(typeof(SurgeonRole))]
        SURGEON = 13,

        [RoleType(typeof(WhoreRole))]
        WHORE = 14,

        [RoleType(typeof(CultistRole))]
        CULTIST = 15,

        [RoleType(typeof(CultusLeaderRole))]
        CULTUS_LEADER = 16,

        [RoleType(typeof(SerialKillerRole))]
        SERIAL_KILLER = 17,

        [RoleType(typeof(TerroristRole))]
        TERRORIST = 18,

        [RoleType(typeof(WitchRole))]
        WITCH = 19,

        [RoleType(typeof(PsychicRole))]
        PSYCHIC = 20,

        [RoleType(typeof(ZombieRole))]
        ZOMBIE = 21
    }
}