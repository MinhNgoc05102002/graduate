using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Business.Service
{
    public class ClassService : IClassService
    {
        public readonly IClassRepository classRepository;

        public ClassService(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public PaginatedResultBase<ClassDTO> GetClassByUsername(SearchBase searchBase)
        {
            return classRepository.GetListClassByUser(searchBase);
        }
    }
}
