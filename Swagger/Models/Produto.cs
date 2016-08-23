using System.ComponentModel.DataAnnotations;

namespace Swagger.Models
{
    /// <summary>
    /// Student Model
    /// </summary>
    public class Produto 
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
         [Required]
        public string Descricao { get; set; }
        /// <summary>
        /// Grupo
        /// </summary>
         [Required] 
        public string Grupo { get; set; }

    }
}