using static System.Reflection.Metadata.BlobBuilder;

namespace CSI120_Final_CalorieCounter_HoangKhoiGiaNguyen
{
    internal class Program
    {
        // Hoang Khoi Gia Nguyen
        // CSI 120 - Computer Programming 1
        // Final - Calorie Counter


        public static Food[] foods = new Food[4]; // Declare new array

        static void Main(string[] args)
        {
            Preload();
            Menu();

        }// Main

        public static void Preload()
        {
            // Loading up content
            foods[0] = new Food("Banana", 1, 105, 2);
            foods[1] = new Food("Chicken", 3, 165, 1);

 
        }// End of Preload

        public static void Menu()
        {
            while (true)
            {
                // Menu layout
            Console.WriteLine("1 - Display all calories you have eaten");
            Console.WriteLine("2 - Add New Items");
            Console.WriteLine("3 - Calculate your total calories eaten");
            Console.WriteLine("4 - Calculate the average calories of the items you have eaten");
            Console.WriteLine("5 - Display all food eaten of a chosen category");
            Console.WriteLine("6 - Search for a food item by name");
            Console.WriteLine("7 - Exit");
            Console.Write("Your choice: ");

            string userInput = Console.ReadLine();

            Console.WriteLine();
            
                switch (userInput)
                {
                
                case "1":
                    DisplayAllItems();
                    break;

                case "2":
                    AddItem();
                    break;

                case "3":
                    
                    double printFinalCalories = TotalCalories();
                        Console.WriteLine($"The total amount of calories you have eaten is: {printFinalCalories}");
                        Console.WriteLine();
                        break;

                case "4":
                        double printAverageCalories = AverageCalories();
                        Console.WriteLine($"Your average calories intake is: {printAverageCalories}");
                        Console.WriteLine();
                        break;

                case "5":
                     DisplayByCategory();
                    break;

                case "6":
                        SearchByName();
                    break;

                case "7":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid");
                    Console.WriteLine();
                    
                    break;

                }
            }
        }// Menu End

        public static void DisplayAllItems()
        {
            Console.Write("Format: ");
            Console.WriteLine("(Name| Category | Calories | Quantity | Total Calories)");
            Console.WriteLine();
            // Checking through array
            for (int i = 0; i < foods.Length; i++)
            {
                // Display base on check result
                if (foods[i] == null)
                {
                    
                    Console.WriteLine("Empty");
                    
                }

            
                else
                {

                    Food currentFood = foods[i];

                    Console.WriteLine(currentFood.DisplayTable(currentFood, currentFood));

                }

            }

            
        }

        public static void AddItem()
        {
            // Input information
            Food newItem = CreateNewFood();

            // Check empty slot
            int firstIndex = FindEmptySlot();

            // If no space, initiate size increase.
            if (firstIndex == -1)
            {

                Console.WriteLine("No more space!");
                DoubleArraySize();
                Console.WriteLine("Array's size has been expanded!");
                firstIndex = FindEmptySlot();

            }

            Console.WriteLine();
            // Add item to first empty slot
            foods[firstIndex] = newItem;
            Console.WriteLine("Item Added!");
            Console.WriteLine();

        }

        public static Food CreateNewFood()
        {
            // Declare values
            string name;
            int category;
            int calories;
            int quantity;                    
            
            // Input, loop until right type
            do
            {
                Console.Write("Enter food's name: ");
                name = Console.ReadLine();
            } while (string.IsNullOrEmpty(name));

            do
            {
                Console.Write("Choose a category: ");
            } while (!int.TryParse(Console.ReadLine(), out category));

            do
            {
                Console.Write("Amount of calories per unit: ");
            } while (!int.TryParse(Console.ReadLine(), out calories));

            do
            {
                Console.Write("Quantity: ");
            } while (!int.TryParse(Console.ReadLine(), out quantity));

            return new Food(name, category, calories, quantity);
        }

