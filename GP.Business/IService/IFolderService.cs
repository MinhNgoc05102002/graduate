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
        /// <param name="username"></param>
        /// <returns></returns>
        public PaginatedResultBase<FolderDTO> GetFolderByUsername(SearchBase searchBase);
    }
}
