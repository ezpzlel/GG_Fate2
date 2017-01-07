namespace GG_Fate.Vars
{
    using LeagueSharp;
    using LeagueSharp.Common;
    using System;
    using System.Linq;

    public static class VarsDecla
    {
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

        public static bool MeTracker => ObjectManager.Player.HasBuff("pickacard_tracker");
        public static bool MeGate => ObjectManager.Player.HasBuff("gate");
        public static bool MeBlue => ObjectManager.Player.HasBuff("bluecardpreattack");
        public static bool MeGold => ObjectManager.Player.HasBuff("goldcardpreattack");
        public static bool MeRed => ObjectManager.Player.HasBuff("redcardpreattack");

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

        public static float GetStunDuration(this Obj_AI_Base target)
        {
            return (target.Buffs.Where(b => b.IsActive && Game.Time < b.EndTime &&
                (b.Type == BuffType.Charm ||
                b.Type == BuffType.Knockback ||
                b.Type == BuffType.Stun ||
                b.Type == BuffType.Suppression ||
                b.Type == BuffType.Snare)).Aggregate(0f, (current, buff) => Math.Max(current, buff.EndTime)) -
                Game.Time) * 1000;
        }
    }
}