        private static int FindEmptySlot()
        {
            for (int i = 0; i < foods.Length; i++)
            {
                // Return the index that is empty or condition to double the size
                if (foods[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }

        private static void DoubleArraySize()
        {
            // Make a temporary array with twice the length of current array then copy it back
            Food[] newArray = new Food[foods.Length * 2];
            Array.Copy(foods, newArray, foods.Length);
            foods = newArray;
        }
    

    public static double TotalCalories()
        {
            // Assign value
            double finalCalories = 0;
            // Check through array to perform equation
            for (int i = 0; i < foods.Length; i++)
            {
                if (foods[i] != null)
                {
                    finalCalories += foods[i].TotalCalories(foods[i]);
                }
            }

            return finalCalories;
            
        }

        public static double AverageCalories()
        {
            // Use total method
            double total = TotalCalories(); 
            int count = 0;
            // Check array for count value
            for (int i = 0; i < foods.Length; i++)
            {
                if (foods[i] != null && foods[i].Quantity >= 1)
                {
                    count++;
                }
            }

            
            // Perform action depends on count value
            if (count == 0) 
            {
                Console.WriteLine("No value.");
                return 0;
            }
            else
            {
                double averageCalories = total / count;
                
                return averageCalories;
            }
        }

        public static void DisplayByCategory()
        {
            try
            {
                // Input category by text
                Console.WriteLine("Select a category: Fruit | Vegetable | Protein | Grain | Dairy ");
                Console.Write("Your selection: ");
                string categoryInput = Console.ReadLine();

                Console.WriteLine();

                Console.Write("Format: ");
                Console.WriteLine("(Name| Category | Calories | Quantity | Total Calories)");

                Console.WriteLine();

                bool found = false; 

                foreach (Food food in foods)
                {
                    // Check through array then display
                    if (categoryInput == food.CategoryNames(food))
                    {
                        Food currentFood = food;

                        Console.WriteLine(currentFood.DisplayTable(currentFood, currentFood));
                        Console.WriteLine();
                        found = true; 
                        break;
                    }
                }

                if (!found) 
                {
                    Console.WriteLine("No food found in that category.");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void SearchByName()
        {
            try
            {
                // Input information
                Console.Write("Enter the food's name: ");
                string nameInput = Console.ReadLine();

                Console.WriteLine();
                // Case insensitive check
                string inputCheck = nameInput.ToLower(); 
                bool found = false; 

                foreach (Food food in foods)
                {
                    // Case insensitive check
                    if (inputCheck == food.Name.ToLower()) 
                    {
                        Food currentFood = food;

                        Console.WriteLine(currentFood.DisplayTable(currentFood, currentFood));
                        Console.WriteLine();
                        found = true; 
                        break;
                    }
                }

                if (!found) 
                {
                    Console.WriteLine($"{nameInput} does not exist!");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }// Class


    public class Food
    {

        public string Name;
        public int Category;
        public int Calories;
        public int Quantity;

        public Food(string name, int category, int calories, int quantity)
        {

            Name = name;
            Category = category;
            Calories = calories;
            Quantity = quantity;

        }// Constructor

        public Food()
        {

            Name = "Unlisted";
            Category = -1;
            Calories = -1;
            Quantity = -1;

        }// Default Constructor

        public double TotalCalories(Food foodInfo)
        {
            // Setting up the equation for total calories value
            if (foodInfo.Calories >= 0 && foodInfo.Quantity >= 0)
            {

                double sumCalories = foodInfo.Calories * foodInfo.Quantity;
                return sumCalories;
            }
            else
            {
                double sumCalories = 0;
                return sumCalories;
            }
           

        }// Total Calories End

        public string CategoryNames(Food foodTypeNum)
        {
            // Set the parameter as number then convert to string when checking the value
            string foodCategory = Convert.ToString(foodTypeNum.Category);
            // Then convert that number string to text
            if (foodTypeNum.Category == 1)
            {

                foodCategory = "Fruit";

            }
            else if (foodTypeNum.Category == 2)
            {

                foodCategory = "Vegetable";

            }
            else if (foodTypeNum.Category == 3)
            {

                foodCategory = "Protein";

            }
            else if (foodTypeNum.Category == 4)
            {

                foodCategory = "Grain";

            }
            else if (foodTypeNum.Category == 5)
            {

                foodCategory = "Dairy";

            }
            else
            {
                foodCategory = "No Category Chosen";
            }

            return foodCategory;

        }// Category Names End

        public string DisplayTable(Food foodInfo, Food foodTypeNum)
        {
            // Call the value of category and total calories
            string typeText = CategoryNames(foodTypeNum);
            double totalCalories = TotalCalories(foodInfo);
            return $"{Name} | {typeText} | {Calories} | {Quantity} | {totalCalories}";

        }// Display End


    }// Food Class End

}// Name
