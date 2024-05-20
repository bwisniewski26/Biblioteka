namespace ProjektZaliczeniowyPR3.DatabaseConnection;
using ProjektZaliczeniowyPR3.Data;

using System.Text.Json;

public class ConnectionInfo {
    public string _host { get; set; } = "";
    public string _port { get; set; } = "";
    public string _username { get; set; } = "";
    public string _password { get; set; } = "";
    public string _database { get; set; } = "";

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
        

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryProject");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path = Path.Combine(path, "library_database.json");

                
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

    private void UpdateInformation(string json)
    {
        ConnectionInfo? newInfo = JsonSerializer.Deserialize<ConnectionInfo>(json);
        if (newInfo is null)
           return;
        this._host = newInfo._host;
        this._port = newInfo._port;
        this._username = newInfo._username;
        this._password = newInfo._password;
        this._database = newInfo._database;
    }

    public string GenerateConnectionString()
    {
        return "Host=" + _host +":"+_port+";Database=" + _database + ";Username=" +_username + ";Password=" + _password;
    }

    public bool TryConnection()
    {
        string connectionInfo = RetrieveConnectionString();
        if (connectionInfo == "File does not exit")
        {
            return false;
        }

        LibraryContext context = new();

        if (context.Database.CanConnect())
        {
            return true;
        }
        return false;

    }

    public string RetrieveConnectionString()
    {
        string json = ReadFromFile();
        if (json == "File does not exist")
        {
            return "File does not exist";
        }
        UpdateInformation(json);
        return GenerateConnectionString();
    }

    public string ReadFromFile()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryProject");
        path = Path.Combine(path, "library_database.json");
        string json = "";
        if (File.Exists(path))
        {
            using (StreamReader outputFile = new StreamReader(path))
            {
                json = outputFile.ReadToEnd();
            }

            return json;
        }
        else 
        {
            return "File does not exist";
        }
    }
}
