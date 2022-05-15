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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class RemoveAttachmentDialog : ContentDialog
    {
        public RemoveAttachmentDialog()
        {
            this.InitializeComponent();
        }

        private StackPanel GetStackFromPivotItem(PivotItem pivot)
        {
            return (StackPanel)((ScrollViewer)pivot.Content).Content;
        }
        private PivotItem GetPivotFromTypeID(int id)
        {
            return new PivotItem[] { OpticPivot, MuzzlePivot, BarrelPivot, BodyPivot, UnderbarrelPivot, MagazinePivot, HandlePivot, StockPivot }[id];
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        private List<CheckBoxExtended> CheckBoxes = new List<CheckBoxExtended>();
        private void AddCheckboxes(List<AttachmentClass.CustomAttachment> atts, PivotItem pivot)
        {
            StackPanel stack = GetStackFromPivotItem(pivot);
            foreach (var item in atts)
            {
                CheckBoxExtended cbe = new CheckBoxExtended(item);
                cbe.Content = item.Name;
                CheckBoxes.Add(cbe);
                cbe.Width = 250;
                stack.Children.Add(cbe);
            }
        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            List<AttachmentClass.CustomAttachment> cus = AttachmentClass.CustomAttachments;
            for (int i = 0; i < 8; i++)
            {
                AddCheckboxes(cus.FindAll(z => z.Type == i), GetPivotFromTypeID(i));
            }
            RemoveEmptyPivots();
        }
        private void RemoveEmptyPivots()
        {
            for (int i = 0; i < 8; i++)
            {
                PivotItem p = GetPivotFromTypeID(i);
                StackPanel s = GetStackFromPivotItem(p);
                if (s.Children.Count == 0 && p.Parent!=null)
                    ((Pivot)p.Parent).Items.Remove(p);
            }
            Grid grid = (Grid)MainPivot.Parent;
            if (MainPivot.Items.Count == 0 && grid.Children.OfType<TextBlock>().Count()==0)
            {
                TextBlock tb = new TextBlock();
                tb.Text = "There are no Custom Attachments";
                tb.FontSize = 16;
                grid.Children.Add(tb);
                PrimaryButtonText = "";
            }
        }
        private class CheckBoxExtended:CheckBox
        {
            public AttachmentClass.CustomAttachment CustomAtt;
            public CheckBoxExtended(AttachmentClass.CustomAttachment a)
            {
                CustomAtt = a;
            }
            public void DeleteAttachment()
            {
                StackPanel parent = ((StackPanel)this.Parent);
                parent?.Children.Remove(this);
                if (CustomAtt != null)
                    AttachmentClass.CustomAttachments.Remove(CustomAtt);
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;
            CheckBoxes.ForEach(e =>
            {
                if (e.IsChecked != true)
                    return;
                e.DeleteAttachment();
            });
            RemoveEmptyPivots();
            
        }
    }
}
