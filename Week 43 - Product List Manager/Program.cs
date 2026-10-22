using System.Text.RegularExpressions;

List<string> products = [];

const int minNumber = 200;
const int maxNumber = 500;
const string Divider = "----------------------------------------";


PrintHeading("PRODUCT LIST MANAGER - LEVEL 3");

while (true)
{
    Console.Write("Enter a product name: ");
    string input = Console.ReadLine() ?? "";

    input = input.Trim().ToUpper(); //Normalizing input

    if (input == "EXIT") break;

    if (!TryAcceptProduct(input)) continue;

    products.Add(input);
}

products.Sort();

PrintProducts(products);

//Helper functions
static string? GetValidationError(string input)
{
    if (string.IsNullOrWhiteSpace(input))
        return "Input cannot be empty.";

    int dashIndex = input.IndexOf('-');
    if (dashIndex == -1)
        return "Product must contain a dash (-).";

    string leftSide = input.Substring(0, dashIndex);
    string rightSide = input.Substring(dashIndex + 1);

    if (!Regex.IsMatch(leftSide, @"^[A-Z]+\z"))
        return "The left side must contain letters only.";

    if (!Regex.IsMatch(rightSide, @"^[0-9]+\z"))
        return "The right side must contain numbers only.";

    if (!int.TryParse(rightSide, out int number) || number < minNumber || number > maxNumber)
        return $"The numeric part must be between {minNumber} and {maxNumber}.";
    
    return null;
}

static bool TryAcceptProduct(string input)
{
    string? error = GetValidationError(input);
    if (error is null)
        return true;

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(error);
    Console.ResetColor();
    return false;
}

static void PrintHeading(string title)
{
    Console.WriteLine(Divider);
    Console.WriteLine(title);
    Console.WriteLine(Divider);
}

static void PrintProducts(List<string> prod)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine();

    if (prod.Count == 0)
    {
        PrintHeading("No products found.");
    }
    else
    {
        PrintHeading("All products:");

        foreach (string product in prod)
            Console.WriteLine(" - " + product);

        Console.WriteLine(Divider);
    }

    Console.ResetColor();
}