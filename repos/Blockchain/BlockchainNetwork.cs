using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    public static class BlockchainNetwork
    {
        public static List<Block> GetBlocksFromHost(string ip)
        {
            string respons = SendRequest(ip, "getblocks", "");
            
            if (string.IsNullOrEmpty(respons))
                return null;

            return respons.TryConvertToBlockList();
        }

        private static string SendRequest(string ip, string method, string data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(20);

                // http://localhost:28451/BlockchainService.svc/api/getchain/ пример запроса.

                //string repUri = $"{ip}/BlockchainService.svc/api/{method}/{data}";
                string repUri = $"{ip}/blockchain/{data}";
                var response = client.GetAsync(repUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }

            return null;
        }

        public static bool SendBlockToHost(string ip, Block block)
        {
            var result = SendRequest(ip, "sendblock", block.Serialize());
            var successStatus = !string.IsNullOrEmpty(result);

            return successStatus;
        }
    }
}
