using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cold_War_Class_Builder_V3
{
    public class AttachmentClass
    {
        private static bool hasupdated;
        public static bool HasUpdated { get { return hasupdated; }set { hasupdated = value;Debug.WriteLine("AttachmentUpdated"); } }        public string Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock;
        public static List<CustomAttachment> CustomAttachments = new List<CustomAttachment>();
        public static String GetCustomSaveString()
        {
            if (CustomAttachments.Count == 0)
                return "None";
            return String.Join(';', CustomAttachments);
        }
        public static List<String> GetCustomAttachmentOfType(int type)
        {
            return (from ca in CustomAttachments where ca.Type == type select ca.Name).ToList();
        }
        public static void LoadCustomFromString(string s)
        {
            CustomAttachments = new List<CustomAttachment>();
            if (s.Equals("None"))
                return;
            foreach (var a in s.Split(";"))
            {
                CustomAttachment ca = new CustomAttachment(a);
                if(!Data.AllAttachments[ca.Type].Contains(ca.Name))//checking to see if i added the attachment
                    CustomAttachments.Add(ca); 
            }
        }
        public class CustomAttachment {
            //types Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock
            public int Type;
            public String Name;
            public CustomAttachment(int type,string name)
            {
                Type = type;
                Name = name;
            }
            /// <summary>
            /// for load from string
            /// </summary>
            /// <param name="s"></param>
            public CustomAttachment(string s)
            {
                string[] vs = s.Split("\t");
                Type = int.Parse(vs[0]);
                Name = vs[1];
            }
            public override string ToString()
            {
                return Type + "\t" + Name;
            }
        }

        public AttachmentClass(string optic, string muzzle, string barrel, string body, string underbarrel, string magazine, string handle, string stock)
        {
            Optic = optic;
            Muzzle = muzzle;
            Barrel = barrel;
            Body = body;
            Underbarrel = underbarrel;
            Magazine = magazine;
            GunHandle = handle;
            Stock = stock;
        }
        public AttachmentClass()
        {
            Optic = "None";
            Muzzle = "None";
            Barrel = "None";
            Body = "None";
            Underbarrel = "None";
            Magazine = "None";
            GunHandle = "None";
            Stock = "None";
        }
        public int GetAttachmentCount()
        {
            int ret = 0;
            string[] vs = new string[] { Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock };
            for (int i = 0; i < vs.Length; i++)
            {
                ret += vs[i] != "None" ? 1 : 0;
            }
            return ret;
        }
        public void RemoveLastThreeAttachments()
        {
            
            string b = "None";
            //if (Stock != b)
            if (GetAttachmentCount() > 5)
                Stock = b;
            if (GetAttachmentCount() > 5)
                GunHandle = b;
            if (GetAttachmentCount() > 5)
                Magazine = b;
        }
        public AttachmentClass Clone()
        {
            return new AttachmentClass(Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock);
        }
        public override string ToString()
        {
            return string.Join("|", new string[] { Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock });
        }
        public static AttachmentClass LoadFromString(String s)
        {
            string[] vs = s.Split('|');
            Console.WriteLine("AttachmentClass.GetAttachmentClassFromString: " + s);
            AttachmentClass ret = new AttachmentClass();
            /* Console.WriteLine("loading atts:");
             foreach (var item in vs)
             {
                 Console.WriteLine(item);
             }*/

            ret.Optic = vs[0];
            ret.Muzzle = vs[1];
            ret.Barrel = vs[2];
            ret.Body = vs[3];
            ret.Underbarrel = vs[4];
            ret.Magazine = vs[5];
            ret.GunHandle = vs[6];
            ret.Stock = vs[7];
            return ret;
        }
        public bool isEqual(AttachmentClass b)
        {
            return (ToString().Equals(b.ToString()));
        }
    }
}
