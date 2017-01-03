namespace Utils
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;

    public static class SkinManager
    {
        private static Menu skinMenu;
        private static readonly int OwnSkinID;

        static SkinManager()
        {
            OwnSkinID = ObjectManager.Player.BaseSkinId;

            Game.OnUpdate += OnUpdate;
        }

        public static void AddToMenu(Menu mainMenu, int SkinCount = 15)
        {
            skinMenu = mainMenu;

            mainMenu.AddItem(new MenuItem("EnabledSkin", "Enabled", true).SetValue(false));
            mainMenu.AddItem(new MenuItem("SelectSkin", "Select Skin ID: ", true).SetValue(new Slider(0, 0, SkinCount)));

            mainMenu.Item("EnabledSkin", true).ValueChanged += delegate (object obj, OnValueChangeEventArgs Args)
            {
                if (!Args.GetNewValue<bool>())
                {
                    ObjectManager.Player.SetSkin(ObjectManager.Player.CharData.BaseSkinName, OwnSkinID);
                }
            };
        }

        private static void OnUpdate(EventArgs args)
        {
            if (skinMenu.Item("EnabledSkin", true).GetValue<bool>())
            {
                ObjectManager.Player.SetSkin(ObjectManager.Player.CharData.BaseSkinName,
                    skinMenu.Item("SelectSkin", true).GetValue<Slider>().Value);
            }
        }
    }
}