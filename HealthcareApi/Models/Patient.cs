using System.ComponentModel.DataAnnotations;

public class Patient
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; }

    [Required, Range(0, 120)]
    public int Age { get; set; }

    [Required, StringLength(50)]
    public string Username { get; set; }

    [Required, StringLength(100, MinimumLength = 5)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
