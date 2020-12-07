using System;
using System.Windows.Forms;
using Presentation.Forms;

namespace WinFormsApp.Forms
{
    public partial class Form1 : Form,IMainForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        public event Action ButtonPressed;
        public string TextBox { get; set; }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonPressed?.Invoke();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox = textBox1.Text;
        }
    }
}
