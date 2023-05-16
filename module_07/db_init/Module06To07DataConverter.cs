using System.Text.Json;

void Main()
{
    const string inputPath = @"C:\Src\archi\ArchitectureBasicCourse\module_06\service_db_init\ExportJson.json";
    const string outputPath = @"C:\Src\archi\ArchitectureBasicCourse\module_07\db_init\ExportJson.json";

    string jsonString = File.ReadAllText(inputPath);
    var authors = JsonSerializer.Deserialize<Author[]>(jsonString)!;

    var id = 1;
    foreach (var author in authors)
    {
		author._id = id.ToString();
		author.birth_year = author.birth_date.Substring(0, 4);
		id++;
    }

    jsonString = JsonSerializer.Serialize(authors);
    File.WriteAllText(outputPath, jsonString);
}

public class Author
{
    public int _id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string title { get; set; }
    public string birth_date { get; set; }
    public string birth_year { get; set; }
}
