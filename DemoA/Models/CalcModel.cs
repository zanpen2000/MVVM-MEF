using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{
    [Export(typeof(ICalc))]
    public class CalcModel : ICalc
    {
        [ImportMany]
        public IEnumerable<Lazy<IOperation, IOperationData>> Operations;

        public void DoCalc(string method)
        {
            foreach (var oper in Operations)
            {
                
                if (oper.Metadata.Method.Equals(method))
                    oper.Value.Operate();
            }
        }
    }
}
