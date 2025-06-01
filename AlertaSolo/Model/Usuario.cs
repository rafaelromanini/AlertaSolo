namespace AlertaSolo.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int Idade { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        public string Email { get; set; }
        public string Senha { get; set; }

        public ICollection<LocalRisco> LocaisRisco { get; set; }
    }
}
