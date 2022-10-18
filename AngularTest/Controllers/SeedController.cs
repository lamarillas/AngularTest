using AngularTest.Data;
using AngularTest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.XlsIO;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AngularTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDataContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(ApplicationDataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Import()
        {
            ExcelEngine excelEngine = new ExcelEngine();
            IApplication application = excelEngine.Excel;

            application.DefaultVersion = ExcelVersion.Xlsx;

            var path = Path.Combine(
             _env.ContentRootPath,
             "Data/Source/contactos.xlsx");

            FileStream sampleFile = new FileStream(path, FileMode.Open);
            IWorkbook workbook = application.Workbooks.Open(sampleFile);

            IWorksheet worksheet = workbook.Worksheets[0];

            // initialize the record counters 
            var numberOfContactosAdded = 0;

            //Access the used range of the Excel file
            IRange usedRange = worksheet.UsedRange;
            int lastRow = usedRange.LastRow;
            int lastColumn = usedRange.LastColumn;
            //Iterate the cells in the used range and print the cell values
            for (int row = 1; row <= lastRow; row++)
            {
                var nombre = worksheet[row, 1].Value.ToString();
                var direccion = worksheet[row, 2].Value.ToString();
                var telefono = worksheet[row, 3].Value.ToString();
                var curp = worksheet[row, 4].Value.ToString();
                var fechaRegistro = worksheet[row, 5].Value.ToString();
                // create the Country entity and fill it with xlsx data 
                var contacto = new Contacto
                {
                    Nombre = nombre,
                    Direccion = direccion,
                    Telefono = telefono,
                    Curp = curp,
                    FechaRegistro = DateTime.Now
                };

                // add the new country to the DB context 
                await _context.Contactos.AddAsync(contacto);

                // increment the counter 
                numberOfContactosAdded++;
            }

            // save all the countries into the Database 
            if (numberOfContactosAdded > 0) await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
