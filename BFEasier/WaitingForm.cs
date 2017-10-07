using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BFEasier
{
    public partial class WaitingForm : Form
    {
        public WaitingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lässt den Balken wachsen und anschließend wieder von Neuem beginnen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
                progressBar1.Value = progressBar1.Minimum;
            else
                progressBar1.Value++;
        }

        private void WaitingForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
