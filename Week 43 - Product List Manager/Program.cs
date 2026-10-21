Console.WriteLine(
    "----------------------------------------\n PRODUCT LIST MANAGER - LEVEL 1\n----------------------------------------");

// Ask for products in a while loop
// Store products in a list
// exit in any shape breaks the while loop and displays all products added

List<string> products = new List<string>();

while (true)
{
    Console.Write("Enter a product name: ");
    string input = Console.ReadLine() ?? "";

    if (input.Trim().ToLower() == "exit") break;

    products.Add(input);
}

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine();
Console.WriteLine("---------------------------------------");
Console.WriteLine("All products:");
foreach (string product in products)
{
    Console.WriteLine(product);
}
Console.WriteLine("---------------------------------------");