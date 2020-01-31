using Newtonsoft.Json;
using OmnishopFourSound.DAL;
using OmnishopFourSound.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmnishopToExcelImport
{
    class Program
    {
        private static OmnishopFSContext context = new OmnishopFSContext();

        static void Main(string[] args)
        {
            Directory.CreateDirectory("_data");

            //var info0 = JsonConvert.DeserializeObject<List<ProductImportModel>>(File.ReadAllText("productForImport.json"));
            PreImportConfig.SetCategoryPaths();
        }
    }
}
