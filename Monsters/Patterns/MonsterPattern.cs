using ConsoleGame.PlayerAction;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {

    public static partial class MonsterPattern {

        static Dictionary<MonsterPatternType, Action<Monster, Player, PlayerActionType>> patterns = null;
        const int LEFT_PADDING = 37;

        static void InitPatterns() { 
        
            patterns = new Dictionary<MonsterPatternType, Action<Monster, Player, PlayerActionType>> ();

            patterns.Add(MonsterPatternType.ATTACK, AttackPattern);
            patterns.Add(MonsterPatternType.DEFFENCE, DefensePattern);
            patterns.Add(MonsterPatternType.HEAL, HealPattern);
            patterns.Add(MonsterPatternType.CHARGE, ChargePattern);
            patterns.Add(MonsterPatternType.CHARGE_ATTACK, ChargeAttackPattern);
            patterns.Add(MonsterPatternType.GIGA_CHARGE, GigaChargePattern);
            patterns.Add(MonsterPatternType.GIGA_CHARGE_ATTACK, GigaChargeAttackPattern);
            patterns.Add(MonsterPatternType.PANIC, PanicPattern);

            //패턴 더해주기.
        }

        public static Action<Monster, Player, PlayerActionType> getMonsterAction(MonsterPatternType _type) {

            if (patterns == null) { 
                InitPatterns();
            }

            if (patterns.ContainsKey(_type)) {
                return patterns[_type];
            }

            return null;
        }

    }
}
