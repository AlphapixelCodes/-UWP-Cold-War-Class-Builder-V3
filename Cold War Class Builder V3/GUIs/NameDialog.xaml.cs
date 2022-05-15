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
    public sealed partial class NameDialog : ContentDialog
    {
        public NameDialog()
        {
            this.InitializeComponent();
        }
        public event EventHandler Confirmation_Event;
        public String ReturnValue = "";
        public void SetTextBoxText(String text)
        {
            textBox.Text = text;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ReturnValue = textBox.Text;
            Confirmation_Event?.Invoke(this, new EventArgs());
            args.Cancel = true;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
        public string ErrorTextToWrite { get { return ErrorText.Text; } set 
            {
                ErrorText.Text = value;
            }
            }
        private void textBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                ReturnValue = textBox.Text;
                Confirmation_Event?.Invoke(this, new EventArgs());
            }
            
        }
    }
}
