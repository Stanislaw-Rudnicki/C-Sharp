using System;

namespace hw
{
    public partial class Paper
    {
        public string PaperInfo()
        {
            return $"Format: {InternationalType}\nWeight: {Weight}\nHeight: {Height}\nWidth: {Width}\nType: {Type}\nColor: {Color}";
        }
        public void PaperInfo(out string[] info)
        {
            int size = 6;
            info = new string[size];
            info[1] = "Format: " + InternationalType;
            info[2] = "Weight: " + Weight;
            info[3] = "Height: " + Height;
            info[4] = "Width: " + Width;
            info[5] = "Type: " + Type;
            info[6] = "Color: " + Color;
        }
    }
}
