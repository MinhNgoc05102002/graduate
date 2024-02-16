using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class NotificationDTO
    {
        public string NotiId { get; set; } = null!;

        public string? Content { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool? IsDeleted { get; set; }

        public string? NotiType { get; set; }

        public bool? Seen { get; set; }

        public string? Link { get; set; }

        public string? Username { get; set; }

    }
}
