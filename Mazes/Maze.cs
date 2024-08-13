using ConsoleGame.Monsters;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Mazes {
    public class Maze {


        protected int posX;
        protected int posY;

        protected ISceneChangeable sceneChanger;
        protected IMonsterSetable combatScene;
        protected MazeTile[,] mazeMap;
        int[,] mazeBase;

        protected Player player;

        public Maze(int[,] _mazeBase, ISceneChangeable _scene, IMonsterSetable _combatScene) {

            mazeBase = _mazeBase;
            mazeMap = MazeMaker.MazePasser(_mazeBase);
            posX = 1;
            posY = 1;

            player = Player.GetInstance();

            sceneChanger = _scene;
            combatScene = _combatScene;

        }

        public void InitMaze() {

            //todo : 미로 재사용이 필요할 경우에 작성 및 사용
            posX = 1;
            posY = 1;
            mazeMap = MazeMaker.MazePasser(mazeBase);

        }

        public virtual void Print() {

            //맵 출력
            for (int i = 0; i < mazeMap.GetLength(0); i++) {

                for (int j = 0; j < mazeMap.GetLength(1); j++) {


                    if (mazeMap[i, j].tileType == TileType.WALL) {

                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(mazeMap[i, j].texture);
                        Console.ResetColor();

                    } else {

                        Console.Write(mazeMap[i, j].texture);
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

        public void Input(ConsoleKey _key) {

            switch (_key) {

                case ConsoleKey.RightArrow:
                    if (CheckMove(posX + 1, posY))
                        posX++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (CheckMove(posX - 1, posY))
                        posX--;
                    break;
                case ConsoleKey.DownArrow:
                    if (CheckMove(posX, posY + 1))
                        posY++;
                    break;
                case ConsoleKey.UpArrow:
                    if (CheckMove(posX, posY - 1))
                        posY--;
                    break;

            }

        }

        bool CheckMove(int _posX, int _posY) {

            //외벽 체크
            if (!CheckPosValidation(_posX, _posY)) return false;

            TileType type = mazeMap[_posY, _posX].tileType;

            //벽이 아닌경우
            if (type != TileType.WALL && type != TileType.OUTER_WALL) {

                if (type != TileType.NONE) {

                    Console.SetCursorPosition(0, mazeMap.GetLength(0));
                    Console.Write($"정면에 몬스터 {type}이(가) 존재합니다.");
                    if (mazeMap[_posY, _posX].isGoal) {
                        Console.WriteLine(" [ 보스 ]");
                    } else {
                        Console.WriteLine();
                    }
                    Console.WriteLine($"진행하시겠습니까? ( z [진행] / x [취소] )");

                    ConsoleKey inputKey;

                    while (true) {

                        inputKey = Console.ReadKey(true).Key;

                        if (ConsoleKey.Z == inputKey) {

                            Monster monster = MonsterFactory.MakeMonster(type);

                            combatScene.SetMonster(monster, mazeMap[_posY, _posX].isGoal);
                            sceneChanger.ChangeScene(SceneType.COMBAT);
                            RemoveObject(_posX, _posY);

                            return true;
                        }

                        if (ConsoleKey.X == inputKey) {
                            return false;
                        }

                    }

                } else {
                    return true;
                }


            }


            return false;

        }

        bool CheckPosValidation(int _posX, int _posY) {

            if (_posX < 0 || _posX >= mazeMap.GetLength(1) || _posY < 0 || _posY >= mazeMap.GetLength(0))
                return false;

            return true;

        }

        void RemoveObject(int _posX, int _posY) {

            if (!CheckPosValidation(_posX, _posY)) return;

            mazeMap[_posY, _posX].tileType = TileType.NONE;
            mazeMap[_posY, _posX].texture = " ";

        }

    }
}
