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

                var numberOfContactosAdded = 0;

                IRange usedRange = worksheet.UsedRange;
                int lastRow = usedRange.LastRow;
                int lastColumn = usedRange.LastColumn;

                for (int row = 2; row <= lastRow; row++)
                {
                    var nombre = worksheet[row, 1].Value;
                    var direccion = worksheet[row, 2].Value;
                    var telefono = worksheet[row, 3].Value;
                    var curp = worksheet[row, 4].Value;
                    var fechaRegistro = worksheet[row, 5].Value;

                    int nombreLen = nombre.Length > 100 ? 100 : nombre.Length;
                    int direccionLen = direccion.Length > 200 ? 200 : direccion.Length;
                    int telefonoLen = telefono.Length > 50 ? 50 : telefono.Length;
                    int curpLen = curp.Length > 18 ? 18 : curp.Length;
                    var contacto = new Contacto
                    {
                        Nombre = nombre.ToString().Substring(0, nombreLen),
                        Direccion = direccion.ToString().Substring(0, direccionLen),
                        Telefono = telefono.ToString().Substring(0, telefonoLen),
                        Curp = curp.ToString().Substring(0, curpLen),
                        FechaRegistro = fechaRegistro == "" ? DateTime.Now : Convert.ToDateTime(fechaRegistro)
                    };


                    await _context.Contactos.AddAsync(contacto);


                    numberOfContactosAdded++;
                }


                if (numberOfContactosAdded > 0) await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

    }
}
