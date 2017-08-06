using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            string market_file = "market.csv";
            int loan_amount = 0;

            if (args.Length == 2) //This was originally going to be: args.Length != 0 && args.Length == 2;
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[0].Contains(market_file)) //This was firstly going to be: args[1] == "market.csv"; this could be added to the Helper Collection; this was secondly going to be: string.Compare(args[0], market_file, true) == 0;
                    {
                        if (HelperCollection.IsNumeric(args[1]) == true)
                        {
                            loan_amount = Convert.ToInt32(args[1]);

                            i++;
                        }
                        else
                        {
                            Console.WriteLine(ConfigurationSettings.AppSettings["loan_amount_message"]);

                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(ConfigurationSettings.AppSettings["market_file_message"]);

                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine(ConfigurationSettings.AppSettings["argument_message"]);
            }

            if (File.Exists(args[0]) == true)
            {
                string market_file_path = args[0];
                string market_file_contents = "";

                using (StreamReader _reader = new StreamReader(File.OpenRead(market_file_path)))
                {
                    market_file_contents = _reader.ReadToEnd();
                }

                List<string[]> _list = new List<string[]>();

                string[] _separator = market_file_contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                foreach (string _line in _separator)
                {
                    _list.Add(_line.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                }

                int _column = 0;
                int _row = 0;

                Console.WriteLine("Column{0}, Row{1} = \"{2}\"", _column, _row, _list[_column][_row]);

                /*_column = 4;
                _row = 2;

                Console.WriteLine("Column{0}, Row{1} = \"{2}\"", _column, _row, _list[_column][_row]);*/
            }
            else
            {
                Console.WriteLine(ConfigurationSettings.AppSettings["file_message"]);
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