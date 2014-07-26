using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Method","Multiply")]
    public class Multiply:IOperation
    {
        [Import]
        public MetaNumber Metadata
        {
            get;
            set;
        }

        public void Operate()
        {
            this.Metadata.Result = this.Metadata.Input1 * this.Metadata.Input2;
        }
    }
}
