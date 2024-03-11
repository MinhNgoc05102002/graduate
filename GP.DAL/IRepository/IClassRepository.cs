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
    public interface IClassRepository
    {
        /// <summary>
        /// Lấy danh sách Class của một user 
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<ClassDTO> GetListClassByUser(SearchBase searchBase);

        /// <summary>
        /// Lấy Class theo classId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public Class GetClassById(string classId);
    }
}
