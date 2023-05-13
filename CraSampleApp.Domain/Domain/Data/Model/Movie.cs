using AutoMapper;
using CraSampleApp.Entity;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;

namespace CraSampleApp.Domain.Domain.Data.Model
{
    public class MovieList
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string MovieType { get; set; }
        public int ActorCount { get; set; }
    }

    public class MovieManagement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MovieTypeId { get; set; }
    }



    public class MovieMap : Profile
    {
        public MovieMap()
        {
            CreateMap<MovieEntity, MovieList>().ForMember(c => c.MovieType, opt => opt.MapFrom(d => d.MovieType.Name))
                .ForMember(c => c.ActorCount, opt => opt.MapFrom(d => d.Actors.Count));
            ;
            CreateMap<MovieEntity, MovieManagement>();
            CreateMap<MovieManagement, MovieEntity>();
        }
    }


    public class MovieValidator : AbstractValidator<MovieManagement>
    {
        public MovieValidator()
        {
            RuleFor(c => c.Name).NotEmpty().DependentRules(() =>
            {
                RuleFor(c => c.Name).Must(c =>

                {
                    return c.Contains("Movie");
                }).WithMessage("Movie içersin");
            });
            RuleFor(c => c.MovieTypeId).NotEmpty();
        }
    }
}
