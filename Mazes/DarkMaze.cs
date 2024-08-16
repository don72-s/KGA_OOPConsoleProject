using ConsoleGame.Scenes;

namespace ConsoleGame.Mazes {
    public class DarkMaze : Maze {
        public DarkMaze(int[,] _mazeBase, int _sightRadius, ISceneChangeable _scene, IMonsterSetable _combatScene) : base(_mazeBase, _scene, _combatScene) {
        
            SIGHT_RADIUS = _sightRadius;

        }

        private int SIGHT_RADIUS;

        public override void PrintOnEnter() {

            Console.Clear();


            for (int y = -SIGHT_RADIUS; y <= SIGHT_RADIUS; y++) {

                for (int x = -SIGHT_RADIUS; x <= SIGHT_RADIUS; x++) {

                    if (Math.Abs(x) + Math.Abs(y) <= SIGHT_RADIUS &&
                       posX + x >= 0 && posX + x < mazeMap.GetLength(1) &&
                       posY + y >= 0 && posY + y < mazeMap.GetLength(0)) {


                        if (mazeMap[posY + y, posX + x].tileType == TileType.WALL) {

                            Console.BackgroundColor = ConsoleColor.Gray;

                        }

                        PrintSystem.WriteAt(posX + x, posY + y, mazeMap[posY + y, posX + x].texture);

                        Console.ResetColor();
                    }

                }

            }

            player.PrintStatus(mazeMap.GetLength(1));

            PrintCharacter();

        }


        public override void Print() {

            for (int y = -SIGHT_RADIUS; y <= SIGHT_RADIUS; y++) {

                for (int x = -SIGHT_RADIUS; x <= SIGHT_RADIUS; x++) {

                    if(x == 0 && y == 0) continue;

                    if (Math.Abs(x) + Math.Abs(y) <= SIGHT_RADIUS &&
                       posX + x >= 0 && posX + x < mazeMap.GetLength(1) &&
                       posY + y >= 0 && posY + y < mazeMap.GetLength(0)) {


                        if (mazeMap[posY + y, posX + x].tileType == TileType.WALL) {

                            Console.BackgroundColor = ConsoleColor.Gray;

                        }

                        PrintSystem.WriteAt(posX + x, posY + y, mazeMap[posY + y, posX + x].texture);

                        Console.ResetColor();
                    }

                }

            }

        }


        public override void Input(ConsoleKey _key) {

            if (_key == ConsoleKey.LeftArrow ||
                _key == ConsoleKey.RightArrow ||
                _key == ConsoleKey.UpArrow ||
                _key == ConsoleKey.DownArrow) { // 화살표 방향키일 경우

                int CheckX = posX;
                int CheckY = posY;

                switch (_key) {

                    case ConsoleKey.RightArrow:
                        CheckX++;
                        break;
                    case ConsoleKey.LeftArrow:
                        CheckX--;
                        break;
                    case ConsoleKey.DownArrow:
                        CheckY++;
                        break;
                    case ConsoleKey.UpArrow:
                        CheckY--;
                        break;

                }

                if (CheckMove(CheckX, CheckY)) {
                    PrintSystem.ClearAt(posX, posY);
                    posX = CheckX;
                    posY = CheckY;
                    RemoveOutSight(_key);

                    PrintCharacter();
                }

            }
        }

        public void RemoveOutSight(ConsoleKey _key) {

            int horizontal = 1;
            int vertical = 1;
            int iPositive = 1;

            int offset;
            int iValue;

            if (_key == ConsoleKey.LeftArrow || _key == ConsoleKey.RightArrow) {
                vertical = 0;
            } else if (_key == ConsoleKey.UpArrow || _key == ConsoleKey.DownArrow) {
                horizontal = 0;
            } else {
                Console.WriteLine("유효하지 않은 키 입력");
                return;
            }


            if (_key == ConsoleKey.RightArrow || _key == ConsoleKey.DownArrow) {
                iPositive = -1;
            }


            for (int i = SIGHT_RADIUS + 1; i > 0; i--) {

                offset = SIGHT_RADIUS - i + 1;
                iValue = i * iPositive;

                if (CheckPosValidation(posX + iValue * horizontal + offset * vertical, posY + iValue * vertical + offset * horizontal)) {
                    PrintSystem.WriteAt(posX + iValue * horizontal + offset * vertical, posY + iValue * vertical + offset * horizontal, ' ');
                }

                if (CheckPosValidation(posX + iValue * horizontal - offset * vertical, posY + iValue * vertical - offset * horizontal)) {
                    PrintSystem.WriteAt(posX + iValue * horizontal - offset * vertical, posY + iValue * vertical - offset * horizontal, ' ');
                }
            }

        }

    }
}
