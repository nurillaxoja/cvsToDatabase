using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Task.Data;
using Task.Models;

namespace Task.Controllers
{
    public class Personnel_RecordsController : Controller
    {
        private int columnsCount = 0; //used to count successful read columns
        private readonly ApplicationDbContext _db;
        public Personnel_RecordsController(ApplicationDbContext db) // connect to database 
        {
            _db = db;
        }
        //Personnel_Records

        [HttpGet]
        public IActionResult Index(List<Personnel_Records> records = null)
        {
            records = records == null ? new List<Personnel_Records>() : records;
            return View(records);
        }
        

        /// <summary>
        /// Used to upload file 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="hostingEnvironment"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            //TempData["AllertMessage"] = "";

            #region Upload file
            if (file != null) // 
            {
                if (file.FileName.EndsWith(".csv"))
                {
                    string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
                    using (FileStream fileStream = System.IO.File.Create(fileName)) 
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    var records = this.GetStudentsList(file.FileName); // used to get columns values
                    if (records != null)
                    {
                        TempData["AllertMessage"] = String.Format("{0} Columns were added successfully", columnsCount);
                        return Index(records); // refreshes page 
                    }
                    TempData["AllertMessage"] = String.Format("Please check your database columns");
                }
                else
                {
                    TempData["Allert message"] = "Please check the file type";
                }
            }
            return Index();
            #endregion Upload file
        }
        /// <summary>
        /// Used to read cvs file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private List<Personnel_Records> GetStudentsList(string fileName)
        {
            List<Personnel_Records> records = new List<Personnel_Records>(); 

            #region read

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName; // gets file path
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read()) /// reads every cell of csv
                    {
                        columnsCount += 1;
                        var record = csv.GetRecord<Personnel_Records>();
                        _db.Personnel_Records.Add(record); // saves to the database new columns 
                        _db.SaveChanges(); // saves to the database new columns 
                        records.Add(record); // used to return in order to show added columns 
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            #endregion read end

            return records;
        }
        /// <summary>
        /// Gets all information from database and shows  
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllRecordsJson()
        {
            var res = _db.Personnel_Records;
            var data = new
            {
                Items = res,
                TotalCount = res.Count()
            };
            return new JsonResult(data);
        }

        /// <summary>
        /// Used to extract changed values from URL
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(IEnumerable<Personnel_Records> records)
        {
            try
            {
                var form = Request.Form;
                var models = JsonConvert.DeserializeObject<IEnumerable<Personnel_Records>>(Request.Form.FirstOrDefault().Value); // converts to json format in Personnel_Records format
                Personnel_Records _records = new Personnel_Records();
                foreach (Personnel_Records item in models) // used to read data from IEnumerable and saves  to new value
                {
                    _records.Id = item.Id;
                    _records.Payroll_Number = item.Payroll_Number;
                    _records.Forenames = item.Forenames;
                    _records.Surenames = item.Surenames;
                    _records.Date_of_Birth = item.Date_of_Birth;
                    _records.Telephone = item.Telephone;
                    _records.Mobile = item.Mobile;
                    _records.Address = item.Address;
                    _records.Address_2 = item.Address_2;
                    _records.Postcode = item.Postcode;
                    _records.EMail_Home = item.EMail_Home;
                    _records.Start_Date = item.Start_Date;
                }

                _db.Personnel_Records.Update(_records); //updates records by ID
                _db.SaveChanges(); // saves changes in database 

            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(null);
        }

    }
}
