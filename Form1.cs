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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Login
{
    public partial class Form1 : Form
    {
        private const int v = 1;
        KhachHang khachhang = new KhachHang();
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();


        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<KhachHang> KhachHang = dc.GetTable<KhachHang>();
        static Table<NhanVien> NhanVien = dc.GetTable<NhanVien>();
        static Table<NhanVienGiaoHang> NhanVienGiaoHang = dc.GetTable<NhanVienGiaoHang>();

        int pw;
        bool hided;

        public Form1()
        {
            InitializeComponent();
            pw = panel1.Width;
            hided = false;

            //Căn giữa màn hình
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        void IncreaseOpacity(object sender, EventArgs e)
        {
            if (this.Opacity <= 1) // Replace 0.88 with whatever you want
            {
                this.Opacity += 0.01; // Replace 0.01 with whatever you want
            }

            if (this.Opacity == 1) // Replace 0.88 with whatever you want
            {
                timer.Stop();
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = .01;
            timer.Interval = 10;
            timer.Tick += IncreaseOpacity;
            timer.Start();

            //Thêm giá trị vào combobox
            Combobox_Gtinh();

        }

        //COmbobox load giới tính
        private void Combobox_Gtinh()
        {
            cbogioitinh.Items.Add("Nam");
            cbogioitinh.Items.Add("Nữ");
        }



        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtEmail.Text == "")
                {
                    txtEmail.Text = "Enter email";
                    return;
                }
                txtEmail.ForeColor = Color.White;
                panel5.Visible = false;
            }
            catch { }


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                if (txtMatKhau.Text == "")
                {

                    return;
                }
                txtMatKhau.ForeColor = Color.White;
                txtMatKhau.PasswordChar = '*';
                panel7.Visible = false;
            }
            catch { }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            txtMatKhau.SelectAll();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txtEmail.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (txtEmail.Text == "Enter email") // hoặc viết điều kiện riêng
            {
                panel5.Visible = true;
                txtEmail.Focus();
                return;
            }

            if (txtMatKhau.Text == "Enter mật khẩu") // hoặc viết điều kiện riêng
            {
                panel7.Visible = true;
                txtMatKhau.Focus();
                return;

            }



            khachhang.Email = txtEmail.Text;
            khachhang.MatKhau = txtMatKhau.Text;
            khachhang.Ten_TK = txtEmail.Text;

            string getten = TKBLL.CheckInsertBll(khachhang);
            switch (getten)
            {
                case "Tài khoản mật khẩu đã sai!":
                    {
                        MessageBox.Show("TÀI KHOẢN HOẶC MẬT KHẨU ĐÃ SAI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "false":
                    {
                        MessageBox.Show("TÀI KHOẢN CỦA BẠN ĐÃ BỊ KHÓA!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("ĐĂNG NHẬP THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Hide();

                        string email_khachhang = khachhang.Email;

                        PanleMenu openMain = new PanleMenu(email_khachhang);
                        openMain.ShowDialog();
                        break;
                    }
            }

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.White;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);




        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_MouseDown(object sender, MouseEventArgs e)

        {
        

        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(30,30,30);
            button3.ForeColor = Color.White;
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        /*private void button3_Click_1(object sender, EventArgs e)
        {
            



        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlemail.Visible=pnltenkhachhang.Visible=pnlPassword.Visible=pnlxacnhan.Visible=false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
                 
                txtEmail1.ForeColor = Color.White;
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (txtMatKhau1.Text == "Enter mật khẩu") // trong từng cái 
            {
                pnlmatkhau.Visible = true;
                txtMatKhau1.Focus();
                return;
            }
            txtMatKhau1.ForeColor = Color.White;
            txtMatKhau1.ForeColor = Color.White;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (txtXacNhan.Text == "Enter xác nhận mật khẩu") // trong từng cái 
            {
                pnlxacnhan.Visible = true;
                txtXacNhan.Focus();
                return;
            }
            txtXacNhan.ForeColor = Color.White;
            txtXacNhan.ForeColor = Color.White;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
            button3.BackColor = Color.FromArgb(255, 128, 128);
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock = DockStyle.Fill;
            pnlsignup.Visible = false;
            pnlLogo.Dock = DockStyle.Left;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlsignup.Visible = true;
            pnlLogo.Dock = DockStyle.Right;
            pnlsignup.Dock = DockStyle.Fill;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            this.Opacity = 0.5; 
        }

        private void From1_ResizeBegin(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void From1_ResizeEnd(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txttentaikhoan_TextChanged(object sender, EventArgs e)
        {
            if (txttentaikhoan.Text == "Enter tên tài khoản") // trong từng cái 
            {
                pnltentaikhoan.Visible = true;
                txttentaikhoan.Focus();
                return;
            }
            txttentaikhoan.ForeColor = Color.White;
        }

        private void txtsdt_TextChanged(object sender, EventArgs e)
        {
            if (txtsdt.Text == "Enter số điện thoại") // trong từng cái 
            {
                pnlsdt.Visible = true;
                txtsdt.Focus();
                return;
            }
            txtsdt.ForeColor = Color.White;
        }
        private void txtEmail1_TextChaged(object sender, EventArgs e)
        {
            if (txtEmail1.Text == "Enter email") // trong từng cái 
            {
                pnlemail.Visible = true;
                txtEmail1.Focus();
                return;
            }
            txtsdt.ForeColor = Color.White;
        }
        private void cbogioitinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbogioitinh.Text == "Giới Tính")
            {
                pnlgioitinh.Visible= true;
                cbogioitinh.Focus();
                return;
            } 
                
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttentaikhoan_TextChanged_1(object sender, EventArgs e)
        {
            if (txttentaikhoan.Text == "Enter tên tài khoản") // trong từng cái 
            {
                pnltentaikhoan.Visible = true;
                txttentaikhoan.Focus();
                return;
            }
            txttentaikhoan.ForeColor = Color.White;
        }

        private void panel32_Paint(object sender, PaintEventArgs e)
        {

        }
        //Combobox
        private void cbogioitinh_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        //Quay lại
        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock = DockStyle.Fill;
            pnlsignup.Visible = false;
            pnlLogo.Dock = DockStyle.Left;
        }

        private void button3_Click_2(object sender, EventArgs e)
        {

            khachhang.Ten_Khach_Hang = txttenkhachhang.Text;
            khachhang.Email = txtEmail1.Text;
            khachhang.Ten_TK = txttentaikhoan.Text;
            khachhang.MatKhau = txtMatKhau1.Text;
            khachhang.XacNhanMatKhau = txtXacNhan.Text;
            khachhang.DienThoai = txtsdt.Text;

            khachhang.Gioi_Tinh = cbogioitinh.Text;

            if (cbogioitinh.Text == "Nam")
            {
                khachhang.Gioi_Tinh = "1";
            }
            else if (cbogioitinh.Text == "Nữ")
            {
                khachhang.Gioi_Tinh = "0";
            }

            khachhang.TrangThai = "1";


            if (khachhang.Ten_Khach_Hang == "" || khachhang.Ten_TK == "" || khachhang.Email == "" || khachhang.MatKhau == "" || khachhang.XacNhanMatKhau == "" || khachhang.Gioi_Tinh == "" || khachhang.DienThoai == "" || khachhang.TrangThai == "")
            {
                MessageBox.Show("VUI LÒNG NHẬP ĐẦY ĐỦ THÔNG TIN!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            else if (!Regex.IsMatch(khachhang.Email, @"[a-z0-9_.-]{2,64}@gmail.com"))
            {
                MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (Regex.IsMatch(khachhang.Ten_TK, @"[!@#$%^&*()<>/|}{~:]"))
            {
                MessageBox.Show("TÊN TÀI KHOẢN SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!Regex.IsMatch(khachhang.MatKhau, @"[!@#$%^&*()<>/|}{~:]"))
            {
                MessageBox.Show("MẬT KHẨU SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (khachhang.XacNhanMatKhau != khachhang.MatKhau)
            {
                MessageBox.Show("XÁC NHẬN MẬT KHẨU SAI !!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (!Regex.IsMatch(khachhang.DienThoai, @"^(?:\+84|0)(\d{9,10})$"))
            {
                MessageBox.Show("SỐ ĐIỆN THOẠI SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Check tên tài khoản
            var check_taikhoan = from tentk in KhachHang
                                 select tentk.Ten_TK;


            foreach (var s in check_taikhoan)
            {
                if (khachhang.Ten_TK == s)
                {
                    MessageBox.Show("TÊN TÀI KHOẢN ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //Check email
            var check_email = from email in KhachHang
                                 select email.Email;


            foreach (var s in check_email)
            {
                if (khachhang.Email == s)
                {
                    MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var check_email2 = from email in NhanVien
                              select email.Email;


            foreach (var s in check_email)
            {
                if (khachhang.Email == s)
                {
                    MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            var check_email3 = from email in NhanVienGiaoHang
                               select email.Email;


            foreach (var s in check_email)
            {
                if (khachhang.Email == s)
                {
                    MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (khachhang.Ten_Khach_Hang != "" || khachhang.Ten_TK != "" || khachhang.Email != "" || khachhang.MatKhau != "" || khachhang.XacNhanMatKhau != "" || khachhang.Gioi_Tinh != "" || khachhang.DienThoai != "" || khachhang.TrangThai != "")
            {
                string tentk = txttentaikhoan.Text;
                string tenkhachhang = txttenkhachhang.Text;
                string email = txtEmail1.Text;
                string matkhau = txtMatKhau1.Text;
                string xacnhanmatkhau = txtXacNhan.Text;
                string gioitinh = cbogioitinh.Text;
                string dienthoai = txtsdt.Text;
                string trangthai = "1";

                FormOTPEmail OTP = new FormOTPEmail(tentk, tenkhachhang, email, matkhau, xacnhanmatkhau, gioitinh, dienthoai, trangthai);
                OTP.ShowDialog();
                return;
            }

/*
            string get = TKBLL.CheckInsertDangKy(khachhang);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("TÀI KHOẢN KHÔNG ĐƯỢC ĐỂ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                case "requeid_Email":
                    {
                        MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }

                case "requeid_Ten_Tk":
                    {
                        MessageBox.Show("TÊN TÀI KHOẢN SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_MatKhau":
                    {
                        MessageBox.Show("MẬT KHẨU SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_Xacnhanmatkhau":
                    {
                        MessageBox.Show("XÁC NHẬN MẬT KHẨU SAI !!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_DienThoai":
                    {
                        MessageBox.Show("SỐ ĐIỆN THOẠI SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                
                default:
                    {
                        MessageBox.Show("ĐĂNG KÝ THÀNH CÔNG");
                        PanleMenu openMain = new PanleMenu();
                        openMain.ShowDialog();
                        break;
                    }
            }*/



            if (txtEmail1.Text == "Enter email")
            {
                pnlemail.Visible = true;
                txtEmail1.Focus();
                return;
            }
            if (txttenkhachhang.Text == "Enter tên khách hàng")  // trong button
            {
                pnltenkhachhang.Visible = true;
                txttenkhachhang.Focus();
                return;
            }
            if (txtMatKhau1.Text == "Enter mật khẩu")
            {
                pnlPassword.Visible = true;
                txtMatKhau1.Focus();
                txtMatKhau1.SelectAll();
                return;
            }
            if (txtXacNhan.Text == "Enter xác nhận mật khẩu")
            {
                pnlxacnhan.Visible = true;
                txtXacNhan.Focus();
                txtXacNhan.SelectAll();
                return;
            }
            if (txttentaikhoan.Text == "Enter tên tài khoản")  // trong button
            {
                pnltentaikhoan.Visible = true;
                txttentaikhoan.Focus();
                return;
            }
            if (txtsdt.Text == "Enter số điện thoại")  // trong button
            {
                pnlsdt.Visible = true;
                txtsdt.Focus();
                return;
            }
            if (cbogioitinh.Text == "Giới Tính")  // trong button
            {
                pnlgioitinh.Visible = true;
                cbogioitinh.Focus();
                return;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormTimMatKhau form = new FormTimMatKhau();
            form.ShowDialog();
        }
    }
}
