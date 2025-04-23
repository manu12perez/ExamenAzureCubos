using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenAzureCubos.Models
{
    [Table("USUARIOSCUBO")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int IdUsuario;

        [Column("NOMBRE")]
        public string Nombre;

        [Column("EMAIL")]
        public string Email;

        [Column("PASS")]
        public string Pass;

        [Column("IMAGEN")]
        public string Imagen;
    }
}
