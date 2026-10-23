using System.Text.RegularExpressions;

//
// List<string> products = [];
//
// const int minNumber = 200;
// const int maxNumber = 500;
// const string divider = "----------------------------------------";
//
//
// PrintHeading("PRODUCT LIST MANAGER - LEVEL 4");
//
// while (true)
// {
//     Console.Write("Enter a product name: ");
//     string input = Console.ReadLine() ?? "";
//
//     input = input.Trim().ToUpper(); //Normalizing input
//
//     if (input == "EXIT") break;
//
//     if (!TryAcceptProduct(input)) continue;
//
//     products.Add(input);
// }
//
// products.Sort();
//
// PrintProducts(products);
//
// //Helper functions
// static string? GetValidationError(string input)
// {
//     if (string.IsNullOrWhiteSpace(input))
//         return "ERROR: Input cannot be empty.";
//
//     int dashIndex = input.IndexOf('-');
//     if (dashIndex == -1)
//         return "ERROR: Product must contain a dash (-).";
//
//     string leftSide = input.Substring(0, dashIndex);
//     string rightSide = input.Substring(dashIndex + 1);
//
//     if (!Regex.IsMatch(leftSide, @"^[A-Z]+\z"))
//         return "ERROR: The left side must contain letters only.";
//
//     if (!Regex.IsMatch(rightSide, @"^[0-9]+\z"))
//         return "ERROR: The right side must contain numbers only.";
//
//     if (!int.TryParse(rightSide, out int number) || number < minNumber || number > maxNumber)
//         return $"ERROR: The numeric part must be between {minNumber} and {maxNumber}.";
//
//     return null;
// }
//
// static bool TryAcceptProduct(string input)
// {
//     string? error = GetValidationError(input);
//     if (error is null)
//         return true;
//
//     Console.ForegroundColor = ConsoleColor.Red;
//     Console.WriteLine(error);
//     Console.ResetColor();
//     return false;
// }
//
// static void PrintHeading(string title)
// {
//     Console.WriteLine(divider);
//     Console.WriteLine(title);
//     Console.WriteLine(divider);
// }
//
// static void PrintProducts(List<string> prod)
// {
//     Console.ForegroundColor = ConsoleColor.Blue;
//     Console.WriteLine();
//
//     if (prod.Count == 0)
//     {
//         PrintHeading("No products found.");
//     }
//     else
//     {
//         PrintHeading("All products:");
//
//         foreach (string product in prod)
//             Console.WriteLine(" - " + product);
//
//         Console.WriteLine(divider);
//     }
//
//     Console.ResetColor();
// }


//--------------------------------------------------------------------------------------
// Rewrite for level 4
//--------------------------------------------------------------------------------------

// --- Main loop

List<string> products = new List<string>();
List<string> menuItems = new List<string>
{
    "1 - Add Product",
    "2 - View Products",
    "3 - Search Product",
    "0 - Exit",
};
string menuSelection = "";

do
{
    RenderMenu(menuItems);
    RenderTextInline("Select menu item: ");
    menuSelection = Console.ReadLine().Trim();

    switch (menuSelection)
    {
        case "1": AddProduct(); break;
        case "2": ViewProducts(); break;
        case "3": SearchProduct(); break;
        case "0": RenderFeedback("Shutting down...", FeedbackType.Info); break;
        default: RenderFeedback($"Invalid selection: {menuSelection}", FeedbackType.Error); break;
    }
} while (menuSelection != "0");

// Feature Functions
void AddProduct()
{
    while (true)
    {
        RenderTextInline("Product name: ");
        string product = NormalizeString(Console.ReadLine() ?? "");

        string? error = GetValidationError(product);
        if (error is not null)
        {
            RenderFeedback(error, FeedbackType.Error);
            continue;
        }

        products.Add(product);
        RenderFeedback($"{product} added.", FeedbackType.Success);
        return;
    }
}

void ViewProducts()
{
    RenderDivider();
    RenderHeading("Products");
    RenderList(products);
    RenderDivider();
    RenderText("Press enter to go back to main menu");
    Console.ReadLine();
}

void SearchProduct()
{
    RenderDivider();
    while (true)
    {
        RenderHeading("Search a product");
        RenderTextInline("Product name: ");
        //Get input
        string input = NormalizeString(Console.ReadLine() ?? "");

        //Search data for matches
        List<string> searchResults = products.Where(product => product.Contains(input)).ToList();

        if (searchResults.Count == 0)
        {
            //If no results render message saying no result
            Console.ForegroundColor = ConsoleColor.DarkRed;
            RenderText("No results found... Try again!");
            Console.ResetColor();
        }
        else
        {
            //Render results
            RenderHeading("Search results:");
            RenderList(searchResults);
        }

        //Loop until user exits

        Console.WriteLine();
        RenderText("Enter empty input to exit");
        //Switch statement between search, exit or invalid
        if (input == "") return;
    }
}

// Helper Functions
string NormalizeString(string text)
{
    return text.Trim().ToUpper();
}

string? GetValidationError(string name)
{
    const int minNumber = 200;
    const int maxNumber = 500;

    if (string.IsNullOrWhiteSpace(name))
        return "Product name can not be empty";

    int dashIndex = name.IndexOf('-');
    if (dashIndex == -1)
        return "Product name must include a dash '-'";

    string leftSide = name.Substring(0, dashIndex);
    string rightSide = name.Substring(dashIndex + 1);

    if (!Regex.IsMatch(leftSide, @"^[A-Z]+\z"))
        return "Product name can only have letters before the dash '-'";

    if (!Regex.IsMatch(rightSide, @"^[0-9]+\z"))
        return "Product name can only have numbers after the dash '-'";

    if (!int.TryParse(rightSide, out int number) || number < minNumber || number > maxNumber)
        return $"The numeric part must be between {minNumber} and {maxNumber}.";

    return null;
}

// Render Functions
void RenderMenu(List<string> items)
{
    RenderDivider();
    RenderHeading("Product List Manager");
    RenderList(items);
    RenderDivider();
}

void RenderHeading(string text)
{
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine(text);
    Console.ResetColor();
}


void RenderText(string text)
{
    Console.WriteLine(text);
}

void RenderTextInline(string text)
{
    Console.Write(text);
}

void RenderList(List<string> list)
{
    foreach (var item in list)
    {
        RenderText(item);
    }
}

void RenderDivider()
{
    Console.ForegroundColor = ConsoleColor.DarkGray;
    Console.WriteLine("-------------------------------------------------");
    Console.ResetColor();
}

void RenderFeedback(string text, FeedbackType type)
{
    RenderDivider();
    switch (type)
    {
        case FeedbackType.Error: Console.ForegroundColor = ConsoleColor.Red; break;
        case FeedbackType.Warning: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
        case FeedbackType.Success: Console.ForegroundColor = ConsoleColor.Green; break;
        case FeedbackType.Info: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
    }

    RenderText($"{type.ToString().ToUpper()}: {text}");
    RenderDivider();
    Console.ResetColor();
}

internal enum FeedbackType
{
    Error,
    Warning,
    Success,
    Info
}
