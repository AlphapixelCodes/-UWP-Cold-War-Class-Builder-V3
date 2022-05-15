using System;
using System.Collections.Generic;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class GunControl : UserControl
    {
        public GunControl()
        {
            this.InitializeComponent();
            boxes = new Image[] { A1, A2, A3, A4, A5, A6, A7, A8 };
            showContextMenu = true;
        }
        public IconClass iconClass;
        public bool showContextMenu;
        public GunControl(IconClass gun)
        {
            iconClass = gun;
            InitializeComponent();
            boxes = new Image[] { A1, A2, A3, A4, A5, A6, A7, A8 };
        }
        private Image[] boxes;

        public event EventHandler FlyoutMenuItem_Tapped;

        public void UnselectAllBoxes()
        {
            foreach (var item in boxes)
            {
                item.Source= Application.Current.Resources["AttachmentIconEmpty"] as ImageSource;
            }
        }
        public void SetSelectedBoxes(int count)
        {
            UnselectAllBoxes();
            for (int i = 0; i < Math.Min(count,boxes.Length); i++)
            {
                boxes[i].Source = Application.Current.Resources["AttachmentIconFull"] as ImageSource;
            }
        }
        public void ShowHideAllBoxes(bool hide)
        {
            Visibility b;
            if (hide)
                b = Visibility.Collapsed;
            else
                b = Visibility.Visible;
            foreach (var item in boxes)
            {
                item.Visibility = b;
            }
        }
        public void ShowHideExtraAttachments(bool show)
        {
            Visibility b;
            if (show)
                b = Visibility.Visible;
            else
                b = Visibility.Collapsed;
            A6.Visibility =b;
            A7.Visibility = b;
            A8.Visibility = b;
        }
        public void UpdateAttachments(AttachmentClass atts)
        {
            SetSelectedBoxes(atts.GetAttachmentCount());
        }
        public void Update(IconClass g)
        {
            iconClass = g;
            text.Text = g.Name;
            picture.Source = g.Image;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            border.BorderThickness = new Thickness(0);
            if (iconClass!=null)
                Update(iconClass);
            if (showContextMenu)
            {
                MenuFlyout flyout = new MenuFlyout();
                foreach (var a in new string[] { "View", "Load Blueprint For Gun","Load Blueprint", "Save Blueprint"})
                {
                    MenuFlyoutItem mfi = new FlyoutMenuItemExtended(a, this);
                    flyout.Items.Add(mfi);
                    mfi.Click += (c, d) => FlyoutMenuItem_Tapped?.Invoke(c, new EventArgs());
                }
                this.RightTapped += (a, b) => flyout.ShowAt(this);
            }
        }

        private void background_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            border.BorderThickness = new Thickness(1);
        }

        private void background_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            border.BorderThickness = new Thickness(0);
        }
    }
}
