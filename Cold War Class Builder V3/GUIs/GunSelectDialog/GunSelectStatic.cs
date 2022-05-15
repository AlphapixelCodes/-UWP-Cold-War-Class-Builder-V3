using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cold_War_Class_Builder_V3
{
    public static class GunSelectStatic
    {
        public enum Type {Fancy,ComboBox,Normal };
        public static GunSelectInterface GetGunSelectGUI(bool startingtabAR,bool isGunfighter)
        {
            switch (Settings.GunSelectGuiType)
            {
                case Type.ComboBox:
                    return new ComboBoxGunSelectDialog(startingtabAR,isGunfighter);
                case Type.Fancy:
                    return new GunSelectDialogV2(startingtabAR, isGunfighter);
                case Type.Normal:
                    return new GunSelectDialog(startingtabAR, isGunfighter);
            }
            return null;
            
        }
    }
}
