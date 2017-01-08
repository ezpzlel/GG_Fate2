namespace GG_Fate.Manager.Events.Games.Mode
{
    using LeagueSharp.Common;
    using Utils;
    using static Utils.Orbwalking;

    internal class SpellQ : Logic
    {
        internal static bool QENEMY { get { return UsingKey("Q.Mode"); } }

        internal static void Init()
        {
            if (Orbwalker.ActiveMode != OrbwalkingMode.LaneClear)
            {
                if (QENEMY && Q.IsReadyPerfectly())
                {
                    var target = TargetSelector.GetTarget(Q.Range, Q.DamageType);

                    if (target.Check(Q.Range))
                    {
                        var prediction = Q.GetPrediction(target);

                        if (prediction.Hitchance >= Q.MinHitChance)
                        {
                            Q.Cast(prediction.CastPosition);
                        }
                    }
                }
            }
        }

        internal static bool UsingKey(string itemName)
        {
            return Menu.Item(itemName).GetValue<KeyBind>().Active;
        }
    }
}