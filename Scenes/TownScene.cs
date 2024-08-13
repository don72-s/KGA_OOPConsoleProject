using ConsoleGame.userData;
using System.Text;

namespace ConsoleGame.Scenes {
    public class TownScene : Scene {

        enum Selection { MAZE, SHOP, TALK, EXIT, SELECT_COUNT };

        private int cursorIdx;
        private string output;

        private const int LEFT_PADDING = 46;

        Player player;

        public TownScene(ISceneChangeable _game) : base(_game) {

            player = Player.GetInstance();

            StringBuilder sb = new StringBuilder();

            output =
                sb
                .Append("============================================\n")
                .Append("\n")
                .Append("             대충 평화로운 마을               \n")
                .Append("\n")
                .Append("============================================\n")
                .Append("마을은 평화로워 보인다. 이제 무엇을 할까?\n")
                .Append("\n")
                .Append("  미궁으로 향한다.\n")
                .Append("  상점으로 간다.\n")
                .Append("  미궁을 조사한다.\n")
                .Append("  게임을 종료한다.\n")
                .ToString();
            
        }

        public override void Enter() {

            cursorIdx = 0;

        }

        public override void Exit() {

        }

        public override void Input() {

            switch (Console.ReadKey(true).Key) {

                case ConsoleKey.UpArrow:
                    if (cursorIdx <= 0) {
                        cursorIdx = (int)Selection.SELECT_COUNT - 1;
                    } else {
                        cursorIdx--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    cursorIdx++;
                    break;

                case ConsoleKey.Z:
                    TownZInput();
                    break;

            }

        }

        public override void Print() {

            Console.Clear();
            Console.WriteLine(output);
            Console.SetCursorPosition(0, 7 + cursorIdx % (int)Selection.SELECT_COUNT);
            Console.Write("▶");

            player.PrintStatus(LEFT_PADDING);
        }

        public override void Update() {

        }



        private void TownZInput() {

            cursorIdx %= (int)Selection.SELECT_COUNT;

            switch ((Selection)cursorIdx) {

                case Selection.MAZE:
                    //todo :change
                    break;

                case Selection.SHOP:
                    scene.ChangeScene(SceneType.SHOP);
                    break;

                case Selection.TALK:
                    scene.ChangeScene(SceneType.TOWN_TALK);
                    break;

                case Selection.EXIT:
                    //todo :exit
                    break;

                default:
                    break;

            }

        }
    }
}
