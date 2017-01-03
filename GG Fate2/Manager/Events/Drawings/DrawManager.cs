namespace GG_Fate.Manager.Events.Drawings
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;
    using System.Drawing;
    using System.Linq;
    using Utils;
    using Vars;

    internal class DrawManager : Logic
    {
        internal static void Init(EventArgs args)
        {
            if (!Me.IsDead && !MenuGUI.IsShopOpen && !MenuGUI.IsChatOpen && !MenuGUI.IsScoreboardOpen)
            {
                if (R.IsReady())
                {
                    Render.Circle.DrawCircle(Me.Position, R.Range, Color.FromArgb(19, 130, 234), 1);
                }

                //EndScene

                if (R.IsReadyPerfectly())
                {
                    Utility.DrawCircle(ObjectManager.Player.Position, 5500, Color.PaleGreen, 2, 23, true);
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
                            Render.Circle.DrawCircle(t.Position, 75, Color.BlueViolet);
                            Drawing.DrawLine(Drawing.WorldToScreen(t.Position), Drawing.WorldToScreen(Me.Position), 1, Color.BlueViolet);
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