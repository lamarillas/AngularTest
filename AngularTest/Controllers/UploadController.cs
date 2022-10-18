using AngularTest.Data;
using AngularTest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.XlsIO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AngularTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        private readonly ApplicationDataContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadController(ApplicationDataContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                if (file.Length <= 0)
                    return BadRequest();

                // 
                ExcelEngine excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook workbook = application.Workbooks.Open(file.OpenReadStream());


                //
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


                //var folderName = Path.Combine("Resources", "images");
                //var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                //if (file.Length > 0)
                //{
                //    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                //    var fullPath = Path.Combine(pathToSave, fileName);
                //    var dbPath = Path.Combine(folderName, fileName);
                //    using (var stream = new FileStream(fullPath, FileMode.Create))
                //    {
                //        file.CopyTo(stream);
                //    }
                //    return Ok(new { dbPath });
                //}
                //else
                //{
                //    return BadRequest();
                //}
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
