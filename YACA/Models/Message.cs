using System;
using System.Collections.Generic;

namespace YACA.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public DateTime SendTime { get; set; }
        public int FromPersonId { get; set; }
        public bool PrivateMessage { get; set; }
        public int? ToPersonId { get; set; }
        public DateTime? ExpireAt { get; set; }
        public string Category { get; set; }

        public virtual Person FromPerson { get; set; }
    }
}
