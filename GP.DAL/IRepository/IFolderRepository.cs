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
    public interface IFolderRepository
    {
        /// <summary>
        /// Lấy danh sách Folder của một user 
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<FolderDTO> GetListFolderByUser(SearchBase searchBase);

        /// <summary>
        /// Lấy Folder theo folderId
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public FolderDTO GetFolderById(SearchBase searchBase);

        /// <summary>
        /// Lấy Folder theo classId
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        public PaginatedResultBase<FolderDTO> GetListFolderByClass(SearchBase searchBase);
    }
}
