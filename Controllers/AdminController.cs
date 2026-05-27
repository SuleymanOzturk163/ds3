using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ds3.Data;
using ds3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
namespace ds3.Controllers
{
    [Route("Admin/[action]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AdminController(ILogger<AdminController> logger, ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }



        public async Task<IActionResult> Index()
        {
            ViewBag.ToplamLokasyon = await _context.Lokasyon.CountAsync();
            ViewBag.ToplamRehber = await _context.rehberim.CountAsync();
            ViewBag.ToplamBoss = await _context.Bosslar.CountAsync();
            ViewBag.ToplamUye = await _context.Uyeler.CountAsync();

            return View();
        }


        public IActionResult UyeListesi()
        {
            var uyeler = _context.Uyeler.ToList();
            return View(uyeler);
        }

        [HttpGet]
        public IActionResult UyeSil(int id)
        {
            var uye = _context.Uyeler.Find(id);
            if (uye != null)
            {
                _context.Uyeler.Remove(uye);
                _context.SaveChanges();
            }
            return RedirectToAction("UyeListesi");
        }

        [HttpGet]
        public IActionResult UyeGuncelle(int id)
        {
            var uye = _context.Uyeler.Find(id);
            if (uye == null) return NotFound();
            return View(uye);
        }

        [HttpPost]
        public IActionResult UyeGuncelle(Uye model)
        {
            var uye = _context.Uyeler.Find(model.Id);
            if (uye != null)
            {
                uye.KullaniciAdi = model.KullaniciAdi;
                uye.Email = model.Email;
                _context.SaveChanges();
            }
            return RedirectToAction("UyeListesi");
        }




        public IActionResult RehberListesi()
        {
            var rehberler = _context.rehberim.OrderByDescending(x => x.Id).ToList();
            return View(rehberler);
        }

        
        [HttpGet]
        public IActionResult RehberEkle()
        {
            return View(new RehberViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> RehberEkle(RehberViewModel p)
        {
            if (ModelState.IsValid)
            {
                rehber yeniRehber = new rehber()
                {
                    Baslik = p.Baslik,
                    Ozet = p.Ozet,
                    Icerik = p.Icerik,
                    Yazar = p.Yazar,

                    Tarih = DateTime.Now,

                    UzunIcerik = p.UzunIcerik,
                    VideoUrl = p.VideoUrl,

                    ResimUrl = await FileUploadAsync(p.ResimDosyasi)
                };

                _context.rehberim.Add(yeniRehber);
                await _context.SaveChangesAsync();

                return RedirectToAction("RehberListesi", "Admin");
            }

            return View(p);
        }


        public async Task<IActionResult> RehberSil(int id)
        {
            var silinecek = await _context.rehberim.FindAsync(id);

            if (silinecek != null)
            {
                _context.rehberim.Remove(silinecek);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("RehberListesi");
        }

        [HttpGet]
        public IActionResult RehberGuncelle(int id)
        {
            var _p = _context.rehberim.Find(id);
            if (_p == null) return NotFound();

            var viewModel = new RehberViewModel()
            {
                Baslik = _p.Baslik,
                Ozet = _p.Ozet,
                Icerik = _p.Icerik,
                Yazar = _p.Yazar,
                UzunIcerik = _p.UzunIcerik,
                VideoUrl = _p.VideoUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> RehberGuncelle(int id, RehberViewModel p)
        {
            var orjinalRehber = _context.rehberim.Find(id);
            if (orjinalRehber == null) return NotFound();



            ModelState.Remove("ResimDosyasi");



            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                foreach (var err in errors)
                {
                    System.Diagnostics.Debug.WriteLine("MÜHÜRLENMEYEN HATA: " + err);
                }

                return View(p);
            }













            if (ModelState.IsValid)
            {
                orjinalRehber.Baslik = p.Baslik;
                orjinalRehber.Ozet = p.Ozet;
                orjinalRehber.Icerik = p.Icerik;
                orjinalRehber.Yazar = p.Yazar;
                orjinalRehber.UzunIcerik = p.UzunIcerik;
                orjinalRehber.VideoUrl = p.VideoUrl;

                if (p.ResimDosyasi != null && p.ResimDosyasi.Length > 0)
                {
                    orjinalRehber.ResimUrl = await FileUploadAsync(p.ResimDosyasi);
                }

                _context.rehberim.Update(orjinalRehber);
                await _context.SaveChangesAsync();

                return RedirectToAction("RehberListesi", "Admin");
            }

            return View(p);
        }

        public IActionResult Update(int Id)
        {
            var loka = _context.Lokasyon.Find(Id);
            if (loka == null) return NotFound();

            var viewModello = new LocationViewModel()
            {
                Title = loka.Title,
                InfoSubtitle = loka.InfoSubtitle,
                Description = loka.Description,
                Category = loka.Category
            };

            ViewBag.EskiResim = loka.ImageUrl;

            return View(viewModello);
        }
        public IActionResult Lokasyon()
        {
            var lokasyonlar = _context.Lokasyon.OrderByDescending(x => x.Id).ToList();
            return View(lokasyonlar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int Id, LocationViewModel p)
        {
            var orjinalLokasyon = _context.Lokasyon.Find(Id);
            if (orjinalLokasyon == null) return NotFound();


            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                orjinalLokasyon.Title = p.Title;
                orjinalLokasyon.InfoSubtitle = p.InfoSubtitle;
                orjinalLokasyon.Description = p.Description;
                orjinalLokasyon.Category = p.Category;

                if (p.ImageUrl != null && p.ImageUrl.Length > 0)
                {
                    orjinalLokasyon.ImageUrl = await FileUploadAsync(p.ImageUrl);
                }

                _context.Lokasyon.Update(orjinalLokasyon);
                await _context.SaveChangesAsync();

                return RedirectToAction("Lokasyon", "Admin");
            }

            return View(p);
        }

        public IActionResult Add()
        {
            return View(new LocationViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Add(LocationViewModel p)
        {
            if (ModelState.IsValid)
            {
                Location loc = new Location()
                {
                    Title = p.Title,
                    Category = p.Category,
                    Description = p.Description,
                    InfoSubtitle = p.InfoSubtitle,
                    ImageUrl = await FileUploadAsync(p.ImageUrl)
                };

                _context.Lokasyon.Add(loc);

                await _context.SaveChangesAsync();


                return RedirectToAction("Lokasyon", "Admin");
            }


            return View(p);
        }



        public async Task<IActionResult> Delete(int Id)
        {
            var silinecek = await _context.Lokasyon.FindAsync(Id);

            if (silinecek != null)
            {
                _context.Lokasyon.Remove(silinecek);
                await _context.SaveChangesAsync(); 
            }

            return RedirectToAction("Lokasyon"); 
        }




        public async Task<IActionResult> BossListesi()
        {
            var bosslar = await _context.Bosslar.OrderByDescending(x => x.Id).ToListAsync();
            return View(bosslar);
        }

        [HttpGet]
        public IActionResult BossEkle()
        {
            return View(new BossViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BossEkle(BossViewModel model)
        {
            if (ModelState.IsValid)
            {
                string benzersizResimAdi = null;

                if (model.ImageUrl != null)
                {
                    benzersizResimAdi = await FileUploadAsync(model.ImageUrl);
                }

                var yeniBoss = new Boss
                {
                    Name = model.Name,
                    Location = model.Location,
                    Health = model.Health,
                    Souls = model.Souls,
                    IsRequired = model.IsRequired,
                    Weaknesses = model.Weaknesses,
                    VideoUrl = model.VideoUrl,
                    ImageUrl = benzersizResimAdi
                };

                _context.Bosslar.Add(yeniBoss);
                await _context.SaveChangesAsync();
                return RedirectToAction("BossListesi");
            }

            return View(model);
        }

        public async Task<IActionResult> BossSil(int id)
        {
            var silinecek = await _context.Bosslar.FindAsync(id);

            if (silinecek != null)
            {
                _context.Bosslar.Remove(silinecek);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("BossListesi");
        }

        [HttpGet]
        public async Task<IActionResult> BossGuncelle(int id)
        {
            var boss = await _context.Bosslar.FindAsync(id);
            if (boss == null) return NotFound();

            var viewModel = new BossViewModel
            {
                Name = boss.Name,
                Location = boss.Location,
                Health = boss.Health,
                Souls = boss.Souls,
                IsRequired = boss.IsRequired,
                Weaknesses = boss.Weaknesses,
                VideoUrl = boss.VideoUrl
            };

            ViewBag.MevcutResim = boss.ImageUrl;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BossGuncelle(int id, BossViewModel model)
        {
            var boss = await _context.Bosslar.FindAsync(id);
            if (boss == null) return NotFound();

            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                if (model.ImageUrl != null && model.ImageUrl.Length > 0)
                {
                    if (!string.IsNullOrEmpty(boss.ImageUrl))
                    {
                        string eskiDosyaYolu = Path.Combine(_hostEnvironment.WebRootPath, "images", boss.ImageUrl);
                        if (System.IO.File.Exists(eskiDosyaYolu))
                        {
                            System.IO.File.Delete(eskiDosyaYolu);
                        }
                    }

                    boss.ImageUrl = await FileUploadAsync(model.ImageUrl);
                }

                boss.Name = model.Name;
                boss.Location = model.Location;
                boss.Health = model.Health;
                boss.Souls = model.Souls;
                boss.IsRequired = model.IsRequired;
                boss.Weaknesses = model.Weaknesses;
                boss.VideoUrl = model.VideoUrl;

                _context.Bosslar.Update(boss);
                await _context.SaveChangesAsync();
                return RedirectToAction("BossListesi");
            }

            ViewBag.MevcutResim = boss.ImageUrl;
            return View(model);
        }












































        private async Task<string> FileUploadAsync(IFormFile file)
        {
            //   ~/img/r1.jpg
            string filename = Guid.NewGuid().ToString() + file.FileName;
            string path = Path.Combine(_hostEnvironment.WebRootPath, "images", filename);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}