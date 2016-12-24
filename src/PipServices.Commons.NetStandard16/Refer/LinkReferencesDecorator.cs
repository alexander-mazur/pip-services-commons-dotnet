using System.Collections.Generic;

namespace PipServices.Commons.Refer
{
    public class LinkReferencesDecorator : ReferencesDecorator
    {
        public LinkReferencesDecorator(IReferences baseReferences = null, IReferences parentReferences = null)
            : base(baseReferences, parentReferences)
        {
            LinkEnabled = true;
        }

        public bool LinkEnabled { get; set; }

        public override void Put(object component, object locator = null)
        {
            base.Put(component, locator);

            if (LinkEnabled)
                Referencer.SetReferencesForOne(ParentReferences, component);
        }

        public override object Remove(object locator)
        {
            var component = base.Remove(locator);

            if (LinkEnabled)
                Referencer.UnsetReferencesForOne(component);

            return component;
        }

        public override List<object> RemoveAll(object locator)
        {
            var components = base.RemoveAll(locator);

            if (LinkEnabled)
                Referencer.UnsetReferences(components);

            return components;
        }

    }
}
