using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    const string FILE_PATH = "data.txt";
    const int MAX_ITEMS = 100;

    static List<Item> items = new List<Item>();

    static void Main()
    {
        LoadFromFile();

        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1 - Vypsat záznamy");
            Console.WriteLine("2 - Přidat záznam");
            Console.WriteLine("3 - Upravit záznam");
            Console.WriteLine("4 - Smazat záznam");
            Console.WriteLine("5 - Výpis s filtrem");
            Console.WriteLine("6 - Seřazený výpis");
            Console.WriteLine("0 - Konec");
            Console.Write("Volba: ");

            switch (Console.ReadLine())
            {
                case "1": PrintItems(items); break;
                case "2": AddItem(); break;
                case "3": EditItem(); break;
                case "4": DeleteItem(); break;
                case "5": FilteredPrint(); break;
                case "6": SortedPrint(); break;
                case "0":
                    SaveToFile();
                    return;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }
        }
    }

    class Item
    {
        public int Value;
        public string Name;
        public string Category;
    }

    static void LoadFromFile()
    {
        if (!File.Exists(FILE_PATH)) return;

        foreach (var line in File.ReadAllLines(FILE_PATH))
        {
            var parts = line.Split(';');
            if (parts.Length == 3 && int.TryParse(parts[0], out int val))
            {
                items.Add(new Item
                {
                    Value = val,
                    Name = parts[1],
                    Category = parts[2]
                });
            }
        }
    }

    static void SaveToFile()
    {
        var lines = items.Select(i => $"{i.Value};{i.Name};{i.Category}");
        File.WriteAllLines(FILE_PATH, lines);
    }

    static void PrintItems(IEnumerable<Item> list)
    {
        Console.WriteLine("\n{0,-5} {1,10} {2,-20} {3,-15}",
            "ID", "Hodnota", "Název", "Kategorie");

        int index = 0;
        foreach (var i in list)
        {
            Console.WriteLine("{0,-5} {1,10} {2,-20} {3,-15}",
                index++, i.Value, i.Name, i.Category);
        }

        PrintStatistics();
        PrintCategorySums();
    }

    static void PrintStatistics()
    {
        int totalSum = items.Sum(i => i.Value);

        var incomes = items.Where(i => i.Value > 0).Select(i => i.Value);
        var expenses = items.Where(i => i.Value < 0).Select(i => i.Value);

        Console.WriteLine("\n--- Statistiky ---");
        Console.WriteLine($"Celkový součet: {totalSum}");

        Console.WriteLine($"Příjmy: počet={incomes.Count()}, součet={incomes.Sum()}");
        Console.WriteLine($"Výdaje: počet={expenses.Count()}, součet={expenses.Sum()}");

        if (incomes.Any())
            Console.WriteLine($"Min příjem={incomes.Min()}, Max příjem={incomes.Max()}");

        if (expenses.Any())
            Console.WriteLine($"Min výdaj={expenses.Min()}, Max výdaj={expenses.Max()}");
    }

    static void PrintCategorySums()
    {
        Console.WriteLine("\n--- Součty podle kategorií ---");
        var dict = new Dictionary<string, int>();

        foreach (var i in items)
        {
            if (!dict.ContainsKey(i.Category))
                dict[i.Category] = 0;
            dict[i.Category] += i.Value;
        }

        foreach (var kv in dict)
            Console.WriteLine($"{kv.Key,-15} {kv.Value}");
    }

    static void AddItem()
    {
        if (items.Count >= MAX_ITEMS)
        {
            Console.WriteLine("Seznam je plný.");
            return;
        }

        int value = ReadInt("Zadej hodnotu: ");
        Console.Write("Zadej název: ");
        string name = Console.ReadLine();
        Console.Write("Zadej kategorii: ");
        string category = Console.ReadLine();

        items.Add(new Item { Value = value, Name = name, Category = category });
    }

    static void EditItem()
    {
        int index = ReadIndex();
        int value = ReadInt("Nová hodnota: ");

        Console.Write("Nový název: ");
        items[index].Name = Console.ReadLine();

        Console.Write("Nová kategorie: ");
        items[index].Category = Console.ReadLine();

        items[index].Value = value;
    }

    static void DeleteItem()
    {
        int index = ReadIndex();
        items.RemoveAt(index);
    }

    static void FilteredPrint()
    {
        Console.Write("Zadej hledaný text: ");
        string text = Console.ReadLine();

        var filtered = items.Where(i =>
            i.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0
        );

        PrintItems(filtered);
    }

    static void SortedPrint()
    {
        Console.Write("Řadit podle (h = hodnota, n = název): ");
        string key = Console.ReadLine();

        Console.Write("Vzestupně? (a/n): ");
        bool asc = Console.ReadLine() == "a";

        Item[] copy = (Item[])items.ToArray().Clone();

        if (key == "h")
            Array.Sort(copy, (a, b) => a.Value.CompareTo(b.Value));
        else
            Array.Sort(copy, (a, b) => a.Name.CompareTo(b.Name));

        if (!asc)
            Array.Reverse(copy);

        PrintItems(copy);
    }

    static int ReadInt(string msg)
    {
        int val;
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out val))
                return val;
            Console.WriteLine("Neplatné číslo.");
        }
    }

    static int ReadIndex()
    {
        int index;
        while (true)
        {
            index = ReadInt("Zadej číslo řádku: ");
            if (index >= 0 && index < items.Count)
                return index;
            Console.WriteLine("Index mimo rozsah.");
        }
    }
}
