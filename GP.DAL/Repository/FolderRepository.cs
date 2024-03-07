using GP.Common.DTO;
using GP.Common.Helpers;
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
        private readonly MappingProfile _mapper;
        public FolderRepository(QuizletDbContext dbContext, MappingProfile mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PaginatedResultBase<FolderDTO> GetListFolderByUser(SearchBase searchBase)
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
                                                 folder.Credits.Count(c => c.IsDeleted == false)
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

        public FolderDTO GetFolderById(SearchBase searchBase)
        {
            Folder? folder = _dbContext.Folders.Include(x => x.Credits)
                                        .Where(folder =>
                                            (folder.FolderId == searchBase.ContainerId &&
                                            folder.IsDeleted == false)
                                         ).FirstOrDefault();

            List<CreditDTO> listCredit = _dbContext.Credits
                                        .Include(x => x.Folders)
                                        .Include(x => x.Flashcards)

                                        // Điều kiện tìm kiếm
                                        .Where(x =>
                                            x.Folders.Any(f => f.FolderId == searchBase.ContainerId) &&
                                            x.Name.ToLower().Contains(searchBase.SearchText.ToLower()) &&
                                            x.IsDeleted == false
                                        ).ToList()

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
                                                0,
                                                account.Avatar
                                            );
                                        }).ToList();

            FolderDTO folderDTO = _mapper.MapFolderToDTO(folder);
            if (folder != null)
            {
                folderDTO.CountCredit = folder.Credits.Count(c => c.IsDeleted == false);
                folderDTO.Avatar = _dbContext.Accounts.FirstOrDefault(x => x.Username == folder.CreatedBy)?.Avatar;
                folderDTO.Credits = listCredit;
            }
            return folderDTO;
        }
    }
}
