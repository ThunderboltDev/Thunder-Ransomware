using System;
using System.Net;
using Newtonsoft.Json.Linq;


namespace WindowsFormsApp2
{
    internal static class btcapi
    {
        private static string BlockchainApiAddress = "https://blockchain.info/";

        internal static double GetPrice()
        {
            var request = BlockchainApiAddress + "ticker";
            var client = new WebClient();
            var result = client.DownloadString(request);

            var json = JObject.Parse(result);
            var price = json["USD"].Value<double>("last");

            return price;
        }

        internal static double GetBalanceBtc(string address)
        {
            var request = BlockchainApiAddress + "balance?active=" + address;
            var client = new WebClient();
            var result = client.DownloadString(request);

            var json = JObject.Parse(result);
            var balance = json[address].Value<double>("final_balance") / 100000000;

            return balance;
        }
    }
}
