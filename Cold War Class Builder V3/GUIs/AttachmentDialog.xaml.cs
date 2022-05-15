using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
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
    public sealed partial class AttachmentDialog : ContentDialog
    {
        private AttachmentClass attachmentClass;
        private int maxAttachments;
        public AttachmentDialog()
        {
            this.InitializeComponent();
        }

        public AttachmentDialog(AttachmentClass attachmentClass,int maxsAttachments)
        {
            maxAttachments = maxsAttachments;
            this.InitializeComponent();
            this.attachmentClass = attachmentClass;
        }
        public event EventHandler AttachmentSetEvent;
        public GunControl gunControl;
        public AttachmentClass returnAttachments;
       
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            AttachmentClass a = GetAttachmentClass();
            returnAttachments = a;
            if (a.GetAttachmentCount() > maxAttachments) 
                args.Cancel = true;
            else
                AttachmentSetEvent?.Invoke(this, new EventArgs());
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
        }
        private AttachmentClass GetAttachmentClass()
        {
            //return new AttachmentClass((OpticCombo.SelectedItem ?? "None").ToString(), MuzzleCombo.SelectedItem.ToString() ?? "None", BarrelCombo.SelectedItem.ToString() ?? "None", BodyCombo.SelectedItem.ToString() ?? "None", UnderbarrelCombo.SelectedItem.ToString() ?? "None", MagazineCombo.SelectedItem.ToString() ?? "None", HandleCombo.SelectedItem.ToString() ?? "None", StockCombo.SelectedItem.ToString() ?? "None");
            return new AttachmentClass((OpticCombo.SelectedItem ?? "None").ToString(), (MuzzleCombo.SelectedItem ?? "None").ToString(), (BarrelCombo.SelectedItem ?? "None").ToString(), (BodyCombo.SelectedItem ?? "None").ToString(), (UnderbarrelCombo.SelectedItem ?? "None").ToString(), (MagazineCombo.SelectedItem ?? "None").ToString(), (HandleCombo.SelectedItem ?? "None").ToString(), (StockCombo.SelectedItem ?? "None").ToString());
        }
        private void LoadData()
        {
            OpticCombo.Items.Clear();
            Data.OpticList.ForEach(e => OpticCombo.Items.Add(e));
            MuzzleCombo.Items.Clear();
            Data.MuzzleList.ForEach(e => MuzzleCombo.Items.Add(e));
            BarrelCombo.Items.Clear();
            Data.BarrelList.ForEach(e => BarrelCombo.Items.Add(e));
            BodyCombo.Items.Clear();
            Data.BodyList.ForEach(e => BodyCombo.Items.Add(e));
            UnderbarrelCombo.Items.Clear();
            Data.UnderbarrelList.ForEach(e => UnderbarrelCombo.Items.Add(e));
            MagazineCombo.Items.Clear();
            Data.MagazineList.ForEach(e => MagazineCombo.Items.Add(e));
            HandleCombo.Items.Clear();
            Data.GunHandleList.ForEach(e => HandleCombo.Items.Add(e));
            StockCombo.Items.Clear();
            Data.StockList.ForEach(e => StockCombo.Items.Add(e));
            
        }
        private void LoadCustomAttachments()
        {
            OpticCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(0).ForEach(e => OpticCombo.Items.Add(e));
            OpticCombo.Items.Add("None");
            MuzzleCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(1).ForEach(e => MuzzleCombo.Items.Add(e));
            MuzzleCombo.Items.Add("None");
            BarrelCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(2).ForEach(e => BarrelCombo.Items.Add(e));
            BarrelCombo.Items.Add("None");
            BodyCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(3).ForEach(e => BodyCombo.Items.Add(e));
            BodyCombo.Items.Add("None");
            UnderbarrelCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(4).ForEach(e => UnderbarrelCombo.Items.Add(e));
            UnderbarrelCombo.Items.Add("None");
            MagazineCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(5).ForEach(e => MagazineCombo.Items.Add(e));
            MagazineCombo.Items.Add("None");
            HandleCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(6).ForEach(e => HandleCombo.Items.Add(e));
            HandleCombo.Items.Add("None");
            StockCombo.Items.Remove("None");
            AttachmentClass.GetCustomAttachmentOfType(7).ForEach(e => StockCombo.Items.Add(e));
            StockCombo.Items.Add("None");

        }
        private void LoadAttachmentFromClass(AttachmentClass c)
        {

            OpticCombo.SelectedItem = OpticCombo.Items.Contains(c.Optic) ? c.Optic : "None";
            MuzzleCombo.SelectedItem = MuzzleCombo.Items.Contains(c.Muzzle) ? c.Muzzle : "None";
            BarrelCombo.SelectedItem = BarrelCombo.Items.Contains(c.Barrel) ? c.Barrel : "None";
            BodyCombo.SelectedItem = BodyCombo.Items.Contains(c.Body) ? c.Body : "None";
            UnderbarrelCombo.SelectedItem = UnderbarrelCombo.Items.Contains(c.Underbarrel) ? c.Underbarrel : "None";
            MagazineCombo.SelectedItem = MagazineCombo.Items.Contains(c.Magazine) ? c.Magazine : "None";
            HandleCombo.SelectedItem = HandleCombo.Items.Contains(c.GunHandle) ? c.GunHandle : "None";
            StockCombo.SelectedItem = StockCombo.Items.Contains(c.Stock) ? c.Stock : "None";

        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            LoadCustomAttachments();
            LoadAttachmentFromClass(attachmentClass.Clone());
            OpticCombo.SelectionChanged += ComboSelection_Changed;
            MuzzleCombo.SelectionChanged += ComboSelection_Changed;
            BarrelCombo.SelectionChanged += ComboSelection_Changed;
            BodyCombo.SelectionChanged += ComboSelection_Changed;
            UnderbarrelCombo.SelectionChanged += ComboSelection_Changed;
            MagazineCombo.SelectionChanged += ComboSelection_Changed;
            HandleCombo.SelectionChanged += ComboSelection_Changed;
            StockCombo.SelectionChanged += ComboSelection_Changed;    
            //
            OpticCombo.RightTapped += ComboRight_Tapped;
            MuzzleCombo.RightTapped += ComboRight_Tapped;
            BarrelCombo.RightTapped += ComboRight_Tapped;
            BodyCombo.RightTapped += ComboRight_Tapped;
            UnderbarrelCombo.RightTapped += ComboRight_Tapped;
            MagazineCombo.RightTapped += ComboRight_Tapped;
            HandleCombo.RightTapped += ComboRight_Tapped;
            StockCombo.RightTapped += ComboRight_Tapped;
            checkIfLockNeeded();

        }

        private void ComboRight_Tapped(object sender, RightTappedRoutedEventArgs e)
        {
            ((ComboBox)sender).SelectedItem = "None";
        }

        private void ComboSelection_Changed(object sender, SelectionChangedEventArgs e)
        {
            checkIfLockNeeded();
        }

        private void checkIfLockNeeded()
        {
            ComboBox[] boxes = new ComboBox[] { OpticCombo, MuzzleCombo, BarrelCombo, BodyCombo, UnderbarrelCombo, MagazineCombo, HandleCombo, StockCombo };
            if (GetAttachmentClass().GetAttachmentCount() >= maxAttachments)
            {
                foreach (var item in boxes)
                {
                    if (item.SelectedItem.Equals("None"))
                    {
                        item.IsEnabled = false;
                    }
                }
            }
            else
            {
                foreach (var item in boxes)
                {
                    item.IsEnabled = true;
                }
            }
        }

        private void ClearSelected_Tapped(object sender, TappedRoutedEventArgs e)
        {
            OpticCombo.SelectedItem = "None";
            MuzzleCombo.SelectedItem = "None";
            BarrelCombo.SelectedItem = "None";
            BodyCombo.SelectedItem = "None";
            UnderbarrelCombo.SelectedItem = "None";
            MagazineCombo.SelectedItem = "None";
            HandleCombo.SelectedItem = "None";
            StockCombo.SelectedItem = "None";
        }

        private void AddAttachment_Tapped(object sender, TappedRoutedEventArgs e)
        {
        AddCustomAttachmentDialog gui = new AddCustomAttachmentDialog();
            this.Hide();
            
            
            gui.AttachedObjects.Add(GetAttachmentClass());
            gui.AttachedObjects.Add(maxAttachments);
            gui.AttachedObjects.Add(gunControl);
            gui.AttachedObjects.Add(AttachmentSetEvent);
            gui.Callback += (a, b) => LaunchNewGUI(a);
            gui.Closed += (a, b) => LaunchNewGUI(a);
            gui.ShowAsync();
         
        }
        private void LaunchNewGUI(object sender)
        {
            try
            {
                AddCustomAttachmentDialog gui = (AddCustomAttachmentDialog)sender;
                gui.Hide();
                AttachmentDialog newattdiag = new AttachmentDialog((AttachmentClass)gui.AttachedObjects[0], (int)gui.AttachedObjects[1]);
                newattdiag.AttachmentSetEvent += (EventHandler)gui.AttachedObjects[3];
                newattdiag.gunControl = (GunControl)gui.AttachedObjects[2];
                newattdiag.ShowAsync();
            }
            catch { }
        }

    }
}
