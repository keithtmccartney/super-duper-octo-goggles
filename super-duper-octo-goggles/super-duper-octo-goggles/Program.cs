using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using super_duper_octo_goggles.Helpers;

namespace super_duper_octo_goggles
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2) //This was originally going to be: args.Length != 0 && args.Length == 2;
            {
                var market_file = "";
                int loan_amount = 0;

                for (int i = 0; i < args.Length; i++)
                {
                    if (string.Compare(args[0], "market.csv", true) == 0) //This was originally going to be: args[1] == "market.csv"; this could be added to the Helper Collection;
                    {
                        if (HelperCollection.IsNumeric(args[1]) == true)
                        {
                            i++;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a correct loan amount, e.g. cmd> quote.exe market.csv 1500.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a correct market file, e.g. cmd> quote.exe market.csv 1500.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a correct amount of arguments; provide [1] a market file alongside [2] a loan amount, e.g. cmd> quote.exe market.csv 1500.");
            }
        }

        public int Add(int first, int second)
        {
            return first + second;
        }

        public int Subtract(int first, int second)
        {
            return first - second;
        }

        public int Multi(int first, int second)
        {
            return first * second;
        }

        public int Divide(int first, int second)
        {
            return first / second;
        }
    }
}