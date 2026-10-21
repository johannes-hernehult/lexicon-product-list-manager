Console.WriteLine("----------------------------------------");
Console.WriteLine("PRODUCT LIST MANAGER - LEVEL 1");
Console.WriteLine("----------------------------------------");

List<string> products = [];

while (true)
{
    Console.Write("Enter a product name: ");
    string input = Console.ReadLine() ?? "";

    if (IsExit(input)) break;

    if (IsEmptyString(input))
    {
        Console.WriteLine("Product name can not be empty...");
        continue;
    }

    if (!IsValidProductName(input))
    {
        Console.WriteLine("Invalid product name...");
        continue;
    }

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
Console.ResetColor();

//Helper functions
static bool IsExit(string input)
{
    return input.Trim().ToLower() == "exit";
}

static bool IsEmptyString(string input)
{
    return string.IsNullOrWhiteSpace(input);
}

static bool IsValidProductName(string input)
{
    //TODO: Flesh out product name validation here
    return false;
}