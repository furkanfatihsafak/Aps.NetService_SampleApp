using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraSampleApp.Entity
{
    public class LogEntity
    {
        public Guid Id { get; set; }
        public string Log { get; set; }
    }

    public class LogEntityTypeConfiguration : EntityTypeConfiguration<LogEntity>
    {
        public LogEntityTypeConfiguration()
        {
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Log).IsRequired();
        }
    }
}
