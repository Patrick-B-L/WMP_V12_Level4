// A console-based product management system that allows users to add, search, and display products.
// Utilizes modular design with separate classes for input validation, menu management, and product handling.

using System.Diagnostics;

ProductListManager products = new ProductListManager();

while (true)
{
    MenuDisplay.DisplayMainMenu();

    
    // Get a user ipunt, which should be either a valid category or quit ("q")
    string category = InputValidator.GetValidCategory();
    if (category.ToLower() == "q")
        // The user selected to quit
    {
        // Display the list of products and total price
        ProductListManager.DisplayProductList(products);

        // Display further options
        MenuDisplay.HandleUserOptions(products);
    }
    else
        // The user entered a valid category and continues with the process
    {
        // Collect product details from the user
        string productName = InputValidator.GetValidProductName();
        decimal price = InputValidator.GetValidPrice();

        // Add the new product to the product list
        products.AddProduct(new Product(category, productName, price));

        // Notify the user that the product has been successfully added
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("The product was succeddfully added!");
        Console.ResetColor();
        Console.WriteLine("----------------------------------------------------------");
    }
}

class MenuDisplay
{
    public static void DisplayMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("To enter a new product - follow the steps | to quit - enter: \"Q\"");
        Console.ResetColor();
    }

    public static void HandleUserOptions(ProductListManager products)
    {   while (true)
        {
            // Ask the user whether to add another product, search for a product or quit
            string input = InputValidator.GetValidContinueOption();
            if (input == "q")
            {
                Environment.Exit(0);
            }
            else if (input == "s")
            {
                products.SearchAndDisplayProductName();
            }
            else if (input == "p")
            {
                break;
            }
        }
    }
}
// Represents a single product with category, name, and price
class Product
{
    public Product(string category, string productName, decimal price)
    {
        Category = category;
        ProductName = productName;
        Price = price;
    }

    public string Category { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }

}
// Manages the list of products and provides utility methods
class ProductListManager
{
    // Internal list of products
    private List<Product> productList = new List<Product>();

    // Adds a new product to the list
    public void AddProduct(Product product)
    {
        productList.Add(product);
    }

    // Calculates the total price of all products in the list
    public decimal SumPrice()
    {
        return productList.Sum(product => product.Price);
    }

    // Returns all products in the list
    public List<Product> GetAllItems()
    {
        return productList;
    }

    // Returns the list of products sorted by price (ascending)
    public List<Product> SortedByPrice()
    {
        return productList.OrderBy(product => product.Price).ToList();
    }

    // Asks for a product name, searches for that product name and then returns a list with the row of the product name highlighted i purple
        public void SearchAndDisplayProductName()
    {
        string searchedProduct = InputValidator.GetValidProductName();

        var matchedProducts = GetMatchingProducts(searchedProduct);

        if (matchedProducts.Count > 0)
        {
            DisplayProductListWithHighlight(matchedProducts);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No matching products found.");
            Console.ResetColor();
        }
    }

    public List<Product> GetMatchingProducts(string searchedProduct)
    {
        return SortedByPrice()
            .Where(product => product.ProductName.ToLower().Contains(searchedProduct.ToLower()))
            .ToList();
    }

    public void DisplayProductListWithHighlight(List<Product> matchedProducts)
    {
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");

        foreach (var product in SortedByPrice())
        {
            if (matchedProducts.Contains(product))
            {
                // Highlight the entire row for matching products
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else
            {
                // Default color for non-matching products
                Console.ResetColor();
            }

            Console.WriteLine(product.Category.PadRight(20) +
                              product.ProductName.PadRight(20) +
                              product.Price);
        }

        Console.ResetColor();
        Console.WriteLine("----------------------------------------------------------");
    }




    // Displays the product list and total amount in a formatted way
    public static void DisplayProductList(ProductListManager products)
    {
        Console.WriteLine("----------------------------------------------------------");
        Console.WriteLine("Category".PadRight(20) + "Product".PadRight(20) + "Price");
        foreach (var product in products.SortedByPrice())
            Console.WriteLine(product.Category.PadRight(20) + product.ProductName.PadRight(20) + product.Price);
        Console.WriteLine("\n" + "".PadRight(20) + "Total amount:".PadRight(20) + products.SumPrice());
        Console.WriteLine("----------------------------------------------------------");
    }
}

// Validates user inputs and handles invalid entries
class InputValidator
{
    // Ensures that the user enters a valid non-empty category
    public static string GetValidCategory()
    {
        while (true)
        {
            Console.Write("Enter a Category: ");
            string category = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(category))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Category cannot be empty, please try again!");
                Console.ResetColor();
            }
            else
            {
                return category;
            }
        }
    }
    // Ensures that the user enters a valid non-empty product name
    public static string GetValidProductName()
    {
        while (true)
        {
            Console.Write("Enter a Product: ");
            string productName = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(productName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product cannot be empty, please try again!");
                Console.ResetColor();
            }
            else
            {
                return productName;
            }
        }
    }
    // Ensures that the user enters a valid positive price
    public static decimal GetValidPrice()
    {
        while (true)
        {
            Console.Write("Enter a Price: ");
            if (decimal.TryParse(Console.ReadLine().Trim(), out decimal price) && price > 0)
            {
                return price;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid price, please try again!");
                Console.ResetColor();
            }
        }
    }
    // Ensures that the user selects a valid option: "P" or "Q"    
    public static string GetValidContinueOption()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("To enter a new product - enter \"P\" | To search for a product - enter \"S\" | To quit - enter: \"Q\"");
            Console.ResetColor();
            string inputOption = Console.ReadLine().ToLower().Trim();
            if (inputOption == "p" || inputOption == "s"|| inputOption == "q" )
            {
                return inputOption;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input, please try again!");
                Console.ResetColor();
            }
        }
    }
}



