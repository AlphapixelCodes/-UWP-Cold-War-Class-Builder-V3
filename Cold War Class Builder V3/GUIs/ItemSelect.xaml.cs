using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class ItemSelectDialog : ContentDialog
    {
        public ItemSelectDialog()
        {
            this.InitializeComponent();
        }
        private List<IconClass> Icons;
        public ItemSelectDialog(List<IconClass> icons,String name)
        {
            
            this.InitializeComponent();
            TitleBlock.Title = name;
            Icons = icons;
        }
        private void loadIcons(List<IconClass> icons)
        {
            icons.ForEach(e => {
                SelectControl s = new SelectControl(e);
                s.Tapped += Select_Tapped;
                s.Margin = new Thickness(0, 5, 0, 5);
                Stack.Children.Add(s);
            });
        }
        public event EventHandler ItemSelectedEvent;
        public IconClass returnValue;
        public TextImageControl textImageControl;
        private void Select_Tapped(object sender, TappedRoutedEventArgs e)
        {
            returnValue = ((SelectControl)sender).Iconclass;
            ItemSelectedEvent?.Invoke(this, new EventArgs());
            this.Hide();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            loadIcons(Icons);
        }

        
    }
}
