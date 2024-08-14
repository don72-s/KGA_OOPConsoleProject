using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Scenes {

    public enum SceneType { TUTORIAL, TOWN, SHOP, TOWN_TALK, MAZE, COMBAT };

}


namespace ConsoleGame.Mazes { 

    public enum TileType { NONE, WALL, OUTER_WALL, SLIME, ORC, KNIGHT, WIZARD, SNIPER, DRAGON};

}

namespace ConsoleGame.Monsters { 

    enum MonsterType { SLIME, ORC, KNIGHT, WIZARD, SNIPER, DRAGON};

    public enum PatternType { NONE, ATTACK, DEFFENCE, HEAL, CHARGE, GIGA_CHARGE, CRITICAL, DEADLY_ATTACK, PANIC, HYPER_ATTACK };
namespace ConsoleGame.PlayerAction { 

    public enum PlayerActionType { ATTACK, DEFENSE };

}