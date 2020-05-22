using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Devices;

using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace midi
{
    

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

           
        }


        public static class Globals
        {
            public static InputDevice inputDevice = InputDevice.GetByName("CASIO USB-MIDI");
            
        }

        

        // funcion que ya no tiene uso, pero no la voy a borrar por si me olvido algo, era para que las notas dejaran de estar en color cuando tocas la siguiente, pero ahora lo hacen con el
        //evento off
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

        //TODO: agregar notas a una lista cuando llega un evento on y eliminarlas cuando llega el evento off, para tener lista de todas las teclas presionadas en un determinado momento

        List<string> activas = new List<string>();




        int i = 0;

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            String[] Negras = { "C#", "D#", "_", "F#", "G#", "A#", "_" };
            String[] blancas = { "C", "D", "E", "F", "G", "A", "B" };
            

            string entrada = e.Event.ToString();
            string notaFinal = entrada.Split('(', ',')[1];
            bool on = false;

            


            
            string salida = (Note.Get((SevenBitNumber)Int32.Parse(notaFinal)).ToString());
            salida = salida.Remove(salida.Length - 1);


            if (entrada.Contains("On"))
            {
                on = true;
            }
            else if (entrada.Contains("Off"))
            {
                on = false;
            }

            notaSalida.Text = entrada;
            
            foreach (Button p in panel1.Controls)
                if (p.Name == salida && on == true)
                {
                    p.BackColor = Color.AliceBlue;
                    activas.Add(salida);
                    label1.Text = activas.Count().ToString();
                    

                }
                else if (p.Name == salida && on == false)
                    {
                    activas.RemoveAll(x => x == salida);
                    label1.Text = activas.Count().ToString();

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

        

        private void button1_Click(object sender, EventArgs e)
        {




        }

        
    }

}
