﻿using System.Text;

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

        private bool MatchField(string field1, string field2)
        {
            return field1 == null
                   || field2 == null
                   || field1.Equals(field2);
        }

        public bool Match(Descriptor descriptor)
        {
            return MatchField(Group, descriptor.Group)
                   && MatchField(Type, descriptor.Type)
                   && MatchField(Id, descriptor.Id)
                   && MatchField(Version, descriptor.Version);
        }

        private bool ExactMatchField(string field1, string field2)
        {
            if (field1 == null && field2 == null)
                return true;
            if (field1 == null || field2 == null)
                return false;
            return field1.Equals(field2);
        }

        public bool ExactMatch(Descriptor descriptor)
        {
            return ExactMatchField(Group, descriptor.Group)
                && ExactMatchField(Type, descriptor.Type)
                && ExactMatchField(Id, descriptor.Id)
                && ExactMatchField(Version, descriptor.Version);
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
            builder.Append(Group ?? "*")
                .Append(":").Append(Type ?? "*")
                .Append(":").Append(Id ?? "*")
                .Append(":").Append(Version ?? "*");
            return builder.ToString();
        }
    }
}