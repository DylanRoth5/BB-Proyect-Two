namespace Parking.Controllers
{
    internal class nLot
    {
        public static void Add () {

        }

        public static void List(){

        }
        
        public static void Erase(){

        }
        
        public static void Modify(int i){
            
        }
        public static int Select(){

        }
        
        public static void Menu(){
            string[] opciones = new string[] { "Add", "Modify", "Erase", "List" };
            int seleccion = Seal.Menu("Autores", opciones);
            switch (seleccion)
            {
                case 1: Crear(); Menu(); break;
                case 2: Modificar(Seleccionar()); Menu(); break;
                case 3:
                    if (Libreria.autores.Count > 0)
                    { Eliminar(); }
                    else
                    {
                        Console.WriteLine("No existen datos a eliminar"); Console.ReadKey(true);
                    }; Menu(); break;
                case 4: ListarCantidadLibros(); Console.ReadKey(); Menu(); break;
                case 0: Libreria.Menu(); break;
            }
        }
    }
}