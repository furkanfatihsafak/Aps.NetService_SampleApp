using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Entity
{
    public class MovieEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Guid MovieTypeId { get; set; }
        public virtual MovieTypeEntity MovieType { get; set; }
        public virtual List<MovieActorEntity> Actors { get; set; }
    }

    public class MovieEntityTypeConfiguration : EntityTypeConfiguration<MovieEntity>
    {
        public MovieEntityTypeConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name).IsRequired();
            HasRequired(c => c.MovieType).WithMany(c => c.Movies);
            HasMany(c => c.Actors).WithRequired(c => c.Movie);
        }
    }
}
