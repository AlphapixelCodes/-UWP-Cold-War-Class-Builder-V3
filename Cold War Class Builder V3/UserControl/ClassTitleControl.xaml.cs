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
    public sealed partial class ClassTitleControl : UserControl
    {
        private string title;
        public String Title { get { return title; }
            set {
                textBlock.Text = value;
                title = value;
            }
            }
        private bool fav;
        public bool isFavorite { get {
                return fav;
            }
            set {
                image.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
                fav = value;
            }
        }
        private bool selected;
        public bool Selected { get { return selected; }
            set {
                selected = value;
                hover(value);
            }
        }
        public ClassTitleControl()
        {
            this.InitializeComponent();
        }
        public ClassBuild build;
        public ClassTitleControl(ClassBuild b)
        {
            build = b;
            InitializeComponent();
        }
        public void setText(String text)
        {
            this.textBlock.Text = text;
        }
        private void hover(bool hover)
        {
            if (hover)
            {
                image.Source = Data.BlackStar;
                grid.Background = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
            else if(!selected)
            {
                image.Source = Data.Star;
                grid.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                textBlock.Foreground = new SolidColorBrush(Color.FromArgb(255, 245, 245, 245));
            }
        }
        private void ControlMouse_Enter(object sender, PointerRoutedEventArgs e)
        {
            hover(true);
        }

        private void ControlMouse_Leave(object sender, PointerRoutedEventArgs e)
        {
            hover(false);
        }
        
        private MenuFlyout flyout;
        public event EventHandler FlyoutMenuItem_Tapped;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            flyout = new MenuFlyout();
            foreach(var a in new string[] { "Delete" ,"Toggle Favorite", "Rename", "Duplicate",})
            {
                MenuFlyoutItem mfi = new FlyoutMenuItemExtended(a,this);
                flyout.Items.Add(mfi);
                mfi.Click += (c,d)=> FlyoutMenuItem_Tapped?.Invoke(c, new EventArgs());             
            }
            this.RightTapped += (a, b) => flyout.ShowAt(this);
            if (build != null)
            {
                this.Title = build.Name;
                isFavorite = build.isFavorite;
            }

        }

       
    }
}
