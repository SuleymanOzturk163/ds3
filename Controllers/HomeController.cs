using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ds3.Models;
using ds3.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization; 
using System.Security.Claims;

namespace ds3.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [AllowAnonymous]
    public IActionResult Index()
    {

        var sonUcRehber = _context.rehberim.OrderByDescending(x => x.Id).Take(3).ToList();

        var yorumlar = _context.yorumlar
                          .OrderByDescending(y => y.IsPinned)
                          .ThenByDescending(y => y.Tarih)
                          .ToList();

        var model = new IndexViewModel
        {
            Rehberler = sonUcRehber,
            Yorumlar = yorumlar
        };

        return View(model);
    }


    [HttpPost]
    public IActionResult YorumYap(string Icerik)
    {
        if (!User.Identity.IsAuthenticated) return RedirectToAction("Login");

        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                     ?? "Misafir"; 

        var yeniYorum = new yorum
        {
            Icerik = Icerik,
            Tarih = DateTime.Now,
            KullaniciAd = User.Identity.Name ?? "Bilinmeyen",
            KullaniciId = userId,
            LikeCount = 0,
            DislikeCount = 0,
            IsPinned = false
        };

        _context.yorumlar.Add(yeniYorum);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")] 
    public IActionResult Sil(int id)
    {
        var yorum = _context.yorumlar.Find(id);
        if (yorum != null)
        {
            _context.yorumlar.Remove(yorum);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")] 
    public IActionResult Sabitle(int id)
    {
        var yorum = _context.yorumlar.Find(id);
        if (yorum != null)
        {
            yorum.IsPinned = !yorum.IsPinned; 
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    public IActionResult Like(int id)
    {
        var yorum = _context.yorumlar.Find(id);
        if (yorum != null)
        {
            yorum.LikeCount++;
            _context.SaveChanges();
        }
        return RedirectToAction("Index"); 
    }
    public IActionResult Dislike(int id)
    {
        var yorum = _context.yorumlar.Find(id);
        if (yorum != null)
        {
            yorum.DislikeCount++;
            _context.SaveChanges();
        }
        return RedirectToAction("Index"); 
    }
    public IActionResult buildler()
    {
        return View();
    }
    public async Task<IActionResult> lokasyonlar()
    {
        var lokasyonlarListesi = await _context.Lokasyon.ToListAsync();
        return View(lokasyonlarListesi);
    }

    public async Task<IActionResult> BossRehberi()
    {
        var bosslar = await _context.Bosslar.OrderByDescending(x => x.Id).ToListAsync();
        return View(bosslar);
    }
    public IActionResult YeniBaslayanlar()
    {
        return View();
    }
    public IActionResult Hakkimda()
    {
        return View();
    }
    public IActionResult iletisim()
    {
        return View();
    }
    public IActionResult Alisveris()
    {
        return View();
    }
    [HttpGet]
    public IActionResult login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> login(AuthViewModel model)
    {
        //Admin Kontrolü
        if (model.LoginEmail == "admin@ds3wiki.com" && model.LoginPassword == "PraiseTheSun123")
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, model.LoginEmail),
            new Claim(ClaimTypes.Role, "Admin")
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Admin");
        }

        //Üye Kontrolü
        var uye = _context.Uyeler.FirstOrDefault(u => u.Email == model.LoginEmail && u.Password == model.LoginPassword);

        if (uye != null)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, uye.KullaniciAdi),
            new Claim(ClaimTypes.Role, "Uye")
        };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            return RedirectToAction("Index", "Home");
        }

        // HATA: Eğer buraya düşüyorsa, veritabanında bu mail/şifre eşleşmesi yok
        ViewBag.Hata = "Bilgiler yanlış, Ashen One!";
        return View("login", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(AuthViewModel model)
    {
        if (string.IsNullOrEmpty(model.RegisterEmail))
        {
            ViewBag.Hata = "Email alanı boş geliyor, form veriyi göndermiyor!";
            return View("login", model);
        }

        var yeniUye = new Uye
        {
            KullaniciAdi = model.RegisterUsername,
            Email = model.RegisterEmail,
            Password = model.RegisterPassword
        };

        _context.Uyeler.Add(yeniUye);
        await _context.SaveChangesAsync();

        return RedirectToAction("login");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> rehber(int id)
    {
        var secilen = await _context.rehberim.FindAsync(id);
        if (secilen == null) return NotFound();

        var sonUc = await _context.rehberim
            .OrderByDescending(r => r.Id)
            .Take(3)
            .ToListAsync();

        if (!sonUc.Any(x => x.Id == id))
        {
            sonUc.Insert(0, secilen);
            if (sonUc.Count > 3) sonUc.RemoveAt(3);
        }
        else
        {
            sonUc.Remove(sonUc.First(x => x.Id == id));
            sonUc.Insert(0, secilen);
        }

        ViewBag.TumRehberler = sonUc;
        return View(secilen);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
