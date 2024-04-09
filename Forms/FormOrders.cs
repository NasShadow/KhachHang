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
using System.IO;
using System.Windows.Forms;
using DTO;

namespace Login.Forms
{
    public partial class FormOrders : Form
    {
        ChiTietGioHang chitietgiohang = new ChiTietGioHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        public string email { get; set; }
        public FormOrders(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            DeleteSP();
            LoadTheme();

            //Lấy giá trị mã giỏ hàng
            LayGtriMaGH();

            sum_money();

            //Load Giao Diện
            Load_DesignGioHang();
        }

        static string maGH = "";
        static string maKH = "";


        private void LayGtriMaGH()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_gio_hang, KhachHang.Ma_Khach_Hang FROM dbo.GioHang JOIN dbo.KhachHang ON KhachHang.Ma_Khach_Hang = GioHang.Ma_Khach_Hang\r\nWHERE Ten_TK = N'{this.email}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    maGH = "";
                    maKH = "";
                    maGH += reader.GetString(0);
                    maKH += reader.GetString(1);
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

        double giaban = 0;
        string giaban2 = ""; 
        private void sum_money()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT TOP 1 SUM(dbo.SanPham.Gia_ban * dbo.ChiTietGioHang.So_luong) OVER() AS Tong_thanh_tien, dbo.ChiTietGioHang.Ma_SP, dbo.ChiTietGioHang.So_luong, dbo.SanPham.Ten_SP, dbo.SanPham.Gia_ban, dbo.SanPham.FileNames \r\nFROM dbo.ChiTietGioHang \r\nJOIN dbo.SanPham ON SanPham.Ma_SP = ChiTietGioHang.Ma_SP \r\nWHERE Ma_gio_hang = N'{maGH}'", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        giaban = 0;
                        giaban2 = "";
                        giaban = reader.GetDouble(0);
                        giaban2 = giaban.ToString();
                        lbl_sumtien.Text = "TỔNG TIỀN PHẢI TRẢ: " + giaban.ToString("C0");
                        return;
                    }
                }
                else if (reader.HasRows == false)
                {
                    giaban = 0;
                    giaban2 = "";
                    lbl_sumtien.Text = "TỔNG TIỀN PHẢI TRẢ: " + giaban.ToString("C0");
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

        //Tạo giao diện giỏ hàng
        private void Load_DesignGioHang()
        {

            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.ChiTietGioHang.Ma_SP, dbo.ChiTietGioHang.So_luong, dbo.SanPham.Ten_SP, dbo.SanPham.Gia_ban, dbo.SanPham.FileNames, dbo.SanPham.So_luong N'SLSP' FROM dbo.ChiTietGioHang \r\n\tJOIN dbo.SanPham ON SanPham.Ma_SP = ChiTietGioHang.Ma_SP\r\n\tWHERE Ma_gio_hang = N'{maGH}'", conn);
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
                    btn_thanhtoan.Enabled = true;

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.Gray;
                    panel_Father.FillColor2 = Color.Gray;
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
                    panel_soluong.Dock = DockStyle.Left;
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
                    btn_tru.Image = new Bitmap($@"C:\Users\ADMIN\Desktop\DuAnMau (1)\HinhAnh\minus.png");
                    btn_tru.ImageSize = new System.Drawing.Size(40, 40);
                    btn_tru.ImageAlign = HorizontalAlignment.Center;
                    btn_tru.Font = new Font("SVN-Cintra", 20, FontStyle.Regular);
                    btn_tru.Size = new System.Drawing.Size(50, 50);
                    //btn_tru.FillColor = Color.Black;
                    btn_tru.FillColor = Color.Transparent;
                    btn_tru.BackColor = Color.Transparent;

                    // Tính toán vị trí ngang của nút để đặt nút ở giữa panel
                    int horizontalLocation123 = (panel_tru.Width - btn_tru.Width) / 2;

                    // Đặt vị trí của nút bằng cách tính toán vị trí dọc và ngang
                    btn_tru.Location = new Point(horizontalLocation123, 105);

                    btn_tru.Click += (s, e) => DatHangClick(s, e, MaSanPham, SoLuong); ;
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
                    txt_sluong.KeyPress += new KeyPressEventHandler(textBox1_KeyPress);
                    txt_sluong.Update();



                    Guna2GradientPanel panel_cong = new Guna2GradientPanel();
                    panel_cong.Dock = DockStyle.Left;
                    panel_cong.FillColor = Color.FromArgb(240, 240, 240);
                    panel_cong.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_cong.Width = 166;
                    panel_cong.Update();


                    Guna2CircleButton btn_cong = new Guna2CircleButton();
                    //btn_cong.Text = "+";
                    btn_cong.Image = new Bitmap($@"C:\Users\ADMIN\Desktop\DuAnMau (1)\HinhAnh\plus.png");
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
                    //btn_cong.Click += CongClickHandler;
                    btn_cong.Click += (s, e) => DatHangClick1(s, e, MaSanPham, SoLuong, SoLuongSP); ;
                    btn_cong.Update();

                    Guna2GradientPanel panel_x = new Guna2GradientPanel();
                    panel_x.Dock = DockStyle.Fill;
                    panel_x.FillColor = Color.White;
                    panel_x.FillColor2 = Color.White;
                    panel_x.Update();


                    Guna2Button btn_x = new Guna2Button();
                    //btn_x.Text = "X";
                    btn_x.Image = new Bitmap($@"C:\Users\ADMIN\Desktop\DuAnMau (1)\HinhAnh\delete-button (1).png");
                    btn_x.ImageAlign = HorizontalAlignment.Center;
                    btn_x.ImageSize = new System.Drawing.Size(40, 40);
                    btn_x.Click += (s, e) => DatHangClick2(s, e, MaSanPham, SoLuong); 
                    // Đường dẫn tới file hình ảnh
                    /*string imagePath = @"C:\Users\Admin\Downloads\delete-button(1).png";

                    // Kiểm tra xem file hình ảnh có tồn tại không trước khi thêm vào Button
                    if (File.Exists(imagePath))
                    {
                        // Tạo một đối tượng Image từ file hình ảnh
                        Image image = Image.FromFile(imagePath);

                        // Đặt hình ảnh cho Button
                        btn_x.Image = image;
                    }
                    else
                    {
                        // Xử lý trường hợp file không tồn tại
                        MessageBox.Show("File hình ảnh không tồn tại!");
                    }*/
                    btn_x.Size = new System.Drawing.Size(50, 50);
                    btn_x.FillColor = Color.Transparent;
                    btn_x.BackColor = Color.Transparent;
                    btn_x.Location = new Point(95, 105);
                    btn_x.Update();


                    panel_x.Controls.Add(btn_x);

                    panel_cong.Controls.Add(btn_cong);

                    panel_soluong.Controls.Add(panel_cong);

                    panel_sluong.Controls.Add(txt_sluong);

                    panel_soluong.Controls.Add(panel_sluong);

                    panel_tru.Controls.Add(btn_tru);

                    panel_soluong.Controls.Add(panel_tru);

                    panel_fmaten1.Controls.Add(label_maten1 );

                    panel_content.Controls.Add(panel_fmaten1);

                    panel_fmaten.Controls.Add(label_maten);

                    panel_content.Controls.Add(panel_fmaten);

                    panel_img.Controls.Add(hinhanh);

                    panel_Father.Controls.Add(panel_x);
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


        //Nút ấn giảm số lượng
        private void DatHangClick(object sender, EventArgs e, string maSanPham, string soLuong)
        {
            chitietgiohang.Ma_gio_hang = maGH;
            chitietgiohang.Ma_SP = maSanPham;

            int count = int.Parse(soLuong);
            if (count == 1)
            {
                return;
            }
            count--;
            chitietgiohang.So_luong = count.ToString();

            string get = TKBLL.Update_SLGioHang(chitietgiohang);
            switch (get)
            {
                default:
                    {
                        //MessageBox.Show("Giảm SL Thành Công");
                        break;
                    }
            }
            sum_money();
            Load_DesignGioHang();
        }

        //Nút ấn thêm số lượng
        private void DatHangClick1(object sender, EventArgs e, string maSanPham, string soLuong, string soLuongSP)
        {
            chitietgiohang.Ma_gio_hang = maGH;
            chitietgiohang.Ma_SP = maSanPham;

            int count = int.Parse(soLuong);
            int sl = int.Parse(soLuongSP);
            if (sl == count)
            {
                return;
            }
            count++;
            chitietgiohang.So_luong = count.ToString();

            string get = TKBLL.Update_SLGioHang(chitietgiohang);
            switch (get)
            {
                default:
                    {
                        //MessageBox.Show("Tăng SL Thành Công");
                        break;
                    }
            }
            sum_money();
            Load_DesignGioHang();
        }

        //Nút ấn xóa sản phẩm khỏi giỏ hàng
        private void DatHangClick2(object sender, EventArgs e, string maSanPham, string soLuong)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN XÓA SẢN PHẨM NÀY KHỎI GIỎ HÀNG CỦA MÌNH CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                chitietgiohang.Ma_gio_hang = maGH;
                chitietgiohang.Ma_SP = maSanPham;

                string get = TKBLL.Delete_SPGioHang(chitietgiohang);

                switch (get)
                {
                    default:
                        {
                            break;
                        }
                }
                lbl_sumtien.Text = "TỔNG TIỀN PHẢI TRẢ: ";
                DeleteSP();
                Load_DesignGioHang();
            }
            else if(result == DialogResult.No)
            {
                return;
            }
        }

        private void DeleteSP()
        {
            btn_thanhtoan.Enabled = false;
        }

        //Kiểm tra ký tự số
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải là số và không phải là ký tự điều khiển (như backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Không cho phép ký tự được nhập vào textbox
                e.Handled = true;
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

        private FormThanhToanDonHang childForm;

        private void btn_thanhtoan_Click(object sender, EventArgs e)
        {
            if (childForm == null || childForm.IsDisposed)
            {
                childForm = new FormThanhToanDonHang(this.email, maGH, maKH, giaban2);
                childForm.FormClosed += FormOrders_FormClosed;
                childForm.ShowDialog();
            }
            FormOrders_Load(null,null);
        }

        private void FormOrders_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Form đã đóng, nên cập nhật lại trạng thái của biến childFormClosed về true
            childForm = null;
        }

        public void ReloadParentForm()
        {
            Load_DesignGioHang();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_sumtien_Click(object sender, EventArgs e)
        {

        }
    }
}
