namespace BFEasier
{
    internal class Waiting
    {
        /// <summary>
        /// Zeigt einen Form, dass gearbeitet wird
        /// </summary>
        public static void Wait()
        {
            try
            {
                var form = new WaitingForm();
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
