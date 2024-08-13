using ConsoleGame.Scenes;

namespace ConsoleGame {
    public static class PrintSystem {

        const string clearLineStr = "                                    ";

        public static void ClearLine((int, int) _pos) {

            Console.SetCursorPosition(_pos.Item1, _pos.Item2);
            Console.Write(clearLineStr);
            Console.SetCursorPosition(_pos.Item1, _pos.Item2);

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
