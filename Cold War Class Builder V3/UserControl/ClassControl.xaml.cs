using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Cold_War_Class_Builder_V3
{
    public sealed partial class ClassControl : UserControl
    {
        public ClassControl()
        {
            this.InitializeComponent();
        }
        private AttachmentClass PrimaryAttachments=new AttachmentClass(), SecondaryAttachments=new AttachmentClass();
        public void SetBuildName(string name)
        {
            this.BuildNameLabel.Text = name;
        }
        private void GunSelected(object SENDER, EventArgs e)
        {
            Debug.WriteLine("new gun selected");
            GunSelectInterface gui = (GunSelectInterface)SENDER;
            ClassBuild cb = ClassBuild.CurrentBuild;
            if (gui.GetGunControl().Name.Equals("PrimaryGunControl"))
            {
                if (gui.GetReturnValue().Name.Equals(PrimaryGunControl.iconClass.Name))
                    return;
                PrimaryAttachments = new AttachmentClass();
                cb.primaryAtt = PrimaryAttachments;
                cb.Primary = gui.GetReturnValue().Name;
            }
            else
            {
                if (gui.GetReturnValue().Name.Equals(SecondaryGunControl.iconClass.Name))
                    return;
                SecondaryAttachments = new AttachmentClass();
                cb.secondaryAtt = SecondaryAttachments;
                cb.Secondary = gui.GetReturnValue().Name;
            }
            
            loadGunIntoControl(gui.GetReturnValue(), gui.GetGunControl());
            SaveGunsToBuild();
        }
        private void GunSelectedByGUI(IconClass gun,GunControl control)
        {
            Debug.WriteLine("ClassControl.GunSelectedByGUI");
            ClassBuild cb = ClassBuild.CurrentBuild;
            if (control.Name.Equals("PrimaryGunControl"))
            {
                if (gun.Name.Equals(PrimaryGunControl.iconClass.Name))
                    return;
                PrimaryAttachments = new AttachmentClass();
                cb.primaryAtt = PrimaryAttachments;
                cb.Primary = gun.Name;
            }
            else
            {
                if (gun.Name.Equals(SecondaryGunControl.iconClass.Name))
                    return;
                SecondaryAttachments = new AttachmentClass();
                cb.secondaryAtt = SecondaryAttachments;
                cb.Secondary = gun.Name;
            }
            loadGunIntoControl(gun, control);
            SaveGunsToBuild();
        }
        private async void GunControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GunControl gun = (GunControl)sender;
            GunSelectInterface gsi = GunSelectStatic.GetGunSelectGUI(gun.Equals(PrimaryGunControl), WildCardIcon.IconClass.Name == "LAW BREAKER");
            gsi.SetGunControl(gun);
            gsi.UpdateEvent += (a, b) => GunSelectedByGUI(gsi.GetReturnValue(), gsi.GetGunControl());
            ((ContentDialog)gsi).ShowAsync();
        }

        public void LoadClassBuild(ClassBuild b)
        {
            BuildNameLabel.Text = b.Name;
            PrimaryGunControl.Update(Data.getIconByName(Data.AllGuns,b.Primary));
            PrimaryAttachments = b.primaryAtt;
            SecondaryGunControl.Update(Data.getIconByName(Data.AllGuns,b.Secondary));
            SecondaryAttachments = b.secondaryAtt;
            SecondaryGunControl.SetSelectedBoxes(b.secondaryAtt.GetAttachmentCount());
            LethalIcon.Update(Data.getIconByName(Data.LethalList, b.Lethal));
            TacticalIcon.Update(Data.getIconByName(Data.TacticalList, b.Tactical));
            FieldUpgradeIcon.Update(Data.getIconByName(Data.FieldUpgradeList, b.FieldUpgrade));
            IconClass p1, p2, p3, p4, p5, p6;
            p1 = Data.getIconByName(Data.AllPerks,b.Perk1);
            p2 = Data.getIconByName(Data.AllPerks, b.Perk2);
            p3 = Data.getIconByName(Data.AllPerks, b.Perk3);
            p4 = Data.getIconByName(Data.AllPerks, b.Perk4);
            p5 = Data.getIconByName(Data.AllPerks, b.Perk5);
            p6 = Data.getIconByName(Data.AllPerks, b.Perk6);
            LoadPerkRow(new IconClass[] { p1, p2, p3 }.ToList(), PerkRow1);
            LoadPerkRow(new IconClass[] { p4, p5, p6 }.ToList(), PerkRow2);
            WildCardIcon.Update(Data.getIconByName(Data.WildCardList, b.Wildcard));
            Gunfighter();
            PerkGreed();

        }
        private void SavePerksToBuild()
        {
            ClassBuild.HasUpdated = true;
            ClassBuild CB = ClassBuild.CurrentBuild;
            List<IconClass> prow = GetPerkRowPerks(PerkRow1);
            CB.Perk1 = prow[0].Name;
            CB.Perk2 = prow[1].Name;
            CB.Perk3 = prow[2].Name;
            prow = GetPerkRowPerks(PerkRow2);
            CB.Perk4 = prow[0].Name;
            CB.Perk5 = prow[1].Name;
            CB.Perk6 = prow[2].Name;
        }
        private void SaveGunsToBuild()
        {
            ClassBuild CB = ClassBuild.CurrentBuild;
            CB.Primary = PrimaryGunControl.iconClass.Name;
            Debug.WriteLine(PrimaryGunControl.iconClass.Name);
            CB.primaryAtt = PrimaryAttachments.Clone();
            CB.Secondary = SecondaryGunControl.iconClass.Name;
            CB.secondaryAtt = SecondaryAttachments;
            ClassBuild.HasUpdated = true;
        }
        public void SaveClassBuild()
        {
            
            ClassBuild CB = ClassBuild.CurrentBuild;
            SaveGunsToBuild();
            CB.Wildcard = WildCardIcon.IconClass.Name;
            CB.Lethal = LethalIcon.IconClass.Name;
            CB.Tactical = TacticalIcon.IconClass.Name;
            CB.FieldUpgrade = FieldUpgradeIcon.IconClass.Name;
            SavePerksToBuild();
            ClassBuild.HasUpdated = true;

        }
        private void loadGunIntoControl(IconClass gun,GunControl gc)
        {
            gc.Update(gun);
            gc.SetSelectedBoxes(0);
        }
        private void Gunfighter()//bool isGunfighter)
        {
            bool isGunfighter=WildCardIcon.IconClass.Name == "GUNFIGHTER";
            if (PrimaryAttachments == null)
                PrimaryAttachments = new AttachmentClass();
            PrimaryGunControl.ShowHideExtraAttachments(isGunfighter);
            if(PrimaryAttachments.GetAttachmentCount()>5 && !isGunfighter)
            {
                PrimaryAttachments.RemoveLastThreeAttachments();
            }
            PrimaryGunControl.UpdateAttachments(PrimaryAttachments);
        }
        private void PerkGreed()
        {
            bool v = WildCardIcon.IconClass.Name == "PERK GREED";
            PerkRow2.Visibility = v ? Visibility.Visible : Visibility.Collapsed;
            if (!v)
            {
                LoadPerkRow(new IconClass[] {Data.NoneIconClass, Data.NoneIconClass, Data.NoneIconClass }.ToList(), PerkRow2);
            }
            
        }
        
        private bool LawBreakerCheckRow(RelativePanel perkrow)
        {
            bool p1 = false, p2 = false, p3 = false;
            bool needsreset = false;
            foreach (var e in GetPerkRowPerks(PerkRow1))
            {
                if (Data.Perk1.Contains(e))
                {
                    if (p1)
                        return true;
                    p1 = true;
                }
                else if (Data.Perk2.Contains(e))
                {
                    if (p2)
                        return true;
                    p2 = true;
                }
                else if (Data.Perk3.Contains(e))
                {
                    if (p3)
                        return true;
                    p3 = true;
                }
            }
            return false;
        }
        private void LawBreaker()
        {
            bool isLawBreaker = WildCardIcon.IconClass.Name.Equals("LAW BREAKER");
            if (!isLawBreaker)
            {
                if (!Data.SecondaryList.Contains(SecondaryGunControl.iconClass))
                {
                    SecondaryGunControl.Update(Data.getIconByName(Data.SecondaryList, "1911"));
                    SecondaryAttachments = new AttachmentClass();
                    SecondaryGunControl.UpdateAttachments(SecondaryAttachments);
                    SaveGunsToBuild();
                }
                bool v = LawBreakerCheckRow(PerkRow1);
                bool c = LawBreakerCheckRow(PerkRow2);
                if (v || c)
                {
                    Debug.WriteLine("reset");
                    LoadPerkRow(new IconClass[] { Data.Perk1[1], Data.Perk2[1], Data.Perk3[1] }.ToList(), PerkRow1);
                    if(WildCardIcon.IconClass.Name.Equals("PERK GREED"))
                        LoadPerkRow(new IconClass[] { Data.Perk1[2], Data.Perk2[2], Data.Perk3[2] }.ToList(), PerkRow2);
                    SavePerksToBuild();
                }
                    
            }
        }
        private void  SelectIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextImageControl x = (TextImageControl)sender;
            ItemSelectDialog gui;
            switch (x.Name)
            {
                case "TacticalIcon":
                    gui = new ItemSelectDialog(Data.TacticalList, "Select Tactical");
                    gui.textImageControl = x;
                    gui.ItemSelectedEvent += SelectIcon_ItemSelectedEvent;
                    gui.ShowAsync();
                    
                    break;
                case "LethalIcon":
                    gui = new ItemSelectDialog(Data.LethalList, "Select Lethal");
                    gui.textImageControl = x;
                    gui.ItemSelectedEvent += SelectIcon_ItemSelectedEvent;
                    gui.ShowAsync();
                    break;
                case "FieldUpgradeIcon":
                    gui = new ItemSelectDialog(Data.FieldUpgradeList, "Select Field Upgrade");
                    gui.textImageControl = x;
                    gui.ItemSelectedEvent += SelectIcon_ItemSelectedEvent;
                    gui.ShowAsync();
                    break;
                case "WildCardIcon":
                    gui = new ItemSelectDialog(Data.WildCardList, "Select Wildcard");
                    gui.textImageControl = x;
                    gui.ItemSelectedEvent += SelectIcon_ItemSelectedEvent;
                    gui.ShowAsync();
                    break;
            }
        }

        private void SelectIcon_ItemSelectedEvent(object sender, EventArgs e)
        {
            ItemSelectDialog gui = (ItemSelectDialog)sender;
            String retName = gui.returnValue.Name;
            gui.textImageControl.Update(gui.returnValue);
            switch (gui.textImageControl.Name)
            {
                case "WildCardIcon":
                    LawBreaker();
                    Gunfighter();
                    PerkGreed();
                    ClassBuild.CurrentBuild.Wildcard = retName;
                    break;
                case "FieldUpgradeIcon":
                    ClassBuild.CurrentBuild.FieldUpgrade = retName;
                    break;
                case "TacticalIcon":
                    ClassBuild.CurrentBuild.Tactical = retName;
                    break;
                case "LethalIcon":
                    ClassBuild.CurrentBuild.Lethal = retName;
                    break;
            }
            ClassBuild.HasUpdated = true;
        }

        

        private void GunSmith_Hover(object sender, PointerRoutedEventArgs e)
        {
            ((Image)sender).Source = Application.Current.Resources["GunsmithHover"] as BitmapSource;
        }
        private void GunSmith_UnHover(object sender, PointerRoutedEventArgs e)
        {
            ((Image)sender).Source = Application.Current.Resources["Gunsmith"] as BitmapSource;
        }
        private void Gunsmith_Tap(object sender, TappedRoutedEventArgs e)
        {
            Image pic = (Image)sender;
            AttachmentDialogV2 gui;
            if (pic.Name == "PrimaryGunsmith")
            {
                gui = new AttachmentDialogV2(PrimaryAttachments ?? new AttachmentClass(), WildCardIcon.IconClass.Name.Equals("GUNFIGHTER") ? 20 : 5,PrimaryGunControl.iconClass);
                gui.gunControl = PrimaryGunControl;
                
            }
            else
            {
                gui = new AttachmentDialogV2(SecondaryAttachments ?? new AttachmentClass(), 5,SecondaryGunControl.iconClass);
                gui.gunControl = SecondaryGunControl;
            }
            gui.AttachmentSetEvent += (a,b)=> {
                if (gui.gunControl.Name.Equals("PrimaryGunControl"))
                {
                    PrimaryAttachments = gui.returnAttachments?? new AttachmentClass();
                    PrimaryGunControl.SetSelectedBoxes(PrimaryAttachments.GetAttachmentCount());
                }
                else
                {
                    SecondaryAttachments = gui.returnAttachments?? new AttachmentClass();
                    SecondaryGunControl.SetSelectedBoxes(SecondaryAttachments.GetAttachmentCount());
                }
                SaveGunsToBuild();
            };
            gui.ShowAsync();
        }

     
        private List<IconClass> GetPerkRowPerks(RelativePanel perkrow)
        {
            
            return perkrow.Children.OfType<TextImageControl>().ToList().Select(e => e.IconClass).ToList();
        }
        private void PerkRowTapped(object sender, TappedRoutedEventArgs e)
        {
            RelativePanel pan = (RelativePanel)sender;
            List<IconClass> perksdisabled;
            List<IconClass> perksSelected = GetPerkRowPerks(pan);
            if (pan.Equals(PerkRow1))
                perksdisabled = GetPerkRowPerks(PerkRow2);
            else
                perksdisabled = GetPerkRowPerks(PerkRow1);
            perksSelected.ForEach(a => Debug.WriteLine(a.Name));
            if (Settings.FancyPerkDialog)
            {
                PerkDialogV2 gui = new PerkDialogV2(pan, perksSelected, perksdisabled, WildCardIcon.IconClass.Name.Equals("LAW BREAKER"));
                gui.ReturnEvent += (z,b)=> {
                    LoadPerkRow(gui.returnPerks.Select(a => a.Iconclass).ToList(), gui.PerkRow);
                    SavePerksToBuild();
                };
                gui.ShowAsync();
            }
            else
            {
                PerkDialog gui = new PerkDialog(pan, perksSelected, perksdisabled, WildCardIcon.IconClass.Name.Equals("LAW BREAKER"));
                gui.ReturnEvent += (z, b) => {
                    LoadPerkRow(gui.returnPerks.Select(a => a.Iconclass).ToList(), gui.PerkRow);
                    SavePerksToBuild();
                };
                gui.ShowAsync();
            }           
        }
        private void LoadPerkRow(List<IconClass> perks,RelativePanel row)
        {
            List<TextImageControl> tic = row.Children.OfType<TextImageControl>().ToList();
            for (int i = 0; i < tic.Count; i++)
            {
                tic[i].Update(perks[i]);
            }
        }

        private void ViewButton_MouseEnter(object sender, PointerRoutedEventArgs e)
        {
            ((Image)sender).Source = Application.Current.Resources["ViewButtonHover"] as BitmapSource;
        }
        private void ViewButton_MouseExit(object sender, PointerRoutedEventArgs e)
        {
            ((Image)sender).Source = Application.Current.Resources["ViewButton"] as BitmapSource;
        }
        private void ViewButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image s = (Image) sender;
            if (s.Name.Equals("PrimaryViewButton"))
            {
                new ViewGunDialog(PrimaryGunControl.iconClass.Name, PrimaryAttachments).ShowAsync();
            }
            else
            {
                new ViewGunDialog(SecondaryGunControl.iconClass.Name, SecondaryAttachments).ShowAsync();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
           
            PrimaryGunControl.ShowHideExtraAttachments(false);
            SecondaryGunControl.ShowHideExtraAttachments(false);
            PrimaryGunControl.FlyoutMenuItem_Tapped += GunControlFlyoutMenu_Tapped;
            SecondaryGunControl.FlyoutMenuItem_Tapped += GunControlFlyoutMenu_Tapped;
            
        }

       private void CustomBlueprintsDialogShowBuilds(GunControl gc,List<CustomBlueprint> blueprints)
        {
            
            CustomBlueprintsDialogV2 guiv2 = new CustomBlueprintsDialogV2(blueprints);
            guiv2.GunClass = gc;
            CustomBlueprintsDialog gui = new CustomBlueprintsDialog(blueprints);
            gui.GunClass = gc;
            EventHandler selected_event = (a, b) =>
            {
                SelectControl sc = (SelectControl)a;
                CustomBlueprint cb = (CustomBlueprint)sc.AttachedObject;
                gc.Update(cb.gunIcon);
                gc.UpdateAttachments(cb.Attachments);
                AttachmentClass ac = cb.Attachments.Clone();
                if (gc.Equals(PrimaryGunControl))
                {
                    if (!WildCardIcon.IconClass.Name.Equals("GUNFIGHTER"))
                        ac.RemoveLastThreeAttachments();
                    PrimaryAttachments = ac;
                }
                else
                {
                    ac.RemoveLastThreeAttachments();
                    SecondaryAttachments = ac;
                }
                if (gc.iconClass != cb.gunIcon)
                    gc.Update(cb.gunIcon);
                SaveClassBuild();
                if (Settings.FancyBlueprintDialog)
                    guiv2.Hide();
                else
                    gui.Hide();
            };
            gui.BlueprintSelected_Event += selected_event;
            guiv2.BlueprintSelected_Event += selected_event;
            if (Settings.FancyBlueprintDialog)
                guiv2.ShowAsync();
            else
                gui.ShowAsync();
        }

        private void GunControlFlyoutMenu_Tapped(object sender, EventArgs e)
        {
            FlyoutMenuItemExtended fmie = (FlyoutMenuItemExtended) sender;
            
            GunControl gc = (GunControl)fmie.AttachedObject;
            switch (fmie.Text)
            {
                case "View":
                    if (gc.Equals(PrimaryGunControl))
                        new ViewGunDialog(PrimaryGunControl.iconClass.Name, PrimaryAttachments).ShowAsync();
                    else
                        new ViewGunDialog(SecondaryGunControl.iconClass.Name, SecondaryAttachments).ShowAsync();
                    break;
                case "Load Blueprint For Gun":
                    CustomBlueprintsDialogShowBuilds(gc, CustomBlueprint.CustomBlueprints.FindAll(z => z.gunIcon.Name == gc.iconClass.Name));
                    break;
                case "Load Blueprint":
                    List<CustomBlueprint> bs = new List<CustomBlueprint>();
                    if (WildCardIcon.IconClass.Name.Equals("LAW BREAKER"))
                        bs = CustomBlueprint.CustomBlueprints;
                    else if(gc == PrimaryGunControl)
                        bs = CustomBlueprint.CustomBlueprints.FindAll(z => !Data.SecondaryList.Contains(z.gunIcon));
                    else
                        bs = CustomBlueprint.CustomBlueprints.FindAll(z => Data.SecondaryList.Contains(z.gunIcon));
                    CustomBlueprintsDialogShowBuilds(gc, bs);
                    break;
                case "Save Blueprint":
                    NameDialog nd = new NameDialog();
                    CustomBlueprint cb1;
                    if (gc.Equals(PrimaryGunControl))
                        cb1 = new CustomBlueprint("",PrimaryAttachments, PrimaryGunControl.iconClass);
                    else
                        cb1 = new CustomBlueprint("", SecondaryAttachments, SecondaryGunControl.iconClass);

                    if (CustomBlueprint.CustomBlueprints.Any(z => z.isEqual(cb1)))
                    {
                        nd.ErrorTextToWrite = "Blueprint already exists with that same exact gun and attachment loadout";                       
                        return;
                    }
                        nd.Confirmation_Event += (dialog, l) =>
                    {
                        string v = nd.ReturnValue.ToUpper();
                        if (v.Length < 3 || v.Length > 27)
                            nd.ErrorTextToWrite = "Name must be between 3 and 23 characters";
                        else if (Regex.IsMatch(v, "[^A-Z 0-9-]+"))
                        {
                            nd.ErrorTextToWrite = "Name can only contain A-Z 0-9 space and -";
                        }
                        else if (CustomBlueprint.CustomBlueprints.Any(z => z.gunIcon == cb1.gunIcon && z.Name.Equals(v)))
                        {
                            nd.ErrorTextToWrite = "Blueprint already exists by that name for the " + cb1.gunIcon.Name;
                        }
                        else
                        {
                            nd.Hide();
                            cb1.Name = v;
                            CustomBlueprint.CustomBlueprints.Add(cb1);
                            CustomBlueprint.HasUpdated = true;
                        }
                    
                    };
                    nd.ShowAsync();
                    break;

            }
            
        }
    }
}
