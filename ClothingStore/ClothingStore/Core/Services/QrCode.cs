using MessagingToolkit.QRCode.Codec;
using System.Drawing;

namespace ClothingStore.Core.Services
{
    public class QrCode
    {
        public void CreateQrCode(string qrtext, string name)
        {
            QRCodeEncoder encoder = new QRCodeEncoder(); //создаём новую "генерацию кода"
            Bitmap qrcode = encoder.Encode(qrtext);
            qrcode.Save($"Content\\QrCodes\\{name}.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
