namespace GG_Fate.Manager.Events.Drawings
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using SharpDX;
    using System;
    using System.Linq;
    using Utils;

    using static Vars.VarsDecla;

    using Color = System.Drawing.Color;

    internal class DrawManager : Logic
    {
        internal static void drawText(string msg, Vector3 Hero, Color color, int weight = 0)
        {
            var wts = Drawing.WorldToScreen(Hero);

            Drawing.DrawText(wts[0] + (msg.Length), wts[1] + weight, color, msg);
        }

        internal static void Init(EventArgs args)
        {
            if (!Me.IsDead && !MenuGUI.IsShopOpen && !MenuGUI.IsChatOpen && !MenuGUI.IsScoreboardOpen)
            {
                if (R.IsReadyPerfectly())
                {
                    Render.Circle.DrawCircle(Me.Position, R.Range, Color.FromArgb(19, 130, 234), 1);

                    // MiniMap

                    Utility.DrawCircle(Me.Position, 5500, Color.FromArgb(19, 130, 234), 1);

                    // R Helper

                    var rTarget = TargetSelector.GetTarget(R.Range, Q.DamageType);

                    if (rTarget.Check(R.Range))
                    {
                        var comboDMG = Q.GetDamage(rTarget) + W.GetDamage(rTarget) + Me.GetAutoAttackDamage(rTarget) * 3;

                        if (comboDMG > rTarget.Health)
                        {
                            drawText(rTarget.ChampionName, Me.Position, Color.DodgerBlue, 50);
                        }
                    }
                }
            }

            if (Q.IsReadyPerfectly())
            {
                foreach (var t in HeroManager.Enemies.Where(x => x.Check(Q.Range)))
                {
                    foreach (var buff in t.Buffs.Where(b => b.Type == BuffType.Stun || b.Type == BuffType.Snare
                    || b.Type == BuffType.Charm || b.Type == BuffType.Suppression || b.Type == BuffType.Knockup))
                    {
                        if ((Me.Distance(t.Position) / (Q.Speed + Q.Delay)) < (buff.EndTime - Game.Time) + REACTIME)
                        {
                            Render.Circle.DrawCircle(t.Position, 75, Color.DodgerBlue);
                            Drawing.DrawLine(Drawing.WorldToScreen(t.Position), Drawing.WorldToScreen(Me.Position), 1, Color.DodgerBlue);
                        }
                    }
                }
            }

            if (Orbwalker.GetTarget().IsValidTarget())
            {
                Render.Circle.DrawCircle(Orbwalker.GetTarget().Position, 75, Color.GreenYellow);
            }
        }
    }
}