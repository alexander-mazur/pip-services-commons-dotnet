using System.Collections.Generic;

namespace PipServices.Commons.Run
{
    public class Closer
    {
        public void Close(string correlationId, IEnumerable<object> components)
        {
            if (components == null) return;

            foreach(var component in components)
            {
                var closable = component as IClosable;
                if(closable != null)
                {
                    closable.Close(correlationId);
                }
            }
        }
    }
}
