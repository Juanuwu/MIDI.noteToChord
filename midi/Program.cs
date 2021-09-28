using System;
using System.Windows.Forms;
using Python.Included;
using System.Threading.Tasks;
using Python.Runtime;

namespace midi
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]

        static async Task Main(string[] args)
        {

            await Installer.SetupPython();
            Installer.TryInstallPip();
            Installer.PipInstallModule("pychord");
            Installer.PipInstallModule("music21");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            


        }
        

    }
    
}
