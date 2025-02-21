namespace ReactWebApplication.Models
{
    internal class Device
    {
        //public static byte[] ImageToByteArray(PictureBox imageIn)
        //{
        //    var ms = new MemoryStream();
        //    imageIn.Image.Save(ms, imageIn.Image.RawFormat);
        //    return ms.ToArray();
        //}
        //public Image ByteArrayToImage1(byte[] byteArrayIn)
        //{
        //    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
        //    Image img = (Image)converter.ConvertFrom(byteArrayIn);

        //    return img;
        //}
        //public static Image ByteArrayToImage(byte[] byteArrayIn)
        //{
        //    var ms = new MemoryStream(byteArrayIn);
        //    Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}

        public static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}