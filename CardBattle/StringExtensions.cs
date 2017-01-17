using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardBattle
{
    public static class StringExtensions
    {
        public static string With(this string format, params object[] parameters)
        {
            return String.Format(format, parameters);
        }
    }
}
