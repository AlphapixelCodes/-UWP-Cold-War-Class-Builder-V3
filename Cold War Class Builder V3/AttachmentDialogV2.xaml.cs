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
    public sealed partial class AttachmentDialogV2 : ContentDialog
    {
        private AttachmentClass attachmentClass,initialAtts;

        private int maxAttachments;
        public AttachmentDialogV2()
        {
            this.InitializeComponent();
            attachmentClass = new AttachmentClass();
            //initialAtts=
        }

        public AttachmentDialogV2(AttachmentClass attachmentClass, int maxsAttachments,IconClass gun)
        {
            GunIcon = gun;
            initialAtts = attachmentClass.Clone();
            maxAttachments = maxsAttachments;
            this.InitializeComponent();
            this.attachmentClass = attachmentClass;
        }
        public AttachmentDialogV2(AttachmentClass attachmentClass, int maxsAttachments, IconClass gun,AttachmentClass InitialAttachments)
        {
            initialAtts = InitialAttachments;
            GunIcon = gun;
            maxAttachments = maxsAttachments;
            this.InitializeComponent();
            this.attachmentClass = attachmentClass;
        }
        public IconClass GunIcon;
        public event EventHandler AttachmentSetEvent;
        public GunControl gunControl;
        public AttachmentClass returnAttachments;
        private bool ShowAllAtts = Settings.ShowAllAttachments;
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
            returnAttachments = initialAtts;
            AttachmentSetEvent?.Invoke(this, new EventArgs());
        }
        private AttachmentClass GetAttachmentClass()
        {
            return new AttachmentClass((OpticCombo.SelectedItem ?? "None").ToString(), (MuzzleCombo.SelectedItem ?? "None").ToString(), (BarrelCombo.SelectedItem ?? "None").ToString(), (BodyCombo.SelectedItem ?? "None").ToString(), (UnderbarrelCombo.SelectedItem ?? "None").ToString(), (MagazineCombo.SelectedItem ?? "None").ToString(), (HandleCombo.SelectedItem ?? "None").ToString(), (StockCombo.SelectedItem ?? "None").ToString());
        }
        private void ShowAllAttachments()
        {
            ClearCombos();
            List<String> OpticL = AttachmentClass.GetCustomAttachmentOfType(0);
            OpticL.AddRange(Data.OpticList);
            LoadIntoCombo(OpticCombo, OpticL);
            List<String> MuzzleL = AttachmentClass.GetCustomAttachmentOfType(1);
            MuzzleL.AddRange(Data.MuzzleList);
            LoadIntoCombo(MuzzleCombo, MuzzleL);
            List<String> BarrelL = AttachmentClass.GetCustomAttachmentOfType(2);
            BarrelL.AddRange(Data.BarrelList);
            LoadIntoCombo(BarrelCombo, BarrelL);
            List<String> BodyL = AttachmentClass.GetCustomAttachmentOfType(3);
            BodyL.AddRange(Data.BodyList);
            LoadIntoCombo(BodyCombo, BodyL);
            List<String> UnderbarrelL = AttachmentClass.GetCustomAttachmentOfType(4);
            UnderbarrelL.AddRange(Data.UnderbarrelList);
            LoadIntoCombo(UnderbarrelCombo, UnderbarrelL);
            List<String> MagazineL = AttachmentClass.GetCustomAttachmentOfType(5);
            MagazineL.AddRange(Data.MagazineList);
            LoadIntoCombo(MagazineCombo, MagazineL);
            List<String> HandleL = AttachmentClass.GetCustomAttachmentOfType(6);
            HandleL.AddRange(Data.GunHandleList);
            LoadIntoCombo(HandleCombo, HandleL);
            List<String> StockL = AttachmentClass.GetCustomAttachmentOfType(7);
            StockL.AddRange(Data.StockList);
            LoadIntoCombo(StockCombo, StockL);

        }

        private void ClearCombos()
        {
            OpticCombo.Items.Clear();
            MuzzleCombo.Items.Clear();
            BarrelCombo.Items.Clear();
            BodyCombo.Items.Clear();
            UnderbarrelCombo.Items.Clear();
            MagazineCombo.Items.Clear();
            HandleCombo.Items.Clear();
            StockCombo.Items.Clear();
        }

        private void LoadIntoCombo(ComboBox a, List<String> b)
        {
            b.ForEach(e => a.Items.Add(e));
        }
        private void LoadData()
        {
            if ((GunIcon != null && !ShowAllAtts))
            {
                GunAttachment ga = GunAttachment.GunAttachmentList.FirstOrDefault(e => e.Name == GunIcon.Name);
                if (ga != null)
                {
                    if (ga.showAllAttachments)
                    {
                        ShowAllAttachments();
                        AddNoneOptions();
                        return;
                    }
                    List<string> OpticL = ga.GetOptic();
                    OpticL.AddRange(AttachmentClass.GetCustomAttachmentOfType(0));
                    LoadIntoCombo(OpticCombo, OpticL);
                    List<string> MuzzleL = ga.GetMuzzle();
                    MuzzleL.AddRange(AttachmentClass.GetCustomAttachmentOfType(1));
                    LoadIntoCombo(MuzzleCombo, MuzzleL);
                    List<string> BarrelL = ga.GetBarrel();
                    BarrelL.AddRange(AttachmentClass.GetCustomAttachmentOfType(02));
                    LoadIntoCombo(BarrelCombo, BarrelL);
                    List<string> BodyL = ga.GetBody();
                    BodyL.AddRange(AttachmentClass.GetCustomAttachmentOfType(03));
                    LoadIntoCombo(BodyCombo, BodyL);
                    List<string> UnderbarrelL = ga.GetUnderbarrel();
                    UnderbarrelL.AddRange(AttachmentClass.GetCustomAttachmentOfType(04));
                    LoadIntoCombo(UnderbarrelCombo, UnderbarrelL);
                    List<string> MagazineL = ga.GetMagazine();
                    MagazineL.AddRange(AttachmentClass.GetCustomAttachmentOfType(05));
                    LoadIntoCombo(MagazineCombo, MagazineL);
                    List<string> HandleL = ga.GetHandle();
                    HandleL.AddRange(AttachmentClass.GetCustomAttachmentOfType(06));
                    LoadIntoCombo(HandleCombo, HandleL);
                    List<string> StockL = ga.GetStock();
                    StockL.AddRange(AttachmentClass.GetCustomAttachmentOfType(07));
                    LoadIntoCombo(StockCombo, StockL);
                    AddNoneOptions();
                    return;
                }
            }
            //incase it doesnt return and just shows all attachments
            ShowAllAttachments();
            AddNoneOptions();
        }
        private void AddNoneOptions()
        {
            if (!OpticCombo.Items.Contains("None"))
                OpticCombo.Items.Add("None");
            if (!MuzzleCombo.Items.Contains("None"))
                MuzzleCombo.Items.Add("None");
            if (!BarrelCombo.Items.Contains("None"))
                BarrelCombo.Items.Add("None");
            if (!BodyCombo.Items.Contains("None"))
                BodyCombo.Items.Add("None");
            if (!UnderbarrelCombo.Items.Contains("None"))
                UnderbarrelCombo.Items.Add("None");
            if (!MagazineCombo.Items.Contains("None"))
                MagazineCombo.Items.Add("None");
            if (!HandleCombo.Items.Contains("None"))
                HandleCombo.Items.Add("None");
            if (!StockCombo.Items.Contains("None"))
                StockCombo.Items.Add("None");


        }
        private void LoadAttachmentFromClass(AttachmentClass c)
        {
            if (!OpticCombo.Items.Contains(c.Optic))
                OpticCombo.Items.Add(c.Optic);
            OpticCombo.SelectedItem = c.Optic;
            if (!MuzzleCombo.Items.Contains(c.Muzzle))
                MuzzleCombo.Items.Add(c.Muzzle);
            MuzzleCombo.SelectedItem = c.Muzzle;
            if (!BarrelCombo.Items.Contains(c.Barrel))
                BarrelCombo.Items.Add(c.Barrel);
            BarrelCombo.SelectedItem = c.Barrel;
            if (!BodyCombo.Items.Contains(c.Body))
                BodyCombo.Items.Add(c.Body);
            BodyCombo.SelectedItem = c.Body;
            if (!UnderbarrelCombo.Items.Contains(c.Underbarrel))
                UnderbarrelCombo.Items.Add(c.Underbarrel);
            UnderbarrelCombo.SelectedItem = c.Underbarrel;
            if (!MagazineCombo.Items.Contains(c.Magazine))
                MagazineCombo.Items.Add(c.Magazine);
            MagazineCombo.SelectedItem = c.Magazine;
            if (!HandleCombo.Items.Contains(c.GunHandle))
                HandleCombo.Items.Add(c.GunHandle);
            HandleCombo.SelectedItem = c.GunHandle;
            if (!StockCombo.Items.Contains(c.Stock))
                StockCombo.Items.Add(c.Stock);
            StockCombo.SelectedItem = c.Stock;

        }
        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
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
            ShowAllAttachmentsButton.Content = ShowAllAtts ? "Show Attachments For Weapon" : "Show All Attachments";
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
            /*Debug.WriteLine("AttachmentDialogV2.checkIfLockNeeded: " + GetAttachmentClass().GetAttachmentCount());
            Debug.WriteLine("AttachmentDialogV2.checkIfLockNeeded p2: " + maxAttachments);*/
            if (GetAttachmentClass().GetAttachmentCount() >= maxAttachments)
            {
                foreach (var item in boxes)
                {
                    if (item.SelectedItem==null|| item.SelectedItem.Equals("None"))
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
            gui.AttachedObjects.Add(GetAttachmentClass()?? new AttachmentClass());
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
                AttachmentDialogV2 newattdiag = new AttachmentDialogV2((AttachmentClass)gui.AttachedObjects[0]?? new AttachmentClass(), (int)gui.AttachedObjects[1],GunIcon,initialAtts);
                newattdiag.AttachmentSetEvent += (EventHandler)gui.AttachedObjects[3];
                newattdiag.gunControl = (GunControl)gui.AttachedObjects[2];
                newattdiag.ShowAsync();
            }
            catch { }
        }

        private void ShowAllAttachments(object sender, TappedRoutedEventArgs e)
        {
            AttachmentClass ac = GetAttachmentClass();

            ShowAllAtts = !ShowAllAtts;
            ShowAllAttachmentsButton.Content = ShowAllAtts? "Show Attachments For Weapon" : "Show All Attachments";
            ClearCombos();
            LoadData();
            LoadAttachmentFromClass(ac);
            checkIfLockNeeded();
        }
    }
}
