using System;

namespace database;

/*class Program
{
    public static void Main()
        {
        DB db = new DB();
        db.Connect();
        }
    
}*/

 /*class Program
{
   public static async Task Main()
        {
        DB db = new DB();
        Console.WriteLine("Enter title:");
        string? title = Console.ReadLine();
        Console.WriteLine("Enter text:");
        string? text = Console.ReadLine();
        Console.WriteLine("Enter date:");
        string? date = Console.ReadLine();
        Console.WriteLine("Enter avtor:");
        string? avtor = Console.ReadLine();
        await db.InsertData(title, text, date, avtor);
        }
    
    {
        DB db = new DB();

        await db.GetData(2);
    }
}

class Program
{
   public static async Task Main()
        {
        DB db = new DB();
        Console.WriteLine("Enter title:");
        string? title = Console.ReadLine();
        Console.WriteLine("Enter text:");
        string? text = Console.ReadLine();
        Console.WriteLine("Enter date:");
        string? date = Console.ReadLine();
        Console.WriteLine("Enter avtor:");
        string? avtor = Console.ReadLine();
        await db.UpdateData(title, text, date, avtor);
        }
    
}*/


class Program
{
   public static async Task Main()
        {
        DB db = new DB();
        await db.CountData();
        }
    
}
