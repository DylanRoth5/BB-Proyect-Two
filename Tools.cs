namespace Parking;

    public class Tools
    {
        public static string ReadLine(string word){
            Console.Write(word);
            return Console.ReadLine();
        }
        public static void point(int x, int y, char symbol){
            Console.SetCursorPosition(x, y);
            Console.Write("" + symbol); 
        }
        //This function draws a line, receives the x and y parameters for the position. Also the parameters of length, orientation and symbol to print
        public static void line(int x, int y, int lenght, bool horizontal, char symbol){
            Console.SetCursorPosition(x,y);
            //If the orientation is horizontal, the "horizontal" parameter will be true and the line will be drawn
            if (horizontal) { 
                for (int i = 0; i < lenght; i++){
                    Console.Write("" + symbol); 
                }
            }
            //If the orientation is vertical, said parameter would be false, therefore the following code would be executed
            if (!horizontal) {
                for (int i = 0; i < lenght; i++){
                    Console.Write("" + symbol);
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop + 1);
                }
            }
        }
        //This function draws a rectangle, for this it receives the position in x and y as a parameter. In addition, it also receives the height, width, and characters that will be printed on the horizontal, vertical, and corner sides
        public static void rect(int x, int y, int width, int height, char horizontal, char vertical, string corners){
            line(x+1, y, width-1, true, horizontal);
            line(x+1, y+height, width-1, true, horizontal);
            line(x, y+1, height-1, false, vertical);
            line(x+width, y+1, height-1, false, vertical);
            point(x, y, corners[0]);
            point(x+width, y, corners[1]);
            point(x, y+height, corners[2]);
            point(x+width, y + height, corners[3]);
        }
        public static void Flip(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
        public static int Menu(string title, string[] options)
        {
            Console.Clear();
            // settings for the menu
            options = options.Append("Exit").ToArray();
            ConsoleColor background = ConsoleColor.Black;
            ConsoleColor foreground = ConsoleColor.White;
            Console.CursorVisible = false;
            bool running = true;
            int menuWidth = 0;
            int result = 1;
            Flip(background, foreground);
            // we have to assign the menu a width of at least the width of the lasgest word in the options
            for (int i = 0; i < options.Length; i++){ 
                if (options[i].Length >= menuWidth) { menuWidth = options[i].Length; } 
            } 
            if (menuWidth%2 != 0) { menuWidth++; } // The width of the menu must be a pair number for it to be centered correctly
            menuWidth += 20; // this is some x axis padding
            while (running){
                int X = 0,Y = 0; // reseting position x and y
                rect(X, Y, menuWidth, 2,                  '─', '│', "┌┐├┤");
                rect(X, Y+2, menuWidth, options.Length+1, '─', '│', "├┤└┘");
                X=(menuWidth/2)-(title.Length/2); 
                Y++;
                Console.SetCursorPosition(X, Y);
                for (int i = 0; i < title.Length; i++){ Console.Write(""+title[i]); X++; Console.CursorLeft=X; } // printing title
                Y++;
                Console.SetCursorPosition(X, Y);
                for (int i = 0;i < options.Length; i++){ // printing options
                    Y++; 
                    X = (menuWidth / 2) - (options[i].Length / 2); 
                    Console.SetCursorPosition(X, Y);
                    if (result == i+1) {  // if option is selected it must be noticeable
                        Flip(foreground, background); 
                        for (int j = 0; j < options[i].Length; j++){
                            Console.Write("" + options[i][j]); 
                            X++; 
                            Console.CursorLeft=X;
                        }
                        Flip(background, foreground);
                    } else{
                        for (int j = 0; j < options[i].Length; j++){
                            Console.Write("" + options[i][j]); 
                            X++; 
                            Console.CursorLeft=X;
                        }
                    }
                }
                Console.SetCursorPosition(0, 0);
                ConsoleKeyInfo k = Console.ReadKey();
                if (k.Key == ConsoleKey.DownArrow) { result++; }
                if (k.Key == ConsoleKey.UpArrow) { result--; }
                if (k.Key == ConsoleKey.Enter) { running = false; }
                // security check
                if (result < 1) { result = options.Length; }
                if (result > options.Length) { result = 1; }
            }
            if (result == options.Length) { result = 0; } // if you selected the last one throw a 0
            Flip(ConsoleColor.Black, ConsoleColor.White); // reset console colors
            Console.Clear();
            return result; 
        }
    }