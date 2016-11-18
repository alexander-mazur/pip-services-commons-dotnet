using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipServices.Commons.Build
{
    public interface IFactory
    {
        bool CanCreate(object locator);
        object Create(object locator);
    }
}
