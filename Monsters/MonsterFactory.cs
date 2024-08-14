using ConsoleGame.Mazes;

namespace ConsoleGame.Monsters {
    public static class MonsterFactory {

        public static Monster MakeMonster(TileType _monsterTile) {

            Monster ret = null;

            switch (_monsterTile) {

                case TileType.SLIME:
                    ret = new Slime();
                    break;

                case TileType.ORC:
                    ret = new Orc();
                    break;

                case TileType.KNIGHT:
                    ret = new Knight();
                    break;

                case TileType.WIZARD:
                    ret = new Wizard();
                    break;

                case TileType.SNIPER:
                    ret = new Sniper();
                    break;

                case TileType.DRAGON:
                    break;

                default:
                    break;

            }

            return ret;

        }


    }
}
