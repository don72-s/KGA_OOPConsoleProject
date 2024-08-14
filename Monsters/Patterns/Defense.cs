using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void DefensePattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            Console.Write($"  {_monster.name}는 몸을 숨긴다!");
            _player.PrintStatus(LEFT_PADDING);
            _monster.SetDefBuff(1.5f);
            InputSystem.Waiting_Z_Input();

            Console.Clear();
            _monster.PrintMonsterInfo();
            Console.Write($"  {_monster.name}의 방어가 순간 오른다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
