using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Devices;

using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace midi
{
    public static class Globals
    {
        public static InputDevice inputDevice = InputDevice.GetByName("CASIO USB-MIDI");
    }

    public partial class Form1 : Form
    {



        



        public Form1()
        {


            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;


             String[] Negras = { "C#", "D#", "_", "F#", "G#", "A#", "_" };
             String[] blancas = { "C", "D", "E", "F", "G", "A", "B" };



            String[] total = new string[blancas.Length + Negras.Length];


            string[] negrasReturn = teclado.CrearTeclasNegras(Negras, panel1);
            string[] blancasReturn = teclado.CrearTeclasBlancas(blancas, panel1);
            string[] notasTotal = negrasReturn.Concat(blancasReturn).ToArray();




            Globals.inputDevice.EventReceived += OnEventReceived;
            Globals.inputDevice.StartEventsListening();

           

           






            //inputDevice.EventReceived += (object sender, MidiEventReceivedEventArgs e) => OnEventReceived(sender, e, Negras, blancas);
            //inputDevice.StartEventsListening();


        }




       


        private void uwu()
        {
            String[] Negras = { "C#", "D#", "_", "F#", "G#", "A#", "_" };
            String[] blancas = { "C", "D", "E", "F", "G", "A", "B" };

            foreach (Button p in panel1.Controls)
                
            {

                foreach (string thing in Negras)
                {
                    if (p.Name == thing)
                    {

                        p.BackColor = Color.Black;

                    }
                }

                foreach (string thing in blancas)
                {
                    if (p.Name == thing)
                    {

                        p.BackColor = Color.White;
                    }
                }
            }

           
        }

        

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {

            label1.Text = Thread.CurrentThread.Name;
            
            uwu();
            
            string entrada = e.Event.ToString();
            string notaFinal = entrada.Split('(', ',')[1];

            string salida = (Note.Get((SevenBitNumber)Int32.Parse(notaFinal)).ToString());
            salida = salida.Remove(salida.Length - 1);

            notaSalida.Text = salida;
            foreach (Button p in panel1.Controls)
                if (p.Name == salida)
                {
                    p.BackColor = Color.AliceBlue;

                }


        }

        

        private void button1_Click(object sender, EventArgs e)
        {




        }

        
    }

}
