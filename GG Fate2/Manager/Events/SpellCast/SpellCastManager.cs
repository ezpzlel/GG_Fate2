namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using Utils;
    using static Utils.CardSelector;

    internal class SpellCastManager : Logic
    {
        internal static void Init(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs Args)
        {
            if (sender == null || !sender.IsMe || Args.Slot != SpellSlot.R)
            {
                return;
            }

            if (Args.SData.Name.ToLowerInvariant() == "gate")
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
            }
        }
    }
}