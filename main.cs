using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("BMI Calculator");
        Console.WriteLine("---------------");

        // Input: Get weight in kilograms from the user
        Console.Write("Enter your weight in kilograms: ");
        double weight = Convert.ToDouble(Console.ReadLine());

        // Input: Get height in meters from the user
        Console.Write("Enter your height in meters: ");
        double height = Convert.ToDouble(Console.ReadLine());

        // Calculate BMI
        double bmi = CalculateBMI(weight, height);

        // Display the calculated BMI
        Console.WriteLine($"Your BMI is: {bmi:F2}");

        // Interpret the BMI value
        InterpretBMI(bmi);

        Console.ReadLine(); // Pause to see the result
    }

    // Function to calculate BMI
    static double CalculateBMI(double weight, double height)
    {
        // Formula for BMI: weight (kg) / (height (m) * height (m))
        return weight / (height * height);
    }

    // Function to interpret BMI
    static void InterpretBMI(double bmi)
    {
        Console.Write("Interpretation: ");
        if (bmi < 18.5)
        {
            Console.WriteLine("Underweight");
        }
        else if (bmi >= 18.5 && bmi < 25)
        {
            Console.WriteLine("Normal Weight");
        }
        else if (bmi >= 25 && bmi < 30)
        {
            Console.WriteLine("Overweight");
        }
        else
        {
            Console.WriteLine("Obese");
        }
    }
}
