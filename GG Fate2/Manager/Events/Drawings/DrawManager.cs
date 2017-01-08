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
                }

                // Block AA last-hit range

                Render.Circle.DrawCircle(Me.Position, W.Range + 300, Color.FromArgb(111, 98, 234), 1);

                // EndScene

                if (R.IsReadyPerfectly())
                {
                    Utility.DrawCircle(Me.Position, 5500, Color.FromArgb(19, 130, 234), 1);

                    var targetUlt = TargetSelector.GetTarget(R.Range, Q.DamageType);

                    if (targetUlt.Check(R.Range))
                    {
                        var comboDMG = Q.GetDamage(targetUlt) + W.GetDamage(targetUlt) + Me.GetAutoAttackDamage(targetUlt) * 3;

                        if (comboDMG > targetUlt.Health)
                        {
                            drawText(targetUlt.ChampionName, Me.Position, Color.LightGoldenrodYellow, 20);
                        }
                    }
                }
            }

            if (Q.IsReadyPerfectly())
            {
                foreach (var t in HeroManager.Enemies.Where(x => x.Check(Q.Range)))
                {
                    foreach (var buff in t.Buffs.Where(b => (b.Type == BuffType.Slow && t.MoveSpeed <= 250) || b.Type == BuffType.Stun || b.Type == BuffType.Snare
                    || b.Type == BuffType.Charm || b.Type == BuffType.Suppression || b.Type == BuffType.Knockup || t.IsChannelingImportantSpell()))
                    {
                        if ((Me.Distance(t.Position) / (Q.Speed + Q.Delay)) < (buff.EndTime - Game.Time) + 0.15)
                        {
                            Render.Circle.DrawCircle(t.Position, 75, Color.DodgerBlue);
                            Drawing.DrawLine(Drawing.WorldToScreen(t.Position), Drawing.WorldToScreen(Me.Position), 1, Color.DodgerBlue);
                        }
                    }
                }
            }

            if (MeTracker)
            {
                var buff = Me.GetBuff("pickacard_tracker");

                var remainingTime = (buff.EndTime - Game.Time);

                var remainingTimeInt = (int)Math.Round(remainingTime, MidpointRounding.ToEven);

                drawText("" + remainingTimeInt, Me.Position, Color.LightYellow, 40);
            }

            if (Orbwalker.GetTarget().IsValidTarget())
            {
                Render.Circle.DrawCircle(Orbwalker.GetTarget().Position, 75, Color.GreenYellow);
            }

            if (MeRed)
            {
                Render.Circle.DrawCircle(Game.CursorPos, 300, Color.DodgerBlue);
            }
        }
    }
}