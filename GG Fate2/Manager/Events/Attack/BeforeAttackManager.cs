namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using SebbyLib;
    using System.Collections.Generic;
    using System.Linq;

    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;

    using Orbwalking = Utils.Orbwalking;
    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class BeforeAttackManager : Logic
    {
        internal static void Init(Orbwalking.BeforeAttackEventArgs Args)
        {
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    {
                        var targetToGold = TargetSelector.GetTarget(W.Range, W.DamageType);

                        if (!OktwCommon.CollisionYasuo(Me.ServerPosition, Args.Target.Position))
                        {
                            Args.Process = Status != SelectStatus.Selecting && Utils.TickCount - LastWSent > 300;
                        }

                        if (MeGold)
                        {
                            if (targetToGold.Check(W.Range) && CheckStatus.IsBlockingGold(targetToGold))
                            {
                                Args.Process = false;
                            }
                        }

                        break;
                    }
                case OrbwalkingMode.Mixed:
                    {
                        var targetMixed = TargetSelector.GetTarget(W.Range + 200, W.DamageType);

                        if (OktwCommon.CollisionYasuo(Me.ServerPosition, Args.Target.Position))
                        {
                            Args.Process = false;
                        }

                        if (Status == SelectStatus.Ready)
                        {
                            if (targetMixed.Check(W.Range + 200))
                            {
                                Args.Process = false;
                            }
                        }

                        // Double Tap W Logic

                        switch (Status)
                        {
                            case SelectStatus.Ready:
                                {
                                    if (targetMixed.Check(W.Range))
                                    {
                                        StartSelecting(Cards.First);

                                        if (MeAny && Orbwalking.CanAttack())
                                        {
                                            Me.IssueOrder(GameObjectOrder.AttackUnit, targetMixed);
                                        }
                                    }

                                    break;
                                }
                        }

                        break;
                    }

                case OrbwalkingMode.LaneClear:
                    {
                        if (MeRed)
                        {
                            Args.Process = false;

                            IDictionary<Obj_AI_Minion, int> creeps = new Dictionary<Obj_AI_Minion, int>();

                            foreach (var x in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.Team != Me.Team && x.Team != GameObjectTeam.Neutral && Orbwalking.InAutoAttackRange(x)))
                            {
                                creeps.Add(x, ObjectManager.Get<Obj_AI_Minion>().Count(y => y.Team != Me.Team && y.Team != GameObjectTeam.Neutral && y.IsValidTarget() && y.Distance(x.Position) <= 300));
                            }

                            foreach (var x in ObjectManager.Get<Obj_AI_Minion>().Where(x => x.Team == GameObjectTeam.Neutral && Orbwalking.InAutoAttackRange(x)))
                            {
                                creeps.Add(x, ObjectManager.Get<Obj_AI_Minion>().Count(y => y.Team == GameObjectTeam.Neutral && y.IsValidTarget() && y.Distance(x.Position) <= 300));
                            }

                            var sbire = creeps.OrderByDescending(x => x.Value).FirstOrDefault();

                            if (Orbwalking.CanAttack())
                            {
                                Me.IssueOrder(GameObjectOrder.AttackUnit, sbire.Key);
                            }
                        }

                        break;
                    }
            }
        }
    }
}