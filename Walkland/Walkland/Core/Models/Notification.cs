using System;
namespace Walkland.Core.Models
{
    public class Notification
    {
        public long Id { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public string ValidUpToDateTimeUtc { get; set; }
    }
}
