using GP.Business.IService;
using GP.Common.DTO;
using GP.Common.Helpers;
using GP.Common.Models;
using GP.DAL.IRepository;
using GP.DAL.Repository;
using GP.Models.Data;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        public readonly MappingProfile _mapper;

        public ClassService(IClassRepository classRepository, MappingProfile mapper)
        {
            this.classRepository = classRepository;
            _mapper = mapper;
        }

        public PaginatedResultBase<ClassDTO> GetClassByUsername(SearchBase searchBase)
        {
            return classRepository.GetListClassByUser(searchBase);
        }

        public ClassDTO GetClassById(string classId)
        {
            Class _class = classRepository.GetClassById(classId);
            ClassDTO classDTO = _mapper.MapClassToDTO(_class);

            return classDTO;
        }
    }
}
