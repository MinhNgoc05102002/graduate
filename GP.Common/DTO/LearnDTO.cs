using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Common.DTO
{
    public class LearnDTO
    {
        public string Username { get; set; } = null!;

        public string FlashcardId { get; set; } = null!;

        public string? Process { get; set; }

        public DateTime? LastLearnedDate { get; set; }

        public bool? RecentWrongExam { get; set; }

        public bool? RecentWrongLearn { get; set; }

        //public FlashcardDTO Flashcard { get; set; } = null!;

    }
}
