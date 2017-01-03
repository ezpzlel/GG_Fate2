namespace GG_Fate.Manager.Spells
{
    using LeagueSharp;
    using LeagueSharp.Common;

    internal class SpellManager : Logic
    {
        internal static void Init()
        {
            Q = new Spell(SpellSlot.Q,
                1450f,
                    TargetSelector.DamageType.Magical)
            { MinHitChance = HitChance.High };

            W = new Spell(SpellSlot.W,
                Orbwalking.GetRealAutoAttackRange(Me),
                    TargetSelector.DamageType.Magical);

            E = new Spell(SpellSlot.E);

            R = new Spell(SpellSlot.R,
                5500f);

            Q.SetSkillshot(0.25f, 40f, 1000f, false, SkillshotType.SkillshotLine);
        }
    }
}