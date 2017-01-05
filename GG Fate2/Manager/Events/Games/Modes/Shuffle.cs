namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;
    using static Utils.CardSelector;

    internal class Shuffle : Logic
    {
        internal static void Init()
        {
            var target = TargetSelector.GetTarget(1000, Q.DamageType);

            var minionNear = MinionCache.GetMinions(Me.ServerPosition, 1000);

            if (target.Check(1000) && minionNear.Any())
            {
                RotateCards();
            }
        }
    }
}