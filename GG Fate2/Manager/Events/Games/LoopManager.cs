namespace GG_Fate.Manager.Events.Games
{
    using LeagueSharp.Common;
    using Mode;
    using System;

    //Simp
    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class LoopManager : Logic
    {
        internal static void Init(EventArgs args)
        {
            if (Me.IsDead || Me.IsRecalling())
            {
                return;
            }

            Automated.Init();

            CardPicker.Init();

            SpellQ.Init();

            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingMode.Combo:
                    Combo.Init();
                    break;

                case OrbwalkingMode.Mixed:
                    break;

                case OrbwalkingMode.LaneClear:
                    Clear.Init();
                    break;
            }
        }
    }
}