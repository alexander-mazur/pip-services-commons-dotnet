using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipServices.Commons.Refer
{
    public class Locator
    {
        public string Group { get; }
        public string Type { get; }
        public string Id { get; }
        public string Version { get; }

        public Locator(string group, string type, string id, string version)
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

        public bool Match(Locator locator)
        {
            // Matching groups
            if (Group != null && locator.Group != null
                && !Group.Equals(locator.Group))
            {
                return false;
            }

            // Matching types
            if (Type != null && locator.Type != null
                && !Type.Equals(locator.Type))
            {
                return false;
            }

            // Matching ids
            if (Id != null && locator.Id != null
                && !Id.Equals(locator.Id))
            {
                return false;
            }

            // Matching versions
            if (Version != null && locator.Version != null
                && !Version.Equals(locator.Version))
            {
                return false;
            }

            // All checks are passed...
            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Locator)
            {
                return Match((Locator)obj);
            }
            return false;
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