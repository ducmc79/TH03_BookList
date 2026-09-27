using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TH03_BookList.Models;

namespace TH03_BookList.Controllers
{
    public class ChuDeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ChuDe()
        {
            AppDbContext context = new AppDbContext();
            List<ChuDe> dsChude = context.ChuDes.ToList();
            return View(dsChude);
        }
        public ActionResult ChuDeEdit(int id)
        {
            AppDbContext context = new AppDbContext();
            ChuDe? chuDe = new ChuDe { Mcd = 0, TenChuDe = "" };
            if (id == 0) // Nếu là thêm chủ đề
                return View(chuDe);
            // Nếu là sửa chủ đề
            chuDe = context.ChuDes.Find(id);
            if (chuDe == null)
            {
                return RedirectToAction(nameof(ChuDe));
            }
            return View(chuDe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChuDeEdit(ChuDe chude)
        {
            AppDbContext context = new AppDbContext();
            if (ModelState.IsValid)
            {
                if (chude.Mcd == 0) // Thêm mới
                {
                    context.ChuDes.Add(chude);
                    TempData["SuccessMessage"] = "Thêm mới thành công!";
                }
                else
                {
                    context.ChuDes.Update(chude);
                }

                _ = await context.SaveChangesAsync();
                return RedirectToAction("ChuDe");
            }
            return View(chude);
        }
        public ActionResult ChuDeDelete(int id, IFormCollection collection)
        {
            AppDbContext context = new AppDbContext();
            try
            {
                var chude = context.ChuDes.Find(id);
                if (chude != null)
                {
                    context.ChuDes.Remove(chude);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa chủ đề thành công!";
                }
                return RedirectToAction(nameof(ChuDe));
            }
            catch
            {
                return View();
            }
        }
    }
}
