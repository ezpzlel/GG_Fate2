namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;

    internal class SpellQ : Logic
    {
        internal static bool QENEMY { get { return UsingKey("Q.Mode"); } }

        internal static bool QCLEAR { get { return UsingKey("Q.Clear"); } }

        internal static void Init()
        {
            if (QENEMY && Q.IsReadyPerfectly())
            {
                var target = TargetSelector.GetTarget(Q.Range, Q.DamageType);

                if (target.Check(Q.Range))
                {
                    var prediction = Q.GetPrediction(target);

                    if (prediction.Hitchance >= Q.MinHitChance)
                    {
                        Q.Cast(prediction.CastPosition);
                    }
                }
            }

            if (QCLEAR)
            {
                var lineFarm = MinionCache.GetMinions(Me.ServerPosition, Q.Range);

                if (lineFarm.Any() && lineFarm.Count >= 2)
                {
                    var minionPos = lineFarm.Select(x => x.Position.To2D()).ToList();

                    var farm = MinionManager.GetBestLineFarmLocation(minionPos, Q.Width, Q.Range);

                    Q.Cast(farm.Position);
                }

                var jungle = MinionCache.GetMinions(Me.ServerPosition, W.Range + 300, MinionTeam.Neutral);

                var jungleFarm = jungle.FirstOrDefault(x => x.IsValidTarget(Q.Range));

                if (jungle.Any())
                {
                    Q.Cast(jungleFarm);
                }
            }
        }

        internal static bool UsingKey(string itemName)
        {
            return Menu.Item(itemName).GetValue<KeyBind>().Active;
        }
    }
}