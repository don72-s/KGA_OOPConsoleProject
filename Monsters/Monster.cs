using ConsoleGame.Monsters.Patterns;
using ConsoleGame.PlayerAction;
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

        protected int patternIdx;
        protected MonsterPatternType[] patterns;

        public float defBuff {  get; private set; }


        public Monster() {

            patternIdx = 0;
            defBuff = 1;

        }

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

        public void heal(float _rate = 1f) {

            hp += (int)(healPoint * _rate);

            hp = hp > MAX_HP ? MAX_HP : hp;

        }

        public void SetDefBuff(float _rate) { 
        
            defBuff = _rate;

        }

        public void ResetDefBuff() { 

            defBuff = 1;

        }

        public bool IsDead() {

            return hp <= 0;

        }

        public void Rewarding((int, int) _cmdPos, Player _player) {

            //시스템 메세지
            PrintSystem.PrintLine(_cmdPos, $"  {name}을 쓰러뜨렸다!");
            InputSystem.Waiting_Z_Input();

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

        public void PlayPattern(Player _player, PlayerActionType _actionType) {

            patternIdx %= patterns.Length;

            var action = MonsterPattern.getMonsterAction(patterns[patternIdx]);

            if (action == null) {
                UndefinedPattern(_player, _actionType, patterns[patternIdx]);
            } else {
                action.Invoke(this, _player, _actionType);
            }

            patternIdx++;

        }

        public virtual void UndefinedPattern(Player _player, PlayerActionType _actionType, MonsterPatternType _patternType) {

            Console.WriteLine("필살기가 정의되지 않음");
            InputSystem.Waiting_Z_Input();

        }

/*        public virtual void InitMonsterData() {
            //오브젝트 풀 형식으로 사용할 경우 객체 정보 초기화 동작 작성 영역.
            Console.WriteLine("재사용 기능 없음");
        }*/

    }
}
