using ConsoleGame.Mazes;
using ConsoleGame.userData;
using System.Text;

namespace ConsoleGame.Scenes {
    public class MazeScene : Scene {

        Player player;
        List<Maze> mazeArray;
        StringBuilder sb;
        string output;

        public MazeScene(ISceneChangeable _game, IMonsterSetable _combatScene) : base(_game){

            player = Player.GetInstance();
            mazeArray =
            [
                new Maze(MazeBases.maze0MapBase, _game, _combatScene),
                new Maze(MazeBases.maze1MapBase, _game, _combatScene),
                new DarkMaze(MazeBases.maze2MapBase, 4, _game, _combatScene),
            ];

            sb = new StringBuilder();
            output = "";

        }


        public override void Enter() {

            mazeArray[player.curMazeLevel].PrintOnEnter();

        }

        public override void Exit() {

        }

        public override void Input() {

            mazeArray[player.curMazeLevel].Input(Console.ReadKey(true).Key);

        }

        public override void Print() {

            mazeArray[player.curMazeLevel].Print();

        }

        public override void Update() {

        }



    }
}
