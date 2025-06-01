namespace AlertaSolo.DTO
{
    public class SensorDTO
    {
        public int Id { get; set; }
        public string CodigoEsp32 { get; set; }
        public string Status { get; set; }
        public string TipoSensor { get; set; }
        public DateTime DataInstalacao { get; set; }
        public int QntdAlertas { get; set; }
        public int LocalRiscoId { get; set; }
    }
}
