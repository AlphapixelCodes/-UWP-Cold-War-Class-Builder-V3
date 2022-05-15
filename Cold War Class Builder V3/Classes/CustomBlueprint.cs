using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cold_War_Class_Builder_V3
{
    public class CustomBlueprint
    {
        private static bool hasupdated;
        public static bool HasUpdated { get { return hasupdated; } set { hasupdated = value; Debug.WriteLine("CustomBlueprintUpdated");FileManager.PerformAutoSave(); } }
        public static List<CustomBlueprint> CustomBlueprints = new List<CustomBlueprint>();
        public IconClass gunIcon;
        public string Name;
        public AttachmentClass Attachments;
        public static string GetSaveString()
        {
            if (CustomBlueprints.Count == 0)
                return "None";
            return string.Join(';', CustomBlueprints.Select(a => a.ToString()).ToList());
        }
        public static void LoadBlueprints(String saveString)
        {

            CustomBlueprints = new List<CustomBlueprint>();
            if(!saveString.Equals("None"))
            foreach (var item in saveString.Split(';'))
            {
                CustomBlueprints.Add(LoadFromString(item));
            }
        }
        public CustomBlueprint(String buildName, AttachmentClass attachments, IconClass gun)
        {
            Name = buildName;
            Attachments = attachments;
            gunIcon = gun;
        }
        public bool isEqual(CustomBlueprint b)
        {
            return gunIcon.Name.Equals(b.gunIcon.Name) && b.Attachments.isEqual(Attachments);
        }
        public static CustomBlueprint LoadFromString(string s)
        {
            string[] vs = s.Split("\t");
            Debug.WriteLine(s);
            return new CustomBlueprint(vs[0], AttachmentClass.LoadFromString(vs[2]), Data.getIconByName(Data.AllGuns, vs[1]));
        }
        public override string ToString()
        {
            return String.Join('\t', Name,gunIcon.Name,Attachments);
        }
    }
}
