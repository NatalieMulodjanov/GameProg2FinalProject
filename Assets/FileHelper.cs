using System.IO;
using Newtonsoft.Json;

public class FileHelper
{
    public static void WriteToFile(string fileName, object data, bool append)
    {
        string serializedData = JsonConvert.SerializeObject(data);
        StreamWriter writer = new StreamWriter(fileName, append);
        writer.WriteLine(serializedData);
        writer.Close();
    }

    public static T ReadFromFile<T>(string fileName) {
        string fileContent = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject<T>(fileContent);
    }
}