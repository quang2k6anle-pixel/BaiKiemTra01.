using System;
using System.Collections.Generic;
using System.Linq;

namespace LogisticsManagement
{
    public abstract class PhuongTien
    {
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        public string MaPT
        {
            get { return string.IsNullOrWhiteSpace(_maPT) ? "PT000" : _maPT; }
            set { _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value; }
        }

        public string TenHang
        {
            get { return _tenHang; }
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên hãng không được để trống!");
                _tenHang = value; 
            }
        }

        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set 
            { 
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                _namSanXuat = value; 
            }
        }

        public decimal GiaGoc
        {
            get { return _giaGoc; }
            set 
            { 
                if (value <= 0)
                    throw new ArgumentException("Giá gốc phải lớn hơn 0!");
                _giaGoc = value; 
            }
        }

        public PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        public abstract decimal TinhGiaLanBanh();

        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT}, Hãng: {TenHang}, Năm SX: {NamSanXuat}, Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }

    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get { return _soChoNgoi; }
            set
            {
                if (value <= 0) throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get { return _dungTichDongCo; }
            set
            {
                if (value <= 0) throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo) 
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                return GiaGoc + (0.12m * GiaGoc) + (0.30m * GiaGoc);
            }
            else
            {
                return GiaGoc + (0.10m * GiaGoc);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Số chỗ: {SoChoNgoi}, Dung tích: {DungTichDongCo}L";
        }
    }

    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get { return _dungTichXylanh; }
            set
            {
                if (value <= 0) throw new ArgumentException("Dung tích xy lanh phải lớn hơn 0!");
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh) 
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
            {
                return GiaGoc + (0.02m * GiaGoc);
            }
            else
            {
                return GiaGoc + (0.05m * GiaGoc);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $", Dung tích xy lanh: {DungTichXylanh}cc";
        }
    }

    public class QuanLyPhuongTien
    {
        private List<PhuongTien> _danhSachPT = new List<PhuongTien>();

        public void AddPhuongTien(PhuongTien pt)
        {
            _danhSachPT.Add(pt);
        }

        public void DisplayAll()
        {
            foreach (var pt in _danhSachPT)
            {
                Console.WriteLine(pt.GetInfo() + $" | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        public PhuongTien FindMaxGiaLanBanh()
        {
            if (_danhSachPT.Count == 0) return null;
            
            PhuongTien maxPT = _danhSachPT[0];
            foreach (var pt in _danhSachPT)
            {
                if (pt.TinhGiaLanBanh() > maxPT.TinhGiaLanBanh())
                {
                    maxPT = pt;
                }
            }
            return maxPT;
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            return _danhSachPT.Where(pt => pt.TenHang.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            QuanLyPhuongTien ql = new QuanLyPhuongTien();

            Console.WriteLine("--- TC01: Kiem tra Validation Nam san xuat ---");
            try
            {
                OTo otoLoi = new OTo("OT01", "Toyota", 1850, 1000000000, 5, 2.0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kết quả TC01: Đã bắt được lỗi -> {ex.Message}");
            }

            Console.WriteLine("\n--- TC02: Kiem tra Tinh Gia Lan Banh O To ---");
            OTo oto1 = new OTo("OT02", "Honda", 2023, 1000000000m, 5, 2.0);
            ql.AddPhuongTien(oto1);
            Console.WriteLine($"Giá lăn bánh kỳ vọng: 1,420,000,000 VNĐ -> Thực tế: {oto1.TinhGiaLanBanh():N0} VNĐ");

            Console.WriteLine("\n--- TC03: Kiem tra Tinh Gia Lan Banh Xe May ---");
            XeMay xm1 = new XeMay("XM01", "Yamaha", 2023, 50000000m, 150);
            ql.AddPhuongTien(xm1);
            Console.WriteLine($"Giá lăn bánh kỳ vọng: 51,000,000 VNĐ -> Thực tế: {xm1.TinhGiaLanBanh():N0} VNĐ");

            Console.WriteLine("\n--- TC04: Kiem tra Da Hinh List<PhuongTien> ---");
            ql.DisplayAll();

            Console.WriteLine("\n--- TC05: Kiem tra Tim Gia Lan Banh Max ---");
            PhuongTien maxPT = ql.FindMaxGiaLanBanh();
            if (maxPT != null)
            {
                Console.WriteLine($"Phương tiện đắt nhất: {maxPT.GetInfo()}");
                Console.WriteLine($"Với giá lăn bánh: {maxPT.TinhGiaLanBanh():N0} VNĐ");
            }
        }
    }
}
Soạn
Viết
