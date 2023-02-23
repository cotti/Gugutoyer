using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Domain.Entities
{
    public sealed class Palavra : BaseEntity
    {
        [MaxLength(100)]
        [Column("palavra")]
        public string Verbete { get; init; }

        [Column("usado")]
        public bool Usado { get; set; }

        public Palavra(int id, string verbete, bool usado)
        {
            Id = id;
            Verbete = verbete;
            Usado = usado;
        }


    }
}
