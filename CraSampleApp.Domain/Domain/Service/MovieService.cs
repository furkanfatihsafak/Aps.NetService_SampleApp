using AutoMapper;
using AutoMapper.QueryableExtensions;
using CraSampleApp.Domain.Domain.Data.Model;
using CraSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Domain.Domain.Service
{

    public interface IMovieService
    {
        List<MovieList> GetList();
        bool Create(MovieManagement model);
        MovieManagement GetById(Guid id);
        bool Edit(MovieManagement model);
        bool Delete(Guid id);
    }


    public class MovieService : IMovieService
    {

        private readonly IEntityFrameworkRepository<MovieEntity> _repository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        public MovieService(IEntityFrameworkRepository<MovieEntity> repository, ILogger logger,
            IMapper mapper
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        public List<MovieList> GetList()
        {
            _logger.Log("Movie listesi çekildi");
            return _repository.GetQueryable().ProjectTo<MovieList>(_mapper.ConfigurationProvider).ToList();
        }

        public bool Create(MovieManagement model)
        {
            _logger.Log("Movie oluştu");
            var entity = _mapper.Map<MovieEntity>(model);
            entity.Date = DateTime.Now;

            _repository.Add(entity);
            return true;
        }

        public MovieManagement GetById(Guid id)
        {
            var entity = _repository.GetById(id);
            return _mapper.Map<MovieManagement>(entity);

        }


        public bool Edit(MovieManagement model)
        {
            _logger.Log("Movie güncellendi");
            var entity = _repository.GetById(model.Id);
            entity.Name = model.Name;
            entity.MovieTypeId = model.MovieTypeId;
            _repository.Update(entity);
            return true;

        }


        public bool Delete(Guid id)
        {
            _logger.Log("Movie silindi");
            _repository.Remove(id);
            return true;
        }

    }
}
