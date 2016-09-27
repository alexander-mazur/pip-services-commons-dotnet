using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public interface IReferences
    {
        void Put(object reference);
        void Put(object locator, object reference);
        object Remove(object locator);
        List<object> GetAll();
        List<object> GetOptional(object locator);
        List<object> GetRequired(object locator);
        object GetOneOptional(object locator);
        object GetOneRequired(object locator);
        object GetOneBefore(object reference, object locator);
    }
}