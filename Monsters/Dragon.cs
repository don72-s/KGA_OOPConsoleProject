using ConsoleGame.Monsters.Patterns;
using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters {
    public class Dragon : Monster {

        enum HyperArrow { UP, DOWN, LEFT, RIGHT };

        const int LEFT_PADDING = 37;

        HyperArrow[] hyperPatterns;

        int hyperIdx;
        int hyperLevel;
        const int MAX_HYPER_LEVEL = 15;

        public Dragon() {

            name = "드래곤";
            MAX_HP = 200;
            hp = MAX_HP;
            atk = 58;
            def = 20;
            gold = 700;
            exp = 1275;

            healPoint = 40;

            patterns = [
                MonsterPatternType.ATTACK,
                MonsterPatternType.HYPER_ATTACK,
                MonsterPatternType.CHARGE_ATTACK,
                ];


            hyperIdx = 0;
            hyperLevel = 6;//6개부터 시작

            hyperPatterns = [

                            HyperArrow.UP,
                            HyperArrow.DOWN,
                            HyperArrow.DOWN,
                            HyperArrow.LEFT,
                            HyperArrow.RIGHT,
                            HyperArrow.RIGHT,
                            HyperArrow.DOWN,
                            HyperArrow.DOWN,
                            HyperArrow.UP,
                            HyperArrow.LEFT,
                            HyperArrow.RIGHT,
                            HyperArrow.UP

                        ];

        }

        public override void UndefinedPattern(Player _player, PlayerActionType _actionType, MonsterPatternType _patternType) {

            if (_patternType == MonsterPatternType.HYPER_ATTACK) {

                HyperAttack(_player);

            } else {

                Console.WriteLine($"정의되지 않은 몬스터 패턴 : {_patternType}");
                InputSystem.Waiting_Z_Input();

            }

        }

        public void HyperAttack(Player _player) {

            Console.Write($"  {name}이 고대의 주문을\n시전하려 한다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();


            if (PlayHyperPattern(_player)) {//패턴 파훼

                Console.Clear();
                PrintMonsterInfo();
                Console.Write($"  {name}의 캐스팅을 저지했다!");

            } else {//패턴 실패

                Console.Clear();
                heal();
                PrintMonsterInfo();
                Console.Write($"  {name}이 체력을 크게 회복했다!");

            }

            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"  {name}가 힘을 모으고 있다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"  {name}의 공격력이 순간 오른다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

        private bool PlayHyperPattern(Player _player) {

            //기믹 패턴을 받아옴.
            HyperArrow[] pArrows = new HyperArrow[hyperLevel];

            for (int i = 0; i < pArrows.Length; i++) {

                pArrows[i] = hyperPatterns[hyperIdx];
                hyperIdx = (hyperIdx + 1) % hyperPatterns.Length;

            }

            Console.Clear();
            PrintMonsterInfo();

            //기믹 패턴 출력
            foreach (HyperArrow _arr in pArrows) {

                switch (_arr) {

                    case HyperArrow.UP:
                        Console.Write($"↑ ");
                        break;

                    case HyperArrow.DOWN:
                        Console.Write($"↓ ");
                        break;

                    case HyperArrow.LEFT:
                        Console.Write($"← ");
                        break;

                    case HyperArrow.RIGHT:
                        Console.Write($"→ ");
                        break;

                }

            }
            Console.WriteLine();

            //패턴 대응 입력 대기
            int correctCnt = 0;
            for (int i = 0; i < pArrows.Length; i++) {


                ConsoleKey inputKey = InputSystem.Waiting_Arrow_Input();

                if (CmpArrowPattern(inputKey, pArrows[i])) {

                    Console.ForegroundColor = ConsoleColor.Green;
                    correctCnt++;

                } else {

                    Console.ForegroundColor = ConsoleColor.Red;

                }

                switch (inputKey) {

                    case ConsoleKey.UpArrow:
                        Console.Write($"↑ ");
                        break;

                    case ConsoleKey.DownArrow:
                        Console.Write($"↓ ");
                        break;

                    case ConsoleKey.LeftArrow:
                        Console.Write($"← ");
                        break;

                    case ConsoleKey.RightArrow:
                        Console.Write($"→ ");
                        break;

                }

                Console.ResetColor();

            }

            _player.PrintStatus(LEFT_PADDING);

            hyperLevel++;
            if (hyperLevel > MAX_HYPER_LEVEL)
                hyperLevel = MAX_HYPER_LEVEL;

            //패턴 성공여부 반환.
            return correctCnt >= pArrows.Length;

        }

        private bool CmpArrowPattern(ConsoleKey _key, HyperArrow _patternArrow) {

            if (_key == ConsoleKey.UpArrow && _patternArrow == HyperArrow.UP) return true;
            if (_key == ConsoleKey.DownArrow && _patternArrow == HyperArrow.DOWN) return true;
            if (_key == ConsoleKey.LeftArrow && _patternArrow == HyperArrow.LEFT) return true;
            if (_key == ConsoleKey.RightArrow && _patternArrow == HyperArrow.RIGHT) return true;

            return false;
        }
    }
}
