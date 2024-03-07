using GP.Common.DTO;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IFlashcardService
    {
        public List<FlashcardDTO> GetFlashcardByCreditId(CreditReq creditReq);
        //public List<LearnDTO> GetFlashcardByCreditId(CreditReq creditReq);
    }
}
