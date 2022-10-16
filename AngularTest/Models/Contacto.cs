using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularTest.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(200)]
        public string Direccion { get; set; }
        [Required]
        [MaxLength(50)]
        public string Telefono { get; set; }
        [MaxLength(18)]
        public string Curp { get; set; }
        [Column(TypeName = "Date")]
        public DateTime FechaRegistro { get; set; }

    }
}
