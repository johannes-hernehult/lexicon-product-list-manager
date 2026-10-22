using System.Text.RegularExpressions;

Console.WriteLine("----------------------------------------");
Console.WriteLine("PRODUCT LIST MANAGER - LEVEL 1");
Console.WriteLine("----------------------------------------");

List<string> products = [];

while (true)
{
    Console.Write("Enter a product name: ");
    string input = Console.ReadLine() ?? "";

    input = input.Trim().ToUpper(); //Normalizing input

    if (input == "EXIT")
    {
        Console.WriteLine("Exiting program...");
        break;
    }

    string? error = GetValidationError(input);
    if (error is not null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
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

    if (!int.TryParse(rightSide, out int number) || number < 200 || number > 500)
        return "The numeric part must be between 200 and 500.";

    return null;
}