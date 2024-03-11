using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;

namespace GP.Business.Service
{
    public class FolderService : IFolderService
    {
        public readonly IFolderRepository folderRepository;

        public FolderService(IFolderRepository folderRepository)
        {
            this.folderRepository = folderRepository;
        }

        public PaginatedResultBase<FolderDTO> GetFolderByUsername(SearchBase searchBase)
        {
            return folderRepository.GetListFolderByUser(searchBase);
        }

        public FolderDTO GetFolderById(SearchBase searchBase) {
            return folderRepository.GetFolderById(searchBase);
        }

        public PaginatedResultBase<FolderDTO> GetListFolderByClass(SearchBase searchBase)
        {
            return folderRepository.GetListFolderByClass(searchBase);
        }

    }
}
