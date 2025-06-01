namespace AlertaSolo.Model
{
    public class Sensor
    {
        public int Id { get; set; }
        public string CodigoEsp32 { get; set; }
        public string Status { get; set; } // Ativo, Inativo, ComErro
        public string TipoSensor { get; set; } // Umidade, Inclinação, Tremor, Multissensor
        public DateTime DataInstalacao { get; set; } = DateTime.Now;
        public int QntdAlertas { get; set; }

        public int LocalRiscoId { get; set; }
        public LocalRisco LocalRisco { get; set; }
    }
}
