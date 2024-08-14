using ConsoleGame.Monsters;
using ConsoleGame.Scenes;

namespace ConsoleGame {

    public interface ISceneChangeable {

        void ChangeScene(SceneType _destScene);

    }

}

namespace ConsoleGame.Scenes {

    public interface IMonsterSetable { 
    
        void SetMonster(Monster _monster, bool _isBoss);

    }



}
