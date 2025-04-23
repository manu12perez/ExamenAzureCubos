using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenAzureCubos.Models
{
    [Table("COMPRACUBOS")]
    public class Compra
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido;

        [Column("id_cubo")]
        public int IdCubo;

        [Column("id_usuario")]
        public int IdUsuario;

        [Column("fechapedido")]
        public DateTime FechaPedido;
    }
}
