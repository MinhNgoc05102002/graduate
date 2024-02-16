using GP.Common.DTO;
using GP.Common.Models;
using GP.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.IRepository
{
    public interface IAccountRepository
    {
        public Account Create(Account account);
        public Account GetByUsername(string username);
        public Account GetByEmail(string email);

        public Account GetByUsernameOrEmail(string username);

        public void UpdateAccount(Account account);


        /// <summary>
        /// Lấy danh sách Credit tìm kiếm của tất cả user, sắp xếp theo user nổi tiếng nhất 
        /// Điểm = sum(num_learn_credit) + sum(num_join_class)
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<AccountDTO> GetListAccountByFilter(SearchBase searchBase);
        public PaginatedResultBase<Notification> GetNotiByUser(SearchBase searchBase, string username);
    }
}
