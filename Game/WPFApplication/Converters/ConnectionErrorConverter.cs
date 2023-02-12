using Net.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using WPFApplication.Resources;

namespace WPFApplication.Converters
{
    public class ConnectionErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ConnectValidation)value)
            {
                case ConnectValidation.LOBBY_IS_FULL:
                    return ErrorResources.LobbyFull;
                case ConnectValidation.CANNOT_CONNECT:
                    return ErrorResources.CannotConnect;
                case ConnectValidation.GAME_RUNNING:
                    return ErrorResources.GameRunning;
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}