namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;

    using static Vars.VarsDecla;

    internal class Automated : Logic
    {
        internal static void Init()
        {
            if (Q.IsReadyPerfectly())
            {
                foreach (var target in HeroManager.Enemies.Where(t => t.Check(Q.Range)))
                {
                    foreach (var buff in target.Buffs.Where(b => b.Type == BuffType.Stun
                        || b.Type == BuffType.Snare
                            || b.Type == BuffType.Charm
                                || b.Type == BuffType.Suppression
                                    || b.Type == BuffType.Knockup
                    || target.IsChannelingImportantSpell()))
                    {
                        if ((Me.Distance(target.Position) / (Q.Speed + Q.Delay)) < (buff.EndTime - Game.Time) + REACTIME)
                        {
                            var prediction = Q.GetPrediction(target);

                            if (prediction.Hitchance >= Q.MinHitChance)
                            {
                                Q.Cast(prediction.CastPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}