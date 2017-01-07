namespace GG_Fate.Manager.Menu
{
    using LeagueSharp.Common;
    using System.Drawing;
    using Utils;
    using Orbwalking = Utils.Orbwalking;

    internal class MenuManager : Logic
    {
        internal static void Init()
        {
            Menu = new Menu("GG_Fate", "GG_Fate", true).SetFontStyle(FontStyle.Bold, SharpDX.Color.Firebrick);

            var targetSelectMenu = Menu.AddSubMenu(new Menu("Target Selector", "Target Selector"));
            {
                TargetSelector.AddToMenu(targetSelectMenu);
            }

            var orbMenu = Menu.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));
            {
                Orbwalker = new Orbwalking.Orbwalker(orbMenu);
            }

            var QMode = Menu.AddSubMenu(new Menu("Q", "Q"));
            {
                QMode.AddItem(new MenuItem("menu.q", "Q Spell").SetFontStyle(FontStyle.Bold, SharpDX.Color.Firebrick));

                QMode.AddItem(new MenuItem("Q.Mode", "Q Caster").SetValue(new KeyBind("A".ToCharArray()[0], KeyBindType.Press)));
            }

            var Picker = Menu.AddSubMenu(new Menu("W", "W"));
            {
                Picker.AddItem(new MenuItem("menu.pick", "Pick A Card").SetFontStyle(FontStyle.Bold, SharpDX.Color.Firebrick));

                Picker.AddItem(new MenuItem("Picker.b", "B").SetValue(new KeyBind("8".ToCharArray()[0], KeyBindType.Press)));

                Picker.AddItem(new MenuItem("Picker.r", "R").SetValue(new KeyBind("9".ToCharArray()[0], KeyBindType.Press)));
            }

            var miscMenu = Menu.AddSubMenu(new Menu("Misc", "Misc"));
            {
                var skinMenu = miscMenu.AddSubMenu(new Menu("SkinChange", "SkinChange"));
                {
                    SkinManager.AddToMenu(skinMenu, 10);
                }

                var autoLevelMenu = miscMenu.AddSubMenu(new Menu("Auto Levels", "Auto Levels"));
                {
                    LevelsManager.AddToMenu(autoLevelMenu);
                }

                DamageIndicator.AddToMenu(miscMenu, DamageCalculate.GetComboDamage);
            }

            Menu.AddToMainMenu();
        }
    }
}