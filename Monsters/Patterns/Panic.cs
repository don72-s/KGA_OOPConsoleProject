using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void PanicPattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            Console.Write($"  {_monster.name}은 지쳐서 쉬고있다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
