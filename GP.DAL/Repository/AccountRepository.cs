using GP.Common.DTO;
using GP.Common.Models;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly QuizletDbContext _dbContext;
        public AccountRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Auth
        public Account Create(Account account)
        {
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();
            return account;
        }

        public Account GetByEmail(string email)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.Email == email);
        }

        public Account GetByUsername(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.Username == username);
        }

        public Account GetByUsernameOrEmail(string username)
        {
            return _dbContext.Accounts.SingleOrDefault(account => account.Username == username || account.Email == username);
        }

        public void UpdateAccount(Account account)
        {
            _dbContext.Accounts.Update(account);
            _dbContext.SaveChanges();
        }
        #endregion

        public PaginatedResultBase<AccountDTO> GetListAccountByFilter(SearchBase searchBase)
        {
            PaginatedResultBase<AccountDTO> result = new PaginatedResultBase<AccountDTO>();

            var listAccount = _dbContext.Accounts;
            var listCredit = _dbContext.Credits;
            var listClass = _dbContext.Classes;

            List<AccountDTO> listTopAccount = (List<AccountDTO>)(from acc in (from acc in listAccount
                                                        join credit in listCredit on acc.Username equals credit.CreatedBy into t
                                                        from credit in t.DefaultIfEmpty()
                                                        where acc.Username.ToLower().Contains(searchBase.SearchText.ToLower()) && credit.IsDeleted == false
                                                        group new { acc, credit } by acc.Username into gr
                                                        select new
                                                        {
                                                            Username = gr.Key,
                                                            Email = gr.Select(x => x.acc.Email).FirstOrDefault(),
                                                            Avatar = gr.Select(x => x.acc.Avatar).FirstOrDefault(),
                                                            CreatedAt = gr.Select(x => x.acc.CreatedAt).FirstOrDefault(),
                                                            numLearn = gr.SelectMany(x => x.credit.AccountLearnCredits).Count(),
                                                            CountCredit = gr.Count(x => x.credit.CreditId != null)
                                                        }).ToList()
                                           join acc2 in (from acc in listAccount
                                                         join clas in listClass on acc.Username equals clas.CreatedBy into t
                                                         from clas in t.DefaultIfEmpty()
                                                         where acc.Username.ToLower().Contains(searchBase.SearchText.ToLower()) && clas.IsDeleted == false
                                                         group new { acc, clas } by acc.Username into gr
                                                         select new
                                                         {
                                                             Username = gr.Key,
                                                             Email = gr.Select(x => x.acc.Email).FirstOrDefault(),
                                                             Avatar = gr.Select(x => x.acc.Avatar).FirstOrDefault(),
                                                             CreatedAt = gr.Select(x => x.acc.CreatedAt).FirstOrDefault(),
                                                             numJoin = gr.SelectMany(x => x.clas.AccountJoinClasses).Count(),
                                                             CountClass = gr.Count(x => x.clas.ClassId != null)
                                                         }).ToList()
                                           on acc.Username equals acc2.Username into t
                                           from acc2 in t.DefaultIfEmpty()
                                           select new AccountDTO
                                           (
                                               acc.Username,
                                               acc.Email,
                                               acc.Avatar,
                                               acc.CreatedAt,
                                               acc.CountCredit,
                                               acc2.CountClass,
                                               acc.numLearn + acc2.numJoin
                                           )).OrderByDescending(x => x.Mark).ToList();

            // Phân trang
            List<AccountDTO> listAccountPaging = listTopAccount.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                               .Take(searchBase.PageSize).ToList();

            int totalItem = listTopAccount.Count();

            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listAccountPaging;

            return result;
        }

        public PaginatedResultBase<Notification> GetNotiByUser(SearchBase searchBase, string username)
        {
            PaginatedResultBase<Notification> result = new PaginatedResultBase<Notification>();

            //var listClassify = _dbContext.Classifications.Where(clas => clas.Type == "NOTI_TYPE");

            List<Notification> listNoti = _dbContext.Notifications.Where(noti => noti.Username == username)
                                                    .ToList();

            // Phân trang
            List<Notification> listNotiPaging = listNoti.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                               .Take(searchBase.PageSize).ToList();

            int totalItem = listNoti.Count();

            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listNotiPaging;

            return result;
        }
    }
}
