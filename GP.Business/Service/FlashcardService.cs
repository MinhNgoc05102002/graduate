using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.DAL.IRepository;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IFlashcardRepository flashcardRepository;
        private readonly AuthHelper authHelper;
        private readonly MappingProfile _mapper;

        public FlashcardService(IFlashcardRepository flashcardRepository, AuthHelper authHelper, MappingProfile mapper)
        {
            this.flashcardRepository = flashcardRepository;
            this.authHelper = authHelper;
            _mapper = mapper;
        }

        public List<FlashcardDTO> GetFlashcardByCreditId(string creditId, string username)
        {
            List<Flashcard> flashcards = flashcardRepository.GetFlashcardByCreditId(creditId, username);

            List<FlashcardDTO> flashcardDTOs = _mapper.MapFlashcardsToDTOs(flashcards);

            return flashcardDTOs;
        }
    }
}
