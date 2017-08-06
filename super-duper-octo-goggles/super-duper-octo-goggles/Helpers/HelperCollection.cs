using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace super_duper_octo_goggles.Helpers
{
    public static class HelperCollection
    {
        public static bool IsNumeric(object value)
        {
            try
            {
                int _value;

                bool _IsNumber = int.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out _value);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}