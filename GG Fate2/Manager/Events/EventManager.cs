namespace GG_Fate.Manager.Events
{
    using Drawings;
    using Games;
    using LeagueSharp;
    using LeagueSharp.Common;

    using Orbwalking = Utils.Orbwalking;

    internal class EventManager
    {
        internal static void Init()
        {
            Game.OnUpdate += LoopManager.Init;
            Game.OnWndProc += MouseManager.Init;
            Orbwalking.BeforeAttack += BeforeAttackManager.Init;
            AntiGapcloser.OnEnemyGapcloser += AntiGapcloserManager.Init;
            Interrupter2.OnInterruptableTarget += InterruptManager.Init;
            Obj_AI_Base.OnProcessSpellCast += SpellCastManager.Init;
            Drawing.OnDraw += DrawManager.Init;
            Drawing.OnEndScene += DrawManager.Init;
        }
    }
}