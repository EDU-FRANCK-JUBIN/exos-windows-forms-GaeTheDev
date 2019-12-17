using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taquin
{
    public partial class Taquin : Form
    {

        private int[] the_list = {
             1, 2, 3,
             4, 5, 6,
             7, 8, 0
        };

        private List<PictureBox> all_picturebox = new List<PictureBox>();
        private List<Image> all_image = new List<Image>();

        public Taquin()
        {
            InitializeComponent();

            all_picturebox.Add(pictureBox1);
            all_picturebox.Add(pictureBox2);
            all_picturebox.Add(pictureBox3);
            all_picturebox.Add(pictureBox4);
            all_picturebox.Add(pictureBox5);
            all_picturebox.Add(pictureBox6);
            all_picturebox.Add(pictureBox7);
            all_picturebox.Add(pictureBox8);
            all_picturebox.Add(pictureBox9);

            all_image.Add(Properties.Resources._null);
            all_image.Add(Properties.Resources._1);
            all_image.Add(Properties.Resources._2);
            all_image.Add(Properties.Resources._3);
            all_image.Add(Properties.Resources._4);
            all_image.Add(Properties.Resources._5);
            all_image.Add(Properties.Resources._6);
            all_image.Add(Properties.Resources._7);
            all_image.Add(Properties.Resources._8);

            refresh_display();

            foreach (PictureBox pictureBox in all_picturebox)
            {


            }
        }

        private void Taquin_Load(object sender, EventArgs e)
        {

        }

        private void refresh_display()
        {
            for (int i = 0; i < the_list.Length; i++)
            {
                all_picturebox[i].Image = all_image[the_list[i]];
            }
            the_list.ToList().ForEach(i => Console.WriteLine(i.ToString()));
        }

        private void suffle_btn_Click(object sender, EventArgs e)
        {
            Random rng = new Random();
            int n = the_list.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = the_list[k];
                the_list[k] = the_list[n];
                the_list[n] = value;
            }
            refresh_display();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}

