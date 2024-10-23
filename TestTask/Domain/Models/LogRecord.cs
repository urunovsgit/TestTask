using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Models;

public enum LogType
{
    Info,
    Warning,
    Error
}

public class LogRecord
{
    [Key]
    public int Id { get; set; }
    public LogType Type { get; set; }
    public DateTime DateTime { get; set; }

    [Required, StringLength(200)]
    public string Action { get; set; }
}
