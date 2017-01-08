namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using Utils;
    using static Utils.CardSelector;

    internal class CardPicker : Logic
    {
        internal static bool BlueKey { get { return UsingKey("Picker.b"); } }

        internal static bool RedKey { get { return UsingKey("Picker.r"); } }

        internal static void GiveCard(Cards card)
        {
            switch (Status)
            {
                case SelectStatus.Ready:
                    {
                        StartSelecting(card);
                        break;
                    }
                case SelectStatus.Selecting:
                    {
                        JumpToCard(card);
                        break;
                    }
            }
        }

        internal static void Init()
        {
            if (BlueKey)
            {
                GiveCard(Cards.Blue);
            }
            else if (RedKey)
            {
                GiveCard(Cards.Red);
            }
        }

        internal static bool UsingKey(string itemName)
        {
            return Menu.Item(itemName).GetValue<KeyBind>().Active;
        }
    }
}