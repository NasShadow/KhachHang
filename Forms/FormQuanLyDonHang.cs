using BLL;
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
    public partial class FormQuanLyDonHang : Form
    {
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        public string MaDH { get; set; }
        public string ThanhTien { get; set; }
        public string PhuongThucTT { get; set; }
        public string DiaChi { get; set; }
        public string TinhTrang { get; set; }

        public FormQuanLyDonHang(string madh, string thanhtien, string phuongthucthanhtoan, string diachi, string xuly)
        {
            InitializeComponent();
            this.MaDH = madh;
            this.ThanhTien = thanhtien;
            this.PhuongThucTT = phuongthucthanhtoan;
            this.DiaChi = diachi;
            this.TinhTrang = xuly;
        }
        private void LayGiaTriDeF()
        {
            txt_hinhthucthanhtoan.Enabled = false;
            txt_tongtien.Enabled = false;
            txt_diachi.Enabled = false;
            txt_trangthai.Enabled = false;

            txt_tongtien.Text = this.ThanhTien;
            txt_diachi.Text = this.DiaChi;

            txt_hinhthucthanhtoan.Text = this.PhuongThucTT;
            if (txt_hinhthucthanhtoan.Text == "True")
            {
                txt_hinhthucthanhtoan.Text = "Thanh Toán Online";
            }
            else if (txt_hinhthucthanhtoan.Text == "False")
            {
                txt_hinhthucthanhtoan.Text = "Thanh Toán Trực Tiếp";
            }

            txt_trangthai.Text = this.TinhTrang;
        }

        private void FormQuanLyDonHang_Load(object sender, EventArgs e)
        {
            Load_DesignGioHang();
            LayGiaTriDeF();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Tạo giao diện giỏ hàng
        private void Load_DesignGioHang()
        {

            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT SanPham.Ma_SP, Ten_SP, Gia_ban, FileNames\r\nFROM dbo.ChiTietDonHang JOIN dbo.SanPham ON SanPham.Ma_SP = ChiTietDonHang.Ma_SP\r\nWHERE Ma_Don_Hang = N'{this.MaDH}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string TenSP = reader["Ten_SP"].ToString();
                    string GiaBan = reader["Gia_ban"].ToString();
                    string FileNames = reader["FileNames"].ToString();

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
                    label_maten.Text = MaSanPham + " | " + TenSP + " | " + this.TinhTrang;
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
    }
}
