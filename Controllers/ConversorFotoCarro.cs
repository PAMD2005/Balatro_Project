using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Maio10.Controllers
{
    public class ConversorFotoCarro : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = System.Environment.CurrentDirectory;
            path = path.Substring(0, path.IndexOf("bin")) + @"imagens\";
            if (value == null || String.IsNullOrEmpty(value.ToString()))
            {
                path += "noimage.jpg";
            }
            else
            {

                path += value.ToString();
            }

            BitmapImage bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.CacheOption = BitmapCacheOption.OnLoad;
            bmp.CreateOptions = BitmapCreateOptions.DelayCreation;
            bmp.UriSource = new Uri(path, UriKind.Absolute);
            bmp.EndInit();
            return bmp;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
