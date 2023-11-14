using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Vehicle.Dal.Infrastructure.IRepository;
using Vehicle.Model;
using System.IO;
using ExcelDataReader;
using System.Text;
using Microsoft.AspNetCore.Http;
using Vehicle.Dal.Helper;
using Vehicle.Dal;
using Microsoft.EntityFrameworkCore;

namespace Vehicle.Web.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDBContext _context;

       
        public VehicleController(IUnitOfWork unitOfWork, ApplicationDBContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        [HttpGet]

        public async Task<IActionResult> Index(int? pageNumber)
        {
               vehicle v= new vehicle();
            //Pagination 
              try
              {
                int pageSize = 8;
                var vehicles = await _unitOfWork.vehicle.GetAll();
                    //await _unitOfWork.vehicle.GetAll();
                return View(Paginated<vehicle>.Create(vehicles, pageNumber ?? 1, pageSize));
              }
              catch(Exception ex) { 
                string message = ex.Message;
              }
              return View();
            
         }

        [HttpGet]
        public IActionResult UploadVehiclesFromExcel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadVehiclesFromExcel(IFormFile file, vehicle v)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //File Upload Code in our projects public directory
            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Uploads\\";
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    var filePath = Path.Combine(uploadFolder, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);

                    }
                    using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            do
                            {
                            bool isHeaderSkiped = false;

                            while (reader.Read())
                                {
                                if (!isHeaderSkiped)
                                {
                                    isHeaderSkiped = true;
                                    continue;
                                }
                                    // v = vehicle instance

                                    v.RegNo = reader.GetValue(1).ToString();
                                    v.Make = reader.GetValue(2).ToString();
                                    v.Model = reader.GetValue(3).ToString();
                                    v.Color = reader.GetValue(4).ToString();
                                    v.EngineNo = reader.GetValue(5).ToString();
                                    v.ChasisNo = reader.GetValue(6).ToString();
                                    v.DateOfPurchase = reader.GetValue(7).ToString();

                                    //Data Save Into Database
                                    _unitOfWork.vehicle.Add(v);
                                    v.Active = true;
                                    _unitOfWork.Save();
                                    TempData["Success"] = "Data Inserted Successfully into Database...";
                                    return RedirectToAction("Index");
                                }

                            } while (reader.NextResult());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }


            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                vehicle Vehicle = await _unitOfWork.vehicle.GetOneRecord(x => x.Id == id);
                if (Vehicle == null)
                {
                    return NotFound();
                }
                return View(Vehicle);
            }
            catch(Exception ex) { 
            string message = ex.Message;
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(vehicle Vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.vehicle.Update(Vehicle);
                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }catch (Exception ex)
                {
                    string message=ex.Message;
                }
                
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                vehicle Vehicle = await _unitOfWork.vehicle.GetOneRecord(x => x.Id == id);
                if (Vehicle == null)
                {
                    return NotFound();
                }
                return View(Vehicle);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View();
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteData(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var deleteVehicle = await _unitOfWork.vehicle.GetOneRecord(vehicle => vehicle.Id == vehicle.Id);
                if (deleteVehicle == null)
                {
                    return NotFound();
                }
                _unitOfWork.vehicle.Delete(deleteVehicle);
                deleteVehicle.Active = false;
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string message=ex.Message; 
            }
          return View() ;
        }
    }
}
