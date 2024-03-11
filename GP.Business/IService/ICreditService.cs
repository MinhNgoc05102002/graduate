using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface ICreditService
    {
        /// <summary>
        /// Lấy danh sách Credit của một user đang đăng nhập
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<CreditDTO> GetCreditByUser(SearchBase searchBase);


        /// <summary>
        /// Lấy danh sách Credit tìm kiếm của tất cả user, sắp xếp theo credit có nhiều ng học nhất
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<CreditDTO> GetCreditByFilter(SearchBase searchBase);
        public CreditDTO GetCreditById(string creditId);

        public PaginatedResultBase<CreditDTO> GetListCreditByClass(SearchBase searchBase);
    }
}
