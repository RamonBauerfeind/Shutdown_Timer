﻿using System;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace Shutdown_Timer
{
    public partial class Form1 : Form
    {
        //Zeit bis Alarm abläuft
        TimeSpan timeLeft;

        // Konstruktor
        public Form1()
        {
            InitializeComponent();
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            CheckTextBoxValues();
            bool isStarteble = true;

            try
            {
                // Können Werte aus Textboxen in Int konvertiert werden?
                timeLeft = new TimeSpan(Convert.ToInt32(txtHours.Text), Convert.ToInt32(txtMinutes.Text), Convert.ToInt32(txtSeconds.Text));
            }
            catch(FormatException ex)
            {
                isStarteble = false;
                MessageBox.Show(ex.Message);
            }

            if(isStarteble == true)
            {
                timer.Start();
                // Werte in String konvertieren und in Label Timer übernehmen
                lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");
            }
        }

        // wenn nichts eingegeben wird == 0
        private void CheckTextBoxValues()
        {
            if(txtHours.Text.Count() == 0)
            {
                txtHours.Text = "0";
            }

            if (txtMinutes.Text.Count() == 0)
            {
                txtMinutes.Text = "0";
            }

            if (txtSeconds.Text.Count() == 0)
            {
                txtSeconds.Text = "0";
            }
        }

        // Eventhandler für Timer
        private void timer_Tick(object sender, EventArgs e)
        {
            // jede Sekunde - 1 Sekunden
            timeLeft = timeLeft.Subtract(TimeSpan.FromSeconds(1));
            lblTimer.Text = timeLeft.ToString(@"hh\:mm\:ss");

            // wenn Testzeit <= 0
            if(timeLeft.TotalSeconds <= 0)
            {
                timer.Stop();
                PerformAction();
            }
        }

        private void PerformAction()
        {
            if(rbShutdown.Checked)
            {
                // Herunterfahren
                Process.Start("shutdown", "/s");
            }
            else if(rbRestart.Checked)
            {
                // Neustart
                Process.Start("shutdown", "/r");
            }
            else if(rbSavePower.Checked)
            {
                // Energiesparmodus
                Process.Start("run1132.exe", "powrprof.dll,SetSuspendState");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            lblTimer.Text = "00:00:00";
        }
    }
}
