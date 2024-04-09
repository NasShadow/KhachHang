using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class FormQuanLyDanhGia : Form
    {
        ChiTietGioHang chitietgiohang = new ChiTietGioHang();
        KhachHang khachhang = new KhachHang();
        GioHang giohang = new GioHang();
        DonHang donhang = new DonHang();
        DanhGia danhgia = new DanhGia();
        YeuThich yeuthich = new YeuThich();
        public string Masp { get; set; }
        public string Tensp { get; set; }
        public string Hinhanh { get; set; }
        public string Gianhap { get; set; }
        public string Giaban { get; set; }
        public string Soluong { get; set; }

        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<GioHang> GioHang = dc.GetTable<GioHang>();

        public string tentaikhoan { get; set; }

        public FormQuanLyDanhGia(string masp, string tensp, string hinhanh, string gianhap, string giaban, string soluong, string tentk)
        {
            InitializeComponent();

            // Gán giá trị vào các thuộc tính của form 2
            Masp = masp;
            Tensp = tensp;
            Hinhanh = hinhanh;
            Gianhap = gianhap;
            Giaban = giaban;
            Soluong = soluong;

            lbl_tensanpham.Text = tensp;
            lbl_soluongsp.Text = "Số Lượng: " + soluong;

            double gianhapValue = double.Parse(gianhap) + 1000000;
            lbl_gianhap.Text = "Giá gốc: " + gianhapValue.ToString("C0");

            double giabanValue = double.Parse(giaban) + 0;
            lbl_giaban.Text = "Giá KM: " + giabanValue.ToString("C0");
            pic_spham.Image = new Bitmap($@"{Hinhanh}");

            this.tentaikhoan = tentk;
            //MessageBox.Show(this.tentaikhoan);
        }

        private void FormQuanLyDanhGia_Load(object sender, EventArgs e)
        {
            //Load giao diện comment
            Load_DanhGia();
            TaoMa();
        }
        private void TaoMa()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang FROM dbo.KhachHang WHERE Email = N'{this.tentaikhoan}' OR Ten_TK = N'{this.tentaikhoan}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    maKH = "";
                    maKH += reader.GetString(0);
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
        //Hàm load giao diện comment
        private void Load_DanhGia()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.KhachHang.Ten_Khach_Hang, dbo.DanhGia.Like_Dislike, dbo.KhachHang.FileNames, dbo.DanhGia.Noi_Dung, dbo.DanhGia.Ma_Khach_Hang, dbo.DanhGia.Ma_SP\r\nFROM dbo.SanPham \r\nJOIN dbo.DanhGia ON DanhGia.Ma_SP = SanPham.Ma_SP\r\nJOIN dbo.KhachHang ON KhachHang.Ma_Khach_Hang = DanhGia.Ma_Khach_Hang\r\nWHERE DanhGia.Ma_SP = N'{this.Masp}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string LikeDislike = reader["Like_Dislike"].ToString();
                    string TenKhachHang = reader["Ten_Khach_Hang"].ToString();
                    string NoiDung = reader["Noi_Dung"].ToString();
                    string MaKhachHang = reader["Ma_Khach_Hang"].ToString();
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string HinhAnh = reader["FileNames"].ToString();

                    if (LikeDislike == "True")
                    {
                        LikeDislike = "Hài Lòng";
                    }
                    else if (LikeDislike == "True")
                    {
                        LikeDislike = "Không Hài Lòng";
                    }


                    //Thêm pannel = div
                    Guna2GradientPanel panel_CMT = new Guna2GradientPanel();
                    panel_CMT.Size = new System.Drawing.Size(670, 120);
                    panel_CMT.FillColor = Color.FromArgb(224, 224, 224);
                    panel_CMT.FillColor2 = Color.FromArgb(224, 224, 224);
                    panel_CMT.BorderRadius = 10;
                    panel_CMT.Update();

                    //Cha chứa ảnh
                    Guna2GradientPanel panel_IMG = new Guna2GradientPanel();
                    //panel_IMG.FillColor = Color.Red;
                    //panel_IMG.FillColor2 = Color.Red;
                    panel_IMG.Dock = DockStyle.Left;
                    panel_IMG.BorderRadius = 10;
                    panel_IMG.Size = new System.Drawing.Size(120, 120);
                    panel_IMG.Padding = new Padding(10);
                    panel_IMG.Update();

                    //Thẻ img
                    Guna2CirclePictureBox pic_cmt = new Guna2CirclePictureBox();
                    pic_cmt.Image = new Bitmap($@"{HinhAnh}");
                    pic_cmt.Dock = DockStyle.Fill;
                    //pic_cmt.FillColor = Color.AliceBlue;
                    pic_cmt.SizeMode = PictureBoxSizeMode.StretchImage;

                    //Thẻ chữ chứa tên độc giả và đánh giá
                    Guna2GradientPanel panel_Name = new Guna2GradientPanel();
                    //panel_Name.FillColor = Color.Blue;
                    //panel_Name.FillColor2 = Color.Blue;
                    panel_Name.Dock = DockStyle.Fill;
                    panel_Name.BorderRadius = 10;
                    panel_Name.Update();

                    Guna2GradientPanel panel_childname = new Guna2GradientPanel();
                    //panel_childname.FillColor = Color.Yellow;
                    //panel_childname.FillColor2 = Color.Yellow;
                    panel_childname.Dock = DockStyle.Top;
                    panel_childname.BorderRadius = 10;
                    panel_childname.Size = new System.Drawing.Size(60, 60);
                    panel_childname.Update();

                    Guna2HtmlLabel lbl_Hoten_DanhGia = new Guna2HtmlLabel();
                    lbl_Hoten_DanhGia.Text = TenKhachHang + " | " + LikeDislike;
                    //lbl_Hoten_DanhGia.BackColor = Color.Brown;
                    lbl_Hoten_DanhGia.Font = new Font("SVN-Cintra", 15, FontStyle.Regular);
                    //lbl_Hoten_DanhGia.TextAlignment = ContentAlignment.MiddleLeft;
                    // Đặt vị trí của label để căn giữa dọc
                    int verticalLocation = (panel_childname.Height - lbl_Hoten_DanhGia.Height) / 2;
                    lbl_Hoten_DanhGia.Location = new Point(0, verticalLocation);
                    lbl_Hoten_DanhGia.Update();


                    Guna2GradientPanel panel_noidung = new Guna2GradientPanel();
                    //panel_noidung.FillColor = Color.Yellow;
                    //panel_noidung.FillColor2 = Color.Yellow;
                    panel_noidung.Dock = DockStyle.Top;
                    panel_noidung.BorderRadius = 10;
                    panel_noidung.Size = new System.Drawing.Size(60, 60);
                    //panel_noidung.AutoSize = true;
                    panel_noidung.Update();

                    Guna2HtmlLabel lbl_comment = new Guna2HtmlLabel();
                    lbl_comment.Text = NoiDung;
                    //lbl_comment.BackColor = Color.Brown;
                    lbl_comment.Font = new Font("JetBrains Mono NL", 12, FontStyle.Regular);
                    lbl_comment.AutoSize = true;
                    // Đặt vị trí của label để căn giữa dọc
                    int verticalLocation1 = (panel_noidung.Height - lbl_comment.Height) / 2;
                    lbl_comment.Location = new Point(0, verticalLocation);
                    lbl_comment.Update();


                    panel_noidung.Controls.Add(lbl_comment);

                    panel_childname.Controls.Add(lbl_Hoten_DanhGia);

                    //
                    panel_Name.Controls.Add(panel_noidung);

                    panel_Name.Controls.Add(panel_childname);

                    //Pannel img add img
                    panel_IMG.Controls.Add(pic_cmt);

                    //Pannel cha add con vào
                    panel_CMT.Controls.Add(panel_Name);
                    panel_CMT.Controls.Add(panel_IMG);

                    flowLayoutPanel2.Controls.Add(panel_CMT);
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

        string maKH = "";

        //Kiểm tra khi comment thì đã từng mua hàng, và khi mua hàng phải có ảnh profile
        private void txt_comment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                DialogResult result = MessageBox.Show("BẠN MUỐN THÊM ĐÁNH GIÁ NÀY CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();

                        //Tạo vòng lặp và lấy dữ liệu từ csdl
                        SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang FROM dbo.KhachHang WHERE Email = N'{this.tentaikhoan}' OR Ten_TK = N'{this.tentaikhoan}'", conn);
                        SqlDataReader reader = sqlCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            maKH = "";
                            maKH += reader.GetString(0);
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

                    khachhang.Ma_Khach_Hang = maKH;

                    string get = TKBLL.Check_IMGDanhGia(khachhang);

                    switch (get)
                    {

                        case "KhongTonTaiAnh":
                            {
                                MessageBox.Show("VUI LÒNG THÊM ẢNH VÀO TÀI KHOẢN CỦA BẠN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                    }

                    donhang.Ma_Khach_Hang = maKH;
                    donhang.Test_new = this.Masp;

                    string get1 = TKBLL.Check_BuySP(donhang);

                    switch (get1)
                    {
                        case "ChuaMua":
                            {
                                MessageBox.Show("BẠN CHƯA TỪNG MUA SẢN PHẨM NÀY KHÔNG THỂ ĐÁNH GIÁ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                    }


                    danhgia.Ma_Khach_Hang = maKH;
                    danhgia.Ma_SP = this.Masp;

                    string get2 = TKBLL.Check_CommentValid(danhgia);

                    switch (get2)
                    {
                        case "DaCMT":
                            {
                                MessageBox.Show("BẠN ĐÃ ĐÁNH GIÁ SẢN PHẨM NÀY RỒI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                    }

                    danhgia.Noi_Dung = txt_comment.Text;
                    danhgia.Ma_Khach_Hang = maKH;
                    danhgia.Ma_SP = this.Masp;
                    danhgia.Like_Dislike = a;

                    string get4 = TKBLL.Them_DanhGia(danhgia);

                    switch (get4)
                    {
                        case "Botrong":
                            {
                                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        default:
                            {
                                MessageBox.Show("THÊM ĐÁNH GIÁ THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FormQuanLyDanhGia_Load(null,null);
                                break;
                            }
                    }
                    e.Handled = true; // Ngăn chặn ký tự Enter được hiển thị trong TextBox
                }
                else if (result == DialogResult.No)
                {
                    return;
                }
            }
            

        }
        static string a = "";
                        
        //Nút like
        private void btn_like_Click(object sender, EventArgs e)
        {
            a = "";
            a = "1";
            btn_dislike.Enabled = true;
            btn_like.Enabled = false;
        }
        //Nút Dlikes
        private void btn_dislike_Click(object sender, EventArgs e)
        {
            a = "";
            a = "0";
            btn_like.Enabled = true;
            btn_dislike.Enabled = false;
        }


        private void txt_comment_TextChanged(object sender, EventArgs e)
        {

        }


        //Đóng Form
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void guna2CirclePictureBox1_Click_1(object sender, EventArgs e)
        {
            Close();
        }


        //Ấn nút thêm vào giỏ hàng lập tức sẽ thêm dữ liệu vào giỏ hàng của tài khoản đó
        private void btn_them_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN MUỐN THÊM SẢN PHẨM VÀO GIỎ HÀNG CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                //Kiểm tra xem có tồn tại giỏ hàng hay chưa
                try
                {
                    string maKH = "";
                    string maGH = "";

                    using (SqlConnection conn = TKBLL.Load())
                    {
                        conn.Open();

                        SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.KhachHang.Ma_Khach_Hang, dbo.GioHang.Ma_gio_hang FROM dbo.KhachHang\r\n\tJOIN dbo.GioHang ON GioHang.Ma_Khach_Hang = KhachHang.Ma_Khach_Hang\r\n\tWHERE Ten_TK = N'{this.tentaikhoan}'", conn);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            //SqlDataReader reader = sqlCommand.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    maKH += reader.GetString(0);
                                    maGH += reader.GetString(1);
                                }
                            }
                        }
                    }

                    var result_GH = from makhachhang in GioHang
                                    select makhachhang;

                    //Lần thứ 2 mua hàng
                    foreach (var item in result_GH)
                    {
                        if (maKH == item.Ma_Khach_Hang)
                        {
                            //MessageBox.Show("Đã tồn tại");
                            chitietgiohang.Ma_SP = this.Masp;
                            chitietgiohang.Ma_gio_hang = maGH;
                            chitietgiohang.So_luong = "1";

                            string get_Check = TKBLL.Check_InsertTonTaiChiTietGioHang(chitietgiohang);

                            switch (get_Check)
                            {
                                case"TonTai":
                                    {
                                        MessageBox.Show($"GIỎ HÀNG CỦA BẠN ĐÃ TỒN TẠI SẢN PHẨM {this.Tensp}", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                            }

                            chitietgiohang.So_luong = "1";

                            string get = TKBLL.Check_InsertChiTietGioHang(chitietgiohang);

                            switch (get)
                            {
                                default:
                                    {
                                        MessageBox.Show($"ĐÃ THÊM SẢN PHẨM {this.Tensp} VÀO GIỎ HÀNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        break;
                                    }
                            }

                            return;
                        }
                    }


                    using (SqlConnection conn = TKBLL.Load())
                    {
                        conn.Open();

                        SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang FROM dbo.KhachHang WHERE Ten_TK = N'{this.tentaikhoan}'", conn);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            //SqlDataReader reader = sqlCommand.ExecuteReader();
                            if (reader.HasRows == true)
                            {
                                while (reader.Read())
                                {
                                    maKH += reader.GetString(0);
                                }
                            }
                        }
                    }

                    giohang.Ma_Khach_Hang = maKH;

                    string get_giohang = TKBLL.Check_InsertGioHang(giohang);

                    switch (get_giohang)
                    {
                        default:
                            {
                                MessageBox.Show($"ĐÃ TẠO GIỎ HÀNG THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }

            }

            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void pic_yeuthich_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN THÊM SẢN PHẨM NÀY VÀO MỤC YÊU THÍCH KHÔNG?", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                yeuthich.Ma_Khach_Hang = maKH;
                yeuthich.Ma_SP = this.Masp;

                //Kiểm tra
                string get_1 = TKBLL.Check_YeuThich(yeuthich);

                switch (get_1)
                {
                    case "TonTai":
                        {
                            MessageBox.Show("SẢN PHẨM NÀY ĐÃ CÓ TẠI MỤC YÊU THÍCH!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                }

                //Kiểm tra xong bước đó
                string get = TKBLL.Them_YeuThich(yeuthich);

                switch (get)
                {
                    default:
                        {
                            MessageBox.Show("THÊM SẢN PHẨM VÀO MỤC YÊU THÍCH THÀNH CÔNG!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }
    }
}
