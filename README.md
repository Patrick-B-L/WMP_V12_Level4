# WMP_V12_Level4

**Comment about the code:**

This program is a console-based product management application designed to allow users to create, search, and display products. It is modularly structured, with clear separation of responsibilities across classes and methods. The design emphasizes user input validation, menu-driven interaction, and well-organized data handling.

---

**Structural overview:**
1. **Main Program Flow (`while (true)`):**
   - Handles the main loop where users can input categories, products, and prices or quit the program.
   - Delegates tasks to other components like `MenuDisplay`, `InputValidator`, and `ProductListManager`.

2. **Classes:**
   - **`MenuDisplay`:** Manages user interface elements like displaying menus and handling user options.
   - **`Product`:** Represents a single product with properties for category, name, and price.
   - **`ProductListManager`:** Manages the product list and contains methods for adding, displaying, and searching products.
   - **`InputValidator`:** Ensures that all user inputs are valid and handles error messages for invalid inputs.

3. **Key Features:**
   - **Product Validation:** Prevents empty or invalid inputs for category, product name, and price.
   - **Menu Navigation:** Users can add products, search for existing ones, or quit.
   - **Search Capability:** Highlights rows in the product list that match the user's search query.
   - **User Feedback:** Provides real-time messages for success or errors (e.g., "Product successfully added!" or "Invalid price").



