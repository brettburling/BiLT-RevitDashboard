using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace RevitDashboard.Utils
{
    class Utils
    {

        public static BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly a = Assembly.GetExecutingAssembly();
                Stream s = a.GetManifestResourceStream(name);
                return BitmapFrame.Create(s);
            }
            catch
            {
                return null;
            }
        }


        public static double GetSizeInMegabytes(string path)
        {
            //The System.IO.FileInfo.Length property will give you the file size in bytes.
            //Divide by 1,048,576 (1024 x 1024) to get the size in megabytes
            return new FileInfo(path).Length / (1024 * 1024);
        }


    }
}
