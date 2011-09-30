using System.Drawing;
using ThoughtWorks.QRCode.Codec;

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
			var qrCodeEncoder = new QRCodeEncoder();

			//sets encode mode to encode as bytes
			qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
			qrCodeEncoder.QRCodeVersion = 4;
			qrCodeEncoder.QRCodeScale = 5;
            qrCodeEncoder.QRCodeBackgroundColor = Color.White; //must be light
			qrCodeEncoder.QRCodeForegroundColor = Color.Black; //must be dark
			Image qrCode = qrCodeEncoder.Encode(data);
			return qrCode;
		}
	}
}