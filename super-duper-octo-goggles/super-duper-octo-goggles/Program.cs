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

            decimal _loan_amount = Convert.ToInt32(args[1]);

            int _loan_value_month = Convert.ToInt32(ConfigurationSettings.AppSettings["loan_value_month"]);

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

            decimal[][] _jagged = new decimal[][] { };

            string[] _headers;
            string[] _contents;
            string[] _collection;

            string _id_input;
            string _rate_input;
            string _available_input;

            decimal _id_input_result;
            decimal _rate_input_result;
            decimal _available_input_result;

            int _position;

            decimal[][] _jagged_final = new decimal[][] { };

            string _find;
            string _replace;

            double _loan_year_count;
            decimal _loan_rate_value;
            decimal _monthly_repayment;
            decimal _total_repayment;

            Console.WriteLine(ConfigurationSettings.AppSettings["enter_message"]);

            if (args.Length == 2)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[0].Contains(_market_file))
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
                try
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
                            try
                            {
                                if (_collection[i].Contains("ID"))
                                {
                                    _id_column = i;
                                }

                                if (_collection[i].Contains("Rate"))
                                {
                                    _rate_column = i;
                                }

                                if (_collection[i].Contains("Available"))
                                {
                                    _available_column = i;
                                }

                                i++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }

                        for (int i = 0; i < _contents.Length; i++)
                        {
                            _id_input = _contents[i].Split(',').Skip(_id_column).FirstOrDefault();
                            _available_input = _contents[i].Split(',').Skip(_available_column).FirstOrDefault();
                            _rate_input = _contents[i].Split(',').Skip(_rate_column).FirstOrDefault();

                            _jagged[i] = new decimal[3];

                            try
                            {
                                if (!HelperCollection.IsNumeric(_id_input) == true)
                                {
                                    _id_input = HelperCollection.Cleanup(_id_input);

                                    _id_input_result = Convert.ToDecimal(_id_input);

                                    _jagged[i][0] = _id_input_result;
                                }

                                if (!HelperCollection.IsNumeric(_available_input) == true)
                                {
                                    _available_input = HelperCollection.Cleanup(_available_input);

                                    _available_input_result = Convert.ToDecimal(_available_input);

                                    _jagged[i][1] = _available_input_result;
                                }

                                if (!HelperCollection.IsNumeric(_rate_input) == true)
                                {
                                    _rate_input = HelperCollection.Cleanup(_rate_input);

                                    _rate_input_result = Convert.ToDecimal(_rate_input);

                                    _jagged[i][2] = _rate_input_result;
                                }
                            }
                            catch (Exception ex)
                            {
                                /*Console.WriteLine(ex.Message);*/ //Here I need to tidy up the _input types
                            }
                        }

                        _jagged = _jagged.OrderBy(i => i[_rate_column]).ToArray();

                        _continue = false;
                    }
                    else
                    {
                        Console.WriteLine(ConfigurationSettings.AppSettings["market_file_message"]);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            if (_continue == false)
            {
                _continue = true;

                _available_column = _available_column - 2;

                if (_continue == true)
                {
                    while (_continue == true)
                    {
                        foreach (decimal[] i in _jagged)
                        {
                            foreach (decimal e in i) //This was firstly going to be: foreach (int e = 0; e < i.Length; e++)
                            {
                                if (_continue == true)
                                {
                                    _position = Array.IndexOf(i, e);

                                    if (_position == _available_column)
                                    {
                                        if (e != 0)
                                        {
                                            decimal _temporary = e;

                                            _temporary = _temporary - 1;

                                            try
                                            {
                                                if (File.Exists(_market_file_path) == true)
                                                {
                                                    using (StreamReader _reader = new StreamReader(File.OpenRead(_market_file_path)))
                                                    {
                                                        _market_file_contents = _reader.ReadToEnd();
                                                    }

                                                    _contents = _market_file_contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                                    _find = Array.Find(_contents, element => element.Contains(i[0].ToString()) && element.Contains(i[1].ToString()) && element.Contains(i[2].ToString()));

                                                    _replace = _find.Replace(e.ToString(), _temporary.ToString());

                                                    using (StreamWriter _writer = new StreamWriter(File.OpenWrite(_market_file_path)))
                                                    {
                                                        for (int c = 0; c < _contents.Length; c++)
                                                        {
                                                            _writer.WriteLine(_contents[c].Replace(_find, _replace));
                                                        }

                                                        _writer.Dispose();

                                                        _writer.Close();
                                                    }

                                                    _loan_year_count = _loan_value_month / 12;

                                                    _total_repayment = HelperCollection.DoCalculation(_loan_amount, i[_rate_column], 365, _loan_year_count);

                                                    _monthly_repayment = _total_repayment / _loan_value_month;

                                                    Console.WriteLine(ConfigurationSettings.AppSettings["rate_message"] + " " + i[_rate_column] + ConfigurationSettings.AppSettings["rate_symbol_message"]);
                                                    Console.WriteLine(ConfigurationSettings.AppSettings["monthly_repayment_message"] + " " + _monthly_repayment);
                                                    Console.WriteLine(ConfigurationSettings.AppSettings["total_repayment_message"] + " " + _total_repayment);

                                                    _continue = false;

                                                    break;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }

                            Console.WriteLine();
                        }
                    }
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