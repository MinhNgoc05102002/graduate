using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.DAL.Repository
{
    public class FolderRepository : IFolderRepository
    {
        private readonly QuizletDbContext _dbContext;

        public FolderRepository(QuizletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PaginatedResultBase<FolderDTO> GetListCreditByUser(SearchBase searchBase)
        {
            PaginatedResultBase<FolderDTO> result = new PaginatedResultBase<FolderDTO>();

            List<FolderDTO> listFolder = _dbContext.Folders.Include(x => x.Credits)
                                        .Where(folder => 
                                            (folder.CreatedBy == searchBase.Username || string.IsNullOrEmpty(searchBase.Username)) && // trường hợp tìm tất cả class ko theo user
                                            folder.Name.ToLower().Contains(searchBase.SearchText.ToLower()) &&
                                            folder.IsDeleted == false
                                         ).ToList().Select(folder => {
                                             return new FolderDTO(
                                                 folder.FolderId,
                                                 folder.Name,
                                                 folder.Description,
                                                 folder.CreatedAt,
                                                 folder.CreatedBy,
                                                 folder.Credits.Count
                                            );
                                         }).ToList();

            // Phân trang
            List<FolderDTO> listFolderPaging = listFolder.Skip((searchBase.PageIndex - 1) * searchBase.PageSize)
                                                         .Take(searchBase.PageSize).ToList();

            int totalItem = listFolder.Count();

            int totalPage = (int)Math.Ceiling((double)totalItem / searchBase.PageSize);

            if (searchBase.PageSize == 0) totalPage = 0;

            result.PageIndex = searchBase.PageIndex;
            result.TotalPage = totalPage;
            result.ListResult = listFolderPaging;

            return result;

        }
    }
}
