using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void ChargeAttackPattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            int dmg;
            Console.Write($"  {_monster.name}의 회심의 일격!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            dmg = (int)(_monster.atk * 2f) - _player.def;
            if (dmg <= 0) dmg = 1;
            if (_actionType == PlayerActionType.DEFENSE && dmg > 5) dmg = 5;

            _player.TakeDamage(dmg);

            Console.Clear();
            _monster.PrintMonsterInfo();
            Console.Write($"  {dmg}의 피해를 입었다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();
        }

    }
}
