using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_CARROS.Model
{
    public class Carro
    {
        [Required]
        
        public string Placa { get; set; }

        [Required]
        public string Fabricante { get; set; }

        [Required]
        public string Modelo { get; set; }

        public string Ano { get; set; }

        public string Cor { get; set; }

        public string Descricao { get; set; }

    }
}
