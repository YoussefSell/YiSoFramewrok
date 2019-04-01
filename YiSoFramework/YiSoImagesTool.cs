namespace YiSoFramework
{
    using System;
    using System.Drawing;
    using System.IO;

    public static class YiSoImagesTool
    {
        ///<summary>
        ///convert a base 64 string into an image 
        ///</summary>
        ///<param name="Base64Image">the string that will be convert into image</param>
        public static Image LoadImage(string Base64Image)
        {
            byte[] bytes = Convert.FromBase64String(Base64Image);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        ///<summary>
        ///convert an image into base 64 string
        ///</summary>
        ///<param name="image">the image that will be convert into base 64 string</param>
        public static string ConvertImage(Image image)
        {
            string base64String = null;
            using (image)
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }
    }
}
