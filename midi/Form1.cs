using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.MusicTheory;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Python.Runtime;


namespace midi
{


    public partial class Form1 : Form
    {


        public Form1()
        {

            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
            InitializeComponent();
            getDevice();


            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };


            string[] negrasReturn = teclado.CrearTeclasNegras(Negras, panel1);
            string[] blancasReturn = teclado.CrearTeclasBlancas(blancas, panel1);

        }


        public static class Globals
            {

                public static InputDevice inputDevice;
                public static bool stop = true;

            }


        private void getDevice()
        {
            
            try
            {
                
                TextBox.CheckForIllegalCrossThreadCalls = false;
                Globals.inputDevice = InputDevice.GetById(0);
                Globals.inputDevice.EventReceived += OnEventReceived;
                Globals.inputDevice.StartEventsListening();
                Globals.stop = false;

            }

            catch
            {
                
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (Globals.stop)
            {
                getDevice();
            }

            else
            {
                Globals.inputDevice.Dispose();
                getDevice();
            }

            
            
        }

        List<string> activas = new List<string>();
        List<Note> notasAcorde = new List<Note>();

        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {

            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };

            string entrada = e.Event.ToString();
            string notaFinal = entrada.Split('(', ',')[1];
            bool on = false;

            var token = PythonEngine.AcquireLock();
            string salida = (Note.Get((SevenBitNumber)Int32.Parse(notaFinal)).ToString());
            salida = salida.Remove(salida.Length - 1);


            //registro de tipo de evento y notas activas
            if (entrada.Contains("On"))
            {

                on = true;
                activas.Add(notaFinal);
                var notita = Note.Get((SevenBitNumber)Int32.Parse(notaFinal));
                notasAcorde.Add(notita);
                CambiarColor();
                PostRequestChord(salida);
                
            }

            else if (entrada.Contains("Off"))
            {

                on = false;
                activas.RemoveAll(x => x == notaFinal);
                string nota = salida + "c";
                CambiarColor();
                PostRequestChord(nota);
                
            }


            //cambio de colores en la interfaz

            void CambiarColor()
            {
                foreach (Button p in panel1.Controls)
                    if (p.Name == notaFinal && on == true)
                    {
                        p.BackColor = Color.AliceBlue;

                    }
                    else if (p.Name == notaFinal && on == false)
                    {


                        foreach (string nota in Negras)
                        {
                            if (p.Name == nota)
                            {

                                p.BackColor = Color.Black;

                            }
                        }

                        foreach (string nota in blancas)
                        {
                            if (p.Name == nota)
                            {

                                p.BackColor = Color.White;


                            }
                        }

                    }

            }

            PythonEngine.ReleaseLock(token);
        }


        List<string> listaNotas = new List<string>();

        public void PostRequestChord(string input)
        {


            try
            {

                using (Py.GIL())
                {

                    PyList pyTest = new PyList();


                    Console.WriteLine(input);

                    if (input.Contains("c") == false)
                        listaNotas.Add(input);
                    if (input.Contains("c"))
                        listaNotas.Remove(input.Remove(input.Length - 1));


                    using (PyScope scope = Py.CreateScope())
                    {

                        for (int i = 0; i < listaNotas.Count; i++)
                        {
                            pyTest.Append(new PyString(listaNotas[i]));
                            Console.WriteLine(listaNotas[i]);
                        }

                        scope.Set("test", pyTest);
                        dynamic chord = scope.Get("test");
                        dynamic pychord = scope.Import("pychord");
                        dynamic music21 = scope.Import("music21");
                        dynamic myChord = pychord.analyzer.note_to_chord(chord);


                        if (Convert.ToString(myChord) == "[]")
                        {
                            myChord = music21.chord.Chord(chord);
                            myChord = myChord.pitchedCommonName;
                        }
                        
                        Console.WriteLine(myChord);

                        string value = Convert.ToString(myChord);
                        notaSalida.Text = value;


                    }
                }


            }


            catch
            {


            }

            
        }

        
    }
}



