namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;
    using Cache = Utils.MinionCache;

    internal class Harass : Logic
    {
        internal static void Init()
        {
            var x = Cache.GetMinions(Me.ServerPosition, Q.Range);

            if (x.Any() && x.Count >= 2)
            {
                RotateCards();
            }

            if (MeCard != "none" && (MeBlue || MeRed))
            {
                var HarassTarget = TargetSelector.GetTarget(W.Range, W.DamageType);

                if (HarassTarget != null && HarassTarget.IsHPBarRendered)
                {
                    if (HarassTarget.DistanceToPlayer() < 590)
                    {
                        Me.IssueOrder(GameObjectOrder.AttackUnit, HarassTarget);
                    }
                }
            }
        }
    }
}