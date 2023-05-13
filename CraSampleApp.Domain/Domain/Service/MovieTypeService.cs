using CraSampleApp.Domain.Domain.Data.Model;
using CraSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Domain.Domain.Service
{
    public interface IMovieTypeService
    {
        List<MovieTypeList> GetAll();
    }


    public class MovieTypeService : IMovieTypeService
    {
        private readonly IEntityFrameworkRepository<MovieTypeEntity> _repository;
        public MovieTypeService(IEntityFrameworkRepository<MovieTypeEntity> repository)
        {
            _repository = repository;
        }


        public List<MovieTypeList> GetAll()
        {
            return _repository.GetQueryable().Select(c => new MovieTypeList()
            {
                Id = c.Id,
                Name = c.Name

            }).ToList();

        }
    }
}
