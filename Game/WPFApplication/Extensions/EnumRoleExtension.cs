using GameLogic.Attributes;
using GameLogic.Model;
using Net.Extensions;
using Net.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using WPFApplication.Model;
using WPFApplication.Resources;

namespace WPFApplication.Extensions
{
    internal static class EnumRoleExtension
    {
        internal static RoleSignature MapRole(this RoleVisual role) => (RoleSignature)role;

        internal static RoleVisual MapRole(this RoleSignature signature) => (RoleVisual)signature;

        internal static string GetLocalizedName(this RoleVisual role)
        {
            var standart = string.Empty;
            FieldInfo? fi = role.GetType().GetField(role.ToString());
            if(fi != null)
            {
                LocalizedDescriptionAttribute attr =
                    fi.GetCustomAttributes<LocalizedDescriptionAttribute>()
                    .Single(a => a.Locilize == RoleLocilize.NAME);

                return attr?.Description ?? standart;
            }
            return standart;
        }

        internal static string GetLocilizedDescription(this RoleVisual role)
        {
            var standart = string.Empty;
            FieldInfo? fi = role.GetType().GetField(role.ToString());
            if(fi != null)
            {
                LocalizedDescriptionAttribute attr =
                    fi.GetCustomAttributes<LocalizedDescriptionAttribute>()
                    .Single(a => a.Locilize == RoleLocilize.DESCRIPTION);

                return attr?.Description ?? standart;
            }
            return standart;
        }

        internal static string GetLocilizedAbility(this RoleVisual role)
        {
            var standart = string.Empty;
            FieldInfo? fi = role.GetType().GetField(role.ToString());
            if(fi != null)
            {
                LocalizedDescriptionAttribute attr =
                    fi.GetCustomAttributes<LocalizedDescriptionAttribute>()
                    .Single(a => a.Locilize == RoleLocilize.ABILITY);

                return attr?.Description ?? standart;
            }
            return standart;
        }

        internal static string GetLocilizedBelonging(this RoleVisual role)
        {
            switch(role.GetTeam())
            {
                case Team.TOWN:
                    return RoleDescriptions.TownTeam;
                case Team.MAFIA:
                    return RoleDescriptions.MafiaTeam;
                case Team.CULTUS:
                    return RoleDescriptions.CultusTeam;
                case Team.UNDEAD:
                    return RoleDescriptions.UndeadTeam;
                case Team.SERIAL_KILLER:
                    return RoleDescriptions.SerialKillerTeam;
                case Team.WITCH:
                    return RoleDescriptions.WitchTeam;
                case Team.TERRORIST:
                    return RoleDescriptions.TerroristTeam;
                default:
                    throw new ArgumentException("Invalid team");
            }
        }

        internal static (ChatScope scope, bool canWrite)[] GetChatScopes(this RoleVisual role) =>
            role.MapRole().GetChatScopes();

        internal static ExecutorType GetExecutorType(this RoleVisual role) =>
            role.MapRole().GetExecutorType();

        internal static Team GetTeam(this RoleVisual role) => role.MapRole().GetTeam();

        internal static Brush GetColor(this RoleVisual role) => GetColor(role.GetTeam());

        internal static Brush GetColor(this Team team)
        {
            var color = team switch
            {
                Team.TOWN => Color.FromRgb(0x0E, 0xFF, 0x0E),//Green
                Team.MAFIA => Color.FromRgb(0xFD, 0x03, 0x03),//Red
                Team.CULTUS => Color.FromRgb(0xE0, 0x02, 0x80),//Violet
                Team.UNDEAD => Color.FromRgb(0xA8, 0xA8, 0xA8),//Gray
                //Neutrals
                _ => Color.FromRgb(0xF7, 0x53, 0xFC)//Pink
            };
            return new SolidColorBrush(color);
        }

        internal static Color ConvertToColor(this RGB data)
        {
            return Color.FromRgb(data.R, data.G, data.B);
        }

        internal static RGB ConvertToBytes(this Color color)
        {
            return new(color.R, color.G, color.B);
        }
    }
}