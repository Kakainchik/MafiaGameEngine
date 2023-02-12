using Net.Models;
using System.ComponentModel;
using WPFApplication.Converters;
using WPFApplication.Extensions;
using WPFApplication.Resources;

namespace WPFApplication.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum RoleVisual : byte
    {
        [LocalizedDescription("CitizenRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("CitizenRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("CitizenAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        CITIZEN = RoleSignature.CITIZEN,

        [LocalizedDescription("CursedRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("CursedRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("CursedAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        CURSED = RoleSignature.CURSED,

        [LocalizedDescription("DetectiveRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("DetectiveRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("DetectiveAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        DETECTIVE = RoleSignature.DETECTIVE,

        [LocalizedDescription("DoctorRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("DoctorRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("DoctorAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        DOCTOR = RoleSignature.DOCTOR,

        [LocalizedDescription("DriverRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("DriverRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("DriverAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        DRIVER = RoleSignature.DRIVER,

        [LocalizedDescription("MasonRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("MasonRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("MasonAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        MASON = RoleSignature.MASON,

        [LocalizedDescription("PolicemanRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("PolicemanRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("PolicemanAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        POLICEMAN = RoleSignature.POLICEMAN,

        [LocalizedDescription("ProstituteRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("ProstituteRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("ProstituteAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        PROSTITUTE = RoleSignature.PROSTITUTE,

        [LocalizedDescription("RecruitRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("RecruitRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("RecruitAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        RECRUIT = RoleSignature.RECRUIT,

        [LocalizedDescription("VigilanteRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("VigilanteRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("VigilanteAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        VIGILANTE = RoleSignature.VIGILANTE,

        [LocalizedDescription("CounselorRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("CounselorRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("CounselorAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        COUNSELOR = RoleSignature.COUNSELOR,

        [LocalizedDescription("GodfatherRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("GodfatherRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("GodfatherAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        GODFATHER = RoleSignature.GODFATHER,

        [LocalizedDescription("MafiaRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("MafiaRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("MafiaAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        MAFIA = RoleSignature.MAFIA,

        [LocalizedDescription("SurgeonRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("SurgeonRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("SurgeonAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        SURGEON = RoleSignature.SURGEON,

        [LocalizedDescription("WhoreRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("WhoreRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("WhoreAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        WHORE = RoleSignature.WHORE,

        [LocalizedDescription("CultistRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("CultistRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("CultistAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        CULTIST = RoleSignature.CULTIST,

        [LocalizedDescription("CultusLeaderRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("CultusLeaderRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("CultusLeaderAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        CULTUS_LEADER = RoleSignature.CULTUS_LEADER,

        [LocalizedDescription("SerialKillerRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("SerialKillerRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("SerialKillerAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        SERIAL_KILLER = RoleSignature.SERIAL_KILLER,

        [LocalizedDescription("TerroristRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("TerroristRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("TerroristAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        TERRORIST = RoleSignature.TERRORIST,

        [LocalizedDescription("WitchRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("WitchRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("WitchAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        WITCH = RoleSignature.WITCH,

        [LocalizedDescription("PsychicRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("PsychicRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("PsychicAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        PSYCHIC = RoleSignature.PSYCHIC,

        [LocalizedDescription("ZombieRole", typeof(EnumResources), Locilize = RoleLocilize.NAME)]
        [LocalizedDescription("ZombieRole", typeof(RoleDescriptions), Locilize = RoleLocilize.DESCRIPTION)]
        [LocalizedDescription("ZombieAbility", typeof(RoleDescriptions), Locilize = RoleLocilize.ABILITY)]
        ZOMBIE = RoleSignature.ZOMBIE
    }
}