namespace GG_Fate
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName != "TwistedFate")
            {
                return;
            }

            Logic.LoadAssembly();

            Game.PrintChat("~ GG Fate 2 ~ build v.00.16");
        }
    }
}