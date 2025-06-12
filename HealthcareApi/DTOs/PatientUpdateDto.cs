namespace HealthcareApi.DTOs
{
    public class PatientUpdateDto
    {
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
