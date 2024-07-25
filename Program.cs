/*
 * Ankit Bombwal
 * Creates a Bakery object that manipulates and creates Cake objects
 * Another project made for a college class.
 */

namespace Final_Project
{
    using System.Collections.Generic;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bakery Management Sim! Please enter the name of your Bakery below:");
            string sign = Console.ReadLine();

            // Creation of the Bakery object
            Bakery baker = new Bakery(sign);
            Console.WriteLine("New Bakery created, outputting Bakery information now: ");

            baker.displayStore();

            Console.WriteLine("\nWe don't have any stock at the moment, we should make some!");
            baker.bakeCake();
            baker.bakeCake();

            Console.WriteLine("\nNow that we have some cakes, lets try selling one!");
            baker.displayStore();

            // Selling a cake from the store
            Console.WriteLine("\nWhich number cake do you want to sell?");
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                baker.sellCake(select);
            }
            catch (FormatException)
            {
                Console.WriteLine("Sorry, you can only enter a whole number here!");
            }

            baker.displayStore();
            

        }
    }

    class Bakery
    {
        static int ID;
        string name;
        List<Cake> cakes;
        bool isOpen;
        double money;
        // Default Constructor
        public Bakery()
        {
            ID = ID++;
            name = "Test Bakery";
            cakes = new List<Cake>();
            isOpen = false;
            money = 100.0;
        }
        // Parameter Constructor
        public Bakery(string sign)
        {
            ID = ID++;
            name = sign;
            cakes = new List<Cake>();
            isOpen = true;
            money = 100.0;
        }
        // Creates a cake object and adds it to cakes list. Also calls displayRecipe() and tasteTest() from cake class
        public void bakeCake()
        {
            Cake c;

            Console.WriteLine("\nThe cakes can be either Medium or Large. What size of cake do your want to make?");
            string size = Console.ReadLine();
            Console.WriteLine("Now, what kind of cake do you want to bake? We have ingredients for Chocolate, Red Velvet, or Lemon cakes");
            string type = Console.ReadLine();

            // input validation
            if(size == "" || type == "")
            {
                Console.WriteLine("Looks like we're missing an input or two. We'll default to a Medium Chocolate cake, ok?");
                c = new Cake();
            }
            else
            {
                c = new Cake(size, type);
            }

            c.displayRecipe();

            // Calls tastTest() from the cake class
            c.tasteTest();

            // Adds completed cake to cakes list
            cakes.Add(c);
            Console.WriteLine("{0} {1} cake added to the display!",size, type);
        }

        // Removes a cake from the cakes List and adds the cake's price to the money value
        public void sellCake(int select)
        {
            Console.WriteLine("Sold {0} {1} cake for ${2}.", cakes[select-1].getSize(), cakes[select-1].getType(), cakes[select-1].getPrice());
            money += cakes[select - 1].getPrice();
            cakes.RemoveAt(select - 1);
            Console.WriteLine("{0} now has ${1}", name, money);
        }

        // Displays the store information
        public void displayStore()
        {
            int cakenum = 1;
            Console.WriteLine("\nStore ID number: {0}\n - Current funds: ${1}\n", ID, money);
            Console.WriteLine("Welcome to {0}! Here is what we have in stock!", name);
            foreach(Cake c in cakes)
            {
                Console.WriteLine("{0}. {1} {2} cake: ${3}", cakenum,c.getSize(), c.getType(), c.getPrice());
                cakenum++;
            }

            if( isOpen == true )
            {
                Console.WriteLine("We are currently open!");
            }
            else
            {
                Console.WriteLine("We are currently closed.");
            }
        }
    }

    public class Cake
    {
        string size;
        string type;
        string[] ingredients;
        double[] ratios;
        double mass;
        double price;

        // default constructor for the cake class. Defaults to a Medium Chocolate cake
        public Cake()
        {
            size = "Medium";
            type = "Chocolate";
            ingredients = new string[] {"Flour", "Sugar", "Cocoa", "Baking Powder", "Baking Soda", "Salt", "Eggs", "Buttermilk", "Oil", "Vanilla", "Water"};
            ratios = new double[] {15.8, 24.5, 5.6, 0.4, 0.6, 0.4, 9.0, 18.0, 8.1, 0.6, 17.0};
            mass = 50.0;
            price = 15.0;

        }

        // Parameterized constructor which derives the price, ingredients, and ratios from the user-entered size and type
        public Cake(string bigOrSmall, string flavor)
        {
            size = bigOrSmall;
            type = flavor;

            if(size.ToLower() == "large")
            {
                mass = 100.0;
                price = 30.0;
            }
            else
            {
                mass = 50.0;
                price = 15.0;
            }

            if (type.ToLower() == "chocolate")
            {
                ingredients = new string[] {"Flour", "Sugar", "Cocoa", "Baking Powder", "Baking Soda", "Salt", "Eggs", "Buttermilk", "Oil", "Vanilla", "Water"};
                ratios = new double[] {15.8, 24.5, 5.6, 0.4, 0.6, 0.4, 9.0, 18.0, 8.1, 0.6, 17.0};
            }else if (type.ToLower() == "red velvet")
            {
                ingredients = new string[] {"Flour", "Sugar", "Cocoa", "Baking Soda", "Salt", "Eggs", "Buttermilk", "Oil", "Vanilla", "Red Food Coloring", "Vinegar"};
                ratios = new double[] {22.4, 22.4, 0.4, 0.7, 0.4, 9.0, 17.9, 24.0, 0.3, 2.1, 0.4};
            }
            else if (type.ToLower() == "lemon")
            {
                ingredients = new string[] {"Sugar", "Egg", "Buttermilk", "Vanilla", "Butter", "Self Rising Flour", "Frosting - Egg Yolk", "Frosting - Lemon Juice", "Frosting - Sugar", "Frosting - Butter"};
                ratios = new double[] {15.0, 9.0, 9.0, 0.2, 8.5, 15.6, 17.9 , 11.4, 11.3, 2.1};
            }
        }

        // Outputs the recipe to the console.
        public void displayRecipe()
        {
            Console.WriteLine("\nTo make a {0} {1} cake, you need:", size, type);
            for(int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine("{0}, {1}g", ingredients[i], ratios[i]);
            }
        }

        // Changes the price variable based on user input
        public void changePrice()
        {
            Console.WriteLine("\nWhat should the new price be?");
            try
            {
                price = Convert.ToDouble(Console.ReadLine());

            }catch (FormatException)
            {
                Console.WriteLine("Error! Please enter a number.");
            }
        }

        // Calls the changePrice() function if the taste is above or below average.
        public void tasteTest()
        {
            Console.WriteLine("\nLets give the batter a taste test...");
            Random r = new Random();
            int taste = r.Next(0, 3);

            switch (taste)
            {
                case 0:
                    Console.WriteLine("Oh, this tastes terrible! We should discount it.");
                    changePrice();
                    break;
                case 1:
                    Console.WriteLine("The taste is average. We can sell as-is");
                    break;
                case 2:
                    Console.WriteLine("This is amazing! We should charge a premium on this cake!");
                    changePrice();
                    break;
            }
        }

        // Getter method for size
        public string getSize()
        {
            return size;
        }
        // Getter method for type
        public string getType()
        {
            return type;
        }
        // Getter method for price
        public double getPrice()
        {
            return price;
        }

    }
}