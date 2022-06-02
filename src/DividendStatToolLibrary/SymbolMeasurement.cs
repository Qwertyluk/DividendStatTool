using DividendScrapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DividendStatTool
{
    internal class SymbolMeasurement
    {
        private readonly string symbol;
        private readonly Measurement[] measurements;

        public SymbolMeasurement(string symbol, Measurement[] measurements)
        {
            this.symbol = symbol;
            this.measurements = measurements;
        }
    }
}
