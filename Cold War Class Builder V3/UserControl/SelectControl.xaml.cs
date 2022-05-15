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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Cold_War_Class_Builder_V3
{
    public partial class SelectControl : UserControl
    {
        private bool selectable;
        private bool selected;
        public bool Selected
        {
            get { return selected; }
            set {
                selected = value;
                this.image.Source = value? Data.SelectedDot:Data.BlankImage;
            }
        }
        public object AttachedObject;
        public SelectControl()
        {
            this.InitializeComponent();

            background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
            text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));

        }
        public IconClass Iconclass;
        private string settext = "";
        public void setText(string txt)
        {
            text.Text = txt;
            settext = txt;
        }
        public SelectControl(IconClass x)
        {
            Iconclass = x;
            InitializeComponent();
            background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
            text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));
        }
        //for perks
        public SelectControl(IconClass x, bool selected)
        {
            selectable = selected;
            Iconclass = x;
            InitializeComponent();
            background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
            text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));
          

        }
        public void Update(IconClass x)
        {
            Iconclass = x;
            picture.Source = x.Image;
            text.Text = x.Name;
        }

        private void background_Loaded(object sender, RoutedEventArgs e)
        {
            if(Iconclass!=null)
            Update(Iconclass);
            if (selectable)
                selected = false;
            if (settext.Length > 0)
                this.text.Text = settext;
        }

        private void background_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
            text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));
        }

        private void background_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            background.Background = new SolidColorBrush(Color.FromArgb(255, 200, 200, 200));
            text.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        }
        public void SetEnabled(bool isEnabled)
        {
            this.IsEnabled = isEnabled;
            if (isEnabled)
            {
                background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
                text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));
            }
            else
            {
                background.Background = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
                text.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
        }
        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsEnabled)
            {
                background.Background = new SolidColorBrush(Color.FromArgb(255, 88, 79, 74));
                text.Foreground = new SolidColorBrush(Color.FromArgb(255, 222, 218, 218));
            }
            else
            {
                background.Background = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
                text.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)); 
            }
        }
    }
}
