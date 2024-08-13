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
            hp = 7;
            atk = 1;
            def = 1;
            gold = 5;
            exp = 10;

            //todo : 패턴

        }

    }
}
