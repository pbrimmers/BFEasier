using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BFEasier
{
    class Waiting
    {
        /// <summary>
        /// Zeigt einen Form, dass gearbeite wird
        /// </summary>
        public static void wait()
        {
            try
            {
                WaitingForm form = new WaitingForm();
                form.ShowDialog();
            }
            catch
            {
            }
            finally
            {
            }

        }
    }
}
