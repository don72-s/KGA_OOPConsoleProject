using ConsoleGame.userData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Scenes {
    internal class ShopScene : Scene{

        enum Selection { ATTACK, DEFFENSE, HEALTH, TO_TOWN, SELECT_COUNT };

        const int ATTACK_PRICE = 500;
        const int DEFFENCE_PRICE = 500;
        const int MAX_HP_PRICE = 30;

        private const int LEFT_PADDING = 46;

        int cursorIdx;
        string output;

        bool[] isItemSold;
        int[] itemPrice;

        bool notEnoughGoldFlag;
        bool buyFlag;
        bool aleadyBuyFlag;

        Player player;

        public ShopScene(ISceneChangeable _game) : base(_game) {

            cursorIdx = 0;

            notEnoughGoldFlag = false;
            buyFlag = false;
            aleadyBuyFlag = false;

            isItemSold = new bool[(int)Selection.SELECT_COUNT - 1];
            itemPrice = [ ATTACK_PRICE, DEFFENCE_PRICE, MAX_HP_PRICE ];

            player = Player.GetInstance();

            StringBuilder sb = new StringBuilder();

            output =
                sb
                .Append("============================================\n")
                .Append("                  사앙점                    \n")
                .Append(" 공격력 강화  |  방어력 강화  |  체력 강화   \n")
                .Append("  [600골드]       [600골드]      [30골드]    \n")
                .Append("============================================\n")
                .Append("상점이다 여러가지를 파는것 같다.\n")
                .Append("\n")
                .Append("  공격력을 강화한다.\n")
                .Append("  방어력을 강화한다.\n")
                .Append("  체력을 강화한다.\n")
                .Append("  상점을 빠져나간다.")
                .ToString();

        }

        public override void Enter() {

            cursorIdx = 0;

        }

        public override void Exit() {

        }

        public override void Input() {

            switch (Console.ReadKey(true).Key) {

                case ConsoleKey.UpArrow:
                    if (cursorIdx <= 0) {
                        cursorIdx = (int)Selection.SELECT_COUNT - 1;
                    } else {
                        cursorIdx--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    cursorIdx++;
                    break;

                case ConsoleKey.Z:
                    ShopZInput();
                    break;

            }

        }

        public override void Print() {

            Console.Clear();
            Console.WriteLine(output);
            PrintComment();
            Console.SetCursorPosition(0, 7 + cursorIdx % (int)Selection.SELECT_COUNT);
            Console.Write("▶");

            player.PrintStatus(LEFT_PADDING);
        }

        public override void Update() {

        }



        private void ShopZInput() {

            cursorIdx %= (int)Selection.SELECT_COUNT;

            switch ((Selection)cursorIdx) {

                case Selection.ATTACK:

                    if (BuyChecking(cursorIdx)) {
                        isItemSold[cursorIdx] = true;
                        player.AddAtk(5);
                    }

                    break;

                case Selection.DEFFENSE:

                    if (BuyChecking(cursorIdx)) {
                        isItemSold[cursorIdx] = true;
                        player.AddDef(5);
                    }

                    break;

                case Selection.HEALTH:

                    if (BuyChecking(cursorIdx)) { 
                        isItemSold[cursorIdx] = true;
                        player.AddMaxHP(3);
                    }

                    break;

                case Selection.TO_TOWN:
                    scene.ChangeScene(SceneType.TOWN);
                    break;

                default:
                    break;

            }

        }


        private bool BuyChecking(int _priceNum) {

            if (isItemSold[_priceNum]) {
                aleadyBuyFlag = true;
            } else {

                if (player.GetGold() < itemPrice[_priceNum]) {
                    notEnoughGoldFlag = true;
                } else {
                    isItemSold[_priceNum] = true;
                    buyFlag = true;
                    player.UseGold(itemPrice[_priceNum]);
                    return true;
                }

            }

            return false;
        }

        private void PrintComment() {

            if (aleadyBuyFlag) {
                Console.SetCursorPosition(0, 12);
                Console.Write("  이미 구매했습니다!");
                Console.SetCursorPosition(0, 0);

                aleadyBuyFlag = false;

            } else if (notEnoughGoldFlag) {
                Console.SetCursorPosition(0, 12);
                Console.Write("  골드가 부족합니다!");
                Console.SetCursorPosition(0, 0);

                notEnoughGoldFlag = false;

            } else if (buyFlag) {
                Console.SetCursorPosition(0, 12);
                Console.Write("  구매했습니다!");
                Console.SetCursorPosition(0, 0);

                buyFlag = false;

            }
        }

    }
}
