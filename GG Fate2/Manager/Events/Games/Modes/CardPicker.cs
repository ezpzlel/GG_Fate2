namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;
    using System.Linq;
    using Utils;
    using static Vars.VarsDecla;
    using static Utils.CardSelector;
    using Orbwalking = Utils.Orbwalking;

    internal class CardPicker : Logic
    {
        internal static bool BlueKey { get { return UsingKey("Picker.b"); } }

        internal static bool RedKey { get { return UsingKey("Picker.r"); } }

        internal static void GiveCard(Cards card)
        {
            switch (Status)
            {
                case SelectStatus.Ready:
                    {
                        StartSelecting(card);
                        break;
                    }
                case SelectStatus.Selecting:
                    {
                        JumpToCard(card);
                        break;
                    }
            }
        }

        internal static void Init()
        {
            var wName = Me.Spellbook.GetSpell(SpellSlot.W).Name;

            if (BlueKey)
            {
                GiveCard(Cards.Blue);
            }
            else if (RedKey)
            {
                GiveCard(Cards.Red);
            }

            /*if (Orbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed && Status == SelectStatus.Selecting)
            {
                var target = HeroManager.Enemies.FirstOrDefault(
                                 t => !t.IsValidTarget(W.Range) && t.IsValidTarget(W.Range + 300));

                if (target != null)
                {
                    var minionHarass = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(
                        m => m.IsValidTarget(W.Range) && m.Distance(target) < 300);

                    if (minionHarass != null)
                    {
                        if (wName.Equals("RedCardLock", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Me.Spellbook.CastSpell(SpellSlot.W, false);
                        }
                    }
                }
            }*/
        }

        internal static bool UsingKey(string itemName)
        {
            return Menu.Item(itemName).GetValue<KeyBind>().Active;
        }
    }
}