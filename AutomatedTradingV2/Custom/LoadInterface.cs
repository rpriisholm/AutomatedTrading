using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomatedTradingV2.Custom
{
    public interface LoadInterface<T_Output, T_Load>
    {
        T_Output LoadOnDemand(T_Load load);
    }
}
