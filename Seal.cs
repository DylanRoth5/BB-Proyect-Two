namespace Parking
{
    public class Seal
    {
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
        public static int GetX() {
            int x = Console.CursorLeft;
            return x;
        }
        public static int GetY() {
            int y = Console.CursorTop;
            return y;
        }
        public static void SayLine(string? word="") => Console.WriteLine("" + word);
        public static void Spot(int x, int y) => Console.SetCursorPosition(x, y);
        public static void SpotX(int x) => Console.CursorLeft = x;
        public static void SpotY(int y) => Console.CursorTop = y;
        public static void Say(string? word = "") => Console.Write("" + word);
        public static string Flick() => Console.ReadLine();
        public static void Clear() => Console.Clear();
        public static ConsoleKeyInfo Catch() => Console.ReadKey(intercept: true); 
        public static void SayAt(int x,int y, string? word)
        {
            Spot(x, y);
            Say(word);
        }
        public static void SayAt(int x,int y, string? word, bool comeback)
        {
            int nx = GetX();
            int ny = GetY();
            Spot(x, y);
            Say(word);
            if (comeback){
                Spot(nx, ny);
            }
        }
        public static string Flick(string? word = "")
        {
            if (word.Length > 0) { SayLine(word); }
            return Flick();
        }
        public static int Count() => int.Parse(Flick());
        public static bool Judge()
        {
            string answer = Flick();
            if (answer == "yes"||answer == "si"){
                return true;
            } else {
                return false;
            }
        }

        public static bool Judge(string? word = "")
        {
            if (word.Length > 0) { SayLine(word); }
            return Judge();
        }
        public static int Count(string? word = "")
        {
            if (word.Length > 0) { SayLine(word); }
            return Count();
        }
        public static void CatchClear()
        {
            ConsoleKeyInfo keyInfo = Catch();
            Clear();
        }
        public static void Flip(ConsoleColor background, ConsoleColor foreground)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
        }
        public static ConsoleColor[] sealPulse = { //this is the color pattern for the menu 
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.Blue,	
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.DarkCyan,		
                                                ConsoleColor.Cyan,
                                                ConsoleColor.Cyan,
                                                ConsoleColor.Cyan,
                                                ConsoleColor.Cyan,		
                                                ConsoleColor.Cyan,	
                                                ConsoleColor.DarkCyan,	
                                                ConsoleColor.DarkCyan,		
                                                ConsoleColor.DarkCyan,
                                                ConsoleColor.DarkCyan,
                                                };

        public static ConsoleColor BlueSeal = ConsoleColor.Blue;
        public static ConsoleColor DarkCyanSeal = ConsoleColor.DarkCyan;
        public static ConsoleColor CyanSeal = ConsoleColor.Cyan;
        public static ConsoleColor BlackSeal = ConsoleColor.Black;
        public static ConsoleColor WhiteSeal = ConsoleColor.White;
        public static ConsoleColor DarkBlueSeal = ConsoleColor.DarkBlue;
        public static ConsoleColor GraySeal = ConsoleColor.Gray;
        public static ConsoleColor RedSeal = ConsoleColor.Red;
        public static ConsoleColor GreenSeal = ConsoleColor.Green;
        public static ConsoleColor MagentaSeal = ConsoleColor.Magenta;
        public static ConsoleColor YellowSeal = ConsoleColor.Yellow;
        public static ConsoleColor DarkGraySeal = ConsoleColor.DarkGray;
        public static ConsoleColor DarkGreenSeal = ConsoleColor.DarkGreen;
        public static ConsoleColor DarkMagentaSeal = ConsoleColor.DarkMagenta;
        public static ConsoleColor DarkRedSeal = ConsoleColor.DarkRed;
        public static ConsoleColor DarkYellowSeal = ConsoleColor.DarkYellow;

        public static int Menuv2(string[] content)
        {
            Clear();
            content = content.Append("Exit").ToArray();
            bool running = true;
            int result=1;
            int color=0;
            ConsoleColor background = BlackSeal;
            while (running){
                Spot(0,0);
                ConsoleColor foreground = sealPulse[color];
                DrawMenu(content,content.Length,foreground,background,0,result);
                if (Console.KeyAvailable){
                    ConsoleKeyInfo k = Catch(); 
                    if (k.Key == ConsoleKey.DownArrow) { result++; }
                    if (k.Key == ConsoleKey.UpArrow) { result--; }
                    if (k.Key == ConsoleKey.Enter) { running = false; }
                }else{
                    Thread.Sleep(100); 
                    color++;
                }
                if (result < 1) { result = content.Length-1; }
                if (result >= content.Length) { result = 1; }
                if (color >= sealPulse.Length) { color=0; }
                SayAt(25,0,$"posicion: {result}");
            }
            if (result == content.Length-1) { result = 0; }
            return result;
        }

        public static void DrawMenu(string[] content,int contentLength,ConsoleColor foreground, ConsoleColor background,int X,int Y){
            string[,] matrix = new string[contentLength,1];
            for (int i = 0; i < matrix.GetLength(0); i++){
                matrix[i,0]= content[i];
            }
            char topLeft;
            char topRight;
            char bottomLeft = '╚';
            char bottomRight = '╝';
            int x = GetX(); int y = GetY();
            int aux = x;
            for (int i = 0; i < matrix.GetLength(0); i++){
                x = aux;
                for (int j = 0; j < matrix.GetLength(1); j++){
                    Flip(background,foreground);
                    int width = CalculateColumnWidth(j,matrix) + 2;
                    if (i == 0){
                        topLeft = (j == 0) ? '╔' : '╦';
                        topRight = '╗';
                        bottomLeft = (j == 0) ? '╚' : '╩';
                    }
                    else if (i == matrix.GetLength(0) - 1){
                        topLeft = (j == 0) ? '╠' : '╬';
                        topRight = '╣';
                        bottomLeft = (j == 0) ? '╚' : '╩';
                        bottomRight = '╝';
                    }
                    else{
                        topLeft = (j == 0) ? '╠' : '╬';
                        topRight = '╣';
                    }
                    string corners = string.Concat(topLeft, topRight, bottomLeft, bottomRight);
                    rect(x, y, width, 2, '═', '║', corners);
                    if (Y==i){Flip(foreground,background);}
                    SayAt( x + 1, y + 1,$" {matrix[i, j]}");
                    x += width;
                }
                y += 2;
            }
            Flip(background,foreground);
        }
        private static int CalculateColumnWidth(int col,string[,] matrix){
            int width = 0;
            for (int i = 0; i < matrix.GetLength(0); i++){
                width = (matrix[i, col].Length > width) ? matrix[i, col].Length + 2 : width;
            }
            return width;
        }
        
        public static int Menu(string title, string[] options)
        {
            Clear();
            // settings for the menu
            options = options.Append("Exit").ToArray();
            ConsoleColor background = BlackSeal;
            ConsoleColor foreground = WhiteSeal;
            Console.CursorVisible = false;
            bool running = true;
            int menuWidth = 0;
            int result = 1;
            int color=0;
            int apearence=0;
            string word = "(a) Appearence";
            Flip(background, foreground);
            // we have to assign the menu a width of at least the width of the lasgest word in the options
            for (int i = 0; i < options.Length; i++){ 
                if (options[i].Length >= menuWidth) { menuWidth = options[i].Length; } 
            } 
            if (menuWidth%2 != 0) { menuWidth++; } // The width of the menu must be a pair number for it to be centered correctly
            menuWidth += 20; // this is some x axis padding
            while (running){
                foreground = sealPulse[color]; // this will change the color of the menu with each iteration 
                int X = 0,Y = 0; // reseting position x and y
                if (apearence==1){ // choosing apearence
                    rect(X, Y, menuWidth, 2,                  '═', '║', "╔╗╠╣");
                    rect(X, Y+2, menuWidth, options.Length+1, '═', '║', "╠╣╠╣");
                    rect(X, Y+3+options.Length, menuWidth, 2, '═', '║', "╠╣╚╝");
                }else if (apearence==0){
                    rect(X, Y, menuWidth, 2,                  '─', '│', "┌┐├┤");
                    rect(X, Y+2, menuWidth, options.Length+1, '─', '│', "├┤├┤");
                    rect(X, Y+3+options.Length, menuWidth, 2, '─', '│', "├┤└┘");
                }else if (apearence==2){
                    rect(X, Y, menuWidth, 2,                  '█', '█', "████");
                    rect(X, Y+2, menuWidth, options.Length+1, '█', '█', "████");
                    rect(X, Y+3+options.Length, menuWidth, 2, '█', '█', "████");
                }
                SayAt(X=(menuWidth/2)-(word.Length/2), Y+4+options.Length, word); // printing menu's help
                X=(menuWidth/2)-(title.Length/2); 
                Y++;
                Spot(X, Y);
                for (int i = 0; i < title.Length; i++){ Say(""+title[i]); X++; SpotX(X); } // printing title
                Y++;
                Spot(X, Y);
                for (int i = 0;i < options.Length; i++){ // printing options
                    Y++; 
                    X = (menuWidth / 2) - (options[i].Length / 2); 
                    Spot(X, Y);
                    if (result == i+1) {  // if option is selected it must be noticeable
                        Flip(foreground, background); 
                        for (int j = 0; j < options[i].Length; j++){
                            Say("" + options[i][j]); 
                            X++; 
                            SpotX(X);
                        }
                        Flip(background, foreground);
                    } else{
                        for (int j = 0; j < options[i].Length; j++){
                            Say("" + options[i][j]); 
                            X++; 
                            SpotX(X);
                        }
                    }
                }
                Spot(0, 0);
                if (Console.KeyAvailable){ // if a kay has been presed do this ...
                    ConsoleKeyInfo k = Catch(); // read it and check the following ...
                    if (k.Key == ConsoleKey.DownArrow) { result++; }
                    if (k.Key == ConsoleKey.UpArrow) { result--; }
                    if (k.Key == ConsoleKey.Enter) { running = false; }
                    if (k.Key == ConsoleKey.A) { apearence++; }
                }else{
                    Thread.Sleep(100); //this is the fps for che color change
                    color++;
                }
                // security check
                if (result < 1) { result = options.Length; }
                if (result > options.Length) { result = 1; }
                if (color >= sealPulse.Length) { color=0; }
                if (apearence >= 3) { apearence = 0; }
            }
            if (result == options.Length) { result = 0; } // if you selected the last one throw a 0
            Flip(ConsoleColor.Black, ConsoleColor.White); // reset console colors
            Clear();
            return result; 
        }
        public static void point(int x, int y, char symbol){
            Spot(x, y);
            Say("" + symbol); 
        }
        //This function draws a line, receives the x and y parameters for the position. Also the parameters of length, orientation and symbol to print
        public static void line(int x, int y, int lenght, bool horizontal, char symbol){
            Spot(x,y);
            //If the orientation is horizontal, the "horizontal" parameter will be true and the line will be drawn
            if (horizontal) { 
                for (int i = 0; i < lenght; i++){
                    Say("" + symbol); 
                }
            }
            //If the orientation is vertical, said parameter would be false, therefore the following code would be executed
            if (!horizontal) {
                for (int i = 0; i < lenght; i++){
                    Say("" + symbol);
                    Spot(GetX() - 1, GetY() + 1);
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
        //This method draws a grid of cells at a specific location in x and y
        public static void cell(int x, int y,int width, int height, int rows,int columns){
            for (int i = 0;i<columns;i++){
                for (int j = 0;j<rows;j++){
                    rect(x+(i*width), y+(j*height), width, height,'█', '█', "████");
                }
            }
        }
        //The following methods allow us to move on the console in four directions: up, down, right and left
        public static void up(int steps, char? type, char? start, char? end){
            if (start.HasValue){
                Say(""+start);
                steps--;
                Spot(GetX() - 1, GetY() - 1);
            }
            if (end.HasValue) { steps--; }
            // Loop to print the specified character (type) and adjust the console position up
            for (int i = 0; i < steps; i++){
                Say("" + type);
                Spot(GetX()-1, GetY()-1);
            }
            if (end.HasValue) { Say(""+end); }
        }
        public static void down(int steps, char? type, char? start, char? end){
            if (start.HasValue){
                Say("" + start);
                steps--;
                Spot(GetX() - 1, GetY() + 1);
            }
            if (end.HasValue) { steps--; }
             // Loop to print the specified character (type) and adjust the console position down
            for (int i = 0; i < steps; i++){
                Say("" + type);
                Spot(GetX()  - 1, GetY() + 1);
            }
            if (end.HasValue) { Say("" + end); }
        }
        public static void right(int steps, char? type, char? start, char? end){
            if (start.HasValue) { Say("" + start); steps--; }
            if (end.HasValue) { steps--; }
            for (int i = 0; i < steps; i++) { Say("" + type); }
            if (end.HasValue) { Say("" + end); }
        }
        public static void left(int steps, char? type, char? start, char? end){
            if (start.HasValue) { Console.CursorLeft -= 2; Say("" + start); steps--; }
            if (end.HasValue) { steps--; }
            for (int i = 0; i < steps; i++) { Console.CursorLeft -= 2; Say("" + type); }
            if (end.HasValue) { Say("" + end); }
        }
    }
}