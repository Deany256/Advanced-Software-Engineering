using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("BMI Calculator");
        Console.WriteLine("---------------");

        Console.Write("Enter 'I' for Imperial or 'M' for Metric");
        string isMetric = Console.ReadLine();

        // Input: Get weight in kilograms from the user
        Console.Write("Enter your weight in kilograms/pounds: ");
        double weight = Convert.ToDouble(Console.ReadLine());

        // Input: Get height in meters from the user
        Console.Write("Enter your height in meters/inches: ");
        double height = Convert.ToDouble(Console.ReadLine());

        if (isMetric = "M") {
            double bmi = CalculateMBMI(weight, height);
        } else if (isMetric = "I") {
            double bmi = CalculateIBMI(weight, height);
        } else {
            Main()
        }

        // Display the calculated BMI
        Console.WriteLine($"Your BMI is: {bmi:F2}");

        InterpretBMI(bmi);

        Console.ReadLine(); // Pause to see the result
    }

    // Function to calculate BMI
    static double CalculateMBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    static double CalculateIBMI(double weight, double height)
    {
        return weight / (height * height * 703);
    }

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