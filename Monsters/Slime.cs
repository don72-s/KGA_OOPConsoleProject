using ConsoleGame.Monsters.Patterns;
using ConsoleGame.PlayerAction;
using ConsoleGame.userData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Monsters {
    public class Slime : Monster{

        public Slime() {

            name = "슬라임";
            MAX_HP = 7;
            hp = MAX_HP;
            atk = 1;
            def = 1;
            gold = 5;
            exp = 10;

            patterns = [
                MonsterPatternType.ATTACK,
                ];

        }

    }
}
