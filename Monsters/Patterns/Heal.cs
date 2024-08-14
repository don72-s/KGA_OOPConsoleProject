using ConsoleGame.PlayerAction;
using ConsoleGame.Scenes;
using ConsoleGame.userData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Monsters.Patterns {
    public static partial class MonsterPattern {

        public static void HealPattern(Monster _monster, Player _player, PlayerActionType _actionType) {

            Console.Write($"  {_monster.name}가 심호흡을 한다.");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();


            _monster.heal();

            Console.Clear();
            _monster.PrintMonsterInfo();
            Console.Write($"  {_monster.name}가 체력을 회복했다!");
            _player.PrintStatus(LEFT_PADDING);
            InputSystem.Waiting_Z_Input();

        }

    }
}
