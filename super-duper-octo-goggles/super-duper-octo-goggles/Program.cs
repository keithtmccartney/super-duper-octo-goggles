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
            int _id_column = 0;
            int _rate_column = 0;
            int _available_column = 0;
            string _market_file_path = args[0];
            string _market_file_contents;
            string _market_file_headers;
            List<string[]> _list_headers = new List<string[]>();
            decimal[][] _jagged; //This was firstly going to be: List<string[]> _list_contents = new List<string[]>(); see the commented-out Loop below with '_list_contents.Add(_line_contents.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));' logic; this was secondly going to be: List<decimal> _list_contents = new List<decimal>();
            string[] _headers;
            string[] _contents;
            string[] _collection;
            string _id_input;
            string _rate_input;
            string _available_input;
            decimal _id_input_result;
            decimal _rate_input_result;
            decimal _available_input_result;

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

                    _jagged = new decimal[_contents.Length][];

                    foreach (string _line_headers in _headers)
                    {
                        _list_headers.Add(_line_headers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
                    }

                    _collection = _market_file_headers.Split(',');

                    for (int i = 0; i < _collection.Length;)
                    {
                        if (_collection[i].Contains("ID"))
                        {
                            _id_column = i;

                            /*break;*/
                        }

                        if (_collection[i].Contains("Rate"))
                        {
                            _rate_column = i;

                            /*break;*/
                        }

                        if (_collection[i].Contains("Available"))
                        {
                            _available_column = i;

                            /*break;*/
                        }

                        i++;
                    }

                    for (int i = 0; i < _contents.Length; i++) //This was firstly going to be: foreach (string _line_contents in _contents) { _list_contents.Add(_line_contents.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)); };
                    {
                        _id_input = _contents[i].Split(',').Skip(_id_column).FirstOrDefault();
                        _rate_input = _contents[i].Split(',').Skip(_rate_column).FirstOrDefault();
                        _available_input = _contents[i].Split(',').Skip(_available_column).FirstOrDefault();

                        _jagged[i] = new decimal[3];

                        try
                        {
                            if (!HelperCollection.IsNumeric(_id_input) == true)
                            {
                                _id_input = HelperCollection.Cleanup(_id_input);

                                _id_input_result = Convert.ToDecimal(_id_input);

                                /*_list_contents.Insert(i, _result_id_input); //This was firstly going to be: _list_contents.Add(Convert.ToInt32(_result));*/

                                /*_list_contents.Add(_result_id_input);*/

                                _jagged[i][0] = _id_input_result;
                            }

                            if (!HelperCollection.IsNumeric(_rate_input) == true)
                            {
                                _rate_input = HelperCollection.Cleanup(_rate_input);

                                _rate_input_result = Convert.ToDecimal(_rate_input);

                                /*_list_contents.Insert(i, _result_rate_input); //This was firstly going to be: _list_contents.Add(Convert.ToInt32(_result));*/

                                /*_list_contents.Add(_result_rate_input);*/

                                _jagged[i][1] = _rate_input_result;
                            }

                            if (!HelperCollection.IsNumeric(_available_input) == true)
                            {
                                _available_input = HelperCollection.Cleanup(_available_input);

                                _available_input_result = Convert.ToDecimal(_available_input);

                                /*_list_contents.Insert(i, _result_available_input); //This was firstly going to be: _list_contents.Add(Convert.ToInt32(_result));*/

                                /*_list_contents.Add(_result_available_input);*/

                                _jagged[i][2] = _available_input_result;
                            }

                            /*_list_contents.Add(_result); //This was firstly going to be: _list_contents.Add(Convert.ToInt32(_result));*/
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        /*if (i != _id_column && i != _rate_column)
                        {
                            _contents[i].Remove(i); //This was firstly going to be: _input = _contents[i].Split(',').Skip(_rate_column).FirstOrDefault();
                        }*/

                        /*if (i != _id_column && i != _rate_column)
                        {
                            _id_rate.Add(_contents[i]); // FIX THIS FROM STORING RECORD 0 AND RECORD 2 TO COLUMN 0 AND COLUMN 2
                        }*/
                    }

                    /*decimal[] s = _list_contents.ToArray();*/

                    /*_list_contents.Sort();*/
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