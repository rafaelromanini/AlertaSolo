namespace AlertaSolo.DTO
{
    public class LocalRiscoCreateDto
    {
        public string NomeLocal { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public int GrauRisco { get; set; }
        public bool Ativo { get; set; }
        public int UsuarioId { get; set; }
    }
}
