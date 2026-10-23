using System.Text.RegularExpressions;

//--------------------------------------------------------------------------------------
// Rewrite for level 4
//--------------------------------------------------------------------------------------

// --- Main loop

List<string> products = new List<string>();

RunMenu("Product List Manager", new()
{
    [1] = ("Add Product", AddProduct),
    [2] = ("View Products", ViewProducts),
    [3] = ("Search Product", SearchProduct),
    [4] = ("Delete a Product", DeleteProduct),
    [5] = ("Statistics", Statistics),
});

void RunMenu(string title, Dictionary<int, (string Label, Action Run)> items, string exitLabel = "Exit")
{
    while (true)
    {
        // Render
        RenderDivider();
        RenderHeading(title);
        foreach (var (key, item) in items)
            RenderText($"{key} - {item.Label}");
        RenderText($"0 - {exitLabel}");
        RenderDivider();

        // Input
        RenderTextInline("Select menu item: ");
        string input = (Console.ReadLine() ?? "").Trim();
        if (input == "0") return;
        Console.WriteLine();

        if (items.TryGetValue(int.Parse(input), out var selected))
        {
            RenderTextInline(selected.Label);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            RenderText(" (leave input empty to go back)");
            Console.ResetColor();
            selected.Run();
        }
        else
        {
            RenderFeedback($"Invalid selection: {input}", FeedbackType.Error);
        }
    }
}

// Feature Functions
void AddProduct()
{
    while (true)
    {
        RenderTextInline("Product name: ");
        string product = NormalizeString(Console.ReadLine() ?? "");
        if (product == "") return;

        string? error = GetValidationError(product);
        if (error is not null)
        {
            RenderFeedback(error, FeedbackType.Error);
            continue;
        }

        //Check if product exists 
        if (products.Contains(product))
        {
            RenderFeedback("Product already exists.", FeedbackType.Warning);
            continue;
        }
        
        products.Add(product);
        RenderFeedback($"{product} added.", FeedbackType.Success);
    }
}

void ViewProducts()
{
    if (products.Count == 0) RenderHeading("No products found");

    RenderList(products);
    string input = NormalizeString(Console.ReadLine() ?? "");
}

void SearchProduct()
{
    while (true)
    {
        RenderTextInline("Search: ");
        //Get input
        string input = NormalizeString(Console.ReadLine() ?? "");
        if (input == "") return;

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
            Console.WriteLine();
        }
    }
}

void DeleteProduct()
{
    while (true)
    {
        if (products.Count != 0)
        {
            RenderDivider();
            RenderHeading("All available products");
            RenderList(products);
            RenderDivider();
        }
        else
        {
            RenderHeading("No products found");
        }

        string product = NormalizeString(Console.ReadLine() ?? "");
        if (product == "") return;

        //Check if product exists
        if (!products.Contains(product))
        {
            RenderFeedback($"Product not found: {product}", FeedbackType.Error);
        }
        else
        {
            products.Remove(product);
            RenderFeedback($"{product} deleted.", FeedbackType.Success);
        }
    }
}

void Statistics()
{
    if (products.Count == 0)
    {
        RenderHeading("No products found");
        Console.ReadLine();
    }
    else
    {
        List<int> productNumbers = new List<int>();
        foreach (var product in products)
        {
            int dashIndex = product.IndexOf('-');
            string number = product.Substring(dashIndex + 1);
            productNumbers.Add(int.Parse(number));
        }

        while (true)
        {
            RenderTextInline($"Total products: {products.Count()}");
            Console.WriteLine();
            RenderTextInline($"Lowest product number: {productNumbers.Min()}");

            Console.WriteLine();
            RenderTextInline($"Highest product number: {productNumbers.Max()}");

            Console.WriteLine();
            RenderTextInline($"Average product number: {Math.Round(productNumbers.Average())}");

            Console.WriteLine();
            string input = NormalizeString(Console.ReadLine() ?? "");
            if (input == "") return;
        }
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
    switch (type)
    {
        case FeedbackType.Error: Console.ForegroundColor = ConsoleColor.Red; break;
        case FeedbackType.Warning: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
        case FeedbackType.Success: Console.ForegroundColor = ConsoleColor.Green; break;
        case FeedbackType.Info: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
    }

    RenderText($"{type.ToString().ToUpper()}: {text}");
    Console.ResetColor();
}

internal enum FeedbackType
{
    Error,
    Warning,
    Success,
    Info
}