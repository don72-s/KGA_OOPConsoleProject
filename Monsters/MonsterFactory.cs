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
                    break;

                case TileType.KNIGHT:
                    break;

                case TileType.WIZARD:
                    break;

                case TileType.SNIPER:
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
