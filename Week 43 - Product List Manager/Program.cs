Console.WriteLine("----------------------------------------");
Console.WriteLine("PRODUCT LIST MANAGER - LEVEL 1");
Console.WriteLine("----------------------------------------");

List<string> products = new List<string>();

while (true)
{
    Console.Write("Enter a product name: ");
    string input = Console.ReadLine() ?? "";

    if (input.Trim().ToLower() == "exit") break;

    products.Add(input);
}

//Sort list
products.Sort();

//Print all added products sorted
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine();
Console.WriteLine("---------------------------------------");
Console.WriteLine("All products:");
foreach (string product in products)
{
    Console.WriteLine(product);
}

Console.WriteLine("---------------------------------------");