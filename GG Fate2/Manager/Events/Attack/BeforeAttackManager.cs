namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using SebbyLib;
    using System.Collections.Generic;
    using System.Linq;

    //Utils
    using Utils;
    using static Utils.CardSelector;
    using static Vars.VarsDecla;

    //Simp
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
                        if (!OktwCommon.CollisionYasuo(Me.ServerPosition, Args.Target.Position))
                        {
                            Args.Process = Status != SelectStatus.Selecting && Utils.TickCount - LastWSent > 300;
                        }

                        if (MeCard != "none" && MeGold)
                        {
                            var targetToGold = TargetSelector.GetTarget(W.Range, W.DamageType);

                            if (targetToGold.IsValidTarget(W.Range) && CheckStatus.IsBlockingGold(targetToGold))
                            {
                                Args.Process = false;
                            }
                        }

                        break;
                    }
                case OrbwalkingMode.Mixed:
                    {
                        if (OktwCommon.CollisionYasuo(Me.ServerPosition, Args.Target.Position))
                        {
                            Args.Process = false;
                        }

                        if (MeCard != "none" && MeRed)
                        {
                            var aaRange = Orbwalking.GetRealAutoAttackRange(Me);

                            var target = HeroManager.Enemies.FirstOrDefault(
                                t => !t.IsValidTarget(aaRange) && t.IsValidTarget(aaRange + 200));

                            if (target != null)
                            {
                                var minionHarass = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(
                                    m => m.IsValidTarget(aaRange) && m.Distance(target) < 200);

                                if (minionHarass != null)
                                {
                                    Args.Process = false;

                                    if (Orbwalking.CanAttack())
                                    {
                                        Me.IssueOrder(GameObjectOrder.AttackUnit, minionHarass);
                                    }
                                }
                            }
                        }

                        if (Status == SelectStatus.Selecting)
                        {
                            var tHarass = TargetSelector.GetTarget(W.Range, W.DamageType);

                            if (tHarass.Check(W.Range + 300))
                            {
                                Args.Process = false;
                            }

                            if (tHarass.Check(W.Range))
                            {
                                LockCard();

                                if (Orbwalking.CanAttack())
                                {
                                    Me.IssueOrder(GameObjectOrder.AttackUnit, tHarass);
                                }
                            }
                        }

                        break;
                    }
                case OrbwalkingMode.LaneClear:
                    {
                        if (MeCard != "none" && MeRed)
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