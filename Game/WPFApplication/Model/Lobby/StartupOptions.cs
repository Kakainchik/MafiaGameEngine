using WPFApplication.Converters;
using WPFApplication.Extensions;
using WPFApplication.Resources;
using System.ComponentModel;

namespace WPFApplication.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum DayType
    {
        [LocalizedDescription("MajorityDayOption", typeof(EnumResources))]
        MAJORITY,
        [LocalizedDescription("MajorityTrialDayOption", typeof(EnumResources))]
        MAJORITY_TRIAL,
        [LocalizedDescription("BallotDayOption", typeof(EnumResources))]
        BALLOT,
        [LocalizedDescription("BallotTrialDayOption", typeof(EnumResources))]
        BALLOT_TRIAL
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum NightType
    {
        [LocalizedDescription("ClassicNightOption", typeof(EnumResources))]
        CLASSIC,
        [LocalizedDescription("DescriptionNightOption", typeof(EnumResources))]
        DESCRIPTIONS,
        [LocalizedDescription("DetailedNightOption", typeof(EnumResources))]
        DETAILED
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum TrialType
    {

    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Miscellaneouns
    {

    }
}