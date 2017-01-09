namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp.Common;
    using System.Linq;
    using Utils;

    using static Utils.CardSelector;

    internal class Combo : Logic
    {
        internal static void Init()
        {
            foreach (var enemy in HeroManager.Enemies.Where(e => !e.IsDead))
            {
                if (enemy.IsKillableAndValidTarget(W.GetDamage(enemy), W.DamageType, 650))
                {
                    switch (Status)
                    {
                        case SelectStatus.Ready:
                            {
                                StartSelecting(Cards.First);
                                break;
                            }
                        case SelectStatus.Selecting:
                            {
                                JumpToCard(Cards.First);
                                break;
                            }
                    }
                }
                else
                {
                    switch (Status)
                    {
                        case SelectStatus.Ready:
                            {
                                StartSelecting(Cards.Yellow);
                                break;
                            }
                        case SelectStatus.Selecting:
                            {
                                JumpToCard(Cards.Yellow);

                                break;
                            }
                    }
                }
            }
        }
    }
}