using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TurboJpegWrapper;

namespace Web.Controllers
{
    public class LibJpegTurboController : Controller
    {
        public ActionResult Compress(string path)
        {
            using (var bmp = new Bitmap(path))
            {
                using (var tjc = new TJCompressor())
                {
                    // (TJFlags)16384 = Progressive Image
                    var compressed = tjc.Compress(bmp, TJSubsamplingOption.Chrominance420, 85, (TJFlags)16384);
                    var outPath = Path.Combine(Path.GetDirectoryName(path), $"compressed-", Path.GetFileName(path));
                    System.IO.File.WriteAllBytes(outPath, compressed);
                }
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}