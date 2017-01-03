namespace GG_Fate.Vars
{
    using LeagueSharp;
    using LeagueSharp.Common;

    public static class VarsDecla
    {
        public static bool MeBlue => ObjectManager.Player.HasBuff("bluecardpreattack");
        public static bool MeRed => ObjectManager.Player.HasBuff("redcardpreattack");
        public static bool MeGold => ObjectManager.Player.HasBuff("goldcardpreattack");

        public static string MeCard
        {
            get
            {
                if (ObjectManager.Player.HasBuff("bluecardpreattack"))
                    return "blue";
                if (ObjectManager.Player.HasBuff("goldcardpreattack"))
                    return "gold";
                if (ObjectManager.Player.HasBuff("redcardpreattack"))
                    return "red";
                return "none";
            }
        }

        public static float GetRealAutoAttackRange(this AttackableUnit unit, AttackableUnit target,
            int autoAttackRange)
        {
            var result = autoAttackRange + unit.BoundingRadius;
            if (target.IsValidTarget())
                return result + target.BoundingRadius;
            return result;
        }

        public static bool IsReadyPerfectly(this Spell spell)
        {
            return spell != null && spell.Slot != SpellSlot.Unknown && spell.Instance.State != SpellState.Cooldown &&
                   spell.Instance.State != SpellState.Disabled && spell.Instance.State != SpellState.NoMana &&
                   spell.Instance.State != SpellState.NotLearned && spell.Instance.State != SpellState.Surpressed &&
                   spell.Instance.State != SpellState.Unknown && spell.Instance.State == SpellState.Ready;
        }
    }
}