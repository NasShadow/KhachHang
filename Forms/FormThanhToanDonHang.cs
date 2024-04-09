using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class FormThanhToanDonHang : Form
    {
        ChiTietGioHang chitietgiohang = new ChiTietGioHang();
        ChiTietDonHang chitietdonhang = new ChiTietDonHang();
        SanPham sanpham = new SanPham();
        DonHang donhang = new DonHang();
        PhuongThucThanhToan phuongthucthanhtoan = new PhuongThucThanhToan();
        Voucher voucher = new Voucher();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        public string email { get; set; }
        public string magh { get; set; }
        public string makh { get; set; }

        public string tongtien { get; set; }

        public FormThanhToanDonHang(string email, string magio, string makhach, string tongtien)
        {
            InitializeComponent();
            this.email = email;
            this.magh = magio;
            this.makh = makhach;
            this.tongtien = tongtien;
        }

        //FormLoad
        private void FormThanhToanDonHang_Load(object sender, EventArgs e)
        {
            voucher.Ma_Voucher = null;
            //Load sản phẩm khách hàng mua
            Load_DesignGioHang();

            //Load giá trị combobox
            LoadComboBox();
            Check_value();
        }

        private void Check_value()
        {
            double tongtien = double.Parse(this.tongtien);
            if (tongtien >= 100000)
            {
                cbo_voucher.Enabled = true;
            }
            else if (tongtien < 100000)
            {
                cbo_voucher.Enabled = false;
            }
        }

        private void LoadComboBox()
        {
            
            try
            {
                cbo_diachi.Items.Clear();
                cbo_hinhthucthanhtoan.Items.Add("Thanh Toán Online");
                cbo_hinhthucthanhtoan.Items.Add("Thanh Toán Trực Tiếp");
                cbo_voucher.Items.Add("Không sử dụng voucher");

                txt_tongtien.Text = this.tongtien;

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"\t\tSELECT Dia_Chi, Ten_Khach_Hang FROM dbo.KhachHang\r\n\t\tJOIN dbo.DiaChi ON DiaChi.Ma_Khach_Hang = KhachHang.Ma_Khach_Hang\r\n\t\tWHERE DiaChi.Ma_Khach_Hang = N'{this.makh}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    cbo_diachi.Items.Add(reader.GetString(0));
                }
                reader.Close();
                SqlCommand sqlCommand1 = new SqlCommand($"SELECT Gia_Tri FROM dbo.Voucher WHERE Ma_Khach_Hang = N'{makh}' AND Tinh_Trang IS NULL", conn);
                SqlDataReader reader1 = sqlCommand1.ExecuteReader();
                while (reader1.Read())
                {
                    cbo_voucher.Items.Add(reader1.GetString(0) + "%");
                }
                reader1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //Tạo giao diện giỏ hàng
        private void Load_DesignGioHang()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                txt_tongtien.Enabled = false;

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.ChiTietGioHang.Ma_SP, dbo.ChiTietGioHang.So_luong, dbo.SanPham.Ten_SP, dbo.SanPham.Gia_ban, dbo.SanPham.FileNames, dbo.SanPham.So_luong N'SLSP' FROM dbo.ChiTietGioHang \r\n\tJOIN dbo.SanPham ON SanPham.Ma_SP = ChiTietGioHang.Ma_SP\r\n\tWHERE Ma_gio_hang = N'{this.magh}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string SoLuong = reader["So_luong"].ToString();
                    string TenSP = reader["Ten_SP"].ToString();
                    string GiaBan = reader["Gia_ban"].ToString();
                    string FileNames = reader["FileNames"].ToString();
                    string SoLuongSP = reader["SLSP"].ToString();

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.Gray;
                    panel_Father.FillColor2 = Color.Gray;
                    panel_Father.Size = new System.Drawing.Size(400, 100);
                    panel_Father.Update();

                    Guna2GradientPanel panel_img = new Guna2GradientPanel();
                    panel_img.Dock = DockStyle.Left;
                    panel_img.FillColor = Color.FromArgb(240, 240, 240);
                    panel_img.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_img.Size = new System.Drawing.Size(100, 100);
                    panel_img.Padding = new Padding(10);
                    panel_img.Update();

                    Guna2PictureBox hinhanh = new Guna2PictureBox();
                    hinhanh.Image = new Bitmap($@"{FileNames}");
                    hinhanh.Dock = DockStyle.Fill;
                    hinhanh.BackColor = Color.Transparent;
                    hinhanh.BorderRadius = 10;
                    hinhanh.SizeMode = PictureBoxSizeMode.StretchImage;
                    hinhanh.Update();

                    Guna2GradientPanel panel_content = new Guna2GradientPanel();
                    panel_content.Dock = DockStyle.Fill;
                    panel_content.FillColor = Color.Transparent;
                    panel_content.FillColor2 = Color.Transparent;
                    //panel_content.Size = new System.Drawing.Size(500, 100);
                    panel_content.Width = 500;
                    panel_content.Update();


                    Guna2GradientPanel panel_fmaten = new Guna2GradientPanel();
                    panel_fmaten.Dock = DockStyle.Top;
                    panel_fmaten.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten.Height = 50;
                    panel_fmaten.Update();

                    Guna2HtmlLabel label_maten = new Guna2HtmlLabel();
                    label_maten.Text = MaSanPham + " | " + TenSP + " | " + "SL: " + SoLuong;
                    label_maten.Font = new Font("SVN-Cintra", 13, FontStyle.Regular);
                    label_maten.BackColor = Color.Transparent;
                    int verticalLocation = (panel_fmaten.Height - label_maten.Height) / 2;
                    label_maten.Location = new Point(0, verticalLocation);
                    label_maten.Update();

                    Guna2GradientPanel panel_fmaten1 = new Guna2GradientPanel();
                    panel_fmaten1.Dock = DockStyle.Bottom;
                    panel_fmaten1.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.Height = 50;
                    panel_fmaten1.Update();

                    Guna2HtmlLabel label_maten1 = new Guna2HtmlLabel();
                    label_maten1.Text = "THÀNH TIỀN: " + GiaBan + " VNĐ";
                    label_maten1.Font = new Font("SVN-Cintra", 13, FontStyle.Regular);
                    label_maten1.BackColor = Color.Transparent;
                    int verticalLocation1 = (panel_fmaten1.Height - label_maten1.Height) / 2;
                    label_maten1.Location = new Point(0, verticalLocation1);
                    label_maten1.Update();

                    panel_fmaten1.Controls.Add(label_maten1);

                    panel_content.Controls.Add(panel_fmaten1);

                    panel_fmaten.Controls.Add(label_maten);

                    panel_content.Controls.Add(panel_fmaten);

                    panel_img.Controls.Add(hinhanh);

                    panel_Father.Controls.Add(panel_content);
                    panel_Father.Controls.Add(panel_img);

                    //MessageBox.Show(flowLayoutPanel1.Width.ToString());
                    flowLayoutPanel1.Controls.Add(panel_Father);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        //nút thanh toán hóa đơn
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                    //Thêm đơn hàng
                    donhang.Thanh_Tien = txt_tongtien.Text;
               /* donhang.Trang_Thai_Don = "";*/
                donhang.Dia_Chi = cbo_diachi.Text;
                donhang.Ma_Khach_Hang = this.makh;
                /*donhang.Ma_Nhan_Vien = "";
                donhang.Ma_Nhan_VienGH = "";*/

                if (cbo_hinhthucthanhtoan.Text == "")
                {
                    MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //Kiem tra don hang
                double tongtien = double.Parse(this.tongtien);
                if (tongtien >= 100000)
                {
                    if (cbo_voucher.Text == "")
                    {
                        MessageBox.Show("VUI LÒNG CHỌN TRẠNG THÁI VOUCHER!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                string get = TKBLL.Insert_DonHang(donhang);


            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                default:
                    {
                        MessageBox.Show("THANH TOÁN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LayMaDonHang();
                        break;
                    }
            }

            //Thêm phương thức thanh toán
            phuongthucthanhtoan.Phuong_thuc_thanh_toan = cbo_hinhthucthanhtoan.Text;

            if (cbo_hinhthucthanhtoan.Text == "Thanh Toán Online")
            {
                phuongthucthanhtoan.Phuong_thuc_thanh_toan = "1";
            }
            else if (cbo_hinhthucthanhtoan.Text == "Thanh Toán Trực Tiếp")
            {
                phuongthucthanhtoan.Phuong_thuc_thanh_toan = "0";
            }

            phuongthucthanhtoan.Ma_Don_Hang = madh;

            string get1 = TKBLL.Insert_PhuongThucThanhToan(phuongthucthanhtoan);


            switch (get1)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                default:
                    {
                        //MessageBox.Show("Phương Thức THANH TOÁN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }

            //Thềm thông tin vào đơn hàng
            
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.ChiTietGioHang.Ma_SP, dbo.ChiTietGioHang.So_luong, dbo.SanPham.Ten_SP, dbo.SanPham.Gia_ban, dbo.SanPham.FileNames, dbo.SanPham.So_luong N'SLSP' FROM dbo.ChiTietGioHang \r\n\tJOIN dbo.SanPham ON SanPham.Ma_SP = ChiTietGioHang.Ma_SP\r\n\tWHERE Ma_gio_hang = N'{this.magh}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string SoLuong = reader["So_luong"].ToString();
                    string TenSP = reader["Ten_SP"].ToString();
                    string GiaBan = reader["Gia_ban"].ToString();
                    string FileNames = reader["FileNames"].ToString();
                    string SoLuongSP = reader["SLSP"].ToString();

                    chitietdonhang.Ma_Don_Hang = madh;
                    chitietdonhang.Ma_SP = MaSanPham;
                    chitietdonhang.So_luong = SoLuong;

                    string get2 = TKBLL.Insert_ChiTietDonHang(chitietdonhang);

                    switch (get2)
                    {
                        default:
                            {
                                //MessageBox.Show("Thêm sản phẩm vào đơn hàng tcong!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                    }


                    int slcuaminh = int.Parse(SoLuong);
                    int slsp = int.Parse(SoLuongSP);

                    int tru = 0;

                    tru = slsp - slcuaminh;

                    sanpham.So_luong = tru.ToString();

                    //MessageBox.Show(sanpham.So_luong);

                    sanpham.Ma_SP = MaSanPham;

                    string get4 = TKBLL.Cap_NhatSoLuongSanPham(sanpham);

                    switch (get4)
                    {
                        default:
                            {
                                //MessageBox.Show("Thêm sản phẩm vào đơn hàng tcong!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                    }
                    

                }

                chitietgiohang.Ma_gio_hang = magh;

                string get3 = TKBLL.Xoa_SanPhamGioHang(chitietgiohang);
                switch (get3)
                {
                    default:
                        {
                            MessageBox.Show("Xóa Thành Công Giỏ Hàng!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                }


                voucher.Ma_Voucher = mavc;

                if(cbo_voucher.Text!= "Không sử dụng voucher")
                {
                    voucher.Tinh_Trang = "1";
                }
                else if (cbo_voucher.Text == "Không sử dụng voucher"||cbo_voucher.Text=="")
                {
                    voucher.Tinh_Trang = "";
                }
                
                string get5 = TKBLL.CheckUpdateVoucherThanhToan(voucher);
                switch (get5)
                {
                    default:
                        {
/*                            MessageBox.Show("Xóa Thành Công Giỏ Hàng!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
*/                            break;
                        }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        string madh = "";

        private void LayMaDonHang()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Don_Hang FROM dbo.DonHang \r\nWHERE Ma_Khach_Hang = N'{this.makh}'\r\nORDER BY (ID) DESC", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    madh += reader.GetString(0);    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }
        
        static double firstDigit = 0;
        static double tongfinal =0;
        static double tongfinal1 =0;
        static double shipfinal = 0;
        static double voucherfinal = 0;
        static double giamgia = 0;

        private void Reset_VouCher()
        {
            firstDigit = 0;
            tongfinal = 0;
            tongfinal1 = 0;
            shipfinal = 0;
            voucherfinal = 0;
            giamgia = 0;
        }
        private void Reset_VouCher1()
        {
            firstDigit = 0;
            tongfinal = 0;
            tongfinal1 = 0;
            voucherfinal = 0;
            giamgia = 0;
        }
        private void cbo_diachi_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {


                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"\t\tSELECT Dia_Chi, Ten_Khach_Hang,Lat,Lon FROM dbo.KhachHang\r\n\t\tJOIN dbo.DiaChi ON DiaChi.Ma_Khach_Hang = KhachHang.Ma_Khach_Hang\r\n\t\tWHERE DiaChi.Ma_Khach_Hang = N'{this.makh}' and DiaChi.Dia_Chi=N'{cbo_diachi.Text}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Lat.Text = "";
                    Lon.Text = "";
                    Lat.Text += reader.GetString(2);
                    Lon.Text += reader.GetString(3);
                    double lat1 = 16.0757254;
                    double lon1 = 108.1596816;
                    double lat2 = double.Parse(Lat.Text, CultureInfo.InvariantCulture);
                    double lon2 = double.Parse(Lon.Text, CultureInfo.InvariantCulture);
                    var R = 6371e3;
                    var O1 = lat1 * Math.PI / 180;
                    var O2 = lat2 * Math.PI / 180;
                    var @O = (lat2 - lat1) * Math.PI / 180;
                    var @M = (lon2 - lon1) * Math.PI / 180;
                    var a = Math.Sin(@O / 2) * Math.Sin(@O / 2) + Math.Cos(O1) * Math.Cos(O2) * Math.Sin(@M / 2) * Math.Sin(@M / 2);
                    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                    var dist = R * c;
                    var distKM = dist / 1000;
                    firstDigit = Math.Round(distKM);
                    km.Text = firstDigit.ToString();
                    int ship = int.Parse(km.Text) * 10000;
                    if (cbo_voucher.Text == "Không sử dụng voucher")
                    {
                        Reset_VouCher();
                        //Lấy tổng tiền giá ban đầu
                        tongfinal = double.Parse(this.tongtien);

                        //Lấy tổng tiền + ship
                        tongfinal += ship;
                        shipfinal = ship;
                        txt_tongtien.Text = tongfinal.ToString();
                        return;
                    }
                    else if (cbo_voucher.Text == "")
                    {
                        Reset_VouCher();
                        //Lấy tổng tiền giá ban đầu
                        tongfinal = double.Parse(this.tongtien);
                        //Lấy tiền ship
                        shipfinal = ship;
                        //Lấy tổng tiền + ship
                        tongfinal += shipfinal;
                        txt_tongtien.Text = tongfinal.ToString();
                        return;
                    }
                    else if (cbo_voucher.Text != "")
                    {
                        Reset_VouCher();
                        //Lấy tổng tiền giá ban đầu
                        tongfinal = double.Parse(this.tongtien);
                        //Lấy tiền ship
                        shipfinal = ship;

                        //Lấy tổng tiền + ship
                        tongfinal += ship;

                        //Giá tiền được giảm
                        giamgia = tongfinal * (takenumber / 100);

                        //Lấy tiền tổng tiền - giảm giá
                        tongfinal1 = tongfinal - giamgia;
                        txt_tongtien.Text = tongfinal1.ToString();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        static string mavc = "";
        static double takenumber = 0;
        private void cbo_voucher_SelectedIndexChanged(object sender, EventArgs e)
        {
            Match match = Regex.Match(cbo_voucher.Text, @"\d+");
            if (match.Success)
            {
                int number = int.Parse(match.Value);
                takenumber = number;
            }
            if (cbo_voucher.Text == "Không sử dụng voucher")
            {
                Reset_VouCher1();
                //Lấy tổng tiền giá ban đầu
                tongfinal = double.Parse(this.tongtien);
                mavc = "";
                //Lấy tổng tiền + ship
                tongfinal += shipfinal;
                txt_tongtien.Text = tongfinal.ToString();
                return;
            }
            //

            conn.Open();

            //Tạo vòng lặp và lấy dữ liệu từ csdl
            SqlCommand sqlCommand = new SqlCommand($"select top 1 * from Voucher where Gia_Tri='{takenumber}' and Ma_Khach_Hang='{makh}' and Tinh_Trang is NULL", conn);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                mavc = "";
                mavc += reader.GetString(1);
            }

            conn.Close ();

            if (cbo_diachi.Text == "")
            {
                Reset_VouCher1();
                //Lấy tổng tiền giá ban đầu
                tongfinal = double.Parse(this.tongtien);

                //
                giamgia = tongfinal * (takenumber / 100);

                //Lấy tiền tổng tiền - giảm giá
                tongfinal1 = tongfinal - giamgia;
                txt_tongtien.Text = tongfinal1.ToString();
                return;
            }
            else if (cbo_diachi.Text != "")
            {
                Reset_VouCher1();
                //Lấy tổng tiền giá ban đầu
                tongfinal = double.Parse(this.tongtien);

                //Lấy tổng tiền + ship
                tongfinal += shipfinal;

                //Giá tiền được giảm
                giamgia = tongfinal * (takenumber / 100);

                //Lấy tiền tổng tiền - giảm giá
                tongfinal1 = tongfinal - giamgia;
                txt_tongtien.Text = tongfinal1.ToString();
                return;
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
