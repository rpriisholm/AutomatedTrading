using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace Stocks.Export
{
    public static class InteropExcel
    {
        public static void ExportToMySql(string filePath)
        {
            Excel.Application app = new Excel.Application();
            try
            {
                Excel.Workbook workbook = app.Workbooks.Open(filePath, ReadOnly: true);
                //excel.Visible = true;
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    int nrOfColumns = sheet.UsedRange.Rows[1].Cells.Count;
                    int nrOfRows = sheet.UsedRange.Rows.Count;
                    Excel.Range rows = sheet.UsedRange.Rows;

                    Dictionary<string, int> ColumnIndex = new Dictionary<string, int>();

                    for (int column = 1; column < nrOfColumns; column++)
                    {
                        string value = rows[1].Cells[column].Value;

                        switch (value)
                        {
                            case "timestamp":
                                ColumnIndex["StartTime"] = column;
                                break;
                            case "open":
                                ColumnIndex["Start"] = column;
                                break;
                            case "high":
                                ColumnIndex["High"] = column;
                                break;
                            case "low":
                                ColumnIndex["Low"] = column;
                                break;
                            case "close":
                                ColumnIndex["End"] = column;
                                break;
                        }
                    }

                    for (int i = 2; i < nrOfRows; i++)
                    {
                        Excel.Range row = rows[i].Cells;
                        for (int column = 1; column < nrOfColumns; column++)
                        {
                            dynamic value = row[column].Value;
                        }
                    }


                    for (int currentRow = 2; currentRow < nrOfRows; currentRow++)
                    {
                        Excel.Range row = sheet.UsedRange.Rows[currentRow].Cells;

                        dynamic v = row[ColumnIndex["StartTime"]].Value;

                        DateTime startTime = (DateTime)row[ColumnIndex["StartTime"]].Value;
                        double start = (double)row[ColumnIndex["Start"]].Value;
                        double high = (double)row[ColumnIndex["High"]].Value;
                        double low = (double)row[ColumnIndex["Low"]].Value;
                        double end = (double)row[ColumnIndex["End"]].Value;
                    }
                }
            }
            finally
            {
                app.Quit();
            }
        }
    }
}
