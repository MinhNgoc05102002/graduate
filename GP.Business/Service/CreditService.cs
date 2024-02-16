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
        private readonly AuthHelper authHelper;

        public CreditService(ICreditRepository creditRepository, AuthHelper authHelper)
        {
            this.creditRepository = creditRepository;
            this.authHelper = authHelper;
        }

        public PaginatedResultBase<CreditDTO> GetCreditByFilter(SearchBase searchBase)
        {
            var result = creditRepository.GetListCreditByFilter(searchBase);
            return result;
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


    }
}
