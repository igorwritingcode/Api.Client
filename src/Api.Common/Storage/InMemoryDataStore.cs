using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Common
{
    public class InMemoryDataStore : IDataStore
    {
        Dictionary<string, string> storageDict = new Dictionary<string, string>();
        private static readonly Task CompletedTask = Task.FromResult(0);

        public Task ClearAsync()
        {
            storageDict.Clear();
            return CompletedTask;
        }

        public Task DeleteAsync<T>(string key)
        {
            if (storageDict.ContainsKey(key))
            {
                storageDict.Remove(key);
            }
            return CompletedTask;
        }

        public Task<T> GetAsync<T>(string key)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            try
            {
                if (storageDict.TryGetValue(key, out var value))
                {
                    tcs.SetResult(JsonSerializer.Deserialize<T>(value));
                }
                else
                {
                    tcs.SetResult(default);
                }
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            var serialized = JsonSerializer.Serialize(value);

            if (storageDict.ContainsKey(key))
            {
                storageDict[key] = serialized;
            }
            else
            {
                storageDict.Add(key, serialized);
            }

            return CompletedTask;
        }
    }
}
