namespace ProjektZaliczeniowyPR3.DatabaseConnection;

using System.Text.Json;

public class ConnectionInfo {
    private string _host = "";
    private string _port = "";
    private string _username = "";
    private string _password = "";
    private string _database = "";

    public ConnectionInfo() {}
    public ConnectionInfo(string host, string port, string username, string password, string database) 
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
        _database = database;
    }



    public string PrepareToSave(bool ifOverwrite = false)
    {
        string json = JsonSerializer.Serialize(this);

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "library_database.json");

        if (File.Exists(path) && ifOverwrite == false)
            return "File exists";
        
        SaveToFile(json, path);
        
        
        return "Save successful";
    }

    public async void SaveToFile(string json, string path)
    {
        using (StreamWriter outputFile = new StreamWriter(path))
        {
            await outputFile.WriteAsync(json);
        }
    }

    public async Task<string> ReadFromFile()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "library_database.json");
        string json = "";
        if (File.Exists(path))
        {
            using (StreamReader outputFile = new StreamReader(path))
            {
                json = await outputFile.ReadToEndAsync();
            }
        }
  
        return "File does not exist";

    }
}
