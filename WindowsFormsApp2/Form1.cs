using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private int tickCount = 0;
        private const int DelayInSeconds = 10;
        private Timer timer_Tick1;
        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();
            // Set initial properties for pictureBox1
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            WindowState = FormWindowState.Maximized;

            // Set initial properties for pictureBox1
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Visible = false;


            // Create a timer named timer_tick1 and set its interval to 3000 milliseconds (3 seconds)
            Timer timer_tick1 = new Timer();
            timer_tick1.Interval = 10000;

            // Add an event handler for the timer's Tick event
            timer_tick1.Tick += timer1_Tick;

            // Start the timer
            timer_tick1.Start();

            // Create a timer named timer2 and set its interval to 5000 milliseconds (5 seconds)
            Timer timer2 = new Timer();
            timer2.Interval = 15000;

            // Add an event handler for the timer's Tick event
            timer2.Tick += timer2_Tick;

            // Start the timer
            timer2.Start();


            // Set the TopMost property of the form to true
            this.TopMost = true;
        }

        protected override void WndProc(ref Message m)
        {
            // Handle WM_MOUSEACTIVATE message
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                // Return MA_NOACTIVATE to prevent activation and user interaction
                m.Result = (IntPtr)MA_NOACTIVATE;
                return;
            }

            base.WndProc(ref m);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            pictureBox1.Visible = false;
            pictureBox2.Visible = true;
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            WindowState = FormWindowState.Maximized;

            // Set initial properties for pictureBox1
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // Stop the timer to prevent it from triggering again
            timer2.Stop();

            // Check if there's an instance of Form2 already open
            if (Application.OpenForms.OfType<Form2>().Any())
            {
                // Form2 is already open, so ignore opening another form and exit
                return;
            }

            // Create and show Form2 as a pop-up dialog
            using (Form2 form2 = new Form2())
            {
                form2.ShowDialog();
            }

            // Close Form1
            this.Close();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            tickCount++;

            // Check if the desired delay has passed
            if (tickCount >= DelayInSeconds * 1000 / timer3.Interval)
            {
                // Close the main Form1
                Form1 mainForm = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                mainForm?.Close();
                this.TopMost = false;

                // Stop the timer
                timer3.Stop();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
