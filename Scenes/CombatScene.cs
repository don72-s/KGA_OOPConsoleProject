using ConsoleGame.Monsters;
using ConsoleGame.userData;

namespace ConsoleGame.Scenes {
    public class CombatScene : Scene, IMonsterSetable {

        enum CombatState { COMMANDING, WAITING };
        enum ActionType { ATTACK, DEFENSE };

        const int LEFT_PADDING = 37;

        private int cursorIdx;

        Monster monster;
        Player player;
        bool isBoss;
        CombatState curState;
        ActionType act;

        public CombatScene(ISceneChangeable _game) : base(_game) {

            player = Player.GetInstance();
            monster = null;
            act = ActionType.ATTACK;

        }

        public void SetMonster(Monster _monster, bool _isBoss) {
            
            monster = _monster;
            isBoss = _isBoss;

        }

        public override void Enter() {

            curState = CombatState.COMMANDING;
            cursorIdx = 0;

            if (monster == null) { 
                //todo : 더미 몬스터 추가하기.
            }

        }

        public override void Exit() {
            monster = null;
        }

        public override void Input() {

            switch (curState) {

                case CombatState.COMMANDING:
                    CommandingInput();
                    break;

                case CombatState.WAITING:
                    break;

            }
            
        }

        public override void Print() {

            Console.Clear();

            monster.PrintMonsterInfo();

            switch (curState) {

                case CombatState.COMMANDING:
                    PrintCommanding();
                    player.PrintStatus(LEFT_PADDING);
                    break;

                case CombatState.WAITING:
                    PrintWaiting();
                    break;

            }
        }

        public override void Update() {
            
        }


        void PlayerAction() {
        }


        void PrintCommanding() {

            Console.WriteLine("  행동을 선택하세요.");
            Console.WriteLine();
            Console.WriteLine("  공격!");
            Console.WriteLine("  방어!");

            Console.SetCursorPosition(0, 7 + cursorIdx % 2);
            Console.Write("▶");
            
        }

        void PrintWaiting() {

            //플레이어 행동
            PlayerAction();

            //입력 대기
            InputSystem.Waiting_Z_Input();

            //적 쓰러짐 확인.

        void CommandingInput() {

            switch (Console.ReadKey(true).Key) {

                case ConsoleKey.UpArrow:
                    if (cursorIdx <= 0) {
                        cursorIdx = 1;
                    } else {
                        cursorIdx--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    cursorIdx++;
                    break;

                case ConsoleKey.Z:
                    cursorIdx %= 2;

                    if (cursorIdx == 0) {
                        act = ActionType.ATTACK;
                    } else if (cursorIdx == 1) {
                        act = ActionType.DEFENSE;
                    }

                    curState = CombatState.WAITING;

                    break;
            }

        }

    }
}
