﻿using ConsoleGame.Monsters;
using ConsoleGame.PlayerAction;
using ConsoleGame.userData;

namespace ConsoleGame.Scenes {
    public class CombatScene : Scene, IMonsterSetable {

        enum CombatState { COMMANDING, WAITING };

        const int LEFT_PADDING = 37;

        private int cursorIdx;
        private bool isClear;

        Monster monster;
        Player player;
        bool isBoss;
        CombatState curState;
        PlayerActionType act;

        public CombatScene(ISceneChangeable _game) : base(_game) {

            player = Player.GetInstance();
            monster = null;
            act = PlayerActionType.ATTACK;

        }

        public void SetMonster(Monster _monster, bool _isBoss) {
            
            monster = _monster;
            isBoss = _isBoss;

        }

        public override void Enter() {

            curState = CombatState.COMMANDING;
            cursorIdx = 0;
            isClear = false;

            if (monster == null) { 
                //todo : 오류가 발생한다면 더미 몬스터 추가하기.
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
                    WaitingInput();
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

            int dmg = 0;

            if (act == PlayerActionType.ATTACK) {

                dmg = player.atk - (int)(monster.def * monster.defBuff);
                if (dmg <= 0) dmg = 1;
                monster.TakeDamage(dmg);

                Console.Clear();
                monster.PrintMonsterInfo();

                Console.WriteLine($"  {monster.name}에게 {dmg}의 데미지를 입혔다!");

            } else if (act == PlayerActionType.DEFENSE) {

                Console.WriteLine("  위험에 대비해 엄폐물 뒤로 숨었다.");

            }

            player.PrintStatus(LEFT_PADDING);

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
            if (monster.IsDead()) {

                Console.SetCursorPosition(0, 0);
                monster.PrintMonsterInfo();

                //보상 계산
                monster.Rewarding(Console.GetCursorPosition(), player);

                //보스몬스터였을 경우
                if (isBoss) {

                    //게임 클리어 조건
                    if (player.curMazeLevel >= 2) {

                        Console.Clear();
                        Console.WriteLine("게임을 클리어하셨습니다. 축하합니다.");
                        InputSystem.Waiting_Z_Input();
                        Environment.Exit(0);
                    }


                    player.RaiseMazeLevel();
                    Console.WriteLine(" 던전 공략 성공!\n 마을로 귀환합니다.");

                    isClear = true;

                }

                return;
            }


            Console.Clear();
            monster.PrintMonsterInfo();
            monster.ResetDefBuff();
            monster.PlayPattern(player, act);

            if (player.IsDead()) {

                Console.Clear();
                Console.WriteLine("게임오버");
                InputSystem.Waiting_Z_Input();

                Environment.Exit(0);

            }

            curState = CombatState.COMMANDING;

            Print();
            
        }


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
                        act = PlayerActionType.ATTACK;
                    } else if (cursorIdx == 1) {
                        act = PlayerActionType.DEFENSE;
                    }

                    curState = CombatState.WAITING;

                    break;
            }

        }

        void WaitingInput() {

            if (isClear) {

                InputSystem.Waiting_Z_Input();
                scene.ChangeScene(SceneType.TOWN);

            } else {

                Console.Write("  전투를 종료합니다.");
                InputSystem.Waiting_Z_Input();
                scene.ChangeScene(SceneType.MAZE);

            }

        }

    }
}
