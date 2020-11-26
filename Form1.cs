using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;


namespace fridge
{
    public partial class Form1 : Form
    {
        double buf1 = 0;
        double buf2 = 0;

        double i = 0;
        double j = 0;

        int znach_do_otkr1 = 0 ;
        int znach_do_otkr2 = 0 ;

        Random r = new Random();
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = Convert.ToString(trackBar1.Value);
            textBox2.Text = Convert.ToString(trackBar2.Value);
            textBox3.Text = "26";
            timer1.Interval = 10000;
            timer2.Interval = 10000;
            timer3.Interval = 1000;
            timer4.Interval = 1000;
            timer6.Interval = 1000;
            timer7.Interval = 1000;
            buf1 = Convert.ToInt32(trackBar1.Value);
            buf2 = Convert.ToInt32(trackBar2.Value);
            i = trackBar1.Value;
            j = trackBar2.Value;
            timer5.Interval = 1;
            timer5.Start();
        }
        int flag1 = 1;
        int flag2 = 1;
        SoundPlayer Engine = new SoundPlayer(fridge.Properties.Resources.engine);
        SoundPlayer Upper = new SoundPlayer(fridge.Properties.Resources.upper);
        SoundPlayer Down = new SoundPlayer(fridge.Properties.Resources.down);

        
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(trackBar1.Value);
            i = trackBar1.Value;
            buf1 = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox2.Text = Convert.ToString(trackBar2.Value);
            j = trackBar2.Value;
            buf2 = trackBar2.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flag1 == 1) // Камера открыта
            {
                Engine.Stop();
                timer1.Start();
                timer3.Start();

                button1.Text = "Закрыть";
                flag1 = 0;

                timer4.Stop();
                timer4.Dispose();
                znach_do_otkr1 = trackBar1.Value;
                trackBar1.Enabled = false;

                pictureBox1.Image = fridge.Properties.Resources.Круг;
                pictureBox1.Visible = false;

                pictureBox2.Visible = true;

            }
            else
            {
                button1.Text = "Открыть"; // Камера закрыта
                flag1 = 1;

                Down.Stop();

                timer1.Stop();
                timer1.Dispose();
                timer3.Stop();
                timer3.Dispose();

                timer4.Start();

                Engine.PlayLooping();
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag2 == 1) // Холодос открыт
            {
                Engine.Stop();
                timer2.Start();
                timer6.Start();

                button2.Text = "Закрыть";
                flag2 = 0;

                timer7.Stop();
                timer7.Dispose();
                znach_do_otkr2 = trackBar2.Value;
                trackBar2.Enabled = false;

                pictureBox3.Image = fridge.Properties.Resources.Круг;
                pictureBox3.Visible = false;

                pictureBox4.Visible = true;
            }
            else
            {
                button2.Text = "Открыть"; // Холодос закрыт
                flag2 = 1;

                Upper.Stop();

                timer2.Stop();
                timer2.Dispose();
                timer6.Stop();
                timer6.Dispose();

                timer7.Start();

                Engine.PlayLooping();
                pictureBox3.Visible = true;
                pictureBox4.Visible = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // камера открыт 10 секунд
        {
            Down.Play();
        }

        private void timer2_Tick(object sender, EventArgs e) // холодос открыт 10 секунд
        {
            Upper.Play();
        }
        private void timer3_Tick(object sender, EventArgs e) // в камере теплеет
        {
            if (i == Convert.ToInt32(textBox3.Text))
            {
                i = trackBar1.Value;
            }

            if (i < Convert.ToInt32(textBox3.Text)) 
            {
                
                i += 0.1;
                textBox1.Text = Convert.ToString(i);
                
                if (i == znach_do_otkr1)
                {
                    trackBar1.Enabled = true;
                }
            }
        }

        private void timer4_Tick(object sender, EventArgs e) // в камере холодает
        {

            if (i == buf1)
            {
                i = trackBar1.Value;
                trackBar1.Enabled = true;
            }

            if (i > buf1)
            {
                i -= 0.1;
                textBox1.Text = Convert.ToString(i);
               /* if (i == znach_do_otkr1)
                {
                    trackBar1.Enabled = true;
                }*/
            }
            else
            {
                i = trackBar1.Value;
                textBox1.Text = Convert.ToString(i);
                Engine.Stop();
                pictureBox1.Visible = false;
            }
        }

        private void timer5_Tick(object sender, EventArgs e) // Рандом поломки двигателя
        {
            double polomka = 20000;
            if (polomka == r.Next(1, 100000))
            {
                Down.Play();
                pictureBox1.Image = fridge.Properties.Resources.КругК;
                pictureBox1.Visible = true;
            }
        }

        private void timer6_Tick(object sender, EventArgs e) // в холодосе теплеет
        {
            if (j == Convert.ToInt32(textBox3.Text))
            {
                j = trackBar2.Value;
            }

            if (j < Convert.ToInt32(textBox3.Text))
            {

                j += 0.1;
                textBox2.Text = Convert.ToString(j);

               /* if (j == znach_do_otkr2)
                {
                    trackBar2.Enabled = true;
                }*/
            }
        }

        private void timer7_Tick(object sender, EventArgs e) // в холодосе холодает
        {
            if (j == buf2)
            {
                j = trackBar2.Value;
                trackBar2.Enabled = true;
            }

            if (j > buf2)
            {
                j -= 0.1;
                textBox2.Text = Convert.ToString(j);
                if (j == znach_do_otkr2)
                {
                    trackBar2.Enabled = true;
                }
            }
            else
            {
                j = trackBar2.Value;
                textBox2.Text = Convert.ToString(j);
                Engine.Stop();
                pictureBox3.Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
