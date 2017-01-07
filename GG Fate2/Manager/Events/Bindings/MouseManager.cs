﻿namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using Utils;
    using static Vars.VarsDecla;
    using static Utils.CardSelector;
    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class MouseManager : Logic
    {
        internal static void Init(WndEventArgs Args)
        {
            if (!Me.IsDead
                && !MenuGUI.IsShopOpen
                && !MenuGUI.IsChatOpen
                && !MenuGUI.IsScoreboardOpen
                && !MeGate
                && !Me.IsRecalling()
                && (Orbwalker.ActiveMode == OrbwalkingMode.Mixed ||
                Orbwalker.ActiveMode == OrbwalkingMode.LaneClear))
            {
                if (Args.Msg == (ulong)WindowsMessages.WM_LBUTTONDOWN)
                {
                    if (Status == SelectStatus.Ready)
                    {
                        RotateCards();
                    }
                }
            }
        }
    }
}