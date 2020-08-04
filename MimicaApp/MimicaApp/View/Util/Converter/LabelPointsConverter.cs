using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MimicaApp.View.Util.Converter {
    public class LabelPointsConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value + " points";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return short.Parse((value as string).Replace(" points", ""));
        }
    }
}
