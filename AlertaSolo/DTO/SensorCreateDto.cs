namespace AlertaSolo.DTO
{
    public class SensorCreateDto
    {
        public string CodigoEsp32 { get; set; }
        public string Status { get; set; }
        public string TipoSensor { get; set; }
        public int QntdAlertas { get; set; }
        public int LocalRiscoId { get; set; }
    }
}
