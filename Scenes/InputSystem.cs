

namespace ConsoleGame.Scenes {
    public static class InputSystem {

        public static void Waiting_Z_Input() {

            while (true) {
                if (Console.ReadKey(true).Key == ConsoleKey.Z) break;
            }
        }

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

    }
}
