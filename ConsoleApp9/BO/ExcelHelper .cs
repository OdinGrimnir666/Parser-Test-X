using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Parser_Test
{
    class ExcelHelper : IDisposable
    {
        private Excel.Application _excel;
        private Excel.Workbook _workbook;
        private string _filePath;

        public ExcelHelper()
        {
            _excel = new Excel.Application();
        }

        internal bool Open(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    _workbook = _excel.Workbooks.Open(filePath);
                }
                else
                {
                    _workbook = _excel.Workbooks.Add();
                    _filePath = filePath;
                }

                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        internal void Save()
        {
            if (!string.IsNullOrEmpty(_filePath))
            {
                _workbook.SaveAs(_filePath);
                _filePath = null;
            }
            else
            {
                _workbook.Save();
            }
        }

        internal bool SetPhome(List<Phone> Phone)
        {
            try
            {

                // var val = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;
                int  row1=1;
                
                foreach (var item in Phone)
                {
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[row1, "A"] = item.Name;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[row1, "B"] = item.Praice;
                    ((Excel.Worksheet)_excel.ActiveSheet).Cells[row1, "C"] = item.Url;
                    row1++;

                }

                    return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }


        internal bool Set(string column, int row, object data)
        {
            try
            {
                // var val = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;

                ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column] = data;
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }



        internal List<Phone> GetPhone()
        {

              var Phone=new List<Phone>(); 

            try
            {
                
                int row = 1;

                while (null!= ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "A"].value) 
                {
                    Phone.Add(new Phone()
                    {
                        
                        Name = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "A"].value,
                        Praice =((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "B"].Value,
                        Url = ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, "C"].value
                    });

                    row++;

                }                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return Phone;
        }
        internal object Get(string column, int row)
        {
            try
            {
                return ((Excel.Worksheet)_excel.ActiveSheet).Cells[row, column].Value2;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }




        public void Dispose()
        {
            try
            {
                _workbook.Close();
                _excel.Quit();;
                
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}
