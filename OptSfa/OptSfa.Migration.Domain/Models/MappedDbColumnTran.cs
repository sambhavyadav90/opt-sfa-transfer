using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OptSfa.Migration.Domain.Models
{
    public class MappedDbColumnTran
    {
        [Column("db_column_mapping")]
        public string? DbColumnMapping { get; set; }
        [Column("db_column_name")]
        public string? DbColumnName { get; set; }
    }
}
