namespace ProjektZaliczeniowyPR3.Models;

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
        BooksClar,
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