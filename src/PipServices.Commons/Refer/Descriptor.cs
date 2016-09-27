using System.Text;

namespace PipServices.Commons.Refer
{
    public class Descriptor
    {
        public string Group { get; }
        public string Type { get; }
        public string Id { get; }
        public string Version { get; }

        public Descriptor(string group, string type, string id, string version)
        {
            if ("*".Equals(group)) group = null;
            if ("*".Equals(type)) type = null;
            if ("*".Equals(id)) id = null;
            if ("*".Equals(version)) version = null;

            Group = group;
            Type = type;
            Id = id;
            Version = version;
        }

        public bool Match(Descriptor descriptor)
        {
            // Matching groups
            if (Group != null && descriptor.Group != null
                && !Group.Equals(descriptor.Group))
            {
                return false;
            }

            // Matching types
            if (Type != null && descriptor.Type != null
                && !Type.Equals(descriptor.Type))
            {
                return false;
            }

            // Matching ids
            if (Id != null && descriptor.Id != null
                && !Id.Equals(descriptor.Id))
            {
                return false;
            }

            // Matching versions
            if (Version != null && descriptor.Version != null
                && !Version.Equals(descriptor.Version))
            {
                return false;
            }

            // All checks are passed...
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Descriptor)
            {
                return Match((Descriptor)obj);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Group != null ? Group : "*")
                .Append(":").Append(Type != null ? Type : "*")
                .Append(":").Append(Id != null ? Id : "*")
                .Append(":").Append(Version != null ? Version : "*");
            return builder.ToString();
        }
    }
}