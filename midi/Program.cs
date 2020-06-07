using System;
using System.Windows.Forms;


namespace midi
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int[] comboBoxMidiInDevices = new int[50];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Form1());
            






        }
        

    }
    
}
