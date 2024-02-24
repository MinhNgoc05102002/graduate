using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return folderRepository.GetListCreditByUser(searchBase);
        }
    }
}
