using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void GigaChargePattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            Console.Write($"  {_monster.name}가 2턴동안 폭주한다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
