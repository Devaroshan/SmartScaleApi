namespace SmartScaleApi.Domain.Entities
{
    public class Sample
    {
        public long Id { get; set; } // Primary key for the custom unit
        public string Name { get; set; } = null!;
        public string Units { get; set; } = string.Empty;
    }
}
