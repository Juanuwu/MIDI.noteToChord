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
            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };


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

        

       

        //TODO: agregar notas a una lista cuando llega un evento on y eliminarlas cuando llega el evento off, para tener lista de todas las teclas presionadas en un determinado momento

        List<string> activas = new List<string>();




        

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };

            string entrada = e.Event.ToString();
            string notaFinal = entrada.Split('(', ',')[1];
            bool on = false;

            
            string salida = (Note.Get((SevenBitNumber)Int32.Parse(notaFinal)).ToString());
            salida = salida.Remove(salida.Length - 1);
            //salida = entrada;


            //registro de tipo de evento y notas activas
            if (entrada.Contains("On"))
            {
                on = true;
                activas.Add(salida);
                label1.Text = activas.Count().ToString();
            }
            else if (entrada.Contains("Off"))
            {
                on = false;
                activas.RemoveAll(x => x == salida);
                label1.Text = activas.Count().ToString();
            }

            notaSalida.Text = salida;
            

            //cambio de colores en la interfaz
            foreach (Button p in panel1.Controls)
                if (p.Name == notaFinal && on == true)
                {
                    p.BackColor = Color.AliceBlue;
                    
                }
                else if (p.Name == notaFinal && on == false)
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

        

        private void button1_Click(object sender, EventArgs e)
        {




        }

        
    }

}
