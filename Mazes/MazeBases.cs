﻿namespace ConsoleGame.Mazes {

    public static class MazeBases {

        //-----elements-------
        //NONE      [ ]  - 0
        //WALL      [-]  - 1
        //OUTER_WALL[-]  - 2
        //SLIME     [x]  - 3
        //ORC       [▲]  - 4
        //KNIGHT    [■]  - 5
        //WIZARD    [■]  - 6
        //SNIPER    [◎] - 7
        //DRAGON    [◎] - 8
        //
        //Goal : negativeValue
        //-----elements-------

        //미궁 작성 시 ctrl + f [1, ]으로 벽을 그려가며 작성

        //레벨 0
        public static int[,] maze0MapBase = new int[,] {

            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 0, 0, 0, 0, 0, 0, 1,-4, 1, },
            {1, 1, 1, 0, 1, 1, 1, 1, 0, 1, },
            {1, 0, 1, 3, 0, 0, 0, 1, 0, 1, },
            {1, 0, 0, 0, 1, 1, 0, 1, 0, 1, },
            {1, 0, 1, 1, 1, 0, 0, 1, 0, 1, },
            {1, 0, 0, 1, 0, 1, 0, 1, 0, 1, },
            {1, 1, 0, 1, 0, 1, 0, 3, 0, 1, },
            {1, 0, 0, 3, 0, 1, 0, 0, 0, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },

        };

        //레벨 1
        public static int[,] maze1MapBase = new int[,] {

            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1, },
            {1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 4, 0, 0, 1, 0, 1, 1, 0, 1, },
            {1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, },
            {1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 3, 1, },
            {1, 3, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 1, },
            {1, 0, 1, 1, 1, 1, 3, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, 1, },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 0, 0, 0, 1, 1, 0, 0, 1, },
            {1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1, },
            {1, 0, 1, 0, 0, 0, 4, 0, 0, 1, 0, 1, 1, 0, 0, 0, 4, 0, 0, 1, },
            {1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 4, 1, 1, 0, 1, 1, 1, },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 5, 0, 0, 1, },
            {1, 4, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, },
            {1, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, },
            {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 0, 1, 1, 1, 1, },
            {1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 4, 1, },
            {1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1, },
            {1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, },
            {1, 0, 0, 0, 0, 0, 3, 0, 0, 6, 0, 0, 1, 0, 0, 0, 1, 0,-7, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, }

        };

        //레벨 2
        public static int[,] maze2MapBase = new int[,] {

            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
            {1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, },
            {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, },
            {1, 4, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, },
            {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, },
            {1, 0, 0, 0, 1, 4, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, },
            {1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, },
            {1, 0, 0, 0, 0, 0, 1, 0, 4, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, },
            {1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, },
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, },
            {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, },
            {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, },
            {1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, },
            {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, },
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, },
            {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, },
            {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, },
            {1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 0, 0,-8, 0, 0, 1, 1, 1, 1, 1, 1, 1, },
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, },

        };

    }

}
