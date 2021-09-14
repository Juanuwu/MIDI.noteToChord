using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Devices;
using System.IO;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;


namespace midi
{


    public partial class Form1 : Form
    {




        public Form1()
        {


            InitializeComponent();

            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };


            String[] total = new string[blancas.Length + Negras.Length];


            string[] negrasReturn = teclado.CrearTeclasNegras(Negras, panel1);
            string[] blancasReturn = teclado.CrearTeclasBlancas(blancas, panel1);
            string[] notasTotal = negrasReturn.Concat(blancasReturn).ToArray();


            getDevice();
        }


        public static class Globals
        {
            public static InputDevice inputDevice;
        }


        private void getDevice()
            {

            try
            {
                TextBox.CheckForIllegalCrossThreadCalls = false;
                Globals.inputDevice = InputDevice.GetById(0);
                Globals.inputDevice.EventReceived += OnEventReceived;
                Globals.inputDevice.StartEventsListening();
            }

            catch
            {

            }
               
            
            }

        private void Form1_Resize(object sender, EventArgs e)
        {
            
                
            
        }



       

        List<string> activas = new List<string>();
        List<Melanchall.DryWetMidi.MusicTheory.Note> notasAcorde = new List<Note>();



        private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            String[] Negras = { "37", "39", "_", "42", "44", "46", "_", "49", "51", "_", "54", "56", "58", "_", "61", "63", "_", "66", "68", "70", "_", "73", "75", "_", "78", "80", "82", "_", };
            String[] blancas = { "36", "38", "40", "41", "43", "45", "47", "48", "50", "52", "53", "55", "57", "59", "60", "62", "64", "65", "67", "69", "71", "72", "74", "76", "77", "79", "81", "83" };

            string entrada = e.Event.ToString();
            string notaFinal = entrada.Split('(', ',')[1];
            bool on = false;




            string salida = (Note.Get((SevenBitNumber)Int32.Parse(notaFinal)).ToString());
            salida = salida.Remove(salida.Length - 1);


            //registro de tipo de evento y notas activas
            if (entrada.Contains("On"))
            {
                on = true;
                activas.Add(notaFinal);
                var notita = Note.Get((SevenBitNumber)Int32.Parse(notaFinal));
                notasAcorde.Add(notita);
                PostRequestChord(salida);
                //label1.Text = notasAcorde.Count().ToString();

            }

            else if (entrada.Contains("Off"))
            {
                on = false;
                activas.RemoveAll(x => x == notaFinal);
                // label1.Text = notasAcorde.Count().ToString();
                string nota = notaFinal + "c";
                PostRequestChord(nota);
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



        private void GetRequestChord(object sender, EventArgs e)
        {

            string html = string.Empty;
            string url = @"http://127.0.0.1:5000/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;



            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                
                html = reader.ReadToEnd();
                label1.Text = html;


            }

            Console.WriteLine(html);
        }





        private void PostRequestChord(string nota)
        {


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://127.0.0.1:5000/");
            httpWebRequest.ContentType = "text/html";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = nota;
                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                label1.Text = result;
                Console.WriteLine(result);
;            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            getDevice();

        }
    }
}

