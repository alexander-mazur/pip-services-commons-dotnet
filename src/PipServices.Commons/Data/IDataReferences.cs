using System.Collections;
using System.Collections.Generic;

namespace PipServices.Commons.Data
{
    public interface IDataReferences
    {
        IList<T> GetAs<T>();
        IList Get(string name);
        void Set(string name, IList data);
    }
}
