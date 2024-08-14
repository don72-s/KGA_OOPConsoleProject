﻿using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void GigaChargeAttackPattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            int dmg;
            Console.Write($"  {_monster.name}가 치명적인 공격을 날린다!!!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

            dmg = 999999;
            if (_actionType == PlayerActionType.DEFENSE) dmg = 7;

            _player.TakeDamage(dmg);

            Console.Clear();
            _monster.PrintMonsterInfo();
            Console.Write($"  {dmg}의 피해를 입었다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
