using System.ComponentModel;
using System.IO;
using System.Text;
using System;
public class Product
{
    public int price;
    public int id;
    public string name;

    public Product(int p, int i, string n)
    {
        price = p;
        id = i;
        name = n;
    }
    public void Display()
    {
        Console.WriteLine($"ID: {id}, Name: {name}, Price: {price}");
    }
}
internal class Program
{
    static int id = 0;

    static List<Product> products = new List<Product>();
    public static void add_product(int price, string name)
    {
        products.Add(new Product(price, id, name));
        id++;
    }
    public static void show_products()
    {
        foreach (var product in products)
        {
            product.Display();
        }
    }
    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
    public static void save_to_json(string file_name)
    {
        if (File.Exists(file_name))
        {
            File.Delete(file_name);
        }

        
        using (FileStream fs = File.Create(file_name))
        {
            foreach (var product in products) 
            {
                AddText(fs, (product.id).ToString()+":"+(product.price).ToString()+":"+product.name+"\n");

            }
        }

    }
    public static void load_from_json(string file_name)
    {
        if (!File.Exists(file_name))
        {
            Console.WriteLine("file does not exist");
            return;
        }
        products.Clear();
        string[] lines = File.ReadAllLines(file_name);

        foreach (string line in lines) 
        {
            string[] parameters = line.Split(':');
            products.Add(new Product(Int32.Parse(parameters[1]), Int32.Parse(parameters[0]), parameters[2]));
        }
    }
        
    private static void Main(string[] args)
    { //add_product(10, "potatis");
        //show_products();
        //save_to_json("Test_file.json");
        load_from_json("Test_file.json");
        show_products();
    }
}
