namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp.Common;
    using System.Linq;
    using System.Windows.Input;
    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;
    using Cache = Utils.MinionCache;

    internal class Clear : Logic
    {
        internal static void Init()
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                var x = Cache.GetMinions(Me.ServerPosition, Q.Range);

                var mobs = Cache.GetMinions(Me.ServerPosition, 1000, MinionTeam.Neutral);

                if (Q.IsReadyPerfectly())
                {
                    if (x.Any())
                    {
                        var xPos = x.Select(y => y.Position.To2D()).ToList();

                        var xLine = MinionManager.GetBestLineFarmLocation(xPos, Q.Width, Q.Range);

                        if (xLine.MinionsHit >= 2)
                        {
                            Q.Cast(xLine.Position);
                        }
                    }

                    if (mobs.Count > 0)
                    {
                        var mob = mobs[0];

                        if (Status != SelectStatus.Selected)
                        {
                            Q.Cast(mob);
                            return;
                        }
                    }
                }
            }
        }
    }
}