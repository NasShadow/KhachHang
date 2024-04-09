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
    public partial class FormCustomers : Form
    {
        YeuThich yeuthich = new YeuThich();

        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        public string taikhoan { get; set; }
        public FormCustomers(string tk)
        {
            InitializeComponent();
            this.taikhoan = tk;
        }

        private void FormCustomers_Load(object sender, EventArgs e)
        {
            LoadTheme();
            //Hàm lấy mã kh
            TaoMa();
            //load giao diện
            Load_DesignGioHang();
        }
        string maKH = "";
        private void TaoMa()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang FROM dbo.KhachHang WHERE Email = N'{this.taikhoan}' OR Ten_TK = N'{this.taikhoan}'", conn);
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
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
            label4.ForeColor = ThemeColor.SecondaryColor;
            label5.ForeColor = ThemeColor.PrimaryColor;

        }
        private void Load_DesignGioHang()
        {

            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.YeuThich\r\nJOIN dbo.SanPham ON SanPham.Ma_SP = YeuThich.Ma_SP\r\nWHERE Ma_Khach_Hang = N'{maKH}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaSanPham = reader["Ma_SP"].ToString();
                    string SoLuong = reader["So_luong"].ToString();
                    string TenSP = reader["Ten_SP"].ToString();
                    string GiaBan = reader["Gia_ban"].ToString();
                    string FileNames = reader["FileNames"].ToString();
                    string GiaNhap = reader["Gia_nhap"].ToString();

                    if (SoLuong == "0")
                    {
                        continue;
                    }

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.FromArgb(240, 240, 240);
                    panel_Father.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_Father.Size = new System.Drawing.Size(1542, 250);
                    panel_Father.Update();

                    Guna2GradientPanel panel_img = new Guna2GradientPanel();
                    panel_img.Dock = DockStyle.Left;
                    panel_img.FillColor = Color.FromArgb(240, 240, 240);
                    panel_img.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_img.Size = new System.Drawing.Size(300, 300);
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
                    panel_content.Dock = DockStyle.Left;
                    panel_content.FillColor = Color.Transparent;
                    panel_content.FillColor2 = Color.Transparent;
                    //panel_content.Size = new System.Drawing.Size(500, 100);
                    panel_content.Width = 500;
                    panel_content.Update();


                    Guna2GradientPanel panel_fmaten = new Guna2GradientPanel();
                    panel_fmaten.Dock = DockStyle.Top;
                    panel_fmaten.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten.Height = 125;
                    panel_fmaten.Update();

                    Guna2HtmlLabel label_maten = new Guna2HtmlLabel();
                    label_maten.Text = MaSanPham + " | " + TenSP;
                    label_maten.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten.BackColor = Color.Transparent;
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
                    label_maten1.Text = "THÀNH TIỀN: " + GiaBan + " VNĐ";
                    label_maten1.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten1.BackColor = Color.Transparent;
                    int verticalLocation1 = (panel_fmaten1.Height - label_maten1.Height) / 2;
                    label_maten1.Location = new Point(0, verticalLocation1);
                    label_maten1.Update();


                    Guna2GradientPanel panel_soluong = new Guna2GradientPanel();
                    panel_soluong.Dock = DockStyle.Right;
                    panel_soluong.FillColor = Color.Transparent;
                    panel_soluong.FillColor2 = Color.Transparent;
                    panel_soluong.Width = 500;
                    panel_soluong.Update();

                    Guna2GradientPanel panel_tru = new Guna2GradientPanel();
                    panel_tru.Dock = DockStyle.Left;
                    panel_tru.FillColor = Color.FromArgb(240, 240, 240);
                    panel_tru.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_tru.Width = 166;
                    panel_tru.Update();


                    Guna2CircleButton btn_tru = new Guna2CircleButton();
                    //btn_tru.Text = "-";
                    btn_tru.Image = new Bitmap($@"C:\Users\ADMIN\Downloads\search.png");
                    btn_tru.ImageSize = new System.Drawing.Size(40, 40);
                    btn_tru.ImageAlign = HorizontalAlignment.Center;
                    btn_tru.Font = new Font("SVN-Cintra", 20, FontStyle.Regular);
                    btn_tru.Size = new System.Drawing.Size(50, 50);
                    //btn_tru.FillColor = Color.Black;
                    btn_tru.FillColor = Color.Transparent;
                    btn_tru.BackColor = Color.Transparent;
                    btn_tru.Click += (s, e) => DatHangClick(s, e, MaSanPham, TenSP, FileNames, GiaNhap, GiaBan, SoLuong);

                    // Tính toán vị trí ngang của nút để đặt nút ở giữa panel
                    int horizontalLocation123 = (panel_tru.Width - btn_tru.Width) / 2;

                    // Đặt vị trí của nút bằng cách tính toán vị trí dọc và ngang
                    btn_tru.Location = new Point(horizontalLocation123, 105);

                    btn_tru.Update();

                    Guna2GradientPanel panel_sluong = new Guna2GradientPanel();
                    panel_sluong.Dock = DockStyle.Left;
                    panel_sluong.FillColor = Color.FromArgb(240, 240, 240);
                    panel_sluong.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_sluong.Width = 166;
                    panel_sluong.Update();


                    Guna2TextBox txt_sluong = new Guna2TextBox();
                    txt_sluong.Text = SoLuong;
                    txt_sluong.Font = new Font("SVN-Cintra", 10, FontStyle.Regular);
                    txt_sluong.TextAlign = HorizontalAlignment.Center;
                    txt_sluong.Size = new System.Drawing.Size(50, 50);
                    // Tính toán vị trí ngang của nút để đặt nút ở giữa panel
                    int horizontalLocation1234 = (panel_sluong.Width - txt_sluong.Width) / 2;

                    // Đặt vị trí của nút bằng cách tính toán vị trí dọc và ngang
                    txt_sluong.Location = new Point(horizontalLocation1234, 105);
                    txt_sluong.Update();


                    Guna2GradientPanel panel_cong = new Guna2GradientPanel();
                    panel_cong.Dock = DockStyle.Left;
                    panel_cong.FillColor = Color.FromArgb(240, 240, 240);
                    panel_cong.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_cong.Width = 166;
                    panel_cong.Update();


                    Guna2CircleButton btn_cong = new Guna2CircleButton();
                    //btn_cong.Text = "+";
                    btn_cong.Image = new Bitmap($@"C:\Users\ADMIN\Downloads\minus.png");
                    btn_cong.ImageAlign = HorizontalAlignment.Center;
                    btn_cong.ImageSize = new System.Drawing.Size(40, 40);
                    btn_cong.Font = new Font("SVN-Cintra", 20, FontStyle.Regular);
                    btn_cong.Size = new System.Drawing.Size(50, 50);
                    btn_cong.FillColor = Color.Transparent;
                    btn_cong.BackColor = Color.Transparent;

                    // Tính toán vị trí ngang của nút để đặt nút ở giữa panel
                    int horizontalLocation111 = (panel_cong.Width - btn_cong.Width) / 2;

                    // Đặt vị trí của nút bằng cách tính toán vị trí dọc và ngang
                    btn_cong.Location = new Point(horizontalLocation111, 105);
                    btn_cong.Click += (s, e) => DatHangClick2(s, e, MaSanPham);
                    btn_cong.Update();

                    panel_cong.Controls.Add(btn_cong);

                    panel_soluong.Controls.Add(panel_cong);

                    panel_sluong.Controls.Add(txt_sluong);

                    panel_soluong.Controls.Add(panel_sluong);

                    panel_tru.Controls.Add(btn_tru);

                    panel_soluong.Controls.Add(panel_tru);

                    panel_fmaten1.Controls.Add(label_maten1);

                    panel_content.Controls.Add(panel_fmaten1);

                    panel_fmaten.Controls.Add(label_maten);

                    panel_content.Controls.Add(panel_fmaten);

                    panel_img.Controls.Add(hinhanh);

                    panel_Father.Controls.Add(panel_soluong);
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
        string masp = "";
        string tensp = "";
        string hinhanh = "";
        string gianhap = "";
        string giaban = "";
        string soluong = "";
        private void DatHangClick(object sender, EventArgs e, string maSanPham, string tenSanPham, string hinhanhSanPham, string gianhapSanPham, string giabanSanPham, string soluongSanPham)
        {

            masp = maSanPham;
            tensp = tenSanPham;
            hinhanh = hinhanhSanPham;
            gianhap = gianhapSanPham;
            giaban = giabanSanPham;
            soluong = soluongSanPham;

            FormQuanLyDanhGia form = new FormQuanLyDanhGia(masp, tensp, hinhanh, gianhap, giaban, soluong, this.taikhoan);
            form.ShowDialog();
        }
        private void DatHangClick2(object sender, EventArgs e, string maSanPham)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN XÓA SẢN PHẨM NÀY MỤC YÊU THÍCH CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                yeuthich.Ma_Khach_Hang = maKH;
                yeuthich.Ma_SP = maSanPham;

                string get = TKBLL.Delete_YeuThich(yeuthich);

                switch (get)
                {
                    default:
                        {
                            MessageBox.Show("XÓA THÀNH CÔNG SẢN PHẨM YÊU THÍCH!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Load_DesignGioHang();
                            break;
                        }
                }
            }
            else if (result == DialogResult.No)
            {
                return;
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
