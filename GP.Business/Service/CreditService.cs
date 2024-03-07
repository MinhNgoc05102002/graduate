using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class CreditService : ICreditService
    {

        private readonly ICreditRepository creditRepository;
        private readonly IAccountRepository accountRepository;
        private readonly AuthHelper authHelper;
        private readonly MappingProfile _mapper;

        public CreditService(ICreditRepository creditRepository, AuthHelper authHelper, MappingProfile mapper, IAccountRepository accountRepository)
        {
            this.creditRepository = creditRepository;
            this.authHelper = authHelper;
            _mapper = mapper;
            this.accountRepository = accountRepository;
        }

        public PaginatedResultBase<CreditDTO> GetCreditByFilter(SearchBase searchBase)
        {
            var result = creditRepository.GetListCreditByFilter(searchBase);
            return result;
        }

        public CreditDTO GetCreditById(string creditId)
        {
            Credit credit = creditRepository.GetCreditById(creditId);
            CreditDTO creditDTO = _mapper.MapCreditToDTO(credit);

            if (credit != null)
            {
                Account account = accountRepository.GetByUsername(credit.CreatedBy);
                creditDTO.Avatar = account.Avatar;

                string currentUsername = authHelper.GetCurrentUsername();
                creditDTO.IsLearned = creditRepository.IsUserLearnedCredit(creditId, currentUsername);
            }

            return creditDTO;
        }

        public PaginatedResultBase<CreditDTO> GetCreditByUser(SearchBase searchBase)
        {
            string currentUsername = searchBase.Username;
            if (searchBase.Username == string.Empty || searchBase.Username == null){
                currentUsername = authHelper.GetCurrentUsername();
            }
            var result = creditRepository.GetListCreditByUser(searchBase, currentUsername);
            return result;
        }

        //public PaginatedResultBase<CreditDTO> GetListCreditByFolder(string folderId)
        //{
        //    var result = creditRepository.GetListCreditByFolder(folderId);
        //    return result;
        //}

    }
}
