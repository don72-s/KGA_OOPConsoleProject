using ConsoleGame.Monsters.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Monsters {
    public class Sniper : Monster {

        public Sniper() {

            name = "저격수";
            MAX_HP = 130;
            hp = MAX_HP;
            atk = 15;
            def = 10;
            gold = 350;
            exp = 350;

            patterns = [
                MonsterPatternType.GIGA_CHARGE,
                MonsterPatternType.GIGA_CHARGE_ATTACK,
                MonsterPatternType.GIGA_CHARGE_ATTACK,
                MonsterPatternType.DEFFENCE,
                MonsterPatternType.DEFFENCE,
                ];

        }

    }
}
