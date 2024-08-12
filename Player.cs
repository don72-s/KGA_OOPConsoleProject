using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.userData {
    public class Player {

        int MAX_HP = 15;

        public int curMazeLevel { get; private set; }

        int hp;
        int atk;
        int def;

        int level;
        int exp;
        int[] needExp;
        int gold;

        public Player() { 
        
            curMazeLevel = 0;

            hp = MAX_HP;
            atk = 5;
            def = 10;

            level = 1;
            exp = 0;
            needExp = [8, 30, 150, 500, 9999];
            gold = 10;

        }



        public void AddAtk(int _amount) {
            atk += _amount;
        }

        public void AddDef(int _amount) {
            def += _amount;
        }

        public void AddMaxHP(int _amount) {
            MAX_HP = MAX_HP + _amount;
            hp = MAX_HP;
        }


        public void RaiseMazeLevel() {
            curMazeLevel++;
        }

        public void UseGold(int _amount) {

            if (gold - _amount < 0) {
                Console.WriteLine("골드가 -가 됩니다.");
                return;
            }

            gold -= _amount;

        }
        public int GetGold() {
            return gold;
        }

        public void PrintStatus(int leftPos) {
            int yCnter = 0;

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($" =======상태=======");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  Level : {level}");
            if (level >= 5) Console.Write(" [MAX] ");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  hp    : {hp} / {MAX_HP}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  atk   : {atk}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  def   : {def}");

            Console.SetCursorPosition(leftPos, yCnter++);
            if (level >= 5)
                Console.Write($"  exp   : --- / ---");
            else
                Console.Write($"  exp   : {exp} / {needExp[level - 1]}");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($"  골드  : {gold} G");

            Console.SetCursorPosition(leftPos, yCnter++);
            Console.Write($" ==================");
        }


    }
}
