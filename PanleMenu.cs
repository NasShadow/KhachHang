using BLL;
using DTO;
using Guna.UI2.WinForms;
using Login.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class PanleMenu : Form
    {
        Voucher voucher = new Voucher();
        KhachHang khachhang = new KhachHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public string email_khachhang { get; set; }


        //public string email_khachhang { get; set; } 
        public PanleMenu(string email)
        {
            InitializeComponent();
            random = new Random();
            btnCloseChildForm.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;

            //Căn giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;


            //this.email_khachhang = email;
            lbl_addressemail.Text = email;
            this.email_khachhang = email;
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private Color SelectThemColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }
        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {

                    DisableButton();
                    Color color = SelectThemColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    /*      panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);*/
                    ThemeColor.PrimaryColor = color;
                    /*     ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);*/
                    btnCloseChildForm.Visible = true;

                }
            }
        }
        private void LoadGDDonHang()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.Voucher WHERE Ma_Khach_Hang IS NULL AND Ngay_Nhan_Voucher IS NULL AND Tinh_Trang IS NULL", conn);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    // Lấy giá trị từ cột "Ma_loai_hang"
                    string MaVoucher = reader["Ma_Voucher"].ToString();
                    string GiaTri = reader["Gia_Tri"].ToString();
                    string TinhTrang = reader["Tinh_Trang"].ToString();
                    string NgayHetHan = reader["Ngay_Het_Han"].ToString();
                    string MaKhachHang = reader["Ma_Khach_Hang"].ToString();

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.Gray;
                    panel_Father.FillColor2 = Color.Gray;
                    panel_Father.Size = new System.Drawing.Size(1665, 250);
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
                    panel_content.Width = 680;
                    panel_content.Update();


                    Guna2GradientPanel panel_fmaten = new Guna2GradientPanel();
                    panel_fmaten.Dock = DockStyle.Top;
                    panel_fmaten.FillColor = Color.FromArgb(240, 240, 240);
                    panel_fmaten.FillColor2 = Color.FromArgb(240, 240, 240);
                    panel_fmaten.Height = 125;
                    panel_fmaten.Update();

                    Guna2HtmlLabel label_maten = new Guna2HtmlLabel();
                    label_maten.Text = $"VOUCHER GIẢM GIÁ: {GiaTri}%";
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
                    label_maten1.Text = "NGÀY HẾT HẠN: " + NgayHetHan;
                    label_maten1.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    label_maten1.BackColor = Color.Transparent;
                    int verticalLocation1 = (panel_fmaten1.Height - label_maten1.Height) / 2;
                    label_maten1.Location = new Point(0, verticalLocation1);
                    label_maten1.Update();

                    Guna2GradientPanel panel_content1 = new Guna2GradientPanel();
                    panel_content1.Dock = DockStyle.Fill;
                    panel_content1.FillColor = Color.FromArgb(240, 240, 240);
                    panel_content1.FillColor2 = Color.FromArgb(240, 240, 240);
                    //panel_content.Size = new System.Drawing.Size(500, 100);
                    panel_content1.Update();

                    Guna2Button btn_xem = new Guna2Button();
                    btn_xem.Text = "Nhận";
                    btn_xem.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    btn_xem.Height = 80;

                    // Căn giữa theo chiều dọc
                    btn_xem.Location = new Point(450, 90);
                    btn_xem.FillColor = Color.FromArgb(64, 64, 64);
                    btn_xem.Click += (s, e) => DatHangClick(s, e, MaVoucher);
                    btn_xem.Update();

                    panel_content1.Controls.Add(btn_xem);

                    panel_fmaten1.Controls.Add(label_maten1);

                    panel_content.Controls.Add(panel_fmaten1);

                    panel_fmaten.Controls.Add(label_maten);

                    panel_content.Controls.Add(panel_fmaten);

                    panel_img.Controls.Add(hinhanh);
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
        string maKH = "";
        static Db db = new Db();
        static DataContext dc = new DataContext(db.strConnection);
        static Table<Voucher> Voucher = dc.GetTable<Voucher>();
        private void DatHangClick(object sender, EventArgs e, string mavoucher)
        {
            DialogResult result = MessageBox.Show("BẠN CÓ MUỐN NHẬN VOUCHER NÀY CHỨ???", "THÔNG BÁO!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                voucher.Ma_Voucher = mavoucher;
                voucher.Ma_Khach_Hang = maKH;

                var result_Check_1 = from makhachhang in Voucher
                                     select makhachhang.Ma_Khach_Hang;

                // > 1 lần nhận voucher trong tháng
                foreach (var item in result_Check_1)
                {
                    if (maKH == item)
                    {
                        MessageBox.Show("Lần hai mua");
                        //Kiểm tra xem đã đủ 5 ngày hay chưa
                        string get_Check = TKBLL.Days_InertVoucher(voucher);
                        switch (get_Check)
                        {
                            case "TonTai":
                                {
                                    MessageBox.Show("SAU 5 NGÀY NỮA BẠN CÓ THỂ NHẬN VOUCHER!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                        }

                        //Nhận voucher thành công
                        string get_1 = TKBLL.Inser_Voucher(voucher);

                        switch (get_1)
                        {

                            default:
                                {
                                    MessageBox.Show("NHẬN VOUCHER THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadGDDonHang();
                                    break;
                                }
                        }
                        return;
                    }
                }

                //Lần đầu nhận voucher trong tháng
                //Nhận voucher thành công
                string get = TKBLL.Inser_Voucher(voucher);

                switch (get)
                {

                    default:
                        {
                            MessageBox.Show("NHẬN VOUCHER THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void DisableButton()
        {
            foreach (Control previousBtn in panel1.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(30, 30, 30);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();


        }

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

        private void btnProducts_Click(object sender, EventArgs e)
        {
            string tkhoan = lbl_addressemail.Text;
            OpenChildForm(new Forms.FormProduct(tkhoan), sender);
            lblTitle.Text = "SẢN PHẨM";
        }

       
        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "TRANG CHỦ";
            panelTitleBar.BackColor = Color.FromArgb(0, 150, 136);
            panelLogo.BackColor = Color.FromArgb(39, 39, 58);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }
       
        private void PanelMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
      
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
       

        private void PanleMenu_Load(object sender, EventArgs e)
        {
            TaoMa();
            LoadGDDonHang();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string tkhoan = lbl_addressemail.Text;
            OpenChildForm(new Forms.FormOrders(tkhoan), sender);
            lblTitle.Text = "GIỎ HÀNG";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tkhoan = lbl_addressemail.Text;
            OpenChildForm(new Forms.FormCustomers(tkhoan), sender);
            lblTitle.Text = "SẢN PHẨM YÊU THÍCH";


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string tkhoan = lbl_addressemail.Text;
            OpenChildForm(new Forms.FormReporting(tkhoan), sender);
            lblTitle.Text = "VOUCHER";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("BẠN CÓ MUỐN THOÁT CHƯƠNG TRÌNH???","THÔNG BÁO!!!",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (Result == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                return;
            }
/*            OpenChildForm(new Forms.FormNotifications(), sender);
*/        }

        private void button5_Click(object sender, EventArgs e)
        {
            string tkhoan = lbl_addressemail.Text;
            // Tạo một instance của Form2
            OpenChildForm(new Forms.FormSetting(tkhoan), sender);
            lblTitle.Text = "CÀI ĐẶT ";
        }

        private void btnCloseChildForm_Click_2(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();
            Reset();
            LoadGDDonHang();
        }

        private void panelTitleBar_MouseDown_1(object sender, MouseEventArgs e)
        {

            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)

                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lbl_addressemail_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
