using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gugutoyer.Application.DTOs
{
    public class PalavraDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "There ain't no null thing")]
        [MaxLength(100)]
        public string? Verbete { get; set; }
        public bool Usado { get; set; }
    }
}
