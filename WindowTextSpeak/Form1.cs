using System;
using System.Speech;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO;

namespace WindowTextSpeak
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speechSyn;
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            speechSyn = new SpeechSynthesizer();
            buttonResume.Enabled = false;
            buttonPause.Enabled = false;
            buttonStop.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            //MessageBox.Show(text);
            speechSyn.Dispose();
            if(richTextBox1.Text != "")
            {
                speechSyn = new SpeechSynthesizer();
                speechSyn.SpeakAsync(text);
                buttonPause.Enabled = true;
                buttonStop.Enabled = true;
                label1.Text = "Playing";
            }
            else
            {
                MessageBox.Show("Please Enter Some Text or Load it from a File.", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(speechSyn != null)
            {
                speechSyn.Dispose();
                buttonPause.Enabled = false;
                buttonResume.Enabled = false;
                buttonStop.Enabled = false;
                richTextBox1.Text = "";
                label1.Text = "IDLE";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName.ToString());
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if(speechSyn != null && speechSyn.State ==SynthesizerState.Speaking)
            {
                speechSyn.Pause();
                buttonResume.Enabled = true;
                buttonPause.Enabled = false;
                label1.Text = "Paused";
            }
        }

        private void buttonResume_Click(object sender, EventArgs e)
        {
            if(speechSyn != null && speechSyn.State == SynthesizerState.Paused)
            {
                speechSyn.Resume();
                buttonResume.Enabled = false;
                buttonPause.Enabled = true;
                label1.Text = "Playing";
            }
        }

       
    }
}
