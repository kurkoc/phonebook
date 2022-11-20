using OfficeOpenXml;
using System.Reflection;
using System;

namespace Report.API.Infrastructure.Excel
{
    public class ExcelHelper
    {
        public static byte[] GenerateExcel<T>(List<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("");

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var worksheet = excel.Workbook.Worksheets.Add("Sheet1");

            //header
            PropertyInfo[] properties = typeof(T).GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                worksheet.Cells[1, i+1].Value = properties[i].Name;
            }

            int rowNumber = 2;
            foreach (T item in list)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[rowNumber, i+1].Value = typeof(T).GetProperty(properties[i].Name)?.GetValue(item);
                }
                rowNumber++;    
            }
            return excel.GetAsByteArray();
        }
    }
}
