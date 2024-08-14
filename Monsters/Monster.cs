using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters {

    public abstract class Monster {

        const int LEFT_PADDING = 37;

        public string name { get; protected set; }
        public int hp { get; protected set; }
        public int atk { get; protected set; }
        public int def { get; protected set; }
        public int healPoint { get; protected set; }

        public int MAX_HP { get; protected set; }

        public int gold { get; protected set; }
        public int exp { get; protected set; }

        int patternIdx;
        protected PatternType[] patterns;

        public float defBuff {  get; private set; }

        public void PrintMonsterInfo() {

            if (hp <= 0) hp = 0;

            Console.WriteLine($"============== {name} ==============");
            Console.WriteLine();
            Console.WriteLine($"          대충 {name} 이미지");
            Console.WriteLine($"  hp :[ {hp} ]");
            Console.WriteLine($"====================================");

        }

        public void TakeDamage(int _amount) {

            hp = hp - _amount < 0 ? 0 : hp - _amount;

        }

        public bool IsDead() {

            return hp <= 0;

        }

        public void Rewarding((int, int) _cmdPos, Player _player) {

            //시스템 메세지
            PrintSystem.PrintLine(_cmdPos, $"  {name}을 쓰러뜨렸다!");

            //골드정산
            _player.GainGold(gold);
            PrintSystem.PrintLine(_cmdPos, $"  {gold} 골드를 얻었다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            //경험치 정산
            _player.GainExp(exp);
            PrintSystem.PrintLine(_cmdPos, $"  경험치를 {exp} 얻었다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();
            PrintSystem.ClearLine(_cmdPos);

            //레벨업인 경우 처리
            while (_player.CheckLevelUp()) {

                Console.Write("  레벨이 올랐다!");

                _player.PrintStatus(LEFT_PADDING);
                InputSystem.Waiting_Z_Input();
                PrintSystem.ClearLine(_cmdPos);

            }

        }
    }
}
