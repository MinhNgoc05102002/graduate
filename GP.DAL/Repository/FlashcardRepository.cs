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
        public List<Flashcard> GetFlashcardByCreditId(string creditId, string username)
        {
            List<Flashcard> flashcards = _dbContext.Flashcards
                                            .Include(f => f.Learns.Where(l => l.Username == username))
                                            .Where(f => 
                                                f.CreditId == creditId
                                            ).ToList();
            return flashcards;
        }
    }
}
