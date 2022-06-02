using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DividendScrapper.Exceptions
{
    public class TextScrapException : Exception
    {
        public TextScrapException() { }

        public TextScrapException(string message) : base(message) { }

        public TextScrapException(string message, Exception inner) : base(message, inner) { }
    }
}
