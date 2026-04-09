using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace TraineeHub.Cli.Infrastructue.DataStore
{
    public class JsonDataStore
    {
        private readonly string _filePath = "traineehub-data.json";

        public Data LoadData()
        {
            if (!File.Exists(_filePath))
            {
                return new Data();
            }
            var json = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            return JsonSerializer.Deserialize<Data>(json ,options);
        }
        public void Save(Data data)
        {
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true , Converters = { new JsonStringEnumConverter() } });
            
            File.WriteAllText(_filePath, json);

        }
    }
}