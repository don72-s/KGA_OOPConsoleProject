using ConsoleGame;
using System;
using System.ComponentModel.Design;
using System.Dynamic;

namespace ConsoleGame {
    internal class Program {
        enum SceneState { TUTORIAL, TOWN, MAZE, COMBAT };
        enum CombatState { COMMANDING, WATING };
        enum CombatAction { ATTACK, DEFENSE };

        struct GameData {

            public SceneState curScene;
            public int cursorIdx;

            public int curMazeLevel;
            public Monster curMonster;
            public CombatAction act;
            public int monsterActIdx;

            public int MAX_HP;
            public int hp;

            public int attack;
            public int defense;

            public const int ATK_INCREASE = 10;
            public const int DEF_INCREASE = 10;
            public const int MAX_HP_INCREASE = 10;

            public int level;
            public int exp;
            public int[] needExp;

            public int gold;

            public int posX;
            public int posY;
        }


        struct MazeTile {

            public enum TileType { NONE, WALL, OUTER_WALL, SLIME, ORC, KNIGHT, WIZARD, SNIPER, DRAGON };

            public TileType tileType;
            public string texture;

            public bool isGoal;

        }
        static MazeTile[] tileDatas;

        struct Monster {
            public enum PatternType { NONE, ATTACK, DEFFENCE, HEAL, CHARGE, GIGA_CHARGE, CRITICAL, DEADLY_ATTACK, PANIC, HYPER_ATTACK };
            public enum HyperArrow { UP, DOWN, LEFT, RIGHT };

            public string name;
            public int hp;
            public int attck;
            public int defense;
            public int healPoint;

            public int MAX_HP;

            public int gold;
            public int exp;

            public int patternIdx;
            public PatternType[] patterns;

            public int curHyperLength;
            public int hyperIdx;
            public int hyperMinCnt;//출연할 화살표 최소 갯수(시작 갯수)
            public int hyperMaxCnt;//출연할 화살표 최대 갯수
            public HyperArrow[] hyperPattern; 

            public float defBuff;
        }
        static Monster[] monsterDatas;


        static GameData gameData;


        static MazeTile[,] curMazeMapData;

        static int[,] maze0MapBase;
        static int[,] maze1MapBase;
        static int[,] maze2MapBase;
        static MazeTile[,] maze0MapData;
        static MazeTile[,] maze1MapData;
        static MazeTile[,] maze2MapData;
        //startPos : 1, 1

        static CombatState combatState;

        static string clearCmdStr = "                                    ";
        static int leftPadding = 37;


        /// <summary>
        /// 장면을 전환한다.
        /// </summary>
        /// <param name="_destScene">목표로 하는 장면</param>
        static void ChangeScene(SceneState _destScene) {

            gameData.cursorIdx = 0;
            gameData.curScene = _destScene;

            switch (_destScene) {

                case SceneState.TOWN:
                    gameData.hp = gameData.MAX_HP;
                    gameData.posX = 1;
                    gameData.posY = 1;
                    gameData.cursorIdx = 0;
                    break;

                case SceneState.MAZE:
                    curMazeMapData = GetMazeData();
                    break;

                case SceneState.COMBAT:
                    gameData.cursorIdx = 0;
                    gameData.monsterActIdx = 0;
                    combatState = CombatState.COMMANDING;
                    gameData.act = CombatAction.ATTACK;
                    break;
            }

        }


        #region 미궁 관련 기능.

