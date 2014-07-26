using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoA.Models
{
    public interface IOperation
    {
        MetaNumber Metadata { get; set; }
        void Operate();
    }
}
