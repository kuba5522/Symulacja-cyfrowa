﻿using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace Restauracja
{
    public class Excel
    {
        private readonly _Application excel = new _Excel.Application();
        private readonly Workbook Wb;
        private readonly Worksheet Ws;

        public Excel(string path, int sheet)
        {
            Wb = excel.Workbooks.Open(path);
            Ws = Wb.Worksheets[sheet];
        }
        /*
        public string ReadCell(int i, int j)
        {
            return Ws.Cells[i, j].Value2 != null ? (string) Ws.Cells[i, j].Value2 : "";
        }*/

        public void WriteToCell(int i, int j, string val)
        {
            Ws.Cells[i, j].Value2 = val;
            Wb.Save();
        }
        public void Close()
        {
            Wb.Close(0);
        }

    }
}
