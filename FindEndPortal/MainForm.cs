using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace FindEndPortal
{
    public partial class CalculateEndPortal : Form
    {
        double finalX = 0;
        double finalZ = 0;
        public CalculateEndPortal()
        {
            InitializeComponent();
        }

        private string CreateLogFile(Exception ex)
        {
            string dateNow = DateTime.Now.ToLongTimeString().Replace(':', '-');
            string path = Directory.GetCurrentDirectory() + "\\" + "Log_" + dateNow + ".txt";
            string text = ex.ToString();
            using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] buffer = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(buffer, 0, buffer.Length);
            }
            return dateNow + ".txt";
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            try
            {
                float firstX = float.Parse(XFirst.Text.Replace('.', ','));
                float firstZ = float.Parse(ZFirst.Text.Replace('.', ','));
                double firstAngleRad = (Math.PI / 180) * (Convert.ToDouble(AngleFirst.Text.Replace('.', ',')));


                float secondX = float.Parse(XSecond.Text.Replace('.', ',')); // X
                float secondZ = float.Parse(ZSecond.Text.Replace('.', ',')); // Z
                double secondAngleRad = (Math.PI / 180) * (Convert.ToDouble(AngleSecond.Text.Replace('.', ',')));

                finalZ = (secondX - firstX + firstZ * Math.Tan(-firstAngleRad) - secondZ * Math.Tan(-secondAngleRad)) / (Math.Tan(-firstAngleRad) - Math.Tan(-secondAngleRad));
                finalX = (finalZ * Math.Tan(-firstAngleRad) - firstZ * Math.Tan(-firstAngleRad) + firstX);




                XEndPortal.Text = Math.Round(finalX, 1).ToString();
                ZEndPortal.Text = Math.Round(finalZ, 1).ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Invalid input");
            }
            catch (Exception ex)
            {
                string logName = CreateLogFile(ex);
                MessageBox.Show("Something went wrong. Log file: " + logName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
