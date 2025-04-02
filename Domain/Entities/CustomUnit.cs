namespace SmartScaleApi.Domain.Entities
{
    public class CustomUnit
    {
        public long Id { get; set; } // Primary key for the custom unit
        public string Unit { get; set; } = null!;
        public string StandardUnit { get; set; } = null!;
        public string ConversionFormulae { get; set; } = null!;
    }
}