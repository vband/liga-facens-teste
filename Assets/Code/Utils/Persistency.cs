using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Utils
{
    public static class Persistency
    {
        public static async Task<(bool, T)> TryLoadAsync<T>(string path)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);

            if (!File.Exists(fullPath))
                return (false, default);

            var bytes = await File.ReadAllBytesAsync(fullPath);
            T deserializedObj;

            await using (var memoryStream = new MemoryStream(bytes))
            {
                var binaryFormatter = new BinaryFormatter();
                deserializedObj = (T) binaryFormatter.Deserialize(memoryStream);
            }

            return (true, deserializedObj);
        }

        public static async Task SaveAsync(string path, object obj)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            
            await using var memoryStream = new MemoryStream();
            
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            var bytes = memoryStream.ToArray();
            
            await File.WriteAllBytesAsync(fullPath, bytes);
        }
    }
}