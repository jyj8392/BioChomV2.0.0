using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace BioChome.Equipment.Dialog
{
    public partial class UVLampOpening : Form
    {
        public UVLampOpening()
        {
            InitializeComponent();
        }

        Thread th_recvFromUVSerialPort;
        private void UVLampOpening_Load(object sender, EventArgs e)
        {
            th_recvFromUVSerialPort = new Thread(WaitLampOpenning);
            th_recvFromUVSerialPort.Start();

        }
        
        private void WaitLampOpenning()
        {
            if (FrmLeft.uvInstance.UVLampOpen()) return;
            while (!FrmLeft.uvInstance.WaitLampOpening(FrmLeft.uvInstance.t_SerialPortCommu.uvPort)) ;
            UVDetector.UV.t_UVPara.ISLampDeuteriumOpen = true;
            this.Invoke(new EventHandler(delegate
            {
                this.Close();
            }));

        }
    }
}
