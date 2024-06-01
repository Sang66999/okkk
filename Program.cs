using System;
using System.Collections;
using System.Diagnostics;
using System.Net.Http.Headers;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;
class Program
{
    static void Main(string[] args)
    {
        MainMenu();
    }
    static void MainMenu()
    {
        while(true)
        {
            Console.WriteLine("=====MENU=======");
            Console.WriteLine("1.Quan ly Khach Hang ");
            Console.WriteLine("2.Quan ly Don Hang ");
            Console.WriteLine("3.Quan ly Danh Muc ");
            Console.WriteLine("4.Quan ly San Pham");
            Console.WriteLine("0.Thoat");
            string choice = Console.ReadLine();
            switch(choice)
                {
                    case "1":
                        QuanlyKhachHang();
                        break;
                    case "2":
                        QuanlyDonHang();
                        break;
                    case "3":
                        QuanLyDanhMuc();
                        break;
                    case "4":
                        QuanLySanPham();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le. Vui long thu lai ");
                        Console.ReadKey();
                        break;
                }
        }
    }
    static void QuanlyKhachHang()
    {
        string connectionString = "Server=localhost;Database=banhang2;Pwd=12345678;User=root";
        List<KhachHang> danhsachKhachHang = new List<KhachHang>();
        bool tieptuc = true;
        while(tieptuc)
        {
            Console.WriteLine("1.Nhap thong tin khach hang ");
            Console.WriteLine("2. Xem thong tin khach hang ");
            Console.WriteLine("3. Sua thong tin khach hang ");
            Console.WriteLine("4. Xoa thong tin khach hang ");
            Console.WriteLine("0. Tro ve menu chinh ");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    Nhapthongtinkhachhang(danhsachKhachHang, connectionString);
                    break;
                case 2:
                    Xemthongtinkhachhang(danhsachKhachHang);
                    break;
                case 3:
                    Suathongtinkhachhang(danhsachKhachHang, connectionString);
                    break;
                case 4:
                    Xoathongtinkhachhang(danhsachKhachHang, connectionString);
                    break;
                case 0:
                    tieptuc = false;
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le");
                    break;
            }
        }
    }
    static void QuanlyDonHang()
    {
        string connectionString = "Server=localhost;Database=banhang2;User=root;Pwd=12345678";
        List<DonHang> dsDonHang = new List<DonHang>();
        bool tieptuc = true;
        while(tieptuc)
        {
            Console.WriteLine("1.Tao don hang ");
            Console.WriteLine("2.Xem don hang");
            Console.WriteLine("0.Tro ve menu chinh");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    TaoDonHang(dsDonHang , connectionString);
                    break;
                case 2:
                    Xemdonhang(dsDonHang);
                    break;
                case 0:
                    tieptuc = false;
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le");
                    break;
            }
        }
    }
    static void QuanLyDanhMuc()
    {
        string connectionString = "Server=localhost;Database=banhang2;User=root;Pwd=12345678";
        List<DanhMuc> danhsachDanhMuc = new List<DanhMuc>();
        bool tieptuc = true;
        while(tieptuc)
        {
            Console.WriteLine("1.Nhap thong tin danh muc");
            Console.WriteLine("2.Sua thong tin danh muc");
            Console.WriteLine("3. Xoa danh muc");
            Console.WriteLine("4.Xem thong tin danh muc ");
            Console.WriteLine("0.Tro ve menu chinh ");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    Nhapthongtindanhmuc(danhsachDanhMuc, connectionString);
                    break;
                case 2:
                    Suathongtindanhmuc(danhsachDanhMuc, connectionString);
                    break;
                case 3:
                    Xoadanhmuc(danhsachDanhMuc, connectionString);
                    break;
                case 4:
                    Xemthongtindanhmuc(connectionString);
                    break;
                case 0:
                    tieptuc = false;
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le");
                    break;
            }
        }
    }
    static void QuanLySanPham()
    {
        string connectionString = "Server=localhost;Database=banhang2;Pwd=12345678;User=root";
        List<SanPham> danhsachSanPham = new List<SanPham>();
        bool tieptuc = true;
        while(tieptuc)
        {
            Console.WriteLine("1.Nhap thong tin san pham");
            Console.WriteLine("2.Cap nhat thong tin san pham");
            Console.WriteLine("3.Xoa san pham");
            Console.WriteLine("4.Tim kiem san pham");
            Console.WriteLine("0. Tro ve menu chinh");
            int choice = int.Parse(Console.ReadLine());
            switch(choice)
            {
                case 1:
                    Nhapthongtinsanpham(danhsachSanPham, connectionString);
                    break;
                case 2:
                    Capnhatthongtinsanpham(danhsachSanPham, connectionString);
                    break;
                case 3:
                    XoaSanPham(danhsachSanPham, connectionString);
                    break;
                case 4:
                    Timkiemsanpham(danhsachSanPham, connectionString);
                    break;
                case 0:
                    tieptuc = false;
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le");
                    break;
            }
        }
    }
    static void TaoDonHang(List<DonHang> dsDonHang , string connectionString)
    {
        DonHang order = new DonHang();
        List<ChiTietDonHang> chiTietDonHangs = new List<ChiTietDonHang>();
        order.ChiTietDonHangs = new List<ChiTietDonHang>();
        Console.WriteLine("Nhap Ma San Pham :");
        int MaSanPham = int.Parse(Console.ReadLine());
        Console.Write("Nhap so luong : ");
        int SoLuongDatHang = int.Parse(Console.ReadLine());
        using(MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string SanPhamquery = "SELECT * FROM sanpham WHERE MaSanPham = @MaSanPham";
            using (MySqlCommand command = new MySqlCommand(SanPhamquery, connection))
            {
                command.Parameters.AddWithValue("@MaSanPham", MaSanPham);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if(!reader.Read())
                    {
                        Console.WriteLine("San pham khong ton tai ");
                        return;
                    }
                    if(reader.GetInt32 ("SoLuong") < SoLuongDatHang)
                    {
                        Console.WriteLine("Khong du so luong san pham trong kho");
                        return;
                    }
                    order.ChiTietDonHangs.Add(new ChiTietDonHang
                    {
                        MaSanPham = MaSanPham ,
                        TenSanPham = reader.GetString("TenSanPham"),

                    });
                }
            }
        }
        Console.Write("MaKhachHang :");
        order.MaKhachHang = int.Parse(Console.ReadLine());
        Console.Write(" Nhap MaDiaChi :");
        order.MaDiaChi = int.Parse(Console.ReadLine());
        Console.Write("MaDonHang :");
        order.MaDonHang = int.Parse(Console.ReadLine());
        Console.Write("Ngay Dat Hang ");
        order.NgayDatHang = Console.ReadLine();
        Console.Write("Tong Gia");
        order.TongGia = decimal.Parse(Console.ReadLine());
        Console.Write("Trang Thai");
        order.TrangThai = Console.ReadLine();
        using(MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string KhachHangquery = "INSERT INTO donhang(MaDonHang, MaKhachHang, MaDiaChi, NgayDatHang, TongGia, TrangThai) VALUES (@MaDonHang, @MaKhachHang, @MaDiaChi, @NgayDatHang, @TongGia, @TrangThai)";
            using (MySqlCommand command = new MySqlCommand(KhachHangquery , connection))
            {
                command.Parameters.AddWithValue("@MaKhachHang",order.MaKhachHang);
                command.Parameters.AddWithValue("@MaDiaChi",order.MaDiaChi);
                command.Parameters.AddWithValue("@MaDonHang",order.MaDonHang );
                command.Parameters.AddWithValue("@NgayDatHang", order.NgayDatHang);
                command.Parameters.AddWithValue("@TongGia", order.TongGia);
                command.Parameters.AddWithValue("@TrangThai", order.TrangThai);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Tạo đơn hàng thành công!");
    }
    static void Xemdonhang(List<DonHang> dsDonHang)
    {
        foreach(var dh in dsDonHang )
        {
            Console.WriteLine($"MaDonHang: {dh.MaDonHang}, MaKhachHang {dh.MaKhachHang}, MaDiaChi {dh.MaDiaChi}, NgayDatHang {dh.NgayDatHang}, TongGia {dh.TongGia}, TrangThai {dh.TrangThai} ");
        }
    }
    static void Nhapthongtinkhachhang(List<KhachHang> danhsachKhachHang , string connectionString)
    {
        Console.WriteLine("=====Nhap thong tin =====");
        KhachHang kh = new KhachHang();
        Console.Write("Nhap Ma Khach Hang: ");
        kh.MaKhachHang = int.Parse(Console.ReadLine());
        Console.Write("Nhap Email :");
        kh.Email = Console.ReadLine();
        Console.Write("Nhap Ho Ten : ");
        kh.HoTen = Console.ReadLine();
        Console.Write("Nhap Dia Chi :");
        kh.DiaChi = Console.ReadLine();
        Console.Write("Nhap So Dien Thoai :");
        kh.SoDienThoai = int.Parse(Console.ReadLine());
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO khachhang(MaKhachHang , Email, HoTen, DiaChi, SoDienThoai) VALUES (@MaKhachHang , @Email, @HoTen, @DiaChi , @SoDienThoai)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaKhachHang", kh.MaKhachHang);
                command.Parameters.AddWithValue("@Email", kh.Email);
                command.Parameters.AddWithValue("@HoTen", kh.HoTen);
                command.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                command.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                command.ExecuteNonQuery();
            }
        }
        danhsachKhachHang.Add(kh);
        Console.WriteLine("Da nhap thong tin thanh cong");
    }
    static void Xemthongtinkhachhang(List<KhachHang> danhsachKhachHang)
    {
        Console.WriteLine("Danh sach khach hang ");
        foreach(var kh in danhsachKhachHang)
        {
            Console.WriteLine($"MaKhachHang: {kh.MaKhachHang}, Email {kh.Email}, HoTen: {kh.HoTen}, DiaChi: {kh.DiaChi} ");
        }
    }
    static void Suathongtinkhachhang(List<KhachHang> danhsachKhachHang, string connectionString)
    {
        Console.WriteLine("Nhap Ma Khach Hang can sua:  ");
        int MaKhachHang = int.Parse(Console.ReadLine());
        KhachHang kh = danhsachKhachHang.Find(s=>s.MaKhachHang==MaKhachHang);
        Console.Write("Nhap Email :");
        kh.Email = Console.ReadLine();
        Console.Write("Nhap Ho ten : ");
        kh.HoTen = Console.ReadLine();
        Console.Write("Nhap Dia Chi :");
        kh.DiaChi = Console.ReadLine();
        Console.Write("Nhap So Dien Thoai: ");
        kh.SoDienThoai = int.Parse(Console.ReadLine());
        using(MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE khachhang SET Email = @Email , HoTen = @HoTen, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai WHERE MaKhachHang = @MaKhachHang";
            using(MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaKhachHang", kh.MaKhachHang);
                command.Parameters.AddWithValue("@Email", kh.Email);
                command.Parameters.AddWithValue("@HoTen", kh.HoTen);
                command.Parameters.AddWithValue("@DiaChi", kh.DiaChi);
                command.Parameters.AddWithValue("@SoDienThoai", kh.SoDienThoai);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Sua hoan tat");
    }
    static void Xoathongtinkhachhang(List<KhachHang> danhsachKhachHang , string connectionString)
    {
        Console.WriteLine("Nhap ma khach hang can xoa ");
        int MaKhachHang = int.Parse(Console.ReadLine());
        KhachHang kh = danhsachKhachHang.Find(s=>s.MaKhachHang==MaKhachHang);
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM khachhang WHERE MaKhachHang = @MaKhachHang";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaKhachHang", MaKhachHang);
                command.ExecuteNonQuery();
            }
        }
        danhsachKhachHang.Remove(kh);
        Console.WriteLine("Xoa hoan tat");
    }
    static void Nhapthongtindanhmuc(List<DanhMuc> danhsachDanhMuc , string connectionString)
    {
        Console.WriteLine("====Nhap thong tin ======");
        DanhMuc dm = new DanhMuc();
        Console.Write("Nhap Ma Danh Muc :");
        dm.MaDanhMuc = int.Parse(Console.ReadLine());
        Console.Write("Nhap Ten danh muc :");
        dm.TenDanhMuc = Console.ReadLine();
        Console.Write("Nhap Mo ta :");
        dm.MieuTa = Console.ReadLine();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query ="INSERT INTO danhmuc(MaDanhMuc, TenDanhMuc, MieuTa) VALUES (@MaDanhMuc, @TenDanhMuc, @MieuTa)";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaDanhMuc", dm.MaDanhMuc);
                command.Parameters.AddWithValue("@TenDanhMuc", dm.TenDanhMuc);
                command.Parameters.AddWithValue("@MieuTa" , dm.MieuTa);
                command.ExecuteNonQuery();
            }
        }
        danhsachDanhMuc.Add(dm);
        Console.WriteLine("Nhap thong tin danh muc thanh cong ");
    }
    static void Suathongtindanhmuc(List<DanhMuc> danhsachDanhMuc, string connectionString)
    {
        Console.WriteLine("Nhap Ma Danh Muc can  sua : ");
        int MaDanhMuc = int.Parse(Console.ReadLine());
        DanhMuc dm = danhsachDanhMuc.Find(s=>s.MaDanhMuc==MaDanhMuc);
        Console.Write("Nhap Ten Danh Muc :");
        dm.TenDanhMuc = Console.ReadLine();
        Console.Write("Nhap Mieu Ta : ");
        dm.MieuTa = Console.ReadLine();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE danhmuc SET TenDanhMuc = @TenDanhMuc, MieuTa = @MieuTa WHERE MaDanhMuc = @MaDanhMuc";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaDanhMuc", MaDanhMuc);
                command.Parameters.AddWithValue("@TenDanhMuc", dm.TenDanhMuc);
                command.Parameters.AddWithValue("@MieuTa", dm.MieuTa);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Sua thanh cong");
    }
    static void Xoadanhmuc(List<DanhMuc> danhsachDanhMuc, string connectionString)
    {
        Console.WriteLine("Nhap Ma Danh Muc can xoa :");
        int MaDanhMuc = int.Parse(Console.ReadLine());
        DanhMuc dm = danhsachDanhMuc.Find(s=>s.MaDanhMuc== MaDanhMuc);
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM danhmuc WHERE MaDanhMuc = @MaDanhMuc";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaDanhMuc", MaDanhMuc);
                command.ExecuteNonQuery();
            }
        }
        danhsachDanhMuc.Remove(dm);
        Console.WriteLine("Xoa hoan tat ");
    }
    static void Xemthongtindanhmuc(string connectionString)
    {
        using(MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM danhmuc";
            using(MySqlCommand command = new MySqlCommand(query, connection))
            {
                using(MySqlDataReader reader = command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        Console.WriteLine("Thong tin Danh Muc");
                        while(reader.Read())
                        {
                            Console.WriteLine($"MaDanhMuc: {reader.GetInt32("MaDanhMuc")}, TenDanhMuc: {reader.GetString("TenDanhMuc")}, MieuTa: {reader.GetString("MieuTa")}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Khong co khach hang trong co so du lieu ");
                    }
                }
            }
        }
    }
    static void Nhapthongtinsanpham(List<SanPham> danhsachSanPham, string connectionString)
    {
        Console.WriteLine("=====Nhap thong tin =======");
        SanPham sp = new SanPham();
        Console.Write("Nhap Ma San Pham :");
        sp.MaSanPham = int.Parse(Console.ReadLine());
        Console.Write("Nhap Ma Danh Muc :");
        sp.MaDanhMuc = int.Parse(Console.ReadLine());
        Console.Write("Nhap Ten San Pham :");
        sp.TenSanPham = Console.ReadLine();
        Console.Write("Nhap Mieu Ta : ");
        sp.MieuTa = Console.ReadLine();
        Console.Write("Nhap Gia :");
        sp.Gia = decimal.Parse(Console.ReadLine());
        Console.Write("Nhap So Luong :");
        sp.SoLuong = int.Parse(Console.ReadLine());
        Console.Write("Nhap URL Hinh Anh :");
        sp.URLHinhAnh = Console.ReadLine();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO sanpham(MaSanPham,MaDanhMuc,TenSanPham,MieuTa,Gia,SoLuong,URLHinhAnh) VALUES (@MaSanPham,@MaDanhMuc,@TenSanPham,@MieuTa,@Gia,@SoLuong,@URLHinhAnh)";
            using(MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaSanPham", sp.MaSanPham);
                command.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                command.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                command.Parameters.AddWithValue("@MieuTa", sp.MieuTa);
                command.Parameters.AddWithValue("@Gia", sp.Gia);
                command.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                command.Parameters.AddWithValue("@URLHinhAnh", sp.URLHinhAnh);
                command.ExecuteNonQuery();
            }
        }
        danhsachSanPham.Add(sp);
        Console.WriteLine("Nhap thong tin hoan tat");
    }
    static void Capnhatthongtinsanpham(List<SanPham> danhsachSanPham, string connectionString)
    {
        Console.WriteLine("Nhap Ma San Pham can cap nhat:  ");
        int MaSanPham = int.Parse(Console.ReadLine());
        SanPham sp = danhsachSanPham.Find(s=> s.MaSanPham== MaSanPham);
        Console.Write("Nhap Ma Danh Muc : ");
        sp.MaDanhMuc = int.Parse(Console.ReadLine());
        Console.Write("Nhap Ten San Pham : ");
        sp.TenSanPham = Console.ReadLine();
        Console.Write("Nhap Mieu Ta :");
        sp.MieuTa = Console.ReadLine();
        Console.Write("Nhap gia :");
        sp.Gia = decimal.Parse(Console.ReadLine());
        Console.Write("Nhap So Luong :");
        sp.SoLuong = int.Parse(Console.ReadLine());
        Console.Write("Nhap URL Hinh Anh : ");
        sp.URLHinhAnh = Console.ReadLine();
        using(MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "UPDATE sanpham SET MaDanhMuc=@MaDanhMuc, TenSanPham=@TenSanPham, MieuTa=@MieuTa, Gia=@Gia,SoLuong=@SoLuong,URLHinhAnh=@URLHinhAnh WHERE MaSanPham = @MaSanPham";
            using (MySqlCommand command = new MySqlCommand(query,connection))
            {
                command.Parameters.AddWithValue("@MaSanPham", MaSanPham);
                command.Parameters.AddWithValue("@MaDanhMuc", sp.MaDanhMuc);
                command.Parameters.AddWithValue("@TenSanPham", sp.TenSanPham);
                command.Parameters.AddWithValue("@MieuTa",sp.MieuTa);
                command.Parameters.AddWithValue("@Gia", sp.Gia);
                command.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                command.Parameters.AddWithValue("@URLHinhAnh", sp.URLHinhAnh);
                command.ExecuteNonQuery();
            }
        }
        Console.WriteLine("Cap nhat thanh cong ");
    }
    static void XoaSanPham(List<SanPham> danhsachSanPham, string connectionString)
    {
        Console.WriteLine("Nhap Ma San Pham can xoa :");
        int MaSanPham = int.Parse(Console.ReadLine());
        SanPham sp = danhsachSanPham.Find(s=>s.MaSanPham==MaSanPham);
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "DELETE FROM sanpham WHERE MaSanPham = @MaSanPham";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@MaSanPham", MaSanPham);
                command.ExecuteNonQuery();
            }
        }
        danhsachSanPham.Remove(sp);
        Console.WriteLine("Xoa hoan tat ");
    }
    static void Timkiemsanpham(List<SanPham> danhsachSanPham, string connectionString)
    {
       using(MySqlConnection connection = new MySqlConnection(connectionString))
       {
            connection.Open();
            Console.WriteLine("Nhập thông tin tìm kiếm:");

            Console.Write("Nhập tên sản phẩm cần tìm: ");
            string tenCanTim = Console.ReadLine();

            Console.Write("Nhập tên mã danh mục cần tìm: ");
            string madanhMucCanTim = Console.ReadLine();

            Console.Write("Nhập giá tối thiểu: ");
            decimal giaMin = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Nhập giá tối đa: ");
            decimal giaMax = Convert.ToDecimal(Console.ReadLine());
            string query = "SELECT * FROM SanPham WHERE TenSanPham LIKE @tenSanPham AND MaDanhMuc = @maDanhMuc AND Gia BETWEEN @giaMin AND @giaMax";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@tenSanPham", "%" + tenCanTim + "%");
            cmd.Parameters.AddWithValue("@maDanhMuc", madanhMucCanTim);
            cmd.Parameters.AddWithValue("@giaMin", giaMin);
            cmd.Parameters.AddWithValue("@giaMax", giaMax);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    Console.WriteLine("Kết quả tìm kiếm:");
                    while (reader.Read())
                    {
                         Console.WriteLine($"TenSanPham: {reader.GetString("TenSanPham")}, Gia: {reader.GetDecimal("Gia")}, MaDanhMuc: {reader.GetInt32("MaDanhMuc")}");
                    }
                }
            }

        }
    }
}
