using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace Cold_War_Class_Builder_V3
{
    public static class Data
    {
        private static bool loaded;
        public static List<IconClass> AssaultRiflesList, SMGList, TactRifleList, LMGList, SniperList, SecondaryList, AllGuns;//,PrimaryGuns;

        public static List<IconClass> TacticalList,LethalList,FieldUpgradeList,WildCardList;
        public static List<String> OpticList, MuzzleList, BarrelList, BodyList, UnderbarrelList, MagazineList, GunHandleList, StockList;
        public static List<List<String>> AllAttachments;
        public static List<IconClass> Perk1, Perk2, Perk3,AllPerks;
        public static IconClass NoneIconClass;
        public static BitmapSource SelectedDot, BlankImage,BlackStar,Star;
        
        public static IconClass getGunByName(String name)
        {
            return AllGuns.FirstOrDefault(e => e.Name.Equals(name)) ?? NoneIconClass;
        }
        public static IconClass getPerkByName(String name)
        {
            return AllPerks.FirstOrDefault(e => e.Name.Equals(name)) ?? NoneIconClass;
        }
        public static IconClass getIconByName(List<IconClass> icons,String name)
        {
           return icons.FirstOrDefault(e => e.Name.Equals(name)) ?? NoneIconClass;
        }


        public static void Load()
        {
            if (loaded)
                return;
            
            SelectedDot = loadResource("SelectedDot");
            BlankImage = loadResource("BlankImage");
            NoneIconClass = new IconClass("None", loadResource("BlankIcon"));
            BlackStar = loadResource("BlackStar");
            Star = loadResource("Star");
            loadGuns();
            loadTacts();
            loadLethal();
            loadFieldUpgrade();
            loadWildCards();
            loadAttachments();
            loadPerks();
            loaded = true;
        }

     

        public static BitmapSource loadResource(string s)
        {
            return Application.Current.Resources[s] as BitmapSource;

        }
        private static void loadGuns()
        {
            //assault rifles
            AssaultRiflesList = new List<IconClass>();
            AssaultRiflesList.Add(new IconClass("xm4", loadResource("XM4")));
            AssaultRiflesList.Add(new IconClass("AK-47", loadResource("AK-47")));
            AssaultRiflesList.Add(new IconClass("Krig 6", loadResource("Krig_6")));
            AssaultRiflesList.Add(new IconClass("qbz-83", loadResource("QBZ_83")));
            AssaultRiflesList.Add(new IconClass("FFAR 1", loadResource("FFAR_1")));
            AssaultRiflesList.Add(new IconClass("Groza", loadResource("Groza")));
            AssaultRiflesList.Add(new IconClass("Fara 83", loadResource("Fara_83")));
            //smgs
            SMGList = new List<IconClass>();
            SMGList.Add(new IconClass("AK-74u", loadResource("AK_74u")));
            SMGList.Add(new IconClass("Bullfrog", loadResource("Bullfrog")));
            SMGList.Add(new IconClass("ksp 45", loadResource("KSP_45")));
            SMGList.Add(new IconClass("lc10", loadResource("LC10")));
            SMGList.Add(new IconClass("MAC-10", loadResource("MAC_10")));
            SMGList.Add(new IconClass("Milano 821", loadResource("Milano_821")));
            SMGList.Add(new IconClass("mp5", loadResource("MP5")));
            SMGList.Add(new IconClass("PPSH-41", loadResource("PPSH_41")));

            TactRifleList = new List<IconClass>();
            TactRifleList.Add(new IconClass("AUG", loadResource("AUG")));
            TactRifleList.Add(new IconClass("DMR 14", loadResource("DMR 14")));
            TactRifleList.Add(new IconClass("M16", loadResource("M16")));
            TactRifleList.Add(new IconClass("Type 63", loadResource("Type 63")));

            LMGList = new List<IconClass>();
            LMGList.Add(new IconClass("M60", loadResource("M60")));
            LMGList.Add(new IconClass("RPD", loadResource("RPD")));
            LMGList.Add(new IconClass("Stoner 63", loadResource("Stoner 63")));

            SniperList = new List<IconClass>();
            SniperList.Add(new IconClass("LW3-Tundra", loadResource("LW3-Tundra")));
            SniperList.Add(new IconClass("M82", loadResource("M82")));
            SniperList.Add(new IconClass("Pelington 703", loadResource("Pelington 703")));
            SniperList.Add(new IconClass("ZRG 20mm", loadResource("ZRG 20mm")));
            SniperList.Add(new IconClass("Swiss K31", loadResource("Swisss K31")));

            SecondaryList = new List<IconClass>();
            SecondaryList.Add(new IconClass("1911", loadResource("1911")));
            SecondaryList.Add(new IconClass("Cigma 2", loadResource("Cigma 2")));
            SecondaryList.Add(new IconClass("Diamatti", loadResource("Diamatti")));
            SecondaryList.Add(new IconClass("E-Tool", loadResource("E-Tool")));
            SecondaryList.Add(new IconClass("Gallo SA12", loadResource("Gallo SA12")));
            SecondaryList.Add(new IconClass("Hauer 77", loadResource("Hauer 77")));
            SecondaryList.Add(new IconClass("Knife", loadResource("Knife")));
            SecondaryList.Add(new IconClass("M79", loadResource("M79")));
            SecondaryList.Add(new IconClass("Machete", loadResource("Machete")));
            SecondaryList.Add(new IconClass("Magnum", loadResource("Magnum")));
            SecondaryList.Add(new IconClass("R1 Shadowhunter", loadResource("R1 Shadowhunter")));
            SecondaryList.Add(new IconClass("RPG-7", loadResource("RPG-7")));
            SecondaryList.Add(new IconClass("Sledgehammer", loadResource("Sledgehammer")));
            SecondaryList.Add(new IconClass("Streetsweeper", loadResource("Streetsweeper")));
            SecondaryList.Add(new IconClass("Wakizashi", loadResource("Wakizashi")));
            SecondaryList.Add(new IconClass("Ballistic Knife", loadResource("Ballistic Knife")));

            AllGuns = new List<IconClass>();
            AllGuns.AddRange(AssaultRiflesList);
            AllGuns.AddRange(SMGList);
            AllGuns.AddRange(TactRifleList);
            AllGuns.AddRange(LMGList);
            AllGuns.AddRange(SniperList);
            AllGuns.AddRange(SecondaryList);
            
            /*PrimaryGuns = new List<IconClass>();
            PrimaryGuns.AddRange(AssaultRiflesList);
            PrimaryGuns.AddRange(SMGList);
            PrimaryGuns.AddRange(TactRifleList);
            PrimaryGuns.AddRange(LMGList);
            PrimaryGuns.AddRange(SniperList);*/

        }
        private static void loadTacts()
        {
            TacticalList = new List<IconClass>();
            TacticalList.Add(new IconClass("Decoy", loadResource("Decoy")));
            TacticalList.Add(new IconClass("Flashbang", loadResource("Flashbang")));
            TacticalList.Add(new IconClass("Smoke Grenade", loadResource("Smoke Grenade")));
            TacticalList.Add(new IconClass("Stimshot", loadResource("Stimshot")));
            TacticalList.Add(new IconClass("Stun Grenade", loadResource("Stun Grenade")));
        }
        private static void loadLethal()
        {
            LethalList = new List<IconClass>();
            LethalList.Add(new IconClass("C4", loadResource("C4")));
            LethalList.Add(new IconClass("Frag", loadResource("Frag")));
            LethalList.Add(new IconClass("Molotov", loadResource("Molotov")));
            LethalList.Add(new IconClass("Semtex", loadResource("Semtex")));
            LethalList.Add(new IconClass("Tomahawk", loadResource("Tomahawk")));
        }
        private static void loadFieldUpgrade()
        {
            FieldUpgradeList = new List<IconClass>();
            FieldUpgradeList.Add(new IconClass("Assault Pack", loadResource("Assault Pack")));
            FieldUpgradeList.Add(new IconClass("Field Mic", loadResource("Field Mic")));
            FieldUpgradeList.Add(new IconClass("Gas Mine", loadResource("Gas Mine")));
            FieldUpgradeList.Add(new IconClass("Jammer", loadResource("Jammer")));
            FieldUpgradeList.Add(new IconClass("Proximity Mine", loadResource("Proximity Mine")));
            FieldUpgradeList.Add(new IconClass("Sam Turret", loadResource("Sam Turret")));
            FieldUpgradeList.Add(new IconClass("Trophy System", loadResource("Trophy System")));
        }
        private static void loadWildCards()
        {
            WildCardList = new List<IconClass>();
            WildCardList.Add(new IconClass("Danger Close", loadResource("Danger Close")));
            WildCardList.Add(new IconClass("Gunfighter", loadResource("Gunfighter")));
            WildCardList.Add(new IconClass("Law Breaker", loadResource("Law Breaker")));
            WildCardList.Add(new IconClass("Perk Greed", loadResource("Perk Greed")));
        }
        private static void loadAttachments()
        {

            //new attachments \/
            OpticList = new List<string> { "MILLSTOP REFLEX", "VISIONTECH 2X", "KOBRA RED DOT", "QUICKDOT LED", "AXIAL ARMS 3X", "SILLIX HOLOSCOUT", "MICROFLEX LED", "HAWKSMOOR", "ROYAL & KROSS 4X", "SUSAT MULTIZOOM", "DIAMONDBACK REFLEX", "AN/PSV-4 THERMAL ", "NOCH SOVA THERMAL", "SNAPPOINT", "FASTPOINT REFLEX", "VULTURE CUSTOM ZOOM", "HANGMAN RF", "ULTRAZOOM CUSTOM", "IRON SIGHTS", "OTERO MINI REFLEX" };
            MuzzleList = new List<string> { "MUZZLE BRAKE", "FLASHGUARD", "SUPPRESSOR", "INFANTRY COMPENSATOR", "SOCOM ELIMINATOR", "AGENCY SUPPRESSOR", "SPETSNAZ COMPENSATOR", "KGB ELIMINATOR", "GRU SUPPRESSOR", "SOUND SUPPRESSOR", "SILENCER", "STABILIZER", "FLASH HIDER", "SOUND MODERATOR", "INFANTRY STABILIZER", "TASK FRCE SHROUD", "WRAPPED SUPPRESSOR", "DUCKBILL CHOKE", "FLASH CONE 12 GA", "INFANTRY V-CHOKE", "SOCOM BLAST MITIGATOR", "AGENCY CHOKE", "GRU SILENCER", "AGENCY SILENCER", "SIGMA SPECIAL" };
            BarrelList = new List<string> { "EXTENDED", "CAVALRY LANCER", "REINFORCED HEAVY", "RANGER", "TAKEDOWN", "TASKFORCE", "ULTRALIGHT", "VDV REINFORCED", "LIBERATOR", "SPETSNAZ RPK BARREL", "CONTOUR", "CMV MIL-SPEC", "GRU COMPOSITE", "CONTOUR M2", "RIFLED", "RAPID FIRE", "STRIKE TEAM", "MATCH GRADE", "CUT DOWN", "DIVISION", "SOR CUT DOWN", "GRU CUT DOWN", "COMBAT RECON", "TIGER TEAM", "HAMMER FORGED", "CHROME LINED", "TAC OPS", "TIGHT SNUB", "TITANIUM" };
            BodyList = new List<string> { "STEADY AIM LASER", "MOUNTED FLASHLIGHT", "SOF TARGET DESIGNATOR", "SWAT 5MW LASER SIGHT", "TIGER TEAM SPOTLIGHT", "EMBER SIGHTING POINT", "KGB TARGET DESIGNATOR", "GRU 5MW LASER SIGHT" };
            UnderbarrelList = new List<string> { "FOREGRIP", "INFILTRATOR GRIP", "PATROL GRIP", "BRUISER GRIP", "FIELD AGENT GRIP", "SFOD SPEEDGRIP", "SPETSNAZ GRIP", "SPETSNAZ SPEEDGRIP", "RED CELL FOREGRIP", "VDV SPEED GRIP", "BIPOD", "FRONT GRIP" };
            MagazineList = new List<string> { "MAG", "JUNGLE-STYLE MAG", "SPEED MAG", "STANAG", "SAS MAG CLAMP", "SALVO FAST MAG", "TAPED MAGS", "BAKELITE", "GRU MAG CLAMP", "VDV FAST MAG", "DRUM", "SPETSNAZ DRUM", "SPETSNAZ", "STANAG DRUM", "VANDAL SPEED LOADER", "TUBE", "STANAG TUBE", "FAST MAG", "FAST LOADER" };
            GunHandleList = new List<string> { "SPEED TAPE", "DROPSHOT WRAP", "FIELD TAPE", "SASR JUNGLE GRIP", "AIRBORNE ELASTIC WRAP", "SERPENT WRAP", "GRU ELASTIC WRAP", "SPETSNAZ FIELD GRIP", "SPEED GRIP" };
            StockList = new List<string> { "TACTICAL STOCK", "WIRE STOCK", "DUSTER STOCK", "BUFFER TUBE", "SAS COMBAT STOCK", "RAIDER PAD", "NO STOCK", "SPETSNAZ PKM STOCK", "KGB SKELETAL STOCK", "SAS COMBAT STOCK", "MARATHON STOCK", "CQB PAD", "RAIDER STOCK", "COLLAPSED STOCK", "KGB PAD", "DUAL WIELD", "SHOTGUN STOCK", "DUSTER PAD", "SPETSNAZ STOCK" };

            //gun attachment data
            GunAttachment.GunAttachmentList.Add(new GunAttachment("XM4", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("AK-47", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 }, new List<int> { 0, 1, 2, 3, 7, 8 }, new List<int> { 1, 4, 6, 7, 8, 9 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 1, 2, 3, 6, 7 }, new List<int> { 0, 2, 6, 7, 8, 9 }, new List<int> { 0, 1, 2, 5, 6, 7 }, new List<int> { 0, 1, 2, 6, 7, 8 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("KRIG 6", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 3, 4, 6, 10, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 4, 6, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("QBZ-83", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5, 6 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 4, 5, 10, 13 }, new List<int> { 1, 2, 3, 4, 5, 8 }, new List<int> { 0, 2, 9, 10, 11, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("FFAR 1", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5, 6 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 4, 10, 11, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("GROZA", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 }, new List<int> { 0, 1, 2, 6, 7, 8 }, new List<int> { 1, 6, 7, 11, 12, 13 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 1, 2, 3, 6, 7 }, new List<int> { 2, 6, 8, 9, 10, 11 }, new List<int> { 1, 2, 5, 6, 7, 8 }, new List<int> { 0, 2, 7, 10, 11, 14 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("FARA 83", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12 }, new List<int> { 0, 1, 2, 6, 7, 8 }, new List<int> { 1, 4, 6, 8, 9, 10 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 1, 2, 3, 6, 7 }, new List<int> { 0, 2, 6, 8, 9, 12 }, new List<int> { 0, 1, 2, 5, 6, 7 }, new List<int> { 0, 1, 2, 6, 8, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("AK-74U", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 6, 7, 8, 9 }, new List<int> { 0, 1, 5, 7, 8, 14 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 2, 3, 6, 7, 8 }, new List<int> { 2, 6, 8, 9, 10, 11 }, new List<int> { 0, 1, 2, 5, 6, 7 }, new List<int> { 0, 1, 2, 6, 7, 8 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("BULLFROG", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 6, 7, 8, 9 }, new List<int> { 0, 1, 5, 7, 8, 14 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 3, 6, 8, 9 }, new List<int> { 0, 2, 3, 9, 14, 17 }, new List<int> { 0, 1, 2, 5, 6, 7 }, new List<int> { 0, 1, 2, 6, 7, 8 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("KSP 45", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 4, 5, 8 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("LC10", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 4, 5, 8 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("MAC-10", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 4, 5, 8 }, new List<int> { 2, 5, 10, 13, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("MILANO 821", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 4, 5, 8 }, new List<int> { 2, 5, 10, 13, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 9, 10, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("MP5", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 4, 5, 8 }, new List<int> { 1, 2, 4, 5, 10, 13 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 4, 6, 9, 12, 13 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("AUG", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 11, 15, 16 }, new List<int> { 0, 1, 3, 4, 10, 23 }, new List<int> { 1, 5, 15, 16, 17, 28 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5, 11 }, new List<int> { 1, 2, 4, 5, 10, 13 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 4, 5, 10, 11, 17 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("DMR 14", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 11, 15, 16 }, new List<int> { 0, 1, 3, 4, 10, 23 }, new List<int> { 1, 5, 15, 16, 17, 28 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 6, 9, 12, 17 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("M16", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 11, 15, 16 }, new List<int> { 0, 1, 3, 4, 10, 23 }, new List<int> { 1, 5, 15, 16, 17, 28 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5, 11 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4 }, new List<int> { 0, 1, 2, 3, 4, 5 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("TYPE 63", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 12, 15, 16 }, new List<int> { 0, 1, 6, 7, 10, 22 }, new List<int> { 1, 5, 15, 16, 17, 28 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 1, 2, 3, 6, 9, 11 }, new List<int> { 0, 2, 6, 7, 8, 9 }, new List<int> { 1, 2, 5, 6, 7, 8 }, new List<int> { 0, 1, 2, 6, 12, 14 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("M60", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 11, 15, 16 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 5, 17, 18, 19, 20 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 6, 9, 10, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("RPD", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 12, 15, 16 }, new List<int> { 0, 1, 2, 6, 7, 8 }, new List<int> { 5, 9, 17, 18, 19, 21 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 1, 2, 3, 6, 9 }, new List<int> { 0, 2, 9, 14, 17 }, new List<int> { 0, 1, 2, 5, 6, 7 }, new List<int> { 0, 1, 2, 6, 7, 8 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("STONER 63", new List<int> { 0, 1, 2, 3, 4, 5, 7, 8, 9, 11, 15, 16 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 5, 17, 18, 19, 20 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("LW3-TUNDRA", new List<int> { 0, 1, 2, 4, 5, 8, 9, 11, 15, 16, 17, 18 }, new List<int> { 11, 12, 13, 14, 15, 16 }, new List<int> { 0, 1, 15, 22, 23, 24 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 5, 10, 11 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 4, 5, 10, 11, 17 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("M82", new List<int> { 0, 1, 2, 4, 5, 8, 9, 11, 15, 16, 17, 18 }, new List<int> { 11, 12, 13, 14, 15, 16 }, new List<int> { 0, 1, 6, 15, 22, 23 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 3, 5, 10 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 4, 5, 10, 11, 17 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("PELINGTON 703", new List<int> { 0, 1, 2, 4, 5, 8, 9, 11, 15, 16, 17, 18 }, new List<int> { 11, 12, 13, 14, 15, 16 }, new List<int> { 0, 1, 2, 6, 22, 23 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 5, 10, 11 }, new List<int> { 0, 2, 3, 5, 14, 18 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 10, 11, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("1911", new List<int> { 3, 6, 19 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 5, 25, 26 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 15 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("CIGMA 2"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("DIAMATTI", new List<int> { 3, 6, 19 }, new List<int> { 0, 1, 3, 4, 5, 9 }, new List<int> { 0, 1, 2, 5, 25, 26 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 15 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("E-TOOL"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("GALLO SA12", new List<int> { 0, 3, 6 }, new List<int> { 9, 17, 18, 19, 20, 21 }, new List<int> { 0, 1, 2, 3, 5, 24 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { }, new List<int> { 15, 16 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 10 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("HAUER 77", new List<int> { 0, 3, 6 }, new List<int> { 9, 17, 18, 19, 20, 21 }, new List<int> { 0, 1, 2, 3, 5, 24 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { }, new List<int> { 15, 16 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 6, 9, 10, 16, 17 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("KNIFE"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("M79"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("MACHETE"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("MAGNUM", new List<int> { 3, 6, 19 }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("R1 SHADOWHUNTER", new List<int> { 0, 3, 6, 7 }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }, new List<int> { }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("RPG-7"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("SLEDGEHAMMER"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("STREETSWEEPER", new List<int> { 0, 3, 6 }, new List<int> { 9, 17, 18, 19, 20, 21 }, new List<int> { 0, 1, 2, 3, 5, 24 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { }, new List<int> { 10, 13 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 1, 2, 6, 9, 10 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("WAKIZASHI"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("ZRG 20MM", new List<int> { 0, 1, 2, 4, 5, 8, 9, 11, 15, 16, 17, 18 }, new List<int> { 11, 12, 13, 14, 15, 16, 24 }, new List<int> { 0, 1, 6, 15, 22 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 1, 2, 3, 5, 10, 11 }, new List<int> { 0, 2, 3, 5, 14, 17 }, new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 0, 2, 4, 10, 11, 12 }));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("BALLISTIC KNIFE"));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("SWISS K31",true));
            GunAttachment.GunAttachmentList.Add(new GunAttachment("PPSH-41", new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 9, 10, 13, 14 }, new List<int> { 0, 1, 6, 7, 8, 9 }, new List<int> { 0, 1, 2, 3, 5, 14 }, new List<int> { 0, 1, 4, 5, 6, 7 }, new List<int> { 0, 2, 3, 6, 7, 8 }, new List<int> { 2, 9, 10, 11, 14, 17 }, new List<int> { 1, 2, 5, 6, 7, 8 }, new List<int> { 0, 2, 10, 11, 12, 18 }));


            OpticList.Add("None");
            MuzzleList.Add("None");
            BarrelList.Add("None");
            BodyList.Add("None");
            UnderbarrelList.Add("None");
            MagazineList.Add("None");
            GunHandleList.Add("None");
            StockList.Add("None");
             AllAttachments= new List<List<string>>();
            AllAttachments.Add(OpticList);
            AllAttachments.Add(MuzzleList);
            AllAttachments.Add(BarrelList);
            AllAttachments.Add(BodyList);
            AllAttachments.Add(UnderbarrelList);
            AllAttachments.Add(MagazineList);
            AllAttachments.Add(GunHandleList);
            AllAttachments.Add(StockList);

        }
        private static void loadPerks()
        {
            Perk1 = new List<IconClass>();
            Perk2 = new List<IconClass>();
            Perk3 = new List<IconClass>();
            Perk1.Add(new IconClass("Engineer", loadResource("Engineer")));
            Perk1.Add(new IconClass("Flack Jacket", loadResource("Flack Jacket")));
            Perk1.Add(new IconClass("Forward Intel", loadResource("Forward Intel")));
            Perk1.Add(new IconClass("Paranoia", loadResource("Paranoia")));
            Perk1.Add(new IconClass("Tactical Mask", loadResource("Tactical Mask")));

            Perk2.Add(new IconClass("Assassin", loadResource("Assassin")));
            Perk2.Add(new IconClass("Gearhead", loadResource("Gearhead")));
            Perk2.Add(new IconClass("Quarter master", loadResource("Quartermaster")));
            Perk2.Add(new IconClass("Scavenger", loadResource("Scavenger")));
            Perk2.Add(new IconClass("Tracker", loadResource("Tracker")));

            Perk3.Add(new IconClass("Cold Blooded", loadResource("Cold Blooded")));
            Perk3.Add(new IconClass("Ghost", loadResource("Ghost")));
            Perk3.Add(new IconClass("Gung-ho", loadResource("Gung ho")));
            Perk3.Add(new IconClass("Ninja", loadResource("Ninja")));
            Perk3.Add(new IconClass("Spycraft", loadResource("Spycraft")));

            AllPerks = new List<IconClass>();
            AllPerks.AddRange(Perk1);
            AllPerks.AddRange(Perk2);
            AllPerks.AddRange(Perk3);
        }
    }
}
