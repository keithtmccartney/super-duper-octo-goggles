using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
            string _market_file = ConfigurationSettings.AppSettings["market_file"];
            int _loan_amount = Convert.ToInt32(args[1]);
            int _loan_value_minimum = Convert.ToInt32(ConfigurationSettings.AppSettings["loan_value_minimum"]);
            int _loan_value_maximum = Convert.ToInt32(ConfigurationSettings.AppSettings["loan_value_maximum"]);
            bool _continue = false;
            int _rate_column = 0;
            string _market_file_path = args[0];
            string _market_file_contents;
            string _market_file_headers;
            List<string[]> _list_headers = new List<string[]>();
            List<int> _list_contents = new List<int>(); //This was firstly going to be: List<string[]> _list_contents = new List<string[]>(); see the commented-out Loop below with '_list_contents.Add(_line_contents.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));' logic;
            string[] _headers;
            string[] _contents;
            string[] _collection;

            Console.WriteLine(ConfigurationSettings.AppSettings["enter_message"]);

            if (args.Length == 2) //This was originally going to be: args.Length != 0 && args.Length == 2;
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[0].Contains(_market_file)) //This was firstly going to be: args[1] == "market.csv"; this could be added to the Helper Collection; this was secondly going to be: string.Compare(args[0], market_file, true) == 0;
                    {
                        if (HelperCollection.IsNumeric(args[1]) == true)
                        {
                            if (_loan_amount < _loan_value_minimum || _loan_amount > _loan_value_maximum)
                            {
                                Console.WriteLine(ConfigurationSettings.AppSettings["loan_value_message"]);

                                break;
                            }
                            else
                            {
                                Console.WriteLine(ConfigurationSettings.AppSettings["requested_amount_message"] + _loan_amount);

                                i++;

                                _continue = true;
                            }
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

            if (_continue == true)
            {
                if (File.Exists(_market_file_path) == true)
                {
                    using (StreamReader _reader = new StreamReader(File.OpenRead(_market_file_path)))
                    {
                        _market_file_headers = _reader.ReadLine();

                        _market_file_contents = _reader.ReadToEnd();
                    }

                    _headers = _market_file_headers.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    _contents = _market_file_contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    foreach (string _line_headers in _headers)
                    {
                        _list_headers.Add(_line_headers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    }

                    _collection = _market_file_headers.Split(',');

                    for (int i = 0; i < _collection.Length;)
                    {
                        if (_collection[i].Contains("Rate"))
                        {
                            _rate_column = i;

                            break;
                        }

                        i++;
                    }

                    /*foreach (string _line_contents in _contents)
                    {
                        _list_contents.Add(_line_contents.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    }*/

                    for (int i = 0; i < _contents.Length; i++)
                    {
                        var _result = _contents[i].Split(',').Skip(_rate_column).FirstOrDefault();

                        if (!HelperCollection.IsNumeric(_result) == true)
                        {
                            HelperCollection.Cleanup(_result);
                        }

                        _list_contents.Add(Convert.ToInt32(_result));
                    }
                }
                else
                {
                    Console.WriteLine(ConfigurationSettings.AppSettings["market_file_message"]);
                }
            }

            Console.WriteLine(ConfigurationSettings.AppSettings["exit_message"]);
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