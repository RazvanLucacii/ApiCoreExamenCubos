using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCoreExamenCubos.Models
{
    [Table("COMPRACUBOS")]
    public class CompraCubos
    {
        [Key]
        [Column("id_pedido")]
        public int IdPedido { get; set; }
        [Column("id_cubo")]
        public int IDCubo { get; set; }
        [Column("id_usuario")]
        public int IDUsuario { get; set; }
        [Column("fechapedido")]
        public DateTime Fecha { get; set; }
    }
}
