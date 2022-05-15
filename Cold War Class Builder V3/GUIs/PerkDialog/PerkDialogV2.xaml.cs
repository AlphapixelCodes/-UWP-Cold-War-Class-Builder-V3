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
    public sealed partial class PerkDialogV2 : Windows.UI.Xaml.Controls.ContentDialog
    {
        public PerkDialogV2()
        {
            this.InitializeComponent();
        }
        private List<IconClass> preselected, disabledPerks;
        private bool lawBreaker;
        public RelativePanel PerkRow;
        private List<SelectControl> Selects = new List<SelectControl>();
        public List<SelectControl> returnPerks;
        public event EventHandler ReturnEvent;
        public PerkDialogV2(RelativePanel perkrow, List<IconClass> preSelected, List<IconClass> disabledPerks, bool Lawbreaker)
        {
            lawBreaker = Lawbreaker;
            this.disabledPerks = disabledPerks;
            this.InitializeComponent();
            preselected = preSelected;
            PerkRow = perkrow;
        }
        private void addperk(List<IconClass> icons, StackPanel stack)
        {
            icons.ForEach(e => {
                SelectControl sc = new SelectControl(e);
                sc.Margin = new Thickness(0, 5, 0, 2);
                sc.Tapped += SelectControl_Tapped;
                stack.Children.Add(sc);
                Selects.Add(sc);
                if (preselected != null && preselected.Any(a => a.Name.Equals(e.Name)))
                    sc.Selected = true;
                if (disabledPerks.Any(z => z.Name == e.Name))
                    sc.SetEnabled(false);//sc.IsEnabled = false;
            });
        }

        private void SelectControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectControl sc = (SelectControl)sender;
            if (getSelected().Count < 3)
            {
                sc.Selected = !sc.Selected;
            }
            else if (sc.Selected)
            {

                sc.Selected = false;
            }
            LockAllPerksIf3Selected();
            LawBreaker();
            base.PrimaryButtonText = getSelected().Count < 3 ? "" : "Done";
        }

        private List<SelectControl> getSelected()
        {
            return Selects.FindAll(e => e.Selected);
        }
        private void LawBreaker()
        {
            if (lawBreaker)
                return;
            List<IconClass> ic = new List<IconClass>();
            List<SelectControl> selected = getSelected();
            foreach (var a in selected)
            {
                if (Data.Perk1.Contains(a.Iconclass))
                    ic = Data.Perk1;
                else if (Data.Perk2.Contains(a.Iconclass))
                    ic = Data.Perk2;
                else if (Data.Perk3.Contains(a.Iconclass))
                    ic = Data.Perk3;
                if (ic.Count != 0)
                {
                    List<SelectControl> typeselects = Selects.FindAll(e => ic.Contains(e.Iconclass));
                    typeselects.ForEach(e => {
                        if (!selected.Contains(e))
                            e.SetEnabled(false);
                    });
                }

            }

        }
        private void LockAllPerksIf3Selected()
        {
            if (getSelected().Count == 3)
            {
                Selects.ForEach(a => {
                    if (!a.Selected)
                        a.SetEnabled(false);
                });
            }
            else
            {
                Selects.ForEach(a => {
                    if (!disabledPerks.Contains(a.Iconclass))
                        a.SetEnabled(true);
                });
            }
        }

        private void loadPerks()
        {
            Selects = new List<SelectControl>();
            addperk(Data.Perk1,Stack1);
            addperk(Data.Perk2,Stack2);
            addperk(Data.Perk3,Stack3);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            List<SelectControl> ret = getSelected();
            if (ret.Count < 3)
                args.Cancel = true;
            else
            {
                returnPerks = ret;
                ReturnEvent?.Invoke(this, new EventArgs());
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            base.PrimaryButtonText = getSelected().Count < 3 ? "" : "Done";
            base.SecondaryButtonText = "Cancel";
            loadPerks();
            LockAllPerksIf3Selected();
            LawBreaker();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
