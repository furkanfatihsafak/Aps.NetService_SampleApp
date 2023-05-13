using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Entity
{
    public class MovieActorEntity
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public virtual string ActorName { get; set; }
    }

    public class MovieActorEntityTypeConfiguration : EntityTypeConfiguration<MovieActorEntity>
    {
        public MovieActorEntityTypeConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ActorName).IsRequired();
        }
    }
}
