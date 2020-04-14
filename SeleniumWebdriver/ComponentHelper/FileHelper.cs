using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumWebdriver.ComponentHelper
{
    public class FileHelper
    {
        public static string SaveScreenShot(string absolutePath)
        {
            var dir = Directory.Exists(absolutePath);
            if (!dir)
            {
                Directory.CreateDirectory(absolutePath);
            }

            return absolutePath;

        }
    }
}
