namespace GG_Fate.Manager.Events
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using Utils;

    using static Utils.CardSelector;
    using static Vars.VarsDecla;

    using OrbwalkingMode = Utils.Orbwalking.OrbwalkingMode;

    internal class MouseManager : Logic
    {
        internal static void Init(WndEventArgs Args)
        {
            if (Me.IsDead
                || MenuGUI.IsShopOpen
                || MenuGUI.IsChatOpen
                || MenuGUI.IsScoreboardOpen
                || MeGate
                || Me.IsRecalling()
                || (Orbwalker.ActiveMode != OrbwalkingMode.Mixed &&
                Orbwalker.ActiveMode != OrbwalkingMode.LaneClear))
            {
                return;
            }

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