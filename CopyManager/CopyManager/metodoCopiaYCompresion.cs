using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace CopyManager
{
    public class metodoCopiaYCompresion
    {
        public static void comprimir(string archivo, string resultado)
        {
            ZipFile.CreateFromDirectory(archivo, resultado);
        }

        public static void borrar(string ruta)
        {
            File.Delete(ruta);
        }
    }
}
