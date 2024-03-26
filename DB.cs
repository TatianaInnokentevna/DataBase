using System;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;

namespace database;

class DB
{
    private const string HOST = "localhost";
    private const string PORT = "3306";
    private const string DATABASE = "itproger";
    private const string USER = "root";
    private const string PASS = "root";

  
   private string connect;

   // (2 метод) private MySqlConnection conn;
    public DB() 
  
   {
    /*string connect*/ connect = $"Server={HOST};User={USER};Password={PASS};Database={DATABASE};Port={PORT};";
    // (2метод) conn = new MySqlConnection(connect);
   }

// 2 метод 19:20
   /*public async Task Connect()
   {
    try 
    {
      await conn.OpenAsync(); 
      Console.WriteLine("Open connection"); 
    }
    catch (MySqlException ex)
    {
      Console.WriteLine(ex.Message);
    }
    finally 
    {
        if (conn.State == ConnectionState.Open) 
        {
         await conn.CloseAsync();
         Console.WriteLine("Close connection");
        }
    }
   }*/
//1 метод 13:24
   /*public void Connect() {
    Console.WriteLine("Open connection");
    conn.Open();
    conn.Close();
    Console.WriteLine("Close connection");
   }*/


// 3 метод сокращение второго и закоментирую conn и удаляю подклчение к Database из connect
public async Task CreateTable()
{   
string sql = "CREATE TABLE IF NOT EXISTS users(" + 
        "id INT(11) AUTO_INCREMENT PRIMARY KEY," +
         "login VARCHAR(50)," +
         "password VARCHAR(50)" +
         ") ENGINE=MYISAM"; 
await ExecuteQuery(sql);
}

// 13мин ставим ? потому что значение может принимать ноль)
public async Task InsertData(string? title, string? text, string? date, string? avtor)
{
       // в SQL языке можно писать строку в одинарных ковычках что бы избежать ошибок
    string sql = "INSERT INTO articles (title, text, date, avtor) VALUES (@title, @text, @date, @avtor)"; 
   
   Dictionary<string, string?> parameters = new Dictionary<string, string?>();
   parameters.Add("title", title);
   parameters.Add("text", text);
   parameters.Add("date", date);
   parameters.Add("avtor", avtor);
   await ExecuteQuery(sql, parameters);


   /* using (MySqlConnection conn = new MySqlConnection(connect))
    {
    await conn.OpenAsync();

    MySqlCommand command = new MySqlCommand(sql, conn);
    MySqlParameter param1 = new MySqlParameter("@title", title);
    command.Parameters.Add(param1);
    MySqlParameter param2 = new MySqlParameter("@text", text);
    command.Parameters.Add(param2);
    MySqlParameter param3 = new MySqlParameter("@date", date);
    command.Parameters.Add(param3);
    MySqlParameter param4 = new MySqlParameter("@avtor", avtor);
    command.Parameters.Add(param4);
    await command.ExecuteNonQueryAsync();
    }*/
}

private async Task ExecuteQuery(string sql, Dictionary<string, string?>? parameters = null)
{
   using (MySqlConnection conn = new MySqlConnection(connect))
    {
    await conn.OpenAsync();

    MySqlCommand command = new MySqlCommand(sql, conn);
    if(parameters !=null) {
      foreach(var el in parameters) {
        MySqlParameter param = new MySqlParameter($"@{el.Key}", el.Value);
        command.Parameters.Add(param);
      }
    }
    await command.ExecuteNonQueryAsync();
    }
}

public async Task GetData(int number) 
{
   using (MySqlConnection conn = new MySqlConnection(connect))
    {
    await conn.OpenAsync();
    MySqlCommand command = new MySqlCommand("SELECT * FROM articles WHERE id = @number", conn); // DESC по убыванию, в msql >= <> не равно (в си шарп !=) WHERE title LIKE '%oo%' %-все равно какие символы AND OR ВАЖЕН ПОРЯДОК!!!  ORDER BY id ASC LIMIT 2 OFFSET 1
    
    MySqlParameter param = new MySqlParameter("@number", number);
    command.Parameters.Add(param);
    
    MySqlDataReader reader = (MySqlDataReader)await command.ExecuteReaderAsync();

    if(reader.HasRows){
      while(await reader.ReadAsync()) {
      object id = reader["id"];                      //reader.GetValue(0);
      object title = reader["title"];
      object text= reader["text"];
      object date = reader["date"];
      object avtor = reader["avtor"];

      Console.WriteLine($"{id}. {title} - {avtor}");
      }
  
    }
    await reader.CloseAsync();

    }
}




public async Task UpdateData(string? title, string? text, string? date, string? avtor)
{
    
  string sql = "UPDATE articles SET title = @title, text = @text, date = @date, avtor = @avtor WHERE id = 3"; 
   
   Dictionary<string, string?> parameters = new Dictionary<string, string?>();
   parameters.Add("title", title);
   parameters.Add("text", text);
   parameters.Add("date", date);
   parameters.Add("avtor", avtor);
   await ExecuteQuery(sql, parameters);

}

public async Task DeleteData()
{  
  string sql = "DELETE FROM articles WHERE title = 'Apple'";
   await ExecuteQuery(sql);
}

public async Task CountData() {
  string sql = "SELECT COUNT(id) FROM articles";

using (MySqlConnection conn = new MySqlConnection(connect))
    {
    await conn.OpenAsync();
    MySqlCommand command = new MySqlCommand(sql, conn);
    object? num = await command.ExecuteScalarAsync();
    System.Console.WriteLine(num);
    }

}}