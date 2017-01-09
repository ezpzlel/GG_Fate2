namespace Utils
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;

    public static class LevelsManager
    {
        private static Menu levelMenu;

        public static void AddToMenu(Menu mainMenu)
        {
            levelMenu = mainMenu;

            mainMenu.AddItem(
                new MenuItem("LevelsMode", "Leveling : ", true).SetValue(
                    new StringList(new[]
                        {"AP", "AD"})));

            Obj_AI_Base.OnLevelUp += OnLevelUp;
        }

        private static void OnLevelUp(Obj_AI_Base sender, EventArgs Args)
        {
            if (!sender.IsMe)
            {
                return;
            }

            if ((ObjectManager.Player.Level == 6 || ObjectManager.Player.Level == 11 || ObjectManager.Player.Level == 16))
            {
                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
            }

            if (ObjectManager.Player.Level >= 3)
            {
                int Delay = 300;

                if (ObjectManager.Player.Level < 3)
                {
                    switch (levelMenu.Item("LevelsMode", true).GetValue<StringList>().SelectedIndex)
                    {
                        case 0:
                            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                            break;

                        case 1:
                            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                            break;
                    }
                }
                else if (ObjectManager.Player.Level > 3)
                {
                    switch (levelMenu.Item("LevelsMode", true).GetValue<StringList>().SelectedIndex)
                    {
                        case 0:
                            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);

                            //Q -> W -> E
                            DelayLevels(Delay, SpellSlot.Q);
                            DelayLevels(Delay + 50, SpellSlot.W);
                            DelayLevels(Delay + 100, SpellSlot.E);
                            break;

                        case 1:
                            if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.E).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.W).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                            else if (ObjectManager.Player.Spellbook.GetSpell(SpellSlot.Q).Level == 0)
                                ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);

                            //E -> W -> Q
                            DelayLevels(Delay, SpellSlot.E);
                            DelayLevels(Delay + 50, SpellSlot.W);
                            DelayLevels(Delay + 100, SpellSlot.Q);
                            break;
                    }
                }
            }
        }

        private static void DelayLevels(int time, SpellSlot slot)
        {
            Utility.DelayAction.Add(time, () => ObjectManager.Player.Spellbook.LevelSpell(slot));
        }
    }
}