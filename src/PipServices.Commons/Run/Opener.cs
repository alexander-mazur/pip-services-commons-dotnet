using System.Collections.Generic;

namespace PipServices.Commons.Run
{
    public class Opener
    {
        public void Open(string correlationId, IEnumerable<object> components)
        {
            if (components == null) return;

            foreach (var component in components)
            {
                var openable = component as IOpenable;
                if (openable != null)
                {
                    openable.Open(correlationId);
                }
            }
        }
    }
}
