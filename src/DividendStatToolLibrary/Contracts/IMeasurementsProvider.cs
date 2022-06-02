using DividendStatTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DividendStatToolLibrary.Contracts
{
    internal interface IMeasurementsProvider
    {
        SymbolMeasurement[] GetMeasurements();
    }
}
