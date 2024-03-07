using GP.Common.DTO;
using GP.DAL.IRepository;
using GP.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly QuizletDbContext _dbContext;

        public FlashcardRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Flashcard> GetFlashcardByCreditId(CreditReq creditReq)
        {
            List<Flashcard> flashcards = _dbContext.Flashcards
                                            .Include(f => f.Learns.Where(l => l.Username == creditReq.Username))
                                            .Where(f =>
                                                f.CreditId == creditReq.CreditId
                                            ).ToList();
            return flashcards;
        }

        //public List<Learn> GetFlashcardByCreditId(CreditReq creditReq)
        //{
        //    List<Learn> learns = _dbContext.Learns
        //                                   .Include(l => l.Flashcard)
        //                                   .Where(l => l.Username == creditReq.Username && l.Flashcard.CreditId == creditReq.CreditId)
        //                                   .ToList();
        //    return learns;
        //}
    }
}
