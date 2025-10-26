using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Maio10.Controllers
{
    internal class Conversor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int number = (int)value;
            string imageName = string.Empty;

            if (parameter != null && parameter.ToString() == "Card")
            {
                imageName = $"{number}.png";
                BitmapImage bmp = new BitmapImage(new Uri($"/cards_image/{imageName}", UriKind.Relative));
                return bmp;
            }
            else
            {
                imageName = $"{number}.png";
                BitmapImage bmp = new BitmapImage(new Uri($"/dados/{imageName}", UriKind.Relative));
                return bmp;
            }

           
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
