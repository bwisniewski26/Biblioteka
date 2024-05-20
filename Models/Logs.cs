using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektZaliczeniowyPR3.Models;
[Table("logs")]
public class Logs
{
    public int Id { get; set; }
    public enum OperationType
    {
        UserRegister,
        UserLogin,
        BookRental,
        BookReturn,
        BookAdd,
        BooksClear,
        BookDelete,
        Default
    }

    public OperationType OperationsType { get; set; } = OperationType.Default;

    public string Username { get; set; } = null!;
    public bool IsUserAdmin { get; set; } = false;

    public Logs()
    {}

    public Logs(string username, OperationType operationType)
    {
        Username = username;
        OperationsType = operationType;
    }
}