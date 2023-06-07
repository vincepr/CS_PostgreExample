namespace CS_PostgreExample.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;   // another way to define it must NOT be null
        public int DriverNumber { get; set; }
    }
}
