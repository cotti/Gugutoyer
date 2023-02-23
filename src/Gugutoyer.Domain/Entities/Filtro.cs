using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Domain.Entities
{
    public class Filtro : BaseEntity
    {

        [Column("palavra_id")]
        public int PalavraId { get; set; }

        public Palavra Palavra { get; set; }

        public Filtro(Palavra palavra)
        {
            Palavra = palavra;
        }

        public Filtro()
        {
            Palavra = new Palavra(0, "", false);
        }
    }
}
