using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login
{
    public partial class FormTimMatKhau : Form
    {
        public FormTimMatKhau()
        {
            InitializeComponent();

            txt_mk.Enabled = false;
        }
        TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        KhachHang khachhang = new KhachHang();
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Tìm Kiếm mật khẩu
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            khachhang.Email = txt_email.Text;
            khachhang.Ten_TK = txt_email.Text;
            string get = TKBLL.Find_PassAdmin(khachhang);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("VUI LÒNG NHẬP EMAIL!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "Email của bạn không tồn tại!":
                    {
                        MessageBox.Show("EMAIL KHÔNG TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("TÌM KIẾM THÀNH CÔNG", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txt_mk.Text = TKBLL.Find_PassAdmin(khachhang);
                        break;
                    }
            }
        }
    }
}
