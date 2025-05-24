using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindEndPortal
{
    public partial class CalculateEndPortal : Form
    {
        double finalX = 0;
        double finalY = 0;
        public CalculateEndPortal()
        {
            InitializeComponent();
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            float firstX = float.Parse(XFirst.Text);
            float firstY = float.Parse(ZFirst.Text);
            double firstAngle = (Convert.ToDouble(AngleFirst.Text) + 360) % 360;
            

            float secondX = float.Parse(XSecond.Text);
            float secondY = float.Parse(ZSecond.Text);
            double secondAngle = (Convert.ToDouble(AngleSecond.Text) + 360) % 360;


            // Все прямые не вертикальные
            if ((firstAngle != 90 || firstAngle != 270) && (secondAngle != 90 || secondAngle != 270))
            {
                firstAngle = (Math.PI / 180) * firstAngle;
                secondAngle = (Math.PI / 180) * secondAngle;

                double firstAngleK = Math.Tan(firstAngle);
                double secondAngleK = Math.Tan(secondAngle);

                double firstB = firstY - firstAngleK * firstX;
                double secondB = secondY - secondAngleK * secondX;

                if ((Math.Abs(firstAngleK - secondAngleK) < 0.000000000000000001) || (Math.Abs(firstB - secondB) < 0.000000000000000001))
                {
                    // Прямые слишком близко/малый угол/прямые почти параленльны TODO
                    XEndPortal.Text = "Прямые слишком близко";
                    ZEndPortal.Text = "Прямые слишком близко";

                }
                else
                {
                    finalX = (secondB - firstB) / (firstAngleK - secondAngleK);
                    finalY = firstAngleK * finalX + firstB;
                }
            }
            // first не вертикальна, second вертикальна
            else if ((firstAngle != 90 || firstAngle != 270) && (secondAngle == 90 || secondAngle == 270))
            {
                firstAngle = (Math.PI / 180) * firstAngle;
                secondAngle = (Math.PI / 180) * secondAngle;

                double firstAngleK = Math.Tan(firstAngle);
                double secondAngleK = Math.Tan(secondAngle);

                double firstB = firstY - firstAngleK * firstX;
                double secondB = secondY - secondAngleK * secondX;

                finalX = secondX;
                finalY = firstAngleK * finalX + firstB;
            }
            // first вертикальна, second невертикальна
            else if ((firstAngle == 90 || firstAngle == 270) && (secondAngle != 90 || secondAngle != 270))
            {

                firstAngle = (Math.PI / 180) * firstAngle;
                secondAngle = (Math.PI / 180) * secondAngle;

                double firstAngleK = Math.Tan(firstAngle);
                double secondAngleK = Math.Tan(secondAngle);

                double firstB = firstY - firstAngleK * firstX;
                double secondB = secondY - secondAngleK * secondX;


                finalX = firstX;
                finalY = secondAngleK * finalX + secondB;
            }
            // Обе вертикальные (по идее это невозможно, но навсякий случай)
            else
            {
                XEndPortal.Text = "Прямые слишком близко";
                ZEndPortal.Text = "Прямые слишком близко";
            }


            XEndPortal.Text = Math.Round(finalX, 1).ToString();
            ZEndPortal.Text = Math.Round(finalY, 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
