namespace ConsoleGame.Scenes {
    public abstract class Scene {

        protected ISceneChangeable scene;

        public Scene(ISceneChangeable _game) { 
            scene = _game;
        }

        public abstract void Enter();
        public abstract void Print();
        public abstract void Input();
        public abstract void Update();
        public abstract void Exit();

    }
}
