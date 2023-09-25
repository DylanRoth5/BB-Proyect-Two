namespace Parking;

    public class Tools
    {
        
        public static DateTime InputDate()
        {
            DateTime result;
            bool isValid = false;

            do
            {
                Console.WriteLine("Por favor, ingrese una fecha y hora en el formato dd/MM/yyyy HH:mm:ss: ");
                string input = Console.ReadLine();

                if (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out result))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Formato incorrecto, intente de nuevo...");
                }
            }
            while (!isValid);

            return result;
        }
        public static char ValidateLetter(){
            //Convert the letter to ASCII
            char letter = ' ';
            ConsoleKeyInfo consoleKeyInfo;
            do {
                consoleKeyInfo = Console.ReadKey(intercept: true);
                int ASCII = Convert.ToInt32(consoleKeyInfo.KeyChar);
                if((ASCII >= 97 && ASCII <= 122)||(ASCII >= 65 && ASCII<=90)){
                    letter = consoleKeyInfo.KeyChar;
                }
            }while(letter == ' ');
            if(letter != ' '){
                return char.ToLower(letter);
            }
            //If it is not a letter, it recursively executes it again until a letter is selected
            return ValidateLetter();
        }
        public static int ValidateInt()
        {
            string text = "";
            ConsoleKeyInfo consoleKeyInfo;
            do
            {
                consoleKeyInfo = Console.ReadKey(intercept: true);
                if (consoleKeyInfo.KeyChar > '/' && consoleKeyInfo.KeyChar < ':')
                {
                    Console.Write(consoleKeyInfo.KeyChar);
                    text += consoleKeyInfo.KeyChar;
                }

                if (consoleKeyInfo.Key == ConsoleKey.Backspace && text.Length > 0)
                {
                    Console.CursorLeft--;
                    Console.Write(" ");
                    Console.CursorLeft--;
                    text = text.Substring(0, text.Length - 1);
                }
            }
            while (consoleKeyInfo.Key != ConsoleKey.Enter || text.Length == 0);
            if (text.Length > 0)
            {
                return int.Parse(text);
            }

            return ValidateInt();
        }
        public static void ClearLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, currentLineCursor);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        public static int ValidateInt(int min, int max)
        {
            string number = "";
            ConsoleKeyInfo KeyInfo;
            do
            {
                KeyInfo = Console.ReadKey(intercept: true);
                if (KeyInfo.KeyChar >= '0' && KeyInfo.KeyChar <= '9')
                {
                    Console.Write(KeyInfo.KeyChar);
                    number += KeyInfo.KeyChar;
                }

                if (KeyInfo.Key == ConsoleKey.Backspace && number.Length > 0)
                {
                    Console.CursorLeft--;
                    Console.Write(" ");
                    Console.CursorLeft--;
                    number = number.Substring(0, number.Length - 1);
                }
            }
            while (KeyInfo.Key != ConsoleKey.Enter || number.Length == 0);

            if (number.Length > 0 && int.Parse(number) >= min &&  int.Parse(number) <= max)
            {
                return int.Parse(number);
            }
            ClearLine();
            Console.Write($"Type a number between {min} and {max}: ");
            return ValidateInt(min, max);
        }
        public static void PrintAt(int x, int y, string? word)
        {
            int nx = Console.CursorLeft;
            int ny = Console.CursorTop;
            Console.SetCursorPosition(x, y);
            Console.Write(word);
            Console.SetCursorPosition(nx, ny);
        }
        public  static void DrawTable(string[,] matrix)
        {
            int CalculateColumnWidth(int col)
            {
                // Given the index "col" of a column from the matrix, returns the min widht that the column
                // must have in order that the longest word can be written inside it with one space before
                // and one after it.  
                int width = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    width = (matrix[i, col].Length > width) ? matrix[i, col].Length + 2 : width;
                }
                return width;
            }
            int CalculateTotalWidth()
            {
                int totalWidth = 0;
                for (int i = 0; i < matrix.GetLength(1); i++)
                {
                    totalWidth += CalculateColumnWidth(i) + 2;
                }
                return totalWidth;
            }
            char topLeft;
            char topRight;
            char bottomLeft = '╚';
            char bottomRight = '╝';
            int x = Console.CursorLeft; int y = Console.CursorTop;
            int aux = x;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Stores the value of x at the beginnig of each column
                x = aux;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int width = CalculateColumnWidth(j) + 2;
                    // conditions
                    // if it's the fist row...
                    if (i == 0)
                    {
                        // first cell, that is J == 0, uses '╔' as topLeft the rest of them, the ones
                        // that builds the roof, need to be '╦'
                        topLeft = (j == 0) ? '╔' : '╦';
                        topRight = '╗';
                        // the top right corner in the first row     
                        // in case that it to be the left wall column, that is j = 0, the bottomLeft
                        // char will be a corner, else, it will be and an up and horizontal intersection.
                        // this needs to be written here for the case that the table has only one row
                        bottomLeft = (j == 0) ? '╚' : '╩';
                    }
                    // if it's the last row...
                    else if (i == matrix.GetLength(0) - 1)
                    {
                        // prints a ╠ if in the first column and a ╬ for the rest of them
                        topLeft = (j == 0) ? '╠' : '╬';
                        // always prints ╣ at the top right corner of each cell, if it's not the last
                        // one, it'll just be over-written by the top left corner ()from the one, 
                        // it'll just be over-written by the top left corner from the next cell 
                        topRight = '╣';
                        // in the first column's left end a corner gonna be printed, but in the other
                        // cases (if table has at least two columns) an up and horizontal intersection
                        bottomLeft = (j == 0) ? '╚' : '╩';
                        // assigning the ╝ simbol to the bottom right value here ensures that if the 
                        // has only one column, a coner will properly be written, if it's not the last
                        // column, the symbol'll just be over-written by the bottomLeft char from the
                        // following cell
                        bottomRight = '╝';
                    }
                    // if they are internal rows... 
                    else
                    {
                        // In case of being the first column, the a '╠' should be printed given that 
                        // this are internal rows but in an external column, if they are internal 
                        // columns as well, a '╬' will printed
                        topLeft = (j == 0) ? '╠' : '╬';
                        // Same as in the previous code's block...
                        topRight = '╣';
                    }
                    // Creates an string contatenating the chars to be used as corners so they
                    // can be passed to the Draw.rect()'s corners value
                    string corners = string.Concat(topLeft, topRight, bottomLeft, bottomRight);
                    // Prints the cell corresponding to the matrix[i, j] word
                    rect(x, y, width, 2, '═', '║', corners);
                    // If the first row is the title's row, so if i = 0 the word is going to be written
                    // in red to make it clear that it's a title, else it'll be just printed in white
                    Console.ForegroundColor = (i == 0) ? ConsoleColor.Red : ConsoleColor.White;
                    Tools.PrintAt(x + 1, y + 1, $" {matrix[i, j]}");
                    // Resets the ForegroungColour to white
                    //Console.ForegroundColor = ConsoleColor.White;
                    Console.ResetColor();
                    // increments the x coordinate with the widht value so the next column starts where
                    // this one ends 
                    x += width;
                }
                // increments the y coordinate one space for the floor of the cell and one for the 
                // beginning of the next row's words
                y += 2;
            }
            // Leaves one line after the table
            Console.WriteLine("\n\n");
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