        /// <summary>
        /// 몬스터 객체를 제작하여 반환한다.
        /// </summary>
        /// <param name="_monsterType">제작할 몬스터 객체 타입.</param>
        /// <returns></returns>
        static Monster MonsterFactory(MazeTile.TileType _monsterType) {

            Monster ret = new Monster();

            switch (_monsterType) {

                case MazeTile.TileType.SLIME:
                    ret = new Monster() {
                        name = "슬라임",
                        MAX_HP = 7,
                        hp = 7,
                        attck = 1,
                        defense = 1,
                        exp = 10,
                        gold = 5,
                        patterns = new Monster.PatternType[] { Monster.PatternType.ATTACK }
                    };
                    break;

                case MazeTile.TileType.ORC:
                    ret = new Monster() {
                        name = "오크",
                        MAX_HP = 25,
                        hp = 25,
                        attck = 30,
                        defense = 5,
                        exp = 25,
                        gold = 30,
                        patterns = new Monster.PatternType[] {
                            Monster.PatternType.DEFFENCE,
                            Monster.PatternType.ATTACK,
                            Monster.PatternType.ATTACK,

                        }
                    };
                    break;

                case MazeTile.TileType.KNIGHT:
                    ret = new Monster() {
                        name = "기사",
                        MAX_HP = 70,
                        hp = 70,
                        attck = 37,
                        defense = 11,
                        healPoint = 12,

                        exp = 300,
                        gold = 200,
                        patterns = new Monster.PatternType[] {
                            Monster.PatternType.DEFFENCE,
                            Monster.PatternType.ATTACK,
                            Monster.PatternType.CHARGE,
                            Monster.PatternType.CRITICAL,
                            Monster.PatternType.HEAL,
                        }
                    };
                    break;

                case MazeTile.TileType.WIZARD:
                    ret = new Monster() {
                        name = "마법사",
                        MAX_HP = 70,
                        hp = 70,
                        attck = 30,
                        defense = 8,
                        healPoint = 15,
                        exp = 300,
                        gold = 200,
                        patterns = new Monster.PatternType[] {
                            Monster.PatternType.CHARGE,
                            Monster.PatternType.CRITICAL,
                            Monster.PatternType.PANIC,
                            Monster.PatternType.HEAL,
                        }
                    };
                    break;

                case MazeTile.TileType.SNIPER:
                    ret = new Monster() {
                        name = "저격수",
                        MAX_HP = 130,
                        hp = 130,
                        attck = 15,
                        defense = 10,
                        exp = 350,
                        gold = 350,
                        patterns = new Monster.PatternType[] {
                            Monster.PatternType.GIGA_CHARGE,
                            Monster.PatternType.DEADLY_ATTACK,
                            Monster.PatternType.DEADLY_ATTACK,
                            Monster.PatternType.DEFFENCE,
                            Monster.PatternType.DEFFENCE,
                        }
                    };
                    break;

                case MazeTile.TileType.DRAGON:
                    ret = new Monster() {
                        name = "드래곤",
                        MAX_HP = 200,
                        hp = 200,
                        attck = 58,
                        defense = 20,
                        healPoint = 40,
                        exp = 1275,
                        gold = 700,
                        patterns = new Monster.PatternType[] {
                            Monster.PatternType.ATTACK,
                            Monster.PatternType.HYPER_ATTACK, 
                            Monster.PatternType.CRITICAL,
                        },

                        hyperPattern = new Monster.HyperArrow[] {

                            Monster.HyperArrow.UP,
                            Monster.HyperArrow.DOWN,
                            Monster.HyperArrow.DOWN,
                            Monster.HyperArrow.LEFT,
                            Monster.HyperArrow.RIGHT,
                            Monster.HyperArrow.RIGHT,
                            Monster.HyperArrow.DOWN,
                            Monster.HyperArrow.DOWN,
                            Monster.HyperArrow.UP,
                            Monster.HyperArrow.LEFT,
                            Monster.HyperArrow.RIGHT,
                            Monster.HyperArrow.UP

                        },

                        curHyperLength = 6,
                        hyperMinCnt = 6,
                        hyperMaxCnt = 10,

                    };
                    break;

            }

            ret.patternIdx = 0;
            ret.hyperIdx = 0;
            ret.defBuff = 1;

            return ret;

        }

        /// <summary>
        /// 미궁의 초본 데이터를 미궁 데이터 객체로 파싱해 반환한다.
        /// </summary>
        /// <param name="_mazeBase">미궁 초본 데이터</param>
        /// <returns>정제된 미궁 객체 데이터</returns>
        static MazeTile[,] MazePasser(int[,] _mazeBase) {

            MazeTile[,] mapData = new MazeTile[_mazeBase.GetLength(0), _mazeBase.GetLength(1)];

            for (int i = 0; i < _mazeBase.GetLength(0); i++) {

                for (int j = 0; j < _mazeBase.GetLength(1); j++) {

                    mapData[i, j] = tileDatas[_mazeBase[i, j]];

                }

            }

            return mapData;
        }

        /// <summary>
        /// 현재 미궁 레벨에 맞는 미궁을 가져온다.
        /// </summary>
        /// <returns>미궁 데이터 객체 반환.</returns>
        static MazeTile[,] GetMazeData() {

            MazeTile[,] ret = maze0MapData;

            switch (gameData.curMazeLevel) {

                case 1:
                    ret = maze1MapData;
                    break;

                case 2:
                    ret = maze2MapData;
                    break;

            }

            return ret;

        }

