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
    public class ClassRepository : IClassRepository
    {
        private readonly QuizletDbContext _dbContext;

        public ClassRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PaginatedResultBase<ClassDTO> GetListClassByUser(SearchBase searchBase)
        {
            PaginatedResultBase<ClassDTO> result = new PaginatedResultBase<ClassDTO>();

            // Lấy ra danh sách lớp mà Account đó đã tham gia 
            List<ClassDTO> listClass = _dbContext.Classes
                                        .Include(c => c.AccountJoinClasses)
                                        .Include(c => c.Credits)
                                        .Where(c =>
                                            (string.IsNullOrEmpty(searchBase.Username) || c.AccountJoinClasses.Any(j => j.Username == searchBase.Username)) &&
                                            c.Name.ToLower().Contains(searchBase.SearchText.ToLower()) &&
                                            c.IsDeleted == false && 
                                            c.Status == "AUTH"
                                        ).ToList().Select(c =>
                                        {
                                            return new ClassDTO(
                                                c.ClassId,
                                                c.Name,
                                                c.Description,
                                                c.CreatedAt,
                                                c.CreatedBy,
                                                c.AcceptEdit,
                                                c.AccountJoinClasses.Count,
                                                c.Credits.Count
                                            );
                                        }).ToList();

            // Phân trang
            List<ClassDTO> listClassPaging = listClass.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                         .Take(searchBase.PageSize).ToList();

            int totalItem = listClass.Count();

            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listClassPaging;

            return result;
        }
    }
}
