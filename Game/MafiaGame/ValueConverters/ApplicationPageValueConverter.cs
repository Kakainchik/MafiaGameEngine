using System;
using System.Diagnostics;
using System.Globalization;
using MafiaGame.DataModels;

namespace MafiaGame.ValueConverters
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view\page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Find the approriate page
            switch((ApplicationPage)value)
            {
                case ApplicationPage.StartPage:
                    return new StartPage();

                default: Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
