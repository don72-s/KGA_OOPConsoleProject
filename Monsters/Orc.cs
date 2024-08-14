using ConsoleGame.Monsters.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Monsters {
    public class Orc : Monster {
    
        public Orc() {

            name = "오크";
            MAX_HP = 25;
            hp = MAX_HP;
            atk = 30;
            def = 5;
            gold = 30;
            exp = 25;

            patterns = [
                
                MonsterPatternType.DEFFENCE,
                MonsterPatternType.ATTACK,
                MonsterPatternType.ATTACK,

                ];

        }
    
    }
}
