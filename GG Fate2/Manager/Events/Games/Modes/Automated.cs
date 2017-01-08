namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;

    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    using static Vars.VarsDecla;
    using static Utils.CardSelector;

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

            if (Orbwalker.ActiveMode == OrbwalkingMode.None)
            {
                foreach (var t in HeroManager.Enemies.Where(x => x.Check(350)))
                {
                    if (W.IsReadyPerfectly() && Status == SelectStatus.Ready)
                    {
                        StartSelecting(Cards.Yellow);
                    }
                    else if (Status == SelectStatus.Selecting)
                    {
                        JumpToCard(Cards.Yellow);
                    }
                }
            }
        }
    }
}