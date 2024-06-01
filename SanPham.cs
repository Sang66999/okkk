using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;
using System.Security.Policy;
class SanPham
{
    public int MaSanPham{get; set;}
    public int MaDanhMuc{get; set;}
    public string TenSanPham{get;set;}
    public string MieuTa{get; set;}
    public decimal Gia{get; set;}
    public int SoLuong{get; set;}
    public string URLHinhAnh {get; set;}
}