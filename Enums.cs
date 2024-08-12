using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Scenes {

    public enum SceneType { TUTORIAL, TOWN, SHOP, TOWN_TALK, MAZE, COMBAT };

}


namespace Scenes.Combat {
    enum CombatState { COMMANDING, WATING };
    enum CombatAction { ATTACK, DEFENSE };

}
