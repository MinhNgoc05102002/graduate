using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class CreditRepository : ICreditRepository
    {
        private readonly QuizletDbContext _dbContext;
        public CreditRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Credit CreateCredit()
        {
            throw new NotImplementedException();
        }

        public PaginatedResultBase<CreditDTO> GetListCreditByFilter(SearchBase searchBase)
        {
            PaginatedResultBase<CreditDTO> result = new PaginatedResultBase<CreditDTO>();

            List<CreditDTO> listCredit = _dbContext.Credits
                                        .Include(x => x.AccountLearnCredits)
                                        .Include(x => x.Flashcards)
                                        
                                        // Điều kiện tìm kiếm
                                        .Where(x =>
                                            x.Name.ToLower().Contains(searchBase.SearchText.ToLower()) && 
                                            x.IsDeleted == false
                                        )
                                        // Sắp xếp
                                        .OrderByDescending(x => x.AccountLearnCredits.Count()).ToList()
                                        
                                        // Join lấy avt
                                        .Join(_dbContext.Accounts.ToList(), c => c.CreatedBy, a => a.Username, (credit, account) =>
                                        {
                                            return new CreditDTO
                                            (
                                                credit.CreditId,
                                                credit.CreatedAt,
                                                credit.Name,
                                                credit.CreatedBy,
                                                credit.Flashcards.Count(),
                                                credit.AccountLearnCredits.Count(),
                                                account.Avatar
                                            );
                                        }).ToList();

            // Phân trang
            List<CreditDTO> listCreditPaging = listCredit.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                         .Take(searchBase.PageSize).ToList();

            //List<Credit> listCredit = _dbContext.Credits.Where(x => 
            //                                (string.IsNullOrEmpty(username) || x.CreatedBy == username) && 
            //                                x.Name.ToLower().Contains(searchBase.SearchText.ToLower())
            //                            ).ToList();

            //List<Credit> listCreditPaging = listCredit.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
            //                                          .Take(searchBase.PageSize).ToList();

            int totalItem = listCredit.Count();
            
            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listCreditPaging;

            return result;
        }

        public PaginatedResultBase<CreditDTO> GetListCreditByUser(SearchBase searchBase, string username = "")
        {
            PaginatedResultBase<CreditDTO> result = new PaginatedResultBase<CreditDTO>();

            List<CreditDTO> listCredit =_dbContext.AccountLearnCredits
                                        .Include(learn => learn.Credit)
                                        .ThenInclude(credit => credit.Flashcards)

                                        // Điều kiện tìm kiếm
                                        .Where(x =>
                                            (string.IsNullOrEmpty(username) || x.Username == username) &&
                                            x.Credit.Name.ToLower().Contains(searchBase.SearchText.ToLower()) &&
                                            x.Credit.IsDeleted == false
                                        )

                                        // Sắp xếp học phần học gần nhất trc 
                                        .OrderByDescending(x => x.CreatedAt).ToList()
                                        // Join lấy avt
                                        .Join(_dbContext.Accounts.ToList(), learn => learn.Credit.CreatedBy, a => a.Username, (learn, account) =>
                                        {
                                            return new CreditDTO
                                            (
                                                learn.CreditId,
                                                learn.CreatedAt,
                                                learn.Credit.Name,
                                                learn.Credit.CreatedBy,
                                                learn.Credit.Flashcards.Count(),
                                                0,
                                                account.Avatar
                                            );
                                        }).ToList();

            // Phân trang
            List<CreditDTO> listCreditPaging = listCredit.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                         .Take(searchBase.PageSize).ToList();

            int totalItem = listCredit.Count();

            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listCreditPaging;

            return result;
        }

        //public List<CreditDTO> GetListCreditByFolder(string folderId)
        //{
        //    PaginatedResultBase<CreditDTO> result = new PaginatedResultBase<CreditDTO>();

        //    List<CreditDTO> listCredit = _dbContext.Credits
        //                                .Include(x => x.Folders)
        //                                .Include(x => x.Flashcards)

        //                                // Điều kiện tìm kiếm
        //                                .Where(x =>
        //                                    x.Folders.Any(f => f.FolderId == folderId) && 
        //                                    x.IsDeleted == false
        //                                ).ToList()

        //                                // Join lấy avt
        //                                .Join(_dbContext.Accounts.ToList(), c => c.CreatedBy, a => a.Username, (credit, account) =>
        //                                {
        //                                    return new CreditDTO
        //                                    (
        //                                        credit.CreditId,
        //                                        credit.CreatedAt,
        //                                        credit.Name,
        //                                        credit.CreatedBy,
        //                                        credit.Flashcards.Count(),
        //                                        0,
        //                                        account.Avatar
        //                                    );
        //                                }).ToList();

        //    return listCredit;
        //}

        public Credit GetCreditById(string creditId)
        {
            Credit credit = _dbContext.Credits.FirstOrDefault(c => c.CreditId == creditId && c.IsDeleted == false);

            return credit;
        }

        public Boolean IsUserLearnedCredit(string creditId, string username)
        {
            return _dbContext.AccountLearnCredits.Any(learn =>  learn.Username == username && learn.CreditId == creditId);
        }
    }
}
