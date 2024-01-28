namespace Aplicación_de_filtro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             Ejercicio: Aplicación Genérica de Filtrado

            Crear una aplicación en C# que permita a los usuarios realizar el 
            filtrado de elementos en una lista utilizando criterios específicos.
            La aplicación debe ser capaz de realizar dos tipos de filtrado:

            Filtrar por valor: Los usuarios pueden ingresar un valor y obtener una lista 
            de elementos que sean iguales a ese valor.

            Filtrar por predicado personalizado: Los usuarios pueden definir un predicado
            personalizado y obtener una lista de elementos que cumplan con ese predicado.

            Requisitos:

            Utiliza una clase para representar una lista de elementos. 
            Esta clase debe permitir agregar elementos y realizar filtrados.

            Utiliza delegados para definir funciones que tomen elementos de la 
            lista y un criterio (valor o predicado) y devuelvan una lista filtrada.

            Utiliza lambdas para definir las funciones de filtrado. Por ejemplo, una 
            función de filtrado por valor podría tomar un elemento y un valor,
            y devolver true si el elemento es igual al valor.

            Permite que el usuario interactúe con el programa ingresando elementos
            en la lista y eligiendo entre las dos opciones de filtrado.

            Muestra los resultados de los filtrados al usuario.
             */



            //Variable 
            int op = 0;

            //Instanciamos una lista de tipo entero
            Elements list = new(5);


            //Mostramos menu
            Elements.DisplayMenu();



            //Se ejecuta hasta que la opcion sea 4

            while (op != 4)
            {
                //Pedimos una opcion
                op = Elements.GetValidOp("Seleccione una opcion: ");
                Console.WriteLine();//salto de linea


                //Filtramos lista segun op
                List<int>? listFiltered = Elements.ExecuteOp(op, list);


                //Mostramos el resultado en caso de que sea 3 o 2 xd
                DisplayListFiltered(listFiltered, op);
            }



            //Nos despedimos
            Console.Clear();
            Console.WriteLine("¡Hasta luego!");


            Console.ReadKey();
        }



        //Metodo para mostrar la lista filtrada
        public static void DisplayListFiltered(List<int> elements, int op)
        {

            //Creamos lista
            List<int>? filtered = elements;

            //Primero verificamos si la lista es nula
            if (filtered == null)
            {
                Console.WriteLine("La lista es nula.");
            }
            //Luego verificamos si la lista está vacía
            else if (filtered.Count == 0)
            {
                Console.WriteLine("No hay números que coincidan con el filtro.");
            }
            //Si la lista no es nula y no está vacía, mostramos su contenido
            else if (op == 2 || op == 3)
            {
                Console.Write("Lista filtrada: ");
                foreach (int i in filtered)
                {
                    Console.Write(i + " ");
                }
            }

            //Salto de linea
            Console.WriteLine();
        }



    }

    //Clase
    class Elements
    {

        //Propiedades
        private List<int> values;
        private int cant;


        //constructor
        public Elements(int cantidad)
        {
            //creamos la lista con la cantidad asignada
            values = new List<int>(cantidad);
            cant = cantidad;
        }


        //Metodos:


        //Mostrar menu
        public static void DisplayMenu()
        {

            Console.WriteLine("Bienvenido a la Aplicación de Filtrado Genérico");
            Console.WriteLine();
            Console.WriteLine("1. Agregar elementos a la lista");
            Console.WriteLine("2. Filtrar por valor");
            Console.WriteLine("3. Filtrar por predicado personalizado");
            Console.WriteLine("4. Salir");
            Console.WriteLine();//salto de linea
        }


        //Mostramos resultado
        public static List<int>? ExecuteOp(int op, Elements list)
        {
            //Creamos lista
            List<int>? listFiltered = list.GetValues();


            //Filtramos lista segun op
            listFiltered = op switch
            {
                1 => list.AddValue(ref listFiltered),
                2 => FilterByValue(listFiltered),
                3 => FilterByPredicate(listFiltered),
                _ => null
            };


            //Retornamos lista
            return listFiltered;
        }


        //Agregar valores
        public List<int> AddValue(ref List<int> list)
        {

            //Variable
            int value;

            //verificamos si la lista esta llena
            if (list.Count >= cant) Console.WriteLine("La lista ya está llena.");

            //verificamos si valor es int y lo agregamos
            else
            {
                value = GetValidValue("Digite un elemento: ");
                list.Add(value);
                Console.WriteLine("Elemento agregado exitosamente");
            }

            //Retornamos lista modificada
            return list;
        }




        //Filtrar por valor
        public static List<int> FilterByValue(List<int> list)
        {

            //Pedimos un valor
            int value = GetValidValue("Digite un valor para filtrar: ");


            //Devolvemos lista asi de y una xd
            return FilterListByValue(list, value);
        }



        //Filtrar por predicado
        public static List<int> FilterByPredicate(List<int> lista)
        {

            //creamos lista
            List<int> filteredInts = new();


            //Pedimos un predicado
            string input = GetValidString("Ingresa un predicado exactamente en este formato (por ejemplo, x => x > y): ");


            //Pedimos un valor para el predicado
            int value = GetValidValue("Ahora digite un valor: ");


            //Validamos el predicado
            Predicate<int> predicateFilter = GetValidPredicate(input, value);


            //Verificamos si el predicado esta bien
            if (predicateFilter != null)
            {

                //recorre con un foreach
                foreach (int n in lista)
                {
                    //si es true lo añade a la lista
                    if (predicateFilter(n))
                    {
                        filteredInts.Add(n);
                    }
                }
            }
            //no es valido
            else Console.WriteLine("Error: el predicado no es valido");


            //devolvemos la lista
            return filteredInts;
        }






        //Metodos auxiliares:


        //GetValues() es un método que obtiene la lista de elementos en tu clase Elements.
        public List<int> GetValues()
        {
            return values;
        }




        //Metodo para calcular predicado
        public static Predicate<int> GetValidPredicate(string input, int value)
        {

            //Creamos un predicado y calculamos cuál es según input
            Predicate<int>? predicate = input switch
            {
                "x => x > y" => x => x > value,
                "x => x < y" => x => x < value,
                "x => x == y" => x => x == value,
                "x => x >= y" => x => x >= value,
                "x => x <= y" => x => x <= value,
                "x => x != y" => x => x != value,
                "x => x % y == 0" => x => x % value == 0,  //Corregido para evaluar si x % y == 0
                "x => x % y != 0" => x => x % value != 0, //Corregido para evaluar si x % y != 0
                _ => null, //Manejo de un caso predeterminado si no se encuentra ninguna coincidencia.
            };



            //Retornamos el predicado
            return predicate;
        }



        //Método para devolver lista filtrada por valor
        public static List<int> FilterListByValue(List<int> numbers, int value)
        {
            //Creamos una lista para almacenar los elementos filtrados
            List<int> filteredElements = new List<int>();

            foreach (int number in numbers)
            {
                //Comparamos cada número en la lista con el valor
                if (number == value)
                {
                    filteredElements.Add(number);
                }
            }

            //Devolvemos la lista filtrada
            return filteredElements;
        }




        //Validar elemento
        public static int GetValidValue(string message)
        {
            //variable a usar
            int n;

            do
            {
                Console.Write(message);

                //se repite hasta que se digite un int
            } while (!int.TryParse(Console.ReadLine(), out n));

            return n;
        }

        //Validar opcion
        public static int GetValidOp(string message)
        {
            //variable a usar
            int opcion;

            do
            {
                Console.Write(message);

                //se repite hasta que se digite un int o sea mayor a 4 o menor que 1
            } while (!int.TryParse(Console.ReadLine(), out opcion) || opcion < 1 || opcion > 4);

            return opcion;
        }


        //Validar string a int
        public static string GetValidString(string message)
        {
            string input;

            do
            {
                Console.Write(message);//Muestra el mensaje para ingresar la cadena.
                input = Console.ReadLine();//Lee la entrada del usuario y la asigna a la variable 'input'.

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Error: La cadena no puede estar en blanco.");
                }
            } while (string.IsNullOrWhiteSpace(input));//Repite el ciclo hasta que se ingrese una cadena válida.

            //Retornamos el string
            return input;
        }
    }
}
