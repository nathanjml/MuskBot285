using MuskBot.Core.Extensions;
using MuskBot.Helper;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MuskBot.Core.CryptoService
{
    public interface ICryptoService
    {
        Task<Result<CurrencyDto>> GetCurrencyInfo(string marketIdentifier);
    }

    public class CryptoService : ICryptoService
    {
        private const string CoinbaseSpotEndpointSuffix = "/spot";
        private const string CoinbasePricesEndpointBase = "https://api.coinbase.com/v2/prices/";

        private readonly CryptoCurrencyLookup _cryptoCurrencyLookup;

        public CryptoService(CryptoCurrencyLookup cryptoCurrencyLookup)
        {
            _cryptoCurrencyLookup = cryptoCurrencyLookup;
        }

        public async Task<Result<CurrencyDto>> GetCurrencyInfo(string marketIdentifier)
        {
            var currency = _cryptoCurrencyLookup.Find(marketIdentifier);
            if (currency.WasSuccessful)
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync($"{CoinbasePricesEndpointBase}{marketIdentifier}-USD{CoinbaseSpotEndpointSuffix}");
                    var responseContent = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        //extract just the gif url to post to the channel
                        var amount = JObject.Parse(responseContent).SelectToken("data").Value<string>("amount");

                        return new CurrencyDto
                        {
                            MarketIdentifier = marketIdentifier,
                            Name = currency.Data,
                            Value = amount,
                            ValuedAt = DateTime.Now,
                        }.AsResult();
                    }
                }
            }

            return new CurrencyDto().AsResult(false);
        }
    }

    public class CurrencyDto
    {
        public string MarketIdentifier { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string AppraisalCurrency { get; set; } = "USD";
        public DateTime ValuedAt { get; set; }
    }
}