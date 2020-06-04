namespace BFEasier
{
    using System;
    using System.Windows.Forms;
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
        private void Timer1_Tick(Object sender, EventArgs e)
        {
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = progressBar1.Minimum;
            }
            else
            {
                progressBar1.Value++;
            }
        }

        private void WaitingForm_Load(Object sender, EventArgs e) => timer1.Start();
    }
}
