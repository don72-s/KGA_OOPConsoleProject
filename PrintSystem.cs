using ConsoleGame.Scenes;
using System.Drawing;

namespace ConsoleGame {
    public static class PrintSystem {

        const string clearLineStr = "                                    ";
        const char clearChar = ' ';

        public static void ClearAt((int, int) _pos) {

            Console.BackgroundColor = ConsoleColor.Black;
            WriteAt(_pos, clearChar);

        }
        public static void ClearAt(int _posX, int _posY) {

            ClearAt(new(_posX, _posY));

        }

        public static void WriteAt((int, int) _pos, char _char) {

            (int, int) backPos = Console.GetCursorPosition();

            Console.SetCursorPosition(_pos.Item1, _pos.Item2);
            Console.Write(_char);
            Console.SetCursorPosition(backPos.Item1, backPos.Item2);

        }
        public static void WriteAt(int _posX, int _posY, char _char) {

            WriteAt(new(_posX, _posY), _char);

        }


        public static void WriteAt((int, int) _pos, string _str) {

            (int, int) backPos = Console.GetCursorPosition();

            Console.SetCursorPosition(_pos.Item1, _pos.Item2);
            Console.Write(_str);
            Console.SetCursorPosition(backPos.Item1, backPos.Item2);

        }
        public static void WriteAt(int _posX, int _posY, string _str) {

            WriteAt(new(_posX, _posY), _str);

        }

        public static void PrintLine((int, int) _pos, string _text) {

            ClearLine(_pos);
            Console.WriteLine(_text);

        }

        public static void PrintLineAndWaitZInput((int, int) _pos, string _text) { 
            
            PrintLine(_pos, _text);
            InputSystem.Waiting_Z_Input();

        }

    }
}
