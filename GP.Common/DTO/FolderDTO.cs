using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class FolderDTO
    {
        public FolderDTO()
        {
        }

        public FolderDTO(string folderId, string? name, string? description, DateTime? createdAt, string? createdBy, int? countCredit)
        {
            FolderId = folderId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            CountCredit = countCredit;
        }

        public string FolderId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }
        public virtual ICollection<CreditDTO> Credits { get; set; }

        // thêm thông tin trả về 
        public int? CountCredit { get; set; }
        public string? Avatar { get; set; }

    }
}
