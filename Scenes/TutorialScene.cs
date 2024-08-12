using System.Text;

namespace ConsoleGame.Scenes {
    public class TutorialScene : Scene {

        int idx;
        List<string> outputs;

        public TutorialScene(ISceneChangeable _game) : base(_game) { 
            outputs = new List<string>();

            StringBuilder sb = new StringBuilder();

            outputs.Add(sb
                .Append("========================================================\n")
                .Append("                    -그냥 전설-          [z를 눌러 시작]\n")
                .Append("                                                        \n")
                .Append("부제 - 프로그래머한테머싯는이름짓기를바라는사람은없겠지\n")
                .Append("========================================================\n")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("========================================================\n")
                .Append("\n")
                .Append(" 아무런 맥락없이 마을에 있는 던전을 공략하면 성공합니다.\n")
                .Append("\n")
                .Append("========================================================\n")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("========================================================\n")
                .Append("\n")
                .Append("     던전은 총 3개의 미궁으로 이루어져 있습니다.\n")
                .Append("\n")
                .Append("========================================================\n")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("========================================================\n")
                .Append("     각 던전들에는 몬스터들과 보스가 존재하며\n")
                .Append("       보스를 물리치면 던전이 클리어됩니다.\n")
                .Append(" 클리어 이후 다시 던전을 가면 다음 던전으로 입장합니다.\n")
                .Append("========================================================\n")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("========================================================\n")
                .Append("                        조작법\n")
                .Append("                      확인 : z\n")
                .Append("                    이동 : 화살표\n")
                .Append("========================================================\n")
                .ToString());

            outputs.Add(sb.Clear()
                .Append("========================================================\n")
                .Append("\n")
                .Append("         z를 눌러 마을로 이동한다.\n")
                .Append("\n")
                .Append("========================================================\n")
                .ToString());
        }

        public override void Enter() {
            idx = 0;
        }

        public override void Exit() {
            
        }

        public override void Input() {
            InputSystem.Waiting_Z_Input();
        }

        public override void Print() {
            Console.Clear();
            Console.WriteLine(outputs[idx]);
        }

        public override void Update() {
            idx++;
            if (idx >= outputs.Count) { 
                scene.ChangeScene(SceneType.TOWN);
            }
        }

    }
}
