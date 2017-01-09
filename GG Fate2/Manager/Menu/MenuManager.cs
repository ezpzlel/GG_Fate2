namespace GG_Fate.Manager.Menu
{
    using LeagueSharp.Common;
    using System.Drawing;
    using Utils;

    using Orbwalking = Utils.Orbwalking;
    using Color = SharpDX.Color;

    internal class MenuManager : Logic
    {
        internal static void Init()
        {
            Menu = new Menu("FREE LP", "FREE LP", true).SetFontStyle(FontStyle.Bold, Color.DodgerBlue);

            var targetSelectMenu = Menu.AddSubMenu(new Menu("TS", "TS"));
            {
                TargetSelector.AddToMenu(targetSelectMenu);
            }

            var orbMenu = Menu.AddSubMenu(new Menu("Orb", "Orb"));
            {
                Orbwalker = new Orbwalking.Orbwalker(orbMenu);
            }

            Menu.AddItem(new MenuItem("Q.Mode", "Q ENEMY").SetValue(new KeyBind("A".ToCharArray()[0], KeyBindType.Press)))
                .SetFontStyle(FontStyle.Bold, Color.DodgerBlue);

            Menu.AddItem(new MenuItem("Q.Clear", "Q CLEAR").SetValue(new KeyBind("C".ToCharArray()[0], KeyBindType.Press)))
               .SetFontStyle(FontStyle.Bold, Color.DodgerBlue);

            Menu.AddItem(new MenuItem("Picker.b", "Key (B)").SetValue(new KeyBind("8".ToCharArray()[0], KeyBindType.Press)))
                .SetFontStyle(FontStyle.Bold, Color.DodgerBlue);

            Menu.AddItem(new MenuItem("Picker.r", "Key (R)").SetValue(new KeyBind("9".ToCharArray()[0], KeyBindType.Press)))
                .SetFontStyle(FontStyle.Bold, Color.DodgerBlue);

            LevelsManager.AddToMenu(Menu);

            var skinMenu = Menu.AddSubMenu(new Menu("Skin", "Skin"));
            {
                SkinManager.AddToMenu(skinMenu, 10);
            }

            Menu.AddToMainMenu();
        }
    }
}