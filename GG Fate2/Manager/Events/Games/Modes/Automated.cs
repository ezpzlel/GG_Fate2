namespace GG_Fate.Manager.Events.Games.Mode
{
    using System.Linq;
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;
    using SharpDX;
    using System.Collections.Generic;

    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;

    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class Automated : Logic
    {
        internal static readonly float Qangle = 28 * (float)Math.PI / 180;

        internal static int CountHits(Vector2 position, List<Vector2> points, List<int> hitBoxes)
        {
            var result = 0;

            var startPoint = ObjectManager.Player.ServerPosition.To2D();
            var originalDirection = Q.Range * (position - startPoint).Normalized();
            var originalEndPoint = startPoint + originalDirection;

            for (var i = 0; i < points.Count; i++)
            {
                var point = points[i];

                for (var k = 0; k < 3; k++)
                {
                    var endPoint = new Vector2();
                    if (k == 0) endPoint = originalEndPoint;
                    if (k == 1) endPoint = startPoint + originalDirection.Rotated(Qangle);
                    if (k == 2) endPoint = startPoint + originalDirection.Rotated(-Qangle);

                    if (point.Distance(startPoint, endPoint, true, true) <
                        (Q.Width + hitBoxes[i]) * (Q.Width + hitBoxes[i]))
                    {
                        result++;
                        break;
                    }
                }
            }

            return result;
        }

        internal static void CastQ(Obj_AI_Base unit, Vector2 unitPosition, int minTargets = 0)
        {
            var points = new List<Vector2>();
            var hitBoxes = new List<int>();

            var startPoint = ObjectManager.Player.ServerPosition.To2D();
            var originalDirection = Q.Range * (unitPosition - startPoint).Normalized();

            foreach (var enemy in HeroManager.Enemies)
            {
                if (enemy.IsValidTarget() && enemy.NetworkId != unit.NetworkId)
                {
                    var pos = Q.GetPrediction(enemy);
                    if (pos.Hitchance >= HitChance.Medium)
                    {
                        points.Add(pos.UnitPosition.To2D());
                        hitBoxes.Add((int)enemy.BoundingRadius);
                    }
                }
            }

            var posiblePositions = new List<Vector2>();

            for (var i = 0; i < 3; i++)
            {
                if (i == 0) posiblePositions.Add(unitPosition + originalDirection.Rotated(0));
                if (i == 1) posiblePositions.Add(startPoint + originalDirection.Rotated(Qangle));
                if (i == 2) posiblePositions.Add(startPoint + originalDirection.Rotated(-Qangle));
            }

            if (startPoint.Distance(unitPosition) < 900)
            {
                for (var i = 0; i < 3; i++)
                {
                    var pos = posiblePositions[i];
                    var direction = (pos - startPoint).Normalized().Perpendicular();
                    var k = (2 / 3 * (unit.BoundingRadius + Q.Width));
                    posiblePositions.Add(startPoint - k * direction);
                    posiblePositions.Add(startPoint + k * direction);
                }
            }

            var bestPosition = new Vector2();
            var bestHit = -1;

            foreach (var position in posiblePositions)
            {
                var hits = CountHits(position, points, hitBoxes);
                if (hits > bestHit)
                {
                    bestPosition = position;
                    bestHit = hits;
                }
            }

            if (bestHit + 1 <= minTargets)
                return;

            Q.Cast(bestPosition.To3D(), true);
        }

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
                            var tPos = Q.GetPrediction(t);

                            if (tPos.Hitchance >= HitChance.Immobile)
                            {
                                CastQ(t, tPos.UnitPosition.To2D());
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