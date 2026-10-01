using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TH03_BookList.Models;

namespace TH03_BookList.Controllers
{
    public class ChuDeController : Controller
    {
        private readonly AppDbContext _context;

        public ChuDeController()
        {
            _context = new AppDbContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ChuDe()
        {
            List<ChuDe> dsChude = _context.ChuDes.ToList();
            return View(dsChude);
        }
        public ActionResult ChuDeEdit(int id)
        {
            ChuDe? chuDe = new ChuDe { Mcd = 0, TenChuDe = "" };
            if (id == 0) // Nếu là thêm chủ đề
                return View(chuDe);
            // Nếu là sửa chủ đề
            chuDe = _context.ChuDes.Find(id);
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
            if (ModelState.IsValid)
            {
                if (chude.Mcd == 0) // Thêm mới
                {
                    _context.ChuDes.Add(chude);
                    TempData["SuccessMessage"] = "Thêm mới thành công!";
                }
                else
                {
                    _context.ChuDes.Update(chude);
                }

                _ = await _context.SaveChangesAsync();
                return RedirectToAction("ChuDe");
            }
            return View(chude);
        }
        public ActionResult ChuDeDelete(int id, IFormCollection collection)
        {
            try
            {
                var chude = _context.ChuDes.Find(id);
                if (chude != null)
                {
                    _context.ChuDes.Remove(chude);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Xóa chủ đề thành công!";
                }
                return RedirectToAction(nameof(ChuDe));
            }
            catch
            {
                return RedirectToAction(nameof(ChuDe));
            }
        }

        // =========================================================
        //  TRUY VẤN CSDL BÁN SÁCH BẰNG LINQ (Bài TH03)
        // =========================================================

        /// <summary>
        /// Giao diện thử nghiệm: chọn câu lệnh truy vấn có sẵn ở dropdown,
        /// kết quả trả về dưới dạng List&lt;Dictionary&lt;string, object&gt;&gt;.
        /// </summary>
        public async Task<IActionResult> QueryDemo(int? id)
        {
            List<SachQuery> queries = new List<SachQuery>
            {
                new SachQuery { Id = 1, QueryName = "1. Danh mục chủ đề (mã, tên)" },
                new SachQuery { Id = 2, QueryName = "2. Danh mục chủ đề kèm số lượng sách" },
                new SachQuery { Id = 3, QueryName = "3. Danh mục chủ đề có sách" },
                new SachQuery { Id = 4, QueryName = "4. Danh mục sách thuộc chủ đề Mcd = 5" },
                new SachQuery { Id = 5, QueryName = "5. Danh mục sách mới top 5" },
                new SachQuery { Id = 6, QueryName = "6. Danh mục sách bán chạy top 5" },
                new SachQuery { Id = 7, QueryName = "7. Danh mục quảng cáo còn hạn" },
                new SachQuery { Id = 8, QueryName = "8. Danh mục tác giả viết sách mã 2" },
                new SachQuery { Id = 9, QueryName = "9. Danh sách đơn hàng đã được giao" },
            };

            // Giữ lại giá trị được chọn trên dropdown sau khi reload
            ViewBag.Queries = new SelectList(queries, "Id", "QueryName", id);

            List<Dictionary<string, object>>? result = id switch
            {
                1 => await GetChuDesAsync(),
                2 => await GetChuDesSoLuongSachAsync(),
                3 => await GetChuDesCoSachAsync(),
                4 => await GetSachTheoChuDeAsync(5),
                5 => await GetSachMoiAsync(5),
                6 => await GetSachBanChayAsync(5),
                7 => await GetQuangCaoConHanAsync(),
                8 => await GetTacGiaSachAsync(2),
                9 => await GetDonDaGiaoAsync(),
                _ => null
            };

            ViewBag.QueryName = queries.FirstOrDefault(q => q.Id == id)?.QueryName;
            return View(result);
        }

        /// <summary>
        /// Chuyển kết quả truy vấn (danh sách anonymous object) sang Dictionary.
        /// </summary>
        private static List<Dictionary<string, object>> ToResultList<T>(IEnumerable<T> query)
        {
            return query
                .Select(item => item!.GetType()
                    .GetProperties()
                    .ToDictionary(
                        p => p.Name,
                        p => p.GetValue(item, null) ?? "NULL"))
                .ToList();
        }

        // 1. Lấy danh mục chủ đề gồm mã chủ đề và tên chủ đề
        private async Task<List<Dictionary<string, object>>> GetChuDesAsync()
        {
            var query = await _context.ChuDes
                .OrderBy(c => c.Mcd)
                .Select(c => new
                {
                    c.Mcd,
                    c.TenChuDe
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 2. Lấy danh mục chủ đề gồm mã chủ đề, tên chủ đề và số lượng sách thuộc chủ đề
        private async Task<List<Dictionary<string, object>>> GetChuDesSoLuongSachAsync()
        {
            var query = await _context.ChuDes
                .OrderBy(c => c.Mcd)
                .Select(c => new
                {
                    c.Mcd,
                    c.TenChuDe,
                    SoLuongSach = c.Saches.Count()
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 3. Lấy danh mục chủ đề gồm mã chủ đề, tên chủ đề có sách
        private async Task<List<Dictionary<string, object>>> GetChuDesCoSachAsync()
        {
            var query = await _context.ChuDes
                .Where(c => c.Saches.Any(s => s.Mcd == c.Mcd))
                .OrderBy(c => c.Mcd)
                .Select(c => new
                {
                    c.Mcd,
                    c.TenChuDe
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 4. Lấy danh mục sách thuộc 1 chủ đề cụ thể
        private async Task<List<Dictionary<string, object>>> GetSachTheoChuDeAsync(int mcd)
        {
            var query = await _context.Saches
                .Where(s => s.Mcd == mcd)
                .OrderBy(s => s.Ms)
                .Select(s => new
                {
                    s.Ms,
                    s.TenSach,
                    s.DonGia,
                    s.HinhMinhHoa,
                    s.NgayCapNhat
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 5. Danh mục sách mới top 5 (mã, tên, ảnh) dựa vào ngày cập nhật
        private async Task<List<Dictionary<string, object>>> GetSachMoiAsync(int topN)
        {
            var query = await _context.Saches
                .OrderByDescending(s => s.NgayCapNhat)
                .Take(topN)
                .Select(s => new
                {
                    s.Ms,
                    s.TenSach,
                    s.HinhMinhHoa
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 6. Danh mục 5 sách bán chạy (mã, tên, ảnh) dựa vào số lượng bán trong chi tiết đơn hàng
        private async Task<List<Dictionary<string, object>>> GetSachBanChayAsync(int topN)
        {
            var query = await _context.Saches
                .Where(s => s.CtDatHangs.Any())
                .Select(s => new
                {
                    s.Ms,
                    s.TenSach,
                    s.HinhMinhHoa,
                    TongSoLuongBan = s.CtDatHangs.Sum(ct => ct.SoLuong ?? 0)
                })
                .OrderByDescending(x => x.TongSoLuongBan)
                .Take(topN)
                .ToListAsync();

            return ToResultList(query);
        }

        // 7. Danh mục quảng cáo cần hiển thị (còn hạn)
        private async Task<List<Dictionary<string, object>>> GetQuangCaoConHanAsync()
        {
            var now = DateTime.Now;
            var query = await _context.QuangCaos
                .Where(qc => qc.NgayBatDau <= now && qc.NgayHetHan >= now)
                .OrderBy(qc => qc.Stt)
                .Select(qc => new
                {
                    qc.Stt,
                    qc.TenCty,
                    qc.HinhMinhHoa,
                    qc.Href,
                    qc.NgayBatDau,
                    qc.NgayHetHan
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 8. Danh mục tác giả tham gia viết cuốn sách có mã là 2
        private async Task<List<Dictionary<string, object>>> GetTacGiaSachAsync(int ms)
        {
            var query = await _context.ThamGia
                .Where(tg => tg.Ms == ms)
                .OrderBy(tg => tg.Mtg)
                .Select(tg => new
                {
                    tg.Mtg,
                    tg.MtgNavigation!.TenTacGia,
                    tg.VaiTro
                })
                .ToListAsync();

            return ToResultList(query);
        }

        // 9. Hiển thị danh sách đơn hàng đã được giao gồm thông tin ngày giao hàng
        private async Task<List<Dictionary<string, object>>> GetDonDaGiaoAsync()
        {
            var query = await _context.DonDatHangs
                .Where(dh => dh.DaGiaoHang)
                .OrderBy(dh => dh.Sdh)
                .Select(dh => new
                {
                    dh.Sdh,
                    dh.Mkh,
                    dh.NgayDatHang,
                    dh.NgayGiaoHang,
                    dh.TriGia
                })
                .ToListAsync();

            return ToResultList(query);
        }
    }
}
