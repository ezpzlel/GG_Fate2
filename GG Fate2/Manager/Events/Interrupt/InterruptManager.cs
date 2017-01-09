namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using Utils;

    using static Utils.CardSelector;
    using static Vars.VarsDecla;

    using Orbwalking = Utils.Orbwalking;

    internal class InterruptManager : Logic
    {
        internal static void Init(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs Args)
        {
            if (sender.IsValidTarget(W.Range))
            {
                switch (Status)
                {
                    case SelectStatus.Selecting:
                        {
                            JumpToCard(Cards.Yellow);
                            break;
                        }
                    case SelectStatus.Ready:
                        {
                            StartSelecting(Cards.Yellow);
                            break;
                        }
                }

                if (MeGold)
                {
                    if (Orbwalking.InAutoAttackRange(sender))
                    {
                        if (Orbwalking.CanAttack())
                        {
                            Me.IssueOrder(GameObjectOrder.AttackUnit, sender);
                        }
                    }
                }
            }
        }
    }
}