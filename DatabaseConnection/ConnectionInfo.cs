namespace ProjektZaliczeniowyPR3.DatabaseConnection;
using ProjektZaliczeniowyPR3.Data;

using System.Text.Json;

public class ConnectionInfo {
    public string _host = "";
    public string _port = "";
    public string _username = "";
    public string _password = "";
    public string _database = "";

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

        Console.WriteLine(json);

        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryProject");

        if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LibraryProject")))
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
        if (_host == "file not found")
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

    public async Task<string> RetrieveConnectionString()
    {
        string json = await this.ReadFromFile();
        if (json == "File does not exist")
        {
            return "File does not exist";
        }
        UpdateInformation(json);
        return GenerateConnectionString();
    }

    public void WriteDefault()
    {
        _host = "localhost";
        _port = "5432";
        _username = "postgres";
        _database = "postgres";
        _password = "1234";
        PrepareToSave();
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

            return json;
        }
        else 
        {
            WriteDefault();
            await ReadFromFile();
        }
        _host = "file not found";
        return await Task.Factory.StartNew(() => {
            return "File does not exist";
        });

    }
}
