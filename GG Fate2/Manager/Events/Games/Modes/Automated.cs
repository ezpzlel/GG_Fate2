namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;
    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class Automated : Logic
    {
        internal static void Init()
        {
            if (Q.IsReadyPerfectly())
            {
                foreach (var t in HeroManager.Enemies.Where(x => x.Check(Q.Range)))
                {
                    foreach (var buff in t.Buffs.Where(b => b.Type == BuffType.Stun
                        || b.Type == BuffType.Snare
                            || b.Type == BuffType.Charm
                                || b.Type == BuffType.Suppression
                                    || b.Type == BuffType.Knockup
                    || t.IsChannelingImportantSpell()))
                    {
                        if ((Me.Distance(t.Position) / (Q.Speed + Q.Delay)) < (buff.EndTime - Game.Time) + 0.15)
                        {
                            var prediction = Q.GetPrediction(t);

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
                foreach (var t in HeroManager.Enemies.Where(x => x.IsValidTarget(W.Range - 200) && x != null))
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