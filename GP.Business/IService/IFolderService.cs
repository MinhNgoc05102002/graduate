using GP.Common.DTO;
using GP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IFolderService
    {
        /// <summary>
        /// Lấy danh sách folder theo username
        /// </summary>
        /// <returns></returns>
        public PaginatedResultBase<FolderDTO> GetFolderByUsername(SearchBase searchBase);

        /// <summary>
        /// Lấy danh sách folder theo folderId
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public FolderDTO GetFolderById(SearchBase searchBase);
    }
}
