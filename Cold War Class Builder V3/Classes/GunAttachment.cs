using System;
using System.Collections.Generic;
using System.Linq;

namespace Cold_War_Class_Builder_V3
{
    public class GunAttachment
    {
        public static List<GunAttachment> GunAttachmentList = new List<GunAttachment>();
        public string Name;
        public List<int> Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, GunHandle, Stock;
        bool isEmpty = false;
        public bool showAllAttachments = false;
        private List<String> GetData(List<int> a,List<String> b)
        {
            if (isEmpty)
                return new List<string>();
            if (showAllAttachments)
                return b;
            return a.Select(e => b[e]).ToList(); ;
        }
        public List<String> GetOptic()
        {
            return GetData(Optic, Data.OpticList);
        }
        public List<String> GetMuzzle()
        {
            return GetData(Muzzle, Data.MuzzleList);
        }
        public List<String> GetBarrel()
        {
            return GetData(Barrel, Data.BarrelList);
        }
        public List<String> GetBody()
        {
            return GetData(Body, Data.BodyList);
        }
        public List<String> GetUnderbarrel()
        {
            return GetData(Underbarrel, Data.UnderbarrelList);
        }
        public List<String> GetMagazine()
        {
            return GetData(Magazine, Data.MagazineList);
        }
        public List<String> GetHandle()
        {
            return GetData(GunHandle, Data.GunHandleList);
        }
        public List<String> GetStock()
        {
            return GetData(Stock, Data.StockList);
        }

        public GunAttachment(String name)
        {
            Name = name;
            isEmpty = true;
            Optic = new List<int>();
            Muzzle = new List<int>();
            Barrel = new List<int>();
            Body = new List<int>();
            Underbarrel = new List<int>();
            Magazine = new List<int>();
            GunHandle = new List<int>();
            Stock = new List<int>();
        }
        public GunAttachment(string name, bool showAllAttachments)
        {
            Name = name;
            isEmpty = true;
            this.showAllAttachments = true;
            Optic = new List<int>();
            Muzzle = new List<int>();
            Barrel = new List<int>();
            Body = new List<int>();
            Underbarrel = new List<int>();
            Magazine = new List<int>();
            GunHandle = new List<int>();
            Stock = new List<int>();
        }
        public GunAttachment(string name, List<int> op, List<int> m, List<int> bar, List<int> bod, List<int> under, List<int> mag, List<int> hand, List<int> stock)
        {
            isEmpty = false;
            Name = name;
            Optic = op;
            Muzzle = m;
            Barrel = bar;
            Body = bod;
            Underbarrel = under;
            Magazine = mag;
            GunHandle = hand;
            Stock = stock;
        }
    }
}
