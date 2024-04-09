using BLL;
using DTO;
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

namespace Login
{
    public partial class FormAddDiaChi : Form
    {
        DiaChi diachi = new DiaChi();
        KhachHang khachhang = new KhachHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();
        public string maKH;
        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<DiaChi> DiaChi = dc.GetTable<DiaChi>();

        public string taikhoan { get; set; }
        public FormAddDiaChi(string maKH)
        {
            InitializeComponent();
            InitBrowser();
            this.maKH = maKH;
        }
        private async Task initizated()
        {
            await webView21.EnsureCoreWebView2Async(null);
        }
        public async void InitBrowser()
        {
            await initizated();
            webView21.CoreWebView2.Navigate("https://www.google.com/maps/@16.0757254,108.1596816,15z");
            //see the description for code and other details
        }
        private void webView21_Click(object sender, EventArgs e)
        {

        }

        private void webView21_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {
            string[] urls = webView21.Source.ToString().Split('/');
            string[] paramters;
            if (urls[urls.Length - 1].Contains("data"))
            {
                paramters = urls[urls.Length - 2].Split(',');
            }
            else
            {
                paramters = urls[urls.Length - 1].Split(',');
            }
            string s0 = paramters[0];
            txtLat.Text = s0.Substring(1).ToString();
            txtLon.Text = paramters[1];
        }

        private void btn_luu_Click(object sender, EventArgs e)
        {
            diachi.Dia_Chi = txt_diachi.Text;
            diachi.Ma_Khach_Hang = txt_makhachhang.Text;
            diachi.Lat = txtLat.Text;
            diachi.Lon = txtLon.Text;

            string get = TKBLL.CheckInsertDiaChi(diachi);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case "requeid_short":
                    {
                        MessageBox.Show("ĐỊA CHỈ CỦA BẠN KHÔNG CỤ THỂ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                case "thanhcong":
                    {
                        MessageBox.Show("THÊM ĐỊA CHỈ THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormAddDiaChi_Load(null, null);
                        return;
                    }
            }
            string get_Valid = TKBLL.CheckDiaChiValid(diachi);

            switch (get_Valid)
            {
                case "Tồn Tại!":
                    {
                        MessageBox.Show("ĐỊA CHỈ NÀY CỦA BẠN ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
            }
        }

        private void FormAddDiaChi_Load(object sender, EventArgs e)
        {
            txt_makhachhang.Text = this.maKH;
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
