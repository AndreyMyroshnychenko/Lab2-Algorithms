using System;

namespace ПА_Лаб._2
{
    class Program
    {
        static void Menu()
        {
            DataBase current = new DataBase("index_area.txt");
            Console.WriteLine("F - find; A - add; D - delete; E - edit");
            string str = Console.ReadLine();
            switch (str)
            {
                case "F":
                    Console.WriteLine("Key: ");
                    int key = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Answer:");
                    Console.WriteLine("Value: " + current.Alert(current.Find(key, false)));
                    break;
                case "A":
                    Console.WriteLine("Value: ");
                    int res_2 = Convert.ToInt32(Console.ReadLine());
                    current.Add(res_2);
                    Console.WriteLine("OK");
                    break;
                case "D":
                    Console.WriteLine("Key: ");
                    int key_3 = Convert.ToInt32(Console.ReadLine());
                    current.Find(key_3, true);
                    break;
                case "E":
                    Console.WriteLine("Key: ");
                    int key_4 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Value: ");
                    int res_4 = Convert.ToInt32(Console.ReadLine());
                    current.Edit(key_4, res_4);
                    break;
                default:
                    break;
            }
            Menu();
        }
        static void Main(string[] args)
        {
            Menu();
        }
    }
}
