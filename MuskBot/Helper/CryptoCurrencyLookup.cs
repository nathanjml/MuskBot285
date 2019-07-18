using System.Collections.Concurrent;

namespace MuskBot.Helper
{
    public class CryptoCurrencyLookup
    {
        private ConcurrentDictionary<string, string> _cryptoDictionary;

        public CryptoCurrencyLookup()
        {
            Init();
        }

        private void Init()
        {
            _cryptoDictionary = new ConcurrentDictionary<string, string>();
            _cryptoDictionary.TryAdd("BTC", "Bitcoin");
            _cryptoDictionary.TryAdd("ETH", "Ethereum");
            _cryptoDictionary.TryAdd("LTC", "Litecoin");
        }

        public Result<string> Find(string identifier)
        {
            var success = _cryptoDictionary.TryGetValue(identifier, out var output);
            return new Result<string> { Data = output, WasSuccessful = success };
        }
    }

    //TODO: pull this out
    public class Result<T>
    {
        public T Data { get; set; }
        public bool WasSuccessful { get; set; }
    }
}
