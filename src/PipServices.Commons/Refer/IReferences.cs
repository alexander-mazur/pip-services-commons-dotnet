using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public interface IReferences
    {
        void Set(IDescribable reference);
        void Set(Descriptor descriptor, object reference);
        void Remove(object reference);
        List<object> GetAll();
        List<object> GetOptional(Descriptor descriptor);
        List<object> GetRequired(Descriptor descriptor);
        object GetOneOptional(Descriptor descriptor);
        object GetOneRequired(Descriptor descriptor);
        object GetOneBefore(object reference, Descriptor descriptor);
    }
}