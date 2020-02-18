using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Devices;

using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi
{
    public partial class Form1 : Form
    {

        
        public Form1()
        {

            string input = "F#";
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;


            String[] Negras = { "C#", "D#", "_", "F#", "G#", "A#", "_" };
            String[] blancas = { "C", "D", "E", "F", "G", "A", "B" };



            String[] total = new string[blancas.Length + Negras.Length];


            string[] negrasReturn = teclado.CrearTeclasNegras(Negras, panel1);
            string[] blancasReturn = teclado.CrearTeclasBlancas(blancas, panel1);
            string[] notasTotal = negrasReturn.Concat(blancasReturn).ToArray();

            foreach (var nota in notasTotal)
            {


                foreach (Control p in panel1.Controls)
                    if (p.Name == input)
                    {
                        p.BackColor = Color.AliceBlue;

                    }
                
            }

           
            var inputDevice = InputDevice.GetById(0);
            
                inputDevice.EventReceived += OnEventReceived;
                inputDevice.StartEventsListening();

            var note1 = Note.Get((SevenBitNumber)100);



            

            void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
            {

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

                var midiDevice = (MidiDevice)sender;
                
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


        }

        



    }

}
