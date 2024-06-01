class DonHang{
    public int MaDonHang{get; set;}
    public int MaKhachHang{get; set;}
    public int  MaDiaChi{get; set;}
    public string NgayDatHang{get;set;}
    public decimal TongGia{get; set;}
    public string TrangThai{get;set;}
    public List<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}