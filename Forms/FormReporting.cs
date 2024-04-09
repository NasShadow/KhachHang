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
    public partial class FormReporting : Form
    {
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        public string email_khachhang { get; set; }
        public FormReporting(string tkhoan)
        {
            InitializeComponent();
            this.email_khachhang = tkhoan;
        }

        private void FormReporting_Load(object sender, EventArgs e)
        {
            TaoMa();
            LoadGDDonHang();
        }
        string maKH = "";

        private void TaoMa()
        {
            try
            {
                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT Ma_Khach_Hang FROM dbo.KhachHang WHERE Email = N'{this.email_khachhang}' OR Ten_TK = N'{this.email_khachhang}'", conn);
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


        //Tạo giao diện voucher của bạn
        private void LoadGDDonHang()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.Voucher WHERE  Ma_Khach_Hang = N'{maKH}' AND Tinh_Trang IS NULL", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaVoucher = reader["Ma_Voucher"].ToString();
                    string GiaTri = reader["Gia_Tri"].ToString();
                    string TinhTrang = reader["Tinh_Trang"].ToString();
                    string NgayHetHan = reader["Ngay_Het_Han"].ToString();
                    string MaKhachHang = reader["Ma_Khach_Hang"].ToString();

                    string check = "";
                    if (TinhTrang == "")
                    {
                        check = "CÓ THỂ SỬ DỤNG";
                    }
                    else if (TinhTrang == "True")
                    {
                        check = "ĐÃ SỬ DỤNG";
                    }
                    else if (TinhTrang == "False")
                    {
                        check = "ĐÃ HẾT HẠN";
                    }

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.FromArgb(240, 240, 240);
                    panel_Father.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_Father.Size = new System.Drawing.Size(1500, 250);
                    panel_Father.BorderRadius = 20;
                    panel_Father.Update();

                    Guna2GradientPanel panel_img = new Guna2GradientPanel();
                    panel_img.Dock = DockStyle.Left;
                    panel_img.FillColor = Color.FromArgb(240, 240, 240);
                    panel_img.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_img.Size = new System.Drawing.Size(300, 300);
                    panel_img.Padding = new Padding(10);
                    panel_img.Update();

                    Guna2PictureBox hinhanh = new Guna2PictureBox();
                    hinhanh.Image = new Bitmap($@"C:\Users\ADMIN\Downloads\pngwing.com.png");
                    hinhanh.Dock = DockStyle.Fill;
                    hinhanh.BackColor = Color.Transparent;
                    hinhanh.BorderRadius = 10;
                    hinhanh.SizeMode = PictureBoxSizeMode.Zoom;
                    hinhanh.Update();

                    Guna2GradientPanel panel_content = new Guna2GradientPanel();
                    panel_content.Dock = DockStyle.Left;
                    panel_content.FillColor = Color.Red;
                    panel_content.FillColor2 = Color.Red;
                    //panel_content.Size = new System.Drawing.Size(500, 100);
                    panel_content.Width = 1000;
                    panel_content.Update();


                    Guna2GradientPanel panel_fmaten = new Guna2GradientPanel();
                    panel_fmaten.Dock = DockStyle.Top;
                    panel_fmaten.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten.Height = 125;
                    panel_fmaten.Update();

                    Guna2HtmlLabel label_maten = new Guna2HtmlLabel();
                    label_maten.Text = $"VOUCHER GIẢM GIÁ: {GiaTri}%" + " | " + "Tình Trạng: " + check;
                    label_maten.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten.BackColor = Color.Transparent;
                    int verticalLocation = (panel_fmaten.Height - label_maten.Height) / 2;
                    label_maten.Location = new Point(0, verticalLocation);
                    label_maten.ForeColor = Color.FromArgb(0, 128, 0);
                    label_maten.Update();

                    Guna2GradientPanel panel_fmaten1 = new Guna2GradientPanel();
                    panel_fmaten1.Dock = DockStyle.Bottom;
                    panel_fmaten1.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten1.Height = 125;
                    panel_fmaten1.Update();

                    Guna2HtmlLabel label_maten1 = new Guna2HtmlLabel();
                    label_maten1.Text = "NGÀY HẾT HẠN: " + NgayHetHan;
                    label_maten1.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten1.BackColor = Color.Transparent;
                    int verticalLocation1 = (panel_fmaten1.Height - label_maten1.Height) / 2;
                    label_maten1.Location = new Point(0, verticalLocation1);
                    label_maten1.ForeColor = Color.FromArgb(255, 0, 0);
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
