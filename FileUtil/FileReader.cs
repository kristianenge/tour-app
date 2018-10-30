using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace tourApp.FileUtil
{
    public static class FileReader
    {
        public static string ReadEmbeddedFile(string resourceRelativePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceRelativePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
