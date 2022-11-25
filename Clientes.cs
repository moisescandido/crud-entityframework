using System.ComponentModel.DataAnnotations;

namespace ClientesModel
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60)]
        public string Nome { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }

        [StringLength(50)]
        public string Endereco { get; set; }

        [StringLength(14)]
        public string Telefone { get; set; }
    }
}