        /// <summary>
        /// 미궁의 외벽을 벗어나려 하는지 확인한다.
        /// </summary>
        /// <param name="_posX">확인할 x좌표</param>
        /// <param name="_posY">확인할 y좌표</param>
        /// <returns>미궁의 내부이면 true, 외부 좌표면 false 반환.</returns>
        static bool CheckPosValidation(int _posX, int _posY) {

            if (_posX < 0 || _posX >= curMazeMapData.GetLength(1) || _posY < 0 || _posY >= curMazeMapData.GetLength(0))
                return false;

            return true;

        }

        /// <summary>
        /// 미궁 내 이동 관련 동작을 처리한다.
        /// </summary>
        /// <param name="_posX">목표 x좌표</param>
        /// <param name="_posY">목표 y좌표</param>
        /// <returns></returns>
        static bool CheckMove(int _posX, int _posY) {

            //외벽 체크
            if (!CheckPosValidation(_posX, _posY)) return false;

            MazeTile.TileType type = curMazeMapData[_posY, _posX].tileType;

            //몬스터인경우
            if (type != MazeTile.TileType.WALL && type != MazeTile.TileType.OUTER_WALL) {

                if (type != MazeTile.TileType.NONE) {

                    Console.SetCursorPosition(0, Program.curMazeMapData.GetLength(0));
                    Console.Write($"정면에 몬스터 {type}이(가) 존재합니다.");
                    if (curMazeMapData[_posY, _posX].isGoal) {
                        Console.WriteLine(" [ 보스 ]");
                    } else {
                        Console.WriteLine();
                    }
                    Console.WriteLine($"진행하시겠습니까? ( z [진행] / x [취소] )");

                    ConsoleKey inputKey;

                    while (true) {

                        inputKey = Console.ReadKey(true).Key;

                        if (ConsoleKey.Z == inputKey) {

                            gameData.curMonster = MonsterFactory(type);

                            //전투씬으로 전환.
                            ChangeScene(SceneState.COMBAT);
                            RemoveObject(_posX, _posY);

                            return true;
                        }

                        if (ConsoleKey.X == inputKey) {
                            return false;
                        }

                    }

                } else {
                    return true;
                }


            }


            return false;

        }

        /// <summary>
        /// 미궁 내의 오브젝트를 지운다.
        /// </summary>
        /// <param name="_posX">지울 오브젝트의 x좌표</param>
        /// <param name="_posY">지울 오브젝트의 y좌표</param>
        static void RemoveObject(int _posX, int _posY) {

            if (!CheckPosValidation(_posX, _posY)) return;

            curMazeMapData[_posY, _posX].tileType = MazeTile.TileType.NONE;
            curMazeMapData[_posY, _posX].texture = " ";

        }

        #endregion



        #region 게임 루프

