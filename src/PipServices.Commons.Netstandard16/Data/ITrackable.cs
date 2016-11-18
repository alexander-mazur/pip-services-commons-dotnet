using System;

namespace PipServices.Commons.Data
{
    public interface ITrackable
    {
        DateTime CreatedTime { get; }
        DateTime LastChangeTime { get; }
        bool IsDeleted { get; }
    }
}