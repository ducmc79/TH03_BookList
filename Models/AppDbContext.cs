using Microsoft.EntityFrameworkCore;

namespace TH03_BookList.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChuDe> ChuDes { get; set; }

    public virtual DbSet<CtDatHang> CtDatHangs { get; set; }

    public virtual DbSet<DonDatHang> DonDatHangs { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhaXuatBan> NhaXuatBans { get; set; }

    public virtual DbSet<QuangCao> QuangCaos { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TacGium> TacGia { get; set; }

    public virtual DbSet<ThamGium> ThamGia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=QLBanSach;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<ChuDe>(entity =>
        {
            entity.HasKey(e => e.Mcd)
                .HasName("aaaaaCHU_DE_PK")
                .IsClustered(false);

            entity.ToTable("CHU_DE");

            entity.HasIndex(e => e.Pid, "PID");

            entity.Property(e => e.Pid)
                .HasDefaultValue(0)
                .HasColumnName("PID");
            entity.Property(e => e.TenChuDe)
                .HasMaxLength(50)
                .HasColumnName("Ten_chu_de");
        });

        modelBuilder.Entity<CtDatHang>(entity =>
        {
            entity.HasKey(e => e.Mctddh)
                .HasName("aaaaaCT_DAT_HANG_PK")
                .IsClustered(false);

            entity.ToTable("CT_DAT_HANG");

            entity.HasIndex(e => e.Sdh, "DON_DAT_HANGCT_DAT_HANG");

            entity.HasIndex(e => e.Ms, "SACHCT_DAT_HANG");

            entity.Property(e => e.DonGia).HasColumnName("Don_gia");
            entity.Property(e => e.SoLuong).HasColumnName("So_luong");
            entity.Property(e => e.ThanhTien).HasColumnName("Thanh_tien");

            entity.HasOne(d => d.MsNavigation).WithMany(p => p.CtDatHangs)
                .HasForeignKey(d => d.Ms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CT_DAT_HANG_FK01");

            entity.HasOne(d => d.SdhNavigation).WithMany(p => p.CtDatHangs)
                .HasForeignKey(d => d.Sdh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CT_DAT_HANG_FK00");
        });

        modelBuilder.Entity<DonDatHang>(entity =>
        {
            entity.HasKey(e => e.Sdh)
                .HasName("aaaaaDON_DAT_HANG_PK")
                .IsClustered(false);

            entity.ToTable("DON_DAT_HANG");

            entity.HasIndex(e => e.Mkh, "KHACH_HANGDON_DAT_HANG");

            entity.Property(e => e.DaGiaoHang).HasColumnName("Da_giao_hang");
            entity.Property(e => e.NgayDatHang)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_dat_hang");
            entity.Property(e => e.NgayGiaoHang)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_giao_hang");
            entity.Property(e => e.TriGia)
                .HasColumnType("money")
                .HasColumnName("Tri_gia");

            entity.HasOne(d => d.MkhNavigation).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.Mkh)
                .HasConstraintName("DON_DAT_HANG_FK00");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.Mkh)
                .HasName("aaaaaKHACH_HANG_PK")
                .IsClustered(false);

            entity.ToTable("KHACH_HANG");

            entity.Property(e => e.DiaChi)
                .HasMaxLength(50)
                .HasColumnName("Dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(10)
                .HasColumnName("Dien_thoai");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GioiTinh).HasColumnName("Gioi_tinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .HasColumnName("Ho_ten");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(15)
                .HasColumnName("Mat_khau");
            entity.Property(e => e.NgaySinh)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_sinh");
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(15)
                .HasColumnName("Ten_dang_nhap");
        });

        modelBuilder.Entity<NhaXuatBan>(entity =>
        {
            entity.HasKey(e => e.Mnxb)
                .HasName("aaaaaNHA_XUAT_BAN_PK")
                .IsClustered(false);

            entity.ToTable("NHA_XUAT_BAN");

            entity.Property(e => e.DiaChi)
                .HasMaxLength(150)
                .HasColumnName("Dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .HasColumnName("Dien_thoai");
            entity.Property(e => e.TenNhaXuatBan)
                .HasMaxLength(100)
                .HasColumnName("Ten_nha_xuat_ban");
        });

        modelBuilder.Entity<QuangCao>(entity =>
        {
            entity.HasKey(e => e.Stt)
                .HasName("aaaaaQUANG_CAO_PK")
                .IsClustered(false);

            entity.ToTable("QUANG_CAO");

            entity.Property(e => e.Stt).HasColumnName("STT");
            entity.Property(e => e.HinhMinhHoa)
                .HasMaxLength(100)
                .HasColumnName("Hinh_Minh_Hoa");
            entity.Property(e => e.Href)
                .HasMaxLength(100)
                .HasColumnName("HREF");
            entity.Property(e => e.NgayBatDau)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_bat_dau");
            entity.Property(e => e.NgayHetHan)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_het_han");
            entity.Property(e => e.TenCty)
                .HasMaxLength(200)
                .HasColumnName("TenCTy");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Ms)
                .HasName("aaaaaSACH_PK")
                .IsClustered(false);

            entity.ToTable("SACH");

            entity.HasIndex(e => e.Mcd, "CHU_DESACH");

            entity.HasIndex(e => e.Mnxb, "NHA_XUAT_BANSACH");

            entity.Property(e => e.DonGia)
                .HasColumnType("money")
                .HasColumnName("Don_gia");
            entity.Property(e => e.DonViTinh)
                .HasMaxLength(10)
                .HasDefaultValue("Cu?n")
                .HasColumnName("Don_vi_tinh");
            entity.Property(e => e.HinhMinhHoa)
                .HasMaxLength(50)
                .HasColumnName("Hinh_minh_hoa");
            entity.Property(e => e.MoTa)
                .HasColumnType("ntext")
                .HasColumnName("Mo_ta");
            entity.Property(e => e.NgayCapNhat)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_cap_nhat");
            entity.Property(e => e.SoLanXem)
                .HasDefaultValue(0)
                .HasColumnName("So_lan_xem");
            entity.Property(e => e.SoLuongBan)
                .HasDefaultValue(0)
                .HasColumnName("So_luong_ban");
            entity.Property(e => e.TenSach)
                .HasMaxLength(100)
                .HasColumnName("Ten_sach");

            entity.HasOne(d => d.McdNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Mcd)
                .HasConstraintName("SACH_FK00");

            entity.HasOne(d => d.MnxbNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Mnxb)
                .HasConstraintName("SACH_FK01");
        });

        modelBuilder.Entity<TacGium>(entity =>
        {
            entity.HasKey(e => e.Mtg)
                .HasName("aaaaaTAC_GIA_PK")
                .IsClustered(false);

            entity.ToTable("TAC_GIA");

            entity.Property(e => e.DiaChi)
                .HasMaxLength(100)
                .HasColumnName("Dia_chi");
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .HasColumnName("Dien_thoai");
            entity.Property(e => e.TenTacGia)
                .HasMaxLength(50)
                .HasColumnName("Ten_tac_gia");
        });

        modelBuilder.Entity<ThamGium>(entity =>
        {
            entity.HasKey(e => new { e.Ms, e.Mtg })
                .HasName("aaaaaTHAM_GIA_PK")
                .IsClustered(false);

            entity.ToTable("THAM_GIA");

            entity.HasIndex(e => e.Ms, "HANG_HOATAC_GIA_SACH");

            entity.HasIndex(e => e.Mtg, "TAC_GIATAC_GIA_SACH");

            entity.Property(e => e.VaiTro)
                .HasMaxLength(50)
                .HasColumnName("Vai_tro");

            entity.HasOne(d => d.MsNavigation).WithMany(p => p.ThamGia)
                .HasForeignKey(d => d.Ms)
                .HasConstraintName("THAM_GIA_FK00");

            entity.HasOne(d => d.MtgNavigation).WithMany(p => p.ThamGia)
                .HasForeignKey(d => d.Mtg)
                .HasConstraintName("THAM_GIA_FK01");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
