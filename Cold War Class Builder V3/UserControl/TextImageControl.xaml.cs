using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class TextImageControl : UserControl
    {
        public TextImageControl()
        {
            InitializeComponent();
        }
        public IconClass IconClass;
        public TextImageControl(IconClass iconClass)
        {
            this.InitializeComponent();
            IconClass = iconClass;
            
        }
        public event EventHandler ClickEvent;
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement[] controls = new FrameworkElement[] { background, picturebox, text };
           // new Tools.HoverControl(controls.ToList(), UpdateHover);
            background.Tapped += Item_Tapped;
        }


        private void Item_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ClickEvent?.Invoke(this, new EventArgs());
            Debug.WriteLine("clicked: "+((FrameworkElement)sender).Name);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (IconClass != null)
            {
                picturebox.Source = IconClass.Image;
                text.Text = IconClass.Name;
            }
        }
        
        public void Update(IconClass s)
        {
            IconClass = s;
            picturebox.Source = IconClass.Image;
            text.Text = IconClass.Name;
        }
       
        private void background_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border.BorderBrush = new SolidColorBrush(Colors.White);
        }

        private void background_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border.BorderBrush = new SolidColorBrush(Colors.Black);
        }
    }
}
