using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void ChargePattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            Console.Write($"  {_monster.name}가 힘을 모으고 있다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            Console.Clear();
            _monster.PrintMonsterInfo();
            Console.Write($"  {_monster.name}의 공격력이 순간 오른다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
