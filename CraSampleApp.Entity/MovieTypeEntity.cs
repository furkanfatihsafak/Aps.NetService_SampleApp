using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace CraSampleApp.Entity
{
    public class MovieTypeEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<MovieEntity> Movies { get; set; }
    }

    public class MovieTypeEntityTypeConfiguration : EntityTypeConfiguration<MovieTypeEntity>
    {
        public MovieTypeEntityTypeConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Name).IsRequired();
        }
    }
}
