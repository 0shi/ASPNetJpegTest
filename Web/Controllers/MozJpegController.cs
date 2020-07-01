using System.IO;
using System.Drawing;
using System.Net;
using System.Web.Mvc;
using MozJpegSharp;

namespace Web.Controllers
{
    public class MozJpegController : Controller
    {
        public ActionResult Compress(string path)
        {
            using (var bmp = new Bitmap(path))
            {
                using (var tjc = new TJCompressor())
                {
                    var compressed = tjc.Compress(bmp, TJSubsamplingOption.Chrominance420, 85, TJFlags.None);
                    var outPath = Path.Combine(Path.GetDirectoryName(path), $"compressed-", Path.GetFileName(path));
                    System.IO.File.WriteAllBytes(outPath, compressed);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}