using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using PagedList;
using PagedList.Mvc;
using System.Data.SqlClient;
using System.Runtime.InteropServices.ComTypes;
using AppDataPribadi;

namespace AppDataPribadi.Controllers
{
    public class HomeController : Controller
    {
        DataPribadiDBContext _context = new DataPribadiDBContext();
        DataDB objDataDB = new DataDB();
        Country objcountry = new Country();

        public ActionResult Index()
        {
            var listofData = _context.DataDBs.ToList();
            return View(listofData);
        }       

        //public JsonResult isNikexist(string NIK)
        //{
        //    return Json(data: !DataDB.Any(x => x.NIK == NIK));
        //}

        [HttpGet]
        public async Task<ActionResult> Index(string dataSearch)
        {
            ViewData["Getdatadetails"] = dataSearch;
            var dataQuery = from x in _context.DataDBs select x;
            if(!String.IsNullOrEmpty(dataSearch))
            {
                dataQuery = dataQuery.Where(x => x.Nama_Lengkap.Contains(dataSearch) || x.NIK.Contains(dataSearch));
            }
            return View(await dataQuery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var countryList = _context.Countries.ToList();
            SelectList list = new SelectList(countryList, "CountryID", "Negara");
            ViewBag.CountryList = list;
            return View();
        }       

        [HttpPost]
        public ActionResult Create(DataDB model)
        {
            try
            {
                _context.DataDBs.Add(model);
                _context.SaveChanges();
                TempData["AlertMessage"] = "Data Berhasil Ditambahkan";
            }
            catch (Exception ex)
            {
                _context.DataDBs.Where(x => x.NIK.Equals(x));
                ViewBag.Error = ex.Message;
                TempData["AlertMessage"] = "NIK yang Anda masukkan telah terdaftar. Periksa kembali NIK atau lakukan pencarian pada halaman utama";
                return RedirectToAction("Create");
            }
            //_context.DataDBs.Add(model);
            //_context.SaveChanges();
            //TempData["AlertMessage"] = "Data Berhasil Ditambahkan";
            return RedirectToAction("Index");

        }

        //[AllowAnonymous]
        //public JsonResult IsNIKExist(String NIKnya)
        //{
        //    System.Threading.Thread.Sleep(200);
        //    var SearchData = _context.DataDBs.Where(x => x.NIK == NIKnya).SingleOrDefault();

        //    if (SearchData != null)
        //    {
        //        return Json(1);
        //    }
        //    else
        //    {
        //        return Json(0);
        //    }

        //    return Json(!_context.DataDBs.Any(model => model.NIK == NIKnya), JsonRequestBehavior.AllowGet);
        //}
        public ActionResult Detail(string id)
        {
            var data = _context.DataDBs.Where(x => x.NIK == id).FirstOrDefault();
            return View(data);
        }
        
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var countryList = _context.Countries.ToList();
            SelectList list = new SelectList(countryList, "CountryID", "Negara");
            ViewBag.CountryList = list;
            
            var data = _context.DataDBs.Where(x => x.NIK == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(DataDB Model)
        {
            var data = _context.DataDBs.Where(x => x.NIK == Model.NIK).FirstOrDefault();
            if (data != null)
            {
                data.NIK = Model.NIK;
                data.Nama_Lengkap = Model.Nama_Lengkap;
                data.Jenis_Kelamin = Model.Jenis_Kelamin;
                data.Tanggal_Lahir = Model.Tanggal_Lahir;
                data.Alamat = Model.Alamat;
                data.Country = Model.Country;
                _context.SaveChanges();
                TempData["AlertMessage"] = "Data Berhasil Diupdate";
            }
            return RedirectToAction("index");
        }
        public ActionResult Delete(string id)
        {
            var data = _context.DataDBs.Where(x => x.NIK == id).FirstOrDefault();
            _context.DataDBs.Remove(data);
            _context.SaveChanges();
            TempData["AlertMessage"] = "Data Berhasil Dihapus";
            return RedirectToAction("index");
        }        
        public ActionResult Country()
        {
            var listofData = _context.Countries.ToList();
            return View(listofData);
        }


    }
}