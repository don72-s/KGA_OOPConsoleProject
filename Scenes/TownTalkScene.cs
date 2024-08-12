using ConsoleGame.userData;
using System.Text;

namespace ConsoleGame.Scenes {
    public class TownTalkScene : Scene {

        Player player;

        List<string> outputs;

        private const int LEFT_PADDING = 46;

        public TownTalkScene(ISceneChangeable _game, Player _player) : base(_game) { 
        
            player = _player;

            outputs = new List<string>();
            StringBuilder sb = new StringBuilder();

            outputs.Add(sb.Clear()
                .Append("============================================\n")
                .Append("\n")
                .Append("             대충 평화로운 마을               \n")
                .Append("\n")
                .Append("============================================\n")
                .Append("수상할 정도로 소름돋는 미궁이 있는데도 마을은\n")
                .Append("평화롭다.\n")
                .Append("\n")
                .Append("  돌아간다.")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("============================================\n")
                .Append("\n")
                .Append("             대충 평화로운 마을               \n")
                .Append("\n")
                .Append("============================================\n")
                .Append("미궁에서 진동이 느껴지고 이상한 소리가 \n")
                .Append("나는 것 같다.\n")
                .Append("\n")
                .Append("  돌아간다.")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("============================================\n")
                .Append("\n")
                .Append("             대충 평화로운 마을               \n")
                .Append("\n")
                .Append("============================================\n")
                .Append("미궁에서 굉음이 들려온다. 미궁의 입구는 빛\n")
                .Append("한줄기 들지 않아 마치 칠흑같다.\n")
                .Append("\n")
                .Append("  돌아간다.")
                .ToString());
            

        }

        public override void Enter() {

        }

        public override void Exit() {
        }

        public override void Input() {

            if (Console.ReadKey(true).Key == ConsoleKey.Z) {
                scene.ChangeScene(SceneType.TOWN);
            }

        }

        public override void Print() {

            Console.Clear();
            Console.WriteLine(outputs[player.curMazeLevel]);
            Console.SetCursorPosition(0, 8);
            Console.Write("▶");
            player.PrintStatus(LEFT_PADDING);
        }

        public override void Update() {
        }
    }
}
