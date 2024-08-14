using ConsoleGame.Monsters.Patterns;

namespace ConsoleGame.Monsters {
    public class Wizard : Monster {

        public Wizard() {

            name = "마법사";
            MAX_HP = 70;
            hp = MAX_HP;
            atk = 30;
            def = 8;
            gold = 200;
            exp = 300;

            healPoint = 15;

            patterns = [
                MonsterPatternType.CHARGE,
                MonsterPatternType.CHARGE_ATTACK,
                MonsterPatternType.PANIC,
                MonsterPatternType.HEAL,
                ];

        }

    }
}
