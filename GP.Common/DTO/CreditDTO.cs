using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class CreditDTO
    {
        public CreditDTO(string creditId, DateTime? createdAt, string? name, string? createdBy, int? countFlashcard, int? countLearnCal, string? avatar)
        {
            CreditId = creditId;
            CreatedAt = createdAt;
            Name = name;
            CreatedBy = createdBy;
            CountFlashcard = countFlashcard;
            CountLearnCal = countLearnCal;
            Avatar = avatar;
        }

        public CreditDTO(string creditId, string? name, string? description, DateTime? createdAt, string? createdBy, bool? isDeleted, int? countReport, int? countLearn, ICollection<AccountLearnCredit> accountLearnCredits, ICollection<Flashcard> flashcards, ICollection<Category> categories, ICollection<Class> classes, ICollection<Folder> folders, int? countFlashcard, int? countLearnCal)
        {
            CreditId = creditId;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            IsDeleted = isDeleted;
            CountReport = countReport;
            CountLearn = countLearn;
            AccountLearnCredits = accountLearnCredits;
            Flashcards = flashcards;
            Categories = categories;
            Classes = classes;
            Folders = folders;
            CountFlashcard = countFlashcard;
            CountLearnCal = countLearnCal;
        }

        public string CreditId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public int? CountReport { get; set; }

        public int? CountLearn { get; set; }

        public ICollection<AccountLearnCredit> AccountLearnCredits { get; set; } = new List<AccountLearnCredit>();

        public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Class> Classes { get; set; } = new List<Class>();

        public ICollection<Folder> Folders { get; set; } = new List<Folder>();

        // add new
        public int? CountFlashcard { get; set; }
        public int? CountLearnCal { get; set; }
        public string? Avatar { get; set; }
    }
}
