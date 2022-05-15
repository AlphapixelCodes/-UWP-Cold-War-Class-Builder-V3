using System;
using Windows.UI.Xaml.Controls;
namespace Cold_War_Class_Builder_V3
{
    public class FlyoutMenuItemExtended:MenuFlyoutItem
    {
        public object AttachedObject;
        public FlyoutMenuItemExtended(String Text,object attachedObject)
        {
            AttachedObject = attachedObject;
            this.Text = Text;
        }
    }
}
