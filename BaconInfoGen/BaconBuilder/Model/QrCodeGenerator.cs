using System.Drawing;
using ThoughtWorks.QRCode.Codec;

namespace BaconBuilder.Model
{
	public static class QrCodeGenerator
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="data">the string of data to convert to a qr code</param>
		/// <returns>the image of the qr code generated</returns>
		public static Image GenerateCode(string data)
		{
			var qrCodeEncoder =
				new QRCodeEncoder
					{
						//sets encode mode to encode as bytes
						QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE,
						QRCodeVersion = 4,
						QRCodeScale = 5,
						//must be light
						QRCodeBackgroundColor = Color.White,
						//must be dark
						QRCodeForegroundColor = Color.Black
					};

			return qrCodeEncoder.Encode(data);
		}
	}
}