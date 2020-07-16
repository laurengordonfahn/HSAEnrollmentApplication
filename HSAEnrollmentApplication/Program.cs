using System;

namespace HSAEnrollmentApplication
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            

            Console.WriteLine("At any time you can press the ESC key to exit out of the program, your processes will not complete if you exit early.");

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                //Interactive Console get csv file path and intial date
                new EnrollmentInteractiveConsole().EnrollmentStartInteractiveConsole();


                return;
            }

        }
    }
}
