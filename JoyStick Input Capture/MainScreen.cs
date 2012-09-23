using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Controls;

namespace JoyStick_Input_Capture
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();

            this.deviceScanWorker.DoWork += new DoWorkEventHandler(deviceScanDoWork);

            //TODO not supported at the moment
            //this.deviceScanWorker.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);

            this.deviceScanWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(deviceScanWorkerCompleted);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.deviceScanWorker.IsBusy != true)
            {
                this.deviceScanWorker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Does the work of scanning the devices and updating the DoWorkEventArgs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deviceScanDoWork(object sender, DoWorkEventArgs e)
        {
            // enumerate available devices
            List<Joystick.DeviceInfo> devices = Joystick.GetAvailableDevices();

            string result = "Found " + devices.Count() + " devices";
            foreach (Joystick.DeviceInfo di in devices)
            {
                string deviceInfo = string.Format("{0} : {1} ({2} axes, {3} buttons)",
                                                  di.ID, 
                                                  di.Name, 
                                                  di.Axes, 
                                                  di.Buttons);
                System.Diagnostics.Debug.WriteLine(deviceInfo);
                result += deviceInfo;
            }

            /*
            // create new joystick and initialize it
            Joystick joystick = new Joystick(0);
            // get its current status
            Joystick.Status status = joystick.GetCurrentStatus();
            // check if 1st button is pressed
            if (status.IsButtonPressed(Joystick.Buttons.Button1))
            {
                // 1st button is pressed
            }
            */

            //Set the result to our device information that we found
            e.Result = result;
        }

        /// <summary>
        /// Callback when the device scan is completed. Display the results in the GUI
        /// </summary>
        private void deviceScanWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Cancelled == true))
            {
                //Not implemented yet
                this.textBox1.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                //Not implemented yet
                this.textBox1.Text = ("Error: " + e.Error.Message);
            }

            else
            {
                this.textBox1.Text = e.Result.ToString();
            }
        }
    }
}
