using GP.Common.DTO;
using GP.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.IService
{
    public interface IClassService
    {

        /// <summary>
        /// Lấy danh sách class theo username
        /// </summary>
        /// <param name="searchBase"></param>
        /// <returns></returns>
        public PaginatedResultBase<ClassDTO> GetClassByUsername(SearchBase searchBase);

        /// <summary>
        /// Lấy class theo classId
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        public ClassDTO GetClassById(string classId);
    }
}
