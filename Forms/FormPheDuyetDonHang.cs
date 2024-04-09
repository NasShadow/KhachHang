using BLL;
using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login.Forms
{
    public partial class FormPheDuyetDonHang : Form
    {
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        ChiTietDonHang chitietdonhang = new ChiTietDonHang();
        PhuongThucThanhToan phuongThucThanhToan = new PhuongThucThanhToan();
        DonHang donhang = new DonHang();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        public string makh { get; set; }
        public FormPheDuyetDonHang(string MAKH)
        {
            InitializeComponent();

            this.makh = MAKH;

            LoadGDDonHang();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Load GIao DIện đơn hàng
        private void LoadGDDonHang()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.DonHang.Ma_Don_Hang,Ngay_Xuat_Don, dbo.DonHang.Ma_Khach_Hang,Trang_Thai_Don, Ma_Nhan_Vien, Ma_Nhan_VienGH, Thanh_Tien, Phuong_thuc_thanh_toan, DonHang.Dia_Chi FROM dbo.DonHang\r\nJOIN dbo.PhuongThucThanhToan ON PhuongThucThanhToan.Ma_Don_Hang = DonHang.Ma_Don_Hang WHERE dbo.DonHang.Ma_Khach_Hang = N'{this.makh}' AND Ma_Nhan_Vien IS NULL AND Ma_Nhan_VienGH IS NULL", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaDonHang = reader["Ma_Don_Hang"].ToString();
                    string NgayXuatDon = reader["Ngay_Xuat_Don"].ToString();
                    string MaKhachHang = reader["Ma_Khach_Hang"].ToString();
                    string TrangThaiDon = reader["Trang_Thai_Don"].ToString();
                    string MaNhanVien = reader["Ma_Nhan_Vien"].ToString();
                    string MaNhanVienGH = reader["Ma_Nhan_VienGH"].ToString();
                    string ThanhTien = reader["Thanh_Tien"].ToString();
                    string PhuongThucThanhToan = reader["Phuong_thuc_thanh_toan"].ToString();
                    string DiaChi = reader["Dia_Chi"].ToString();

                    string xuly = "";

                    if (MaNhanVien == "")
                    {
                        xuly = "Chờ Phê Duyệt";
                    }

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.Gray;
                    panel_Father.FillColor2 = Color.Gray;
                    panel_Father.Size = new System.Drawing.Size(1750, 250);
                    panel_Father.Update();

                    Guna2GradientPanel panel_img = new Guna2GradientPanel();
                    panel_img.Dock = DockStyle.Left;
                    panel_img.FillColor = Color.FromArgb(240, 240, 240);
                    panel_img.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_img.Size = new System.Drawing.Size(300, 300);
                    panel_img.Padding = new Padding(10);
                    panel_img.Update();

                    Guna2PictureBox hinhanh = new Guna2PictureBox();
                    hinhanh.Image = new Bitmap($@"C:\Users\ADMIN\Downloads\—Pngtree—invoice icon design vector_4874278.png");
                    hinhanh.Dock = DockStyle.Fill;
                    hinhanh.BackColor = Color.Transparent;
                    hinhanh.BorderRadius = 10;
                    hinhanh.SizeMode = PictureBoxSizeMode.StretchImage;
                    hinhanh.Update();

                    Guna2GradientPanel panel_content = new Guna2GradientPanel();
                    panel_content.Dock = DockStyle.Left;
                    panel_content.FillColor = Color.Red;
                    panel_content.FillColor2 = Color.Red;
                    //panel_content.Size = new System.Drawing.Size(500, 100);
                    panel_content.Width = 680;
                    panel_content.Update();


                    Guna2GradientPanel panel_fmaten = new Guna2GradientPanel();
                    panel_fmaten.Dock = DockStyle.Top;
                    panel_fmaten.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten.Height = 125;
                    panel_fmaten.Update();

                    Guna2HtmlLabel label_maten = new Guna2HtmlLabel();
                    label_maten.Text = MaDonHang + " | " + MaKhachHang + " | " + xuly;
                    label_maten.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten.BackColor = Color.Transparent;
                    label_maten.ForeColor = Color.FromArgb(255, 0, 0);
                    int verticalLocation = (panel_fmaten.Height - label_maten.Height) / 2;
                    label_maten.Location = new Point(0, verticalLocation);
                    label_maten.Update();

                    Guna2GradientPanel panel_fmaten1 = new Guna2GradientPanel();
                    panel_fmaten1.Dock = DockStyle.Bottom;
                    panel_fmaten1.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.Height = 125;
                    panel_fmaten1.Update();

                    Guna2HtmlLabel label_maten1 = new Guna2HtmlLabel();
                    label_maten1.Text = "NGÀY XUẤT ĐƠN: " + NgayXuatDon;
                    label_maten1.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten1.BackColor = Color.Transparent;
                    int verticalLocation1 = (panel_fmaten1.Height - label_maten1.Height) / 2;
                    label_maten1.Location = new Point(0, verticalLocation1);
                    label_maten1.ForeColor = Color.FromArgb(255, 0, 0);
                    label_maten1.Update();

                    Guna2GradientPanel panel_content1 = new Guna2GradientPanel();
                    panel_content1.Dock = DockStyle.Left;
                    panel_content1.FillColor = Color.FromArgb(240, 240, 240);
                    panel_content1.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_content1.Size = new System.Drawing.Size(500, 100);
                    panel_content1.Update();

                    Guna2Button btn_xem = new Guna2Button();
                    btn_xem.Text = "XEM";
                    btn_xem.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    btn_xem.Height = 80;

                    // Căn giữa theo chiều dọc
                    btn_xem.Location = new Point(125, 90);
                    btn_xem.FillColor = Color.FromArgb(64, 64, 64);
                    btn_xem.Click += (s, e) => DatHangClick(s, e, MaDonHang, ThanhTien, PhuongThucThanhToan, DiaChi, xuly);
                    btn_xem.Update();


                    Guna2GradientPanel panel_content2 = new Guna2GradientPanel();
                    panel_content2.Dock = DockStyle.Fill;
                    panel_content2.FillColor = Color.White;
                    panel_content2.FillColor2 = Color.White;
                    panel_content2.Update();

                    Guna2Button btn_xoa = new Guna2Button();
                    btn_xoa.Image = new Bitmap($@"C:\Users\ADMIN\Desktop\DuAnMau (1)\HinhAnh\delete-button (1).png");
                    btn_xoa.ImageAlign = HorizontalAlignment.Center;
                    btn_xoa.ImageSize = new System.Drawing.Size(40, 40);
                    btn_xoa.FillColor = Color.Transparent;
                    btn_xoa.BackColor = Color.Transparent;
                    btn_xoa.Location = new Point(120, 105);
                    btn_xoa.Size = new System.Drawing.Size(50, 50);
                    btn_xoa.Click += (s, e) => DatHangClick1(s, e, MaDonHang, ThanhTien, PhuongThucThanhToan, DiaChi, xuly);
                    btn_xoa.Update();

                    panel_content2.Controls.Add(btn_xoa);

                    panel_content1.Controls.Add(btn_xem);

                    panel_fmaten1.Controls.Add(label_maten1);

                    panel_content.Controls.Add(panel_fmaten1);

                    panel_fmaten.Controls.Add(label_maten);

                    panel_content.Controls.Add(panel_fmaten);

                    panel_img.Controls.Add(hinhanh);
                    panel_Father.Controls.Add(panel_content2);
                    panel_Father.Controls.Add(panel_content1);
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

        private void DatHangClick(object sender, EventArgs e, string madh, string thanhtien, string phuongthucthanhtoan, string diachi, string xuLy)
        {

            FormQuanLyDonHang formQuanLyDonHang = new FormQuanLyDonHang(madh, thanhtien, phuongthucthanhtoan, diachi, xuLy);
            formQuanLyDonHang.ShowDialog();

        }
        //XoaSP
        private void DatHangClick1(object sender, EventArgs e, string madh, string thanhtien, string phuongthucthanhtoan, string diachi, string xuLy)
        {
            DialogResult result = MessageBox.Show("BAN CO MUON HUY DON HANG NAY KHONG???", "THONG BAO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                chitietdonhang.Ma_Don_Hang = madh;
                phuongThucThanhToan.Ma_Don_Hang = madh;
                donhang.Ma_Don_Hang = madh;

                //Xoa chi tiet don hang
                string get = TKBLL.XoaPhe_DuyetChiTiet(chitietdonhang);

                switch (get)
                {
                    default:
                        {
                            break;
                        }
                }

                //XOA PHUONG THUC THANH TOAN
                string get1 = TKBLL.XoaPhe_DuyetPhuongThuc(phuongThucThanhToan);

                switch (get1)
                {
                    default:
                        {
                            break;
                        }
                }

                //XOA PHUONG DON HANG
                string get2 = TKBLL.XoaPhe_DuyetDonHang(donhang);

                switch (get2)
                {
                    default:
                        {
                            MessageBox.Show("HUY DON HANG THANH CONG!!!", "THONG BAO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadGDDonHang();
                            break;
                        }
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }

        }

        private void FormPheDuyetDonHang_Load(object sender, EventArgs e)
        {

        }
    }
}
