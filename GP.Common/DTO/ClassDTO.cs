using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class ClassDTO
    {
        public ClassDTO() { }

        public ClassDTO(string classId, string? name, string? description, DateTime? createdAt, string? createdBy, bool? acceptEdit, int? countJoinDTO, int? countCredit)
        {
            ClassId = classId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            AcceptEdit = acceptEdit;
            CountJoinDTO = countJoinDTO;
            CountCredit = countCredit;
        }

        public string ClassId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public bool? AcceptEdit { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Status { get; set; }

        public int? CountReport { get; set; }

        public int? CountJoin { get; set; }

        // Thêm trường để show lên giao diện
        public int? CountJoinDTO { get; set; }

        public int? CountCredit { get; set; }

    }
}
