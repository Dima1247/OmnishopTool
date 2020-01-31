using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmnishopToExcelImport
{
    public static class ExcelProdvider
    {
        private static void SetHeader(IXLWorksheet ws, int row, int col, string value, XLColor clr)
        {
            ws.Cell(row, col).SetValue(value);
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = clr;
        }

        public static XLWorkbook GetWorkbook(string filePath)
        {
            return new XLWorkbook(new FileStream(filePath, FileMode.Open));
        }

        public static List<List<string>> GetExcelData(XLWorkbook workbook, int workSheetNumber = 1, int colSize = 25, bool skipHeaders = true)
        {
            var data = new List<List<string>>();

            foreach (var row in workbook.Worksheets.ToList()[workSheetNumber - 1].Rows().ToList())
            {
                try
                {
                    var cellValues = new List<string>();
                    
                    if (skipHeaders && row.RowNumber() == 1)
                        continue;
                
                    for (int i = 1; i <= colSize; i++)
                        cellValues.Add(row.Cell(i).Value.ToString());

                    data.Add(cellValues);
                }
                catch
                {
                    Console.WriteLine("Row " + row.RowNumber() + " had an error!");
                }
            }

            return data;
        }

        public static Dictionary<string, int> GetExcelHeaders(List<List<string>> excelContent)
        {
            var excelHeaders = new Dictionary<string, int>();

            for (int i = 0; i < excelContent[0].Count; i++)
            {
                if (!string.IsNullOrEmpty(excelContent[0][i]))
                {
                    excelHeaders.Add(excelContent[0][i].Trim(), i);
                }
            }

            return excelHeaders;
        }

        public static void CreateWorkSheet(List<string> headers, List<List<string>> data, string folderName, string fileName)
        {
            int currentId = 0, previousId = 0, page = 0;
            Directory.CreateDirectory(folderName);

            while (currentId < data.Count)
            {
                int row = 1, col = 1;
                var workbook = new XLWorkbook();
                var ws = workbook.Worksheets.Add("WB1");

                for (int i = 1; i <= headers.Count; i++)
                    SetHeader(ws, row, i, headers[i - 1], XLColor.LightGray);

                row++;

                var currentData = new List<List<string>>();

                for (int i = currentId; i < previousId + 250 && i < data.Count; i++)
                {
                    currentData.Add(data[i]);
                }

                foreach (var d in currentData)
                {
                    col = 1;

                    foreach (var p in d)
                    {
                        ws.Cell(row, col).SetValue(p);
                        col++;
                    }

                    row++;
                    currentId++;
                }

                var mStream = new MemoryStream();

                workbook.SaveAs(mStream);
                previousId = currentId;
                page++;

                using (var fileStream = File.Create(Environment.CurrentDirectory + $"\\{folderName}\\{fileName}_" + page + ".xlsx"))
                {
                    mStream.Seek(0, SeekOrigin.Begin);
                    mStream.CopyTo(fileStream);
                }
            }
        }
    }
}
