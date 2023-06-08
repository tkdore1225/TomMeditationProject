using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TomMeditationsProject
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void instructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Instructor instructor = new Instructor();
            instructor.Show();
        }

        private void meditationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Meditation meditation = new Meditation();
            meditation.Show();
        }

        private void sleepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sleep sleep = new Sleep();
            sleep.Show();
        }

        private void mindfullnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mindfulness mindfulness = new Mindfulness();
            mindfulness.Show();
        }

        private void yogaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yoga yoga = new Yoga();
            yoga.Show();
        }

        private void prayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Prayer prayer = new Prayer();
            prayer.Show();
        }
    }
}
