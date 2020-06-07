using System;
using System.Drawing;
using System.Windows.Forms;

namespace midi
{
    class teclado
    {
        public static int AnchoiTeclasNegras = 40;
        public static int AltoTeclasNegras = 250;

        public static int AnchoTeclasBlancas = 50;
        public static int AltoTeclasBlancar = 300;


        public static string[] CrearTeclasNegras(String[] notasDeTeclasNegras, Panel panel)
        {

            string[] notas = new string[notasDeTeclasNegras.Length];

            for (int i = 0; i < notasDeTeclasNegras.Length; i++)
            {

                if (notasDeTeclasNegras[i] != "_")
                {

                    Button TeclaNegra = new Button();
                    TeclaNegra.BackColor = Color.Black;
                    TeclaNegra.ForeColor = Color.White;
                    

                    TeclaNegra.Size = new Size(AnchoiTeclasNegras, AltoTeclasNegras);
                    notas[i] = TeclaNegra.Name = notasDeTeclasNegras[i].ToString();
                    notasDeTeclasNegras[i].ToString();
                    TeclaNegra.Location = new Point(TeclaNegra.Location.X + (i * AnchoTeclasBlancas) + 30, TeclaNegra.Location.Y);
                    panel.Controls.Add(TeclaNegra);

                }

            }

            return notas;
        }


        public static string[] CrearTeclasBlancas(String[] notasDeTeclasBlancas, Panel panel)
        {

            string[] notas = new string[notasDeTeclasBlancas.Length];

            for (int i = 0; i < notasDeTeclasBlancas.Length; i++)
            {

                Button TeclaBlanca = new Button();
                TeclaBlanca.BackColor = Color.White;
                TeclaBlanca.ForeColor = Color.Black;

                TeclaBlanca.Size = new Size(AnchoTeclasBlancas, AltoTeclasBlancar);
                notas[i] = TeclaBlanca.Name = notasDeTeclasBlancas[i].ToString();
                notasDeTeclasBlancas[i].ToString();
                int TheX = TeclaBlanca.Location.X + (i * AnchoTeclasBlancas);
                TeclaBlanca.Location = new Point(TheX, TeclaBlanca.Location.Y);
                panel.Controls.Add(TeclaBlanca);


            }

            return notas;

            
        }


       








}
}
