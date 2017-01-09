namespace Vars
{
    using LeagueSharp;
    using System;

    public static class VarsDecla
    {
        public static string MeCard
        {
            get
            {
                if (ObjectManager.Player.HasBuff("bluecardpreattack"))
                    return "blue";
                if (ObjectManager.Player.HasBuff("goldcardpreattack"))
                    return "gold";
                if (ObjectManager.Player.HasBuff("redcardpreattack"))
                    return "red";
                return "none";
            }
        }

        public static bool MeAny => ObjectManager.Player.HasBuff("bluecardpreattack") || ObjectManager.Player.HasBuff("goldcardpreattack") || ObjectManager.Player.HasBuff("redcardpreattack");
        public static bool MeTracker => ObjectManager.Player.HasBuff("pickacard_tracker");
        public static bool MeGate => ObjectManager.Player.HasBuff("gate");
        public static bool MeBlue => ObjectManager.Player.HasBuff("bluecardpreattack");
        public static bool MeGold => ObjectManager.Player.HasBuff("goldcardpreattack");
        public static bool MeRed => ObjectManager.Player.HasBuff("redcardpreattack");
        public static bool MeOnRed => ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Name.Equals("RedCardLock", StringComparison.InvariantCultureIgnoreCase);

        public static double REACTIME = 0.15;

        public static int REDRADIUS = 250;
    }
}