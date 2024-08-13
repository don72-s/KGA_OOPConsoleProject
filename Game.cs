using ConsoleGame.userData;
using ConsoleGame.Scenes;

namespace ConsoleGame {
    public class Game : ISceneChangeable {

        private Scene curScene;
        private Player player;
        private Dictionary<SceneType, Scene> sceneDic;

        private bool isRunning;

        public Game() {

            Console.CursorVisible = false;


            player = Player.GetInstance();

            sceneDic = new Dictionary<SceneType, Scene>();

            sceneDic.Add(SceneType.TUTORIAL, new TutorialScene(this));
            sceneDic.Add(SceneType.TOWN, new TownScene(this));
            sceneDic.Add(SceneType.SHOP, new ShopScene(this));
            sceneDic.Add(SceneType.TOWN_TALK, new TownTalkScene(this));
            sceneDic.Add(SceneType.COMBAT, new CombatScene(this));
            sceneDic.Add(SceneType.MAZE, new MazeScene(this, (IMonsterSetable)sceneDic[SceneType.COMBAT]));

            curScene = sceneDic[SceneType.TUTORIAL];

            isRunning = true;

        }

        public void ChangeScene(SceneType _destScene) {

            if (sceneDic.ContainsKey(_destScene)) {
                curScene.Exit();
                curScene = sceneDic[_destScene];
                curScene.Enter();
            } else {
                isRunning = false;
            }

        }

        public void Run() {

            while (isRunning) {

                curScene.Print();
                curScene.Input();
                curScene.Update();

            }

        }

    }
}
