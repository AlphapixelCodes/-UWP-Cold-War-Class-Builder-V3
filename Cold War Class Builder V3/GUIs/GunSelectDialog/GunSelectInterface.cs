using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Cold_War_Class_Builder_V3
{
    public interface GunSelectInterface
    {
        event EventHandler UpdateEvent;
        IconClass GetReturnValue();
        GunControl GetGunControl();
        void SetGunControl(GunControl x);
    }
}
