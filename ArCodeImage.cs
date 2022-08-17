using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YuyukoRecord.mq;
using ZXing;
using ZXing.QrCode;

namespace YuyukoRecord
{
    public partial class ArCodeImage : Form
    {
        public ArCodeImage()
        {
            InitializeComponent();
        }

        private void ArCodeImage_Load(object sender, EventArgs e)
        {
            BarcodeWriter writer = new BarcodeWriter();
            QrCodeEncodingOptions qr = new QrCodeEncodingOptions()
            {
                CharacterSet = "UTF-8",
                ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H,
                Height = this.pictureBoxQrCode.Size.Height,
                Width = this.pictureBoxQrCode.Size.Width
            };
            writer.Options = qr;
            writer.Format = BarcodeFormat.QR_CODE;
            Bitmap bitmap = writer.Write(MqttUtils.CLIENT_ID);
            this.pictureBoxQrCode.Image = bitmap;
        }



    }
}
