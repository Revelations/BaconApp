using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;

namespace BaconBuilder.Model
{
    public class QrCodeGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">the string of data to convert to a qr code</param>
        /// <returns>the image of the qr code generated</returns>
        public Image GenerateCode(string data)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

            //sets encode mode to encode as bytes
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeVersion = 4;
            qrCodeEncoder.QRCodeScale = 5;
            Image qrCode = qrCodeEncoder.Encode(data);
            return qrCode;
        }
    }
}
