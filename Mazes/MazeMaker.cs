namespace ConsoleGame.Mazes {

    public struct MazeTile {

        public TileType tileType;
        public string texture;

        public bool isGoal;

    }

    public static class MazeMaker {

        private static MazeTile[] tileDatas = null;

        static void InitTileData() {

            tileDatas = new MazeTile[] {

                new MazeTile(){ tileType = TileType.NONE, texture = " " },//0
                new MazeTile(){ tileType = TileType.WALL, texture = " " },//1
                new MazeTile(){ tileType = TileType.WALL, texture = " " },//2
                new MazeTile(){ tileType = TileType.SLIME, texture = "x" },//3
                new MazeTile(){ tileType = TileType.ORC, texture = "▲" },//4
                new MazeTile(){ tileType = TileType.KNIGHT, texture = "■" },//5
                new MazeTile(){ tileType = TileType.WIZARD, texture = "■" },//6
                new MazeTile(){ tileType = TileType.SNIPER, texture = "◎" },//7
                new MazeTile(){ tileType = TileType.DRAGON, texture = "◎" },//8
            };

        }

        public static MazeTile[,] MazePasser(int[,] _mazeBase) {

            if (tileDatas == null) InitTileData();

            MazeTile[,] mapData = new MazeTile[_mazeBase.GetLength(0), _mazeBase.GetLength(1)];

            for (int i = 0; i < _mazeBase.GetLength(0); i++) {

                for (int j = 0; j < _mazeBase.GetLength(1); j++) {

                    mapData[i, j] = tileDatas[Math.Abs(_mazeBase[i, j])];

                    if (_mazeBase[i, j] < 0) { 

                        mapData[i, j].isGoal = true;

                    }

                }

            }

            return mapData;
        }

    }
}
