using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Cold_War_Class_Builder_V3
{
    public class IconClass
    {
        public String Name;
        public BitmapSource Image;
        public IconClass(String name, BitmapSource image)
        {
            Name = name.ToUpper();
            Image = image;
        }
    }
}
