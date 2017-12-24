using LanguageBuilder.Data.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace LanguageBuilder.Web.Areas.Admin.Models
{
    public class TranslationsUploadViewModel
    {
        [Required]
        public int FromLanguageId { get; set; }

        [Required]
        public int ToLanguageId { get; set; }

        public List<Language> Languages { get; set; }

        [Required]
        public IFormFile File { get; set; }

        public async Task<Dictionary<string, string>> ParseExcelInputData()
        {
            var dictionary = new Dictionary<string, string>();

            using (var memoryStream = new MemoryStream())
            {
                await this.File.CopyToAsync(memoryStream).ConfigureAwait(false);

                using (var package = new ExcelPackage(memoryStream))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    var rowCount = worksheet.Dimension?.Rows;
                    var colCount = worksheet.Dimension?.Columns;

                    for (int row = 1; row <= rowCount.Value; row++)
                    {
                        var sourceWord = worksheet.Cells[row, 1].Value.ToString();
                        var targetWord = worksheet.Cells[row, 2].Value.ToString();

                        if (String.IsNullOrEmpty(sourceWord) || String.IsNullOrEmpty(targetWord)) { continue; }

                        dictionary.Add(sourceWord, targetWord);
                    }
                }
            }

            return dictionary;
        }
    }
}
