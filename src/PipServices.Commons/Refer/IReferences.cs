using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public interface IReferences
    {
        void Set(ILocateable reference);
        void Set(Locator locator, object reference);
        void Remove(object reference);
        List<object> GetAll();
        List<object> GetOptional(Locator locator);
        List<object> GetRequired(Locator locator);
        object GetOneOptional(Locator locator);
        object GetOneRequired(Locator locator);
        object GetOneBefore(object reference, Locator locator);
    }
}