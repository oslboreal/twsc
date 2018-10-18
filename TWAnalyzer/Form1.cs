using System;
using System.Windows.Forms;

namespace TWAnalyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer_Liker.Start();
            Procesador.Proceso.Inicializar();
            this.Log($"Juan Marcos Vallejo - {DateTime.Now} Inicializando.");
        }

        /// <summary>
        /// Timer AUTO LIKER MENTIONS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
            Log(Vista.InformacionProceso());
        }

        #region Métodos de Dibujado de User Interface

        /// <summary>
        /// Método de actualización de UI
        /// </summary>

        /// <summary>
        /// Método encargado de mostrar por pantalla un anuncio
        /// </summary>
        /// <param name="text"></param>
        private void Log(string text)
        {
            if (this.richTextBox1.Text == "")
                this.richTextBox1.Text = this.richTextBox1.Text + DateTime.Now.ToString() + " - " + text;
            else
                this.richTextBox1.Text = this.richTextBox1.Text + "\n" + DateTime.Now.ToString() + " - " + text;
        }

        /// <summary>
        /// Método encargado de mostrar por pantalla un anuncio con un reporte
        /// </summary>
        /// <param name="text"></param>
        /// <param name="report"></param>
        private void Log(string text, string report)
        {
            if (this.richTextBox1.Text == "")
                this.richTextBox1.Text = this.richTextBox1.Text + DateTime.Now.ToString() + " - " + text;
            else
                this.richTextBox1.Text = this.richTextBox1.Text + "\n" + DateTime.Now.ToString() + " - " + text;
        }

        /// <summary>
        /// Método encargado de Registrar un comando en el Log.
        /// </summary>
        /// <param name="text"></param>
        private void LogCommand(string text)
        {
            if (this.richTextBox1.Text == "")
                this.richTextBox1.Text = this.richTextBox1.Text + DateTime.Now.ToString() + " - COMMAND:" + text;
            else
                this.richTextBox1.Text = this.richTextBox1.Text + "\n" + DateTime.Now.ToString() + " - COMMAND:" + text;
        }
        #endregion
    }
}
