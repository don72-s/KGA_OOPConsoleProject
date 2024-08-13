using ConsoleGame.Scenes;

namespace ConsoleGame.Mazes {
    public class DarkMaze : Maze {
        public DarkMaze(int[,] _mazeBase, ISceneChangeable _scene, IMonsterSetable _combatScene) : base(_mazeBase, _scene, _combatScene) { }

        public override void Print() {
            //맵 출력
            for (int i = 0; i < mazeMap.GetLength(0); i++) {

                for (int j = 0; j < mazeMap.GetLength(1); j++) {

                    //어둠 미로용 기믹(시야 범위)
                    int x = 99;
                    int y = 99;

                    x = Math.Abs(posX - j);
                    y = Math.Abs(posY - i);

                    //0,1레벨이거나 2레벨이면 어둠기믹=>암전조건
                    if (x + y <= 3) {

                        if (mazeMap[i, j].tileType == TileType.WALL) {

                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(mazeMap[i, j].texture);
                            Console.ResetColor();

                        } else {

                            Console.Write(mazeMap[i, j].texture);
                        }

                    } else if (x + y > 3) { //암전단계에서의 패딩
                        Console.Write(" ");
                    }


                    //todo : 깜빡이 출력 최적화하기.
                }

                Console.WriteLine();

            }

            player.PrintStatus(mazeMap.GetLength(1));

            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("@");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

        }

    }
}
