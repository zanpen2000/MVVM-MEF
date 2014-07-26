using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Method", "Subtract")]
    public class SubtractModel : IOperation
    {
        
        public void Operate()
        {
            this.Metadata.Result = this.Metadata.Input1 - this.Metadata.Input2;
        }

        public SubtractModel()
        {
            
        }
        [Import]
        public MetaNumber Metadata
        {
            get;
            set;
        }
    }
}
