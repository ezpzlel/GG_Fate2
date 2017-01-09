namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;
    using System.Linq;

    using static Vars.VarsDecla;
    using static Utils.CardSelector;

    using Orbwalking = Utils.Orbwalking;

    internal class Harass : Logic
    {
        internal static void Init()
        {
            var minion = MinionManager
                        .GetMinions(Me.Position, W.Range, MinionTypes.All, MinionTeam.NotAlly)
                        .FirstOrDefault(x => Orbwalking.CanAttack() && HeroManager.Enemies.Any(a => a.Distance(x) <= REDRADIUS));

            if (minion != null)
            {
                if (MeRed)
                {
                    Me.IssueOrder(GameObjectOrder.AttackUnit, minion);
                }
                else if (MeOnRed)
                {
                    LockCard();
                }
            }
        }
    }
}