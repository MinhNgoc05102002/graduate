using GP.Common.DTO;
using GP.Common.Models;
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
        public PaginatedResultBase<FolderDTO> GetListCreditByUser(SearchBase searchBase);
    }
}
