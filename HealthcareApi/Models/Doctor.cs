using System.ComponentModel.DataAnnotations;

public class Doctor
{
    public Guid Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; }

    [Required, StringLength(50)]
    public string Specialty { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; }

    [Required, StringLength(100, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
