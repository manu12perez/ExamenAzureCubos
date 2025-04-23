using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenAzureCubos.Models
{
    [Table("CUBOS")]
    public class Cubo
    {
        [Key]
        [Column("id_cubo")]
        public int IdCubo;

        [Column("nombre")]
        public string Nombre;

        [Column("marca")]
        public string Marca;

        [Column("imagen")]
        public string Imagen;

        [Column("precio")]
        public int Precio;
    }
}