        static void Start() {

            Console.CursorVisible = false;

            gameData = new GameData {
                curScene = SceneState.TUTORIAL,
                cursorIdx = 0,

                curMazeLevel = 0,

                act = CombatAction.ATTACK,//
                monsterActIdx = 0,//

                MAX_HP = 15,
                hp = 15,

                attack = 5,
                defense = 10,

                level = 1,
                exp = 0,
                needExp = new int[] { 8, 30, 150, 500, 9999 },
                gold = 10,
                posX = 1,//
                posY = 1//
            };



            tileDatas = new MazeTile[] {

                new MazeTile(){ tileType = MazeTile.TileType.NONE, texture = " " },//0
                new MazeTile(){ tileType = MazeTile.TileType.WALL, texture = " " },//1
                new MazeTile(){ tileType = MazeTile.TileType.OUTER_WALL, texture = " " },//2
                new MazeTile(){ tileType = MazeTile.TileType.SLIME, texture = "x" },//3
                new MazeTile(){ tileType = MazeTile.TileType.ORC, texture = "▲" },//4
                new MazeTile(){ tileType = MazeTile.TileType.KNIGHT, texture = "■" },//5
                new MazeTile(){ tileType = MazeTile.TileType.WIZARD, texture = "■" },//6
                new MazeTile(){ tileType = MazeTile.TileType.SNIPER, texture = "◎" },//7
                new MazeTile(){ tileType = MazeTile.TileType.DRAGON, texture = "◎" },//8
            };

            //미궁 초본 데이터
            maze0MapBase = new int[,] {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 1, 4, 1 },
                {1, 1, 1, 0, 1, 1, 1, 1, 0, 1 },
                {1, 0, 1, 3, 0, 0, 0, 1, 0, 1 },
                {1, 0, 0, 0, 1, 1, 0, 1, 0, 1 },
                {1, 0, 1, 1, 1, 0, 0, 1, 0, 1 },
                {1, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
                {1, 1, 0, 1, 0, 1, 0, 3, 0, 1 },
                {1, 0, 0, 3, 0, 1, 0, 0, 0, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            };
            maze1MapBase = new int[,] {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1 },
                {1, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 4, 0, 0, 1, 0, 1, 1, 0, 1 },
                {1, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 3, 1 },
                {1, 3, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 1 },
                {1, 0, 1, 1, 1, 1, 3, 1, 0, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 0, 0, 0, 1, 1, 0, 0, 1 },
                {1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1 },
                {1, 0, 1, 0, 0, 0, 4, 0, 0, 1, 0, 1, 1, 0, 0, 0, 4, 0, 0, 1 },
                {1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 4, 1, 1, 0, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 5, 0, 0, 1 },
                {1, 4, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 3, 0, 1, 1, 1, 1 },
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 0, 4, 1 },
                {1, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 0, 1, 1 },
                {1, 0, 1, 1, 1, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1 },
                {1, 0, 0, 0, 0, 0, 3, 0, 0, 6, 0, 0, 1, 0, 0, 0, 1, 0, 7, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }

            };
            maze2MapBase = new int[,] {
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
                {1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 1, 1 },
                {1, 4, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                {1, 0, 0, 0, 1, 4, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
                {1, 0, 0, 0, 0, 0, 1, 0, 4, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                {1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
                {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                {1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                {1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
                {1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1 },
                {1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1 },
                {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 0, 0, 0, 1 },
                {1, 0, 1, 0, 1, 4, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1 },
                {1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1 },
                {1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 8, 0, 0, 1, 1, 1, 1, 1, 1, 1 },
                {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },

            };

            //파싱된 미궁 객체 데이터, 골인 좌표 지정
            maze0MapData = MazePasser(maze0MapBase);
            maze0MapData[1, 8].isGoal = true;

            maze1MapData = MazePasser(maze1MapBase);
            maze1MapData[18, 18].isGoal = true;

            maze2MapData = MazePasser(maze2MapBase);
            maze2MapData[25, 10].isGoal = true;

            //초기 미궁맵과 레벨 설정
            curMazeMapData = maze0MapData;
            gameData.curMazeLevel = 0;

            combatState = CombatState.COMMANDING;

        }

        static void Render() {

            Console.Clear();

            switch (gameData.curScene) {

                case SceneState.MAZE:
                    PrintMazeScene();
                    break;

                case SceneState.COMBAT:
                    PrintCombatScene();
                    break;

            }

        }

        #region 랜더링 내부 함수.

        #region 상태별 장면 출력 함수.

        static void PrintMazeScene() {

            //맵 출력
            for (int i = 0; i < curMazeMapData.GetLength(0); i++) {

                for (int j = 0; j < curMazeMapData.GetLength(1); j++) {

                    //어둠 미로용 기믹
                    int x = 99;
                    int y = 99;

                    if (gameData.curMazeLevel >= 2) {
                        x = Math.Abs(gameData.posX - j);
                        y = Math.Abs(gameData.posY - i);
                    }
                    //기믹 설정부

                    //0,1레벨이거나 2레벨이면 어둠기믹
                    if (gameData.curMazeLevel < 2 || x + y <= 3) {

                        if (curMazeMapData[i, j].tileType == MazeTile.TileType.WALL ||
                           curMazeMapData[i, j].tileType == MazeTile.TileType.OUTER_WALL) {

                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.Write(curMazeMapData[i, j].texture);
                            Console.ResetColor();

                        } else {

                            Console.Write(curMazeMapData[i, j].texture);
                        }

                    } else if (x + y > 3) { //암전단계에서의 패딩
                        Console.Write(" ");
                    }

                }

                Console.WriteLine();

            }

            //정보 출력

            int leftPos = curMazeMapData.GetLength(1);
            PrintStatus(leftPos);



            //캐릭터 출력
            Console.SetCursorPosition(gameData.posX, gameData.posY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("@");
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

        }
        static void PrintCombatScene() {

            PrintMonsterInfo();

            switch (combatState) {

                case CombatState.COMMANDING:
                    PrintCommanding();
                    PrintStatus(leftPadding);
                    break;

                case CombatState.WATING:
                    PrintWaiting();
                    break;

            }

 

        }

        #endregion



        #region 전투 출력 관련 로직 함수 

        /// <summary>
        /// 몬스터 상태 창 출력
        /// </summary>
        static void PrintMonsterInfo() {

            if (gameData.curMonster.hp <= 0) gameData.curMonster.hp = 0;

            Console.WriteLine($"============== {gameData.curMonster.name} ==============");
            Console.WriteLine();
            Console.WriteLine($"          대충 {gameData.curMonster.name} 이미지");
            Console.WriteLine($"  hp :[ {gameData.curMonster.hp} ]");
            Console.WriteLine($"====================================");

        }



        /// <summary>
        /// 명령 선택창 출력
        /// </summary>
        static void PrintCommanding() {

            Console.WriteLine("  행동을 선택하세요.");
            Console.WriteLine();
            Console.WriteLine("  공격!");
            Console.WriteLine("  방어!");

            Console.SetCursorPosition(0, 7 + gameData.cursorIdx % 2);
            Console.Write("▶");
        }
        /// <summary>
        /// 캐릭터/몬스터 행동 출력
        /// </summary>
        static void PrintWaiting() {

            (int, int) cmdPos = Console.GetCursorPosition();


            int dmg = 0;

            //플레이어 행동
            dmg = PlayerAction();

            //입력 대기
            Waiting_Z_Input();

            //적 쓰러짐 확인.
            if (gameData.curMonster.hp <= 0) {

                //보상 계산
                Rewarding(cmdPos);

                //보스몬스터였을 경우
                if (curMazeMapData[gameData.posY, gameData.posX].isGoal) {

                    //게임 클리어 조건
                    if (gameData.curMazeLevel >= 2) { 

                        Console.Clear();
                        Console.WriteLine("게임을 클리어하셨습니다. 축하합니다.");
                        Waiting_Z_Input();
                        Environment.Exit(0);
                    }


                    gameData.curMazeLevel++;
                    Console.WriteLine(" 던전 공략 성공!\n 마을로 귀환합니다.");

                    Waiting_Z_Input();
                    ChangeScene(SceneState.TOWN);
                    Render();

                } else { //보스가 아닐때
                    Console.Write(" 전투를 종료합니다.");
                    ChangeScene(SceneState.MAZE);
                }

                return;
            }

            //몬스터의 행동.

            Monster.PatternType[] pattern = gameData.curMonster.patterns;

            Console.Clear();
            PrintMonsterInfo();
            gameData.curMonster.defBuff = 1;

            //행동 분기
            switch (pattern[gameData.curMonster.patternIdx]) {

                case Monster.PatternType.ATTACK:
                    MonsterAttack();
                    break;

                case Monster.PatternType.DEFFENCE:
                    MonsterDeffence();
                    break;

                case Monster.PatternType.CHARGE:
                    MonsterCharge();
                    break;

                case Monster.PatternType.CRITICAL:
                    MonsterCritical();
                    break;

                case Monster.PatternType.HEAL:
                    MonsterHeal();
                    break;

                case Monster.PatternType.PANIC:
                    MonsterPanic();
                    break;

                case Monster.PatternType.GIGA_CHARGE:
                    MonsterGigaCharge();
                    break;

                case Monster.PatternType.DEADLY_ATTACK:
                    MonsterDeadlyAttack();
                    break;

                case Monster.PatternType.HYPER_ATTACK:
                    MonsterHyperAttack();
                    break;
            }

            //다음 패턴으로 인덱싱
            gameData.curMonster.patternIdx = (gameData.curMonster.patternIdx + 1) % pattern.Length;

            //패배(사망) 확인
            if (gameData.hp <= 0) {

                Console.Clear();
                Console.WriteLine("게임오버");
                Waiting_Z_Input();

                Environment.Exit(0);
            }

            //전투를 이어감
            combatState = CombatState.COMMANDING;
            Render();

        }



        /// <summary>
        /// 캐릭터 상태 창 출력
        /// </summary>
        /// <param name="leftPos">왼쪽으로 떨어질 만큼의 크기 입력</param>
        static void PrintStatus(int leftPos) {
            int yCnter = 0;

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($" =======상태=======");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  Level : {gameData.level}");
            if (gameData.level >= 5) Console.Write(" [MAX] ");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  hp    : {gameData.hp} / {gameData.MAX_HP}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  atk   : {gameData.attack}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  def   : {gameData.defense}");

            Console.SetCursorPosition(leftPos, yCnter++);
            if(gameData.level >= 5)
                Console.Write($"  exp   : --- / ---");
            else
                Console.Write($"  exp   : {gameData.exp} / {gameData.needExp[gameData.level - 1]}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  골드  : {gameData.gold} G");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($" ==================");
        }


        /// <summary>
        /// 액션 실행.
        /// </summary>
        /// <returns></returns>
        static int PlayerAction() {

            int dmg = 0;

            if (gameData.act == CombatAction.ATTACK) {

                dmg = gameData.attack - (int)(gameData.curMonster.defense * gameData.curMonster.defBuff);
                if (dmg <= 0) dmg = 1;
                gameData.curMonster.hp -= dmg;

                Console.Clear();
                PrintMonsterInfo();

                Console.WriteLine($" {gameData.curMonster.name}에게 {dmg}의 데미지를 입혔다!");

            } else if (gameData.act == CombatAction.DEFENSE) {

                Console.WriteLine(" 위험에 대비해 엄폐물 뒤로 숨었다.");

            }

            PrintStatus(leftPadding);

            Console.SetCursorPosition(0, 0);

            return dmg;
        }

        /// <summary>
        /// 해당 좌표 이후의 텍스트를 일부 지우고 해당 좌표로 커서 이동
        /// </summary>
        /// <param name="_cmdPos">수행할 좌표 튜플</param>
        static void ClearCmdLine((int, int) _cmdPos) {

            Console.SetCursorPosition(_cmdPos.Item1, _cmdPos.Item2);
            Console.Write(clearCmdStr);
            Console.SetCursorPosition(_cmdPos.Item1, _cmdPos.Item2);

        }

        /// <summary>
        /// 보상 정산 함수
        /// </summary>
        /// <param name="_cmdPos">시스템 메세지가 뜰 콘솔상 좌표 튜플</param>
        static void Rewarding((int, int) _cmdPos) {

            ClearCmdLine(_cmdPos);

            //시스템 메세지
            Console.Write($" {gameData.curMonster.name}을 쓰러뜨렸다!");
            ClearCmdLine(_cmdPos);

            //골드정산
            gameData.gold += gameData.curMonster.gold;
            Console.Write($" {gameData.curMonster.gold} 골드를 얻었다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
            ClearCmdLine(_cmdPos);

            //경험치 정산
            gameData.exp += gameData.curMonster.exp;
            Console.Write($" 경험치를 {gameData.curMonster.exp} 얻었다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
            ClearCmdLine(_cmdPos);

            //레벨업인 경우 처리
            while (gameData.exp >= gameData.needExp[gameData.level - 1]) {
                gameData.exp -= gameData.needExp[gameData.level - 1];
                gameData.level++;

                Console.Write(" 레벨이 올랐다!");

                gameData.attack += GameData.ATK_INCREASE;
                gameData.defense += GameData.DEF_INCREASE;
                gameData.MAX_HP += GameData.MAX_HP_INCREASE;
                gameData.hp = gameData.MAX_HP;

                PrintStatus(leftPadding);
                Waiting_Z_Input();
                ClearCmdLine(_cmdPos);

            }

        }

        #region 몬스터 행동 대응 함수.

        static void MonsterAttack() {
            int dmg;
            Console.Write($"{gameData.curMonster.name}의 공격!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            dmg = gameData.curMonster.attck - gameData.defense;
            if (dmg <= 0) dmg = 1;
            if (gameData.act == CombatAction.DEFENSE) dmg = 1;

            gameData.hp -= dmg;

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{dmg}의 피해를 입었다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterDeffence() {
            Console.Write($"{gameData.curMonster.name}는 몸을 숨긴다!");
            PrintStatus(leftPadding);
            gameData.curMonster.defBuff = 1.5f;
            Waiting_Z_Input();

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{gameData.curMonster.name}의 방어가 순간 오른다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterCharge() {
            Console.Write($"{gameData.curMonster.name}가 힘을 모으고 있다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{gameData.curMonster.name}의 공격력이 순간 오른다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterCritical() {
            int dmg;
            Console.Write($"{gameData.curMonster.name}의 회심의 일격!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            dmg = (int)(gameData.curMonster.attck * 2f) - gameData.defense;
            if (dmg <= 0) dmg = 1;
            if (gameData.act == CombatAction.DEFENSE && dmg > 5) dmg = 5;

            gameData.hp -= dmg;

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{dmg}의 피해를 입었다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterHeal() {
            Console.Write($"{gameData.curMonster.name}가 심호흡을 한다.");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            Console.Clear();

            gameData.curMonster.hp += gameData.curMonster.healPoint;

            if (gameData.curMonster.hp > gameData.curMonster.MAX_HP)
                gameData.curMonster.hp = gameData.curMonster.MAX_HP;


            PrintMonsterInfo();
            Console.Write($"{gameData.curMonster.name}가 체력을 회복했다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterPanic() {
            Console.Write($"{gameData.curMonster.name}은 지쳐서 쉬고있다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterGigaCharge() {
            Console.Write($"{gameData.curMonster.name}가 2턴동안 폭주한다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterDeadlyAttack() {
            int dmg;
            Console.Write($"{gameData.curMonster.name}가 치명적인 공격을 날린다!!!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            dmg = 999999;
            if (gameData.act == CombatAction.DEFENSE) dmg = 7;

            gameData.hp -= dmg;

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{dmg}의 피해를 입었다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }
        static void MonsterHyperAttack() {

            Console.Write($"{gameData.curMonster.name}이 고대의 주문을\n시전하려 한다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();


            if (PlayHyperPattern()) {//패턴 파훼

                Console.Clear();
                PrintMonsterInfo();
                Console.Write($"{gameData.curMonster.name}의 캐스팅을 저지했다!");
                PrintStatus(leftPadding);
                Waiting_Z_Input();

            } else {//패턴 실패

                Console.Clear();
                gameData.curMonster.hp += gameData.curMonster.healPoint;

                if (gameData.curMonster.hp > gameData.curMonster.MAX_HP)
                    gameData.curMonster.hp = gameData.curMonster.MAX_HP;

                PrintMonsterInfo();
                Console.Write($"{gameData.curMonster.name}이 체력을 크게 회복했다!");
                PrintStatus(leftPadding);
                Waiting_Z_Input();

            }

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{gameData.curMonster.name}가 힘을 모으고 있다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();

            Console.Clear();
            PrintMonsterInfo();
            Console.Write($"{gameData.curMonster.name}의 공격력이 순간 오른다!");
            PrintStatus(leftPadding);
            Waiting_Z_Input();
        }


        static bool PlayHyperPattern() {

            //기믹 패턴을 받아옴.
            Monster.HyperArrow[] pattern = new Monster.HyperArrow[gameData.curMonster.curHyperLength];
            int arrowArrLength = gameData.curMonster.hyperPattern.Length;

            for (int i = 0; i < pattern.Length; i++) {

                pattern[i] = gameData.curMonster.hyperPattern[gameData.curMonster.hyperIdx];
                gameData.curMonster.hyperIdx = (gameData.curMonster.hyperIdx + 1) % arrowArrLength;
                
            }

            Console.Clear();
            PrintMonsterInfo();

            //기믹 패턴 출력
            foreach (Monster.HyperArrow _arr in pattern) {

                switch (_arr) {

                    case Monster.HyperArrow.UP:
                        Console.Write($"↑ ");
                        break;

                    case Monster.HyperArrow.DOWN:
                        Console.Write($"↓ ");
                        break;

                    case Monster.HyperArrow.LEFT:
                        Console.Write($"← ");
                        break;

                    case Monster.HyperArrow.RIGHT:
                        Console.Write($"→ ");
                        break;

                }

            }
            Console.WriteLine();

            //패턴 대응 입력 대기
            int correctCnt = 0;
            for (int i = 0; i < pattern.Length; i++) {


                ConsoleKey inputKey = Waiting_Arrow_Input();

                if (CompArrowPattern(inputKey, pattern[i])) {

                    Console.ForegroundColor = ConsoleColor.Green;
                    correctCnt++;

                } else {

                    Console.ForegroundColor = ConsoleColor.Red;

                }

                switch (inputKey) {

                    case ConsoleKey.UpArrow:
                        Console.Write($"↑ ");
                        break;

                    case ConsoleKey.DownArrow:
                        Console.Write($"↓ ");
                        break;

                    case ConsoleKey.LeftArrow:
                        Console.Write($"← ");
                        break;

                    case ConsoleKey.RightArrow:
                        Console.Write($"→ ");
                        break;

                }

                Console.ResetColor();

            }
            
            PrintStatus(leftPadding);

            //패턴 성공여부 판단.
            gameData.curMonster.curHyperLength++;
            if (gameData.curMonster.curHyperLength > gameData.curMonster.hyperMaxCnt)
                gameData.curMonster.curHyperLength = gameData.curMonster.hyperMaxCnt;

            //패턴 성공여부 반환.
            return correctCnt >= pattern.Length;

        }

        /// <summary>
        /// 화살표 입력과 대응패턴 일치 여부를 확인.
        /// </summary>
        /// <param name="_key">입력된 키</param>
        /// <param name="_patternArrow">확인할 대응 기믹 키</param>
        /// <returns>일치 여부</returns>
        static bool CompArrowPattern(ConsoleKey _key, Monster.HyperArrow _patternArrow) {

            if (_key == ConsoleKey.UpArrow && _patternArrow == Monster.HyperArrow.UP) return true;
            if (_key == ConsoleKey.DownArrow && _patternArrow == Monster.HyperArrow.DOWN) return true;
            if (_key == ConsoleKey.LeftArrow && _patternArrow == Monster.HyperArrow.LEFT) return true;
            if (_key == ConsoleKey.RightArrow && _patternArrow == Monster.HyperArrow.RIGHT) return true;

            return false;
        }
        #endregion

        #endregion


        #endregion


        static void Input() {

            ConsoleKey inputKey = Console.ReadKey(true).Key;

            switch (gameData.curScene) {

                case SceneState.MAZE:
                    MazeSceneInput(inputKey);
                    break;

                case SceneState.COMBAT:
                    CombatSceneInput(inputKey);
                    break;

            }


        }

        #region 입력 내부 함수.

        /// <summary>
        /// z가 입력될 때까지 무한 대기
        /// </summary>
        static void Waiting_Z_Input() {

            while (true) {
                if (Console.ReadKey(true).Key == ConsoleKey.Z) break;
            }

        }
        /// <summary>
        /// 화살표 키가 눌릴 때 까지 대기
        /// </summary>
        /// <returns>눌린 방향키 반환</returns>
        static ConsoleKey Waiting_Arrow_Input() {
            
            ConsoleKey key = ConsoleKey.Backspace;

            while (true) {

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow) break;
                if (key == ConsoleKey.DownArrow) break;
                if (key == ConsoleKey.LeftArrow) break;
                if (key == ConsoleKey.RightArrow) break;
            }

            return key;

        }



        #region 상태별 입력 전달 분기 함수


        static void MazeSceneInput(ConsoleKey _input) {

            switch (_input) {

                case ConsoleKey.RightArrow:
                    if (CheckMove(gameData.posX + 1, gameData.posY))
                        gameData.posX++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (CheckMove(gameData.posX - 1, gameData.posY))
                        gameData.posX--;
                    break;
                case ConsoleKey.DownArrow:
                    if (CheckMove(gameData.posX, gameData.posY + 1))
                        gameData.posY++;
                    break;
                case ConsoleKey.UpArrow:
                    if (CheckMove(gameData.posX, gameData.posY - 1))
                        gameData.posY--;
                    break;

            }

        }
        static void CombatSceneInput(ConsoleKey _input) {


            switch (combatState) {

                case CombatState.COMMANDING:
                    InputCommanding(_input);
                    break;

                case CombatState.WATING:
                    break;

            }

            return;

        }

        #endregion



        #region 전투 세부 입력

        static void InputCommanding(ConsoleKey _input) {

            switch (_input) {

                case ConsoleKey.UpArrow:
                    if (gameData.cursorIdx <= 0) {
                        gameData.cursorIdx = 1;
                    } else {
                        gameData.cursorIdx--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    gameData.cursorIdx++;
                    break;

                case ConsoleKey.Z:
                    gameData.cursorIdx %= 2;

                    if (gameData.cursorIdx == 0) {
                        gameData.act = CombatAction.ATTACK;
                    } else if (gameData.cursorIdx == 1) {
                        gameData.act = CombatAction.DEFENSE;
                    }

                    combatState = CombatState.WATING;

                    break;
            }

        }


        #endregion



        #endregion


        #endregion

        static void Main(string[] args) {

            Game game = new Game();
            game.Run();


/*            Start();
            while (true) {

                Render();
                Input();

            }*/

        }
    }
}
