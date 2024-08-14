using ConsoleGame.Monsters.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Monsters {
    public class Knight : Monster {

        public Knight() {

            name = "기사";
            MAX_HP = 70;
            hp = MAX_HP;
            atk = 37;
            def = 11;
            gold = 200;
            exp = 300;

            healPoint = 12;

            patterns = [
                MonsterPatternType.DEFFENCE,
                MonsterPatternType.ATTACK,
                MonsterPatternType.CHARGE,
                MonsterPatternType.CHARGE_ATTACK,
                MonsterPatternType.HEAL,
                ];

        }

    }
}
