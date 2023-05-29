namespace RecipeApp
{
    using System;
    using System.Collections.Generic;
    class Ingredient
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }

        public Ingredient(string name, decimal quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
        }

        public override string ToString()
        {
            return $"{Quantity} {Unit} {Name}";
        }
    }

    class Step
    {
        public string Description { get; set; }

        public Step(string description)
        {
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }

    class Recipe
    {
        private Ingredient[] ingredients;
        private Step[] steps;

        public Recipe(int numIngredients, int numSteps)
        {
            ingredients = new Ingredient[numIngredients];
            steps = new Step[numSteps];
        }

        public void AddIngredient(int index, string name, decimal quantity, string unit)
        {
            ingredients[index] = new Ingredient(name, quantity, unit);
        }

        public void AddStep(int index, string description)
        {
            steps[index] = new Step(description);
        }

        public void Display()
        {
            Console.WriteLine("Ingredients:");
            foreach (Ingredient ingredient in ingredients)
            {
                Console.WriteLine(ingredient.ToString());
            }

            Console.WriteLine("Steps:");
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i].ToString()}");
            }
        }

        public void Scale(decimal factor)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }

        public void Reset()
        {
            foreach (Ingredient ingredient in ingredients)
            {
                ingredient.Quantity /= 4;
            }
        }

        public void Clear()
        {
            ingredients = new Ingredient[ingredients.Length];
            steps = new Step[steps.Length];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the number of ingredients:");
            int numIngredients = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of steps:");
            int numSteps = int.Parse(Console.ReadLine());

            Recipe recipe = new Recipe(numIngredients, numSteps);

            for (int i = 0; i < numIngredients; i++)
            {
                Console.WriteLine($"Enter the name of ingredient #{i + 1}:");
                string name = Console.ReadLine();

                Console.WriteLine($"Enter the quantity of {name} (in grams, milliliters, etc.):");
                decimal quantity = decimal.Parse(Console.ReadLine());

                Console.WriteLine($"Enter the unit of measurement for {quantity} {name} (e.g. grams, milliliters, tablespoons):");
                string unit = Console.ReadLine();

                recipe.AddIngredient(i, name, quantity, unit);
            }

            for (int i = 0; i < numSteps; i++)
            {
                Console.WriteLine($"Enter step #{i + 1}:");
                string description = Console.ReadLine();

                recipe.AddStep(i, description);
            }

            recipe.Display();

            Console.WriteLine("Enter the scaling factor (0.5, 2, or 3):");
            decimal factor = decimal.Parse(Console.ReadLine());

            recipe.Scale(factor);
            recipe.Display();

            Console.WriteLine("Resetting the quantities to the original values...");
            recipe.Reset();
            recipe.Display();

            Console.WriteLine("Clearing the recipe...");
            recipe.Clear();
        }
    }
}


class Recipe
{
    public string Name { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Recipe(string name)
    {
        Name = name;
        Ingredients = new List<Ingredient>();
    }

    public int CalculateTotalCalories()
    {
        int totalCalories = 0;
        foreach (Ingredient ingredient in Ingredients)
        {
            totalCalories += ingredient.Calories;
        }
        return totalCalories;
    }
}

class Ingredient
{
    public string Name { get; set; }
    public int Calories { get; set; }
    public string FoodGroup { get; set; }
}

class Program
{
    static void Main()
    {
        List<Recipe> recipes = new List<Recipe>();

        while (true)
        {
            Console.WriteLine("Enter the name of the recipe (or 'q' to quit):");
            string recipeName = Console.ReadLine();
            if (recipeName.ToLower() == "q")
                break;

            Recipe recipe = new Recipe(recipeName);

            while (true)
            {
                Console.WriteLine("Enter the ingredient name (or 'q' to finish the recipe):");
                string ingredientName = Console.ReadLine();
                if (ingredientName.ToLower() == "q")
                    break;

                Console.WriteLine("Enter the number of calories for the ingredient:");
                int calories = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the food group for the ingredient:");
                string foodGroup = Console.ReadLine();

                Ingredient ingredient = new Ingredient
                {
                    Name = ingredientName,
                    Calories = calories,
                    FoodGroup = foodGroup
                };

                recipe.Ingredients.Add(ingredient);
            }

            recipes.Add(recipe);
        }

        recipes.Sort((r1, r2) => r1.Name.CompareTo(r2.Name));

        Console.WriteLine("Recipes:");
        foreach (Recipe recipe in recipes)
        {
            Console.WriteLine(recipe.Name);
        }

        Console.WriteLine("Enter the name of the recipe to display:");
        string recipeToDisplay = Console.ReadLine();

        Recipe selectedRecipe = recipes.Find(r => r.Name == recipeToDisplay);
        if (selectedRecipe != null)
        {
            Console.WriteLine($"Ingredients of {selectedRecipe.Name}:");
            foreach (Ingredient ingredient in selectedRecipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Name} ({ingredient.Calories} calories, {ingredient.FoodGroup})");
            }

            int totalCalories = selectedRecipe.CalculateTotalCalories();
            Console.WriteLine($"Total calories: {totalCalories}");

            if (totalCalories > 300)
            {
                Console.WriteLine("This recipe exceeds 300 calories.");
            }
        }
        else
        {
            Console.WriteLine("Recipe not found.");
        }

        Console.ReadLine();
    }
}
