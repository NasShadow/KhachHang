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
    public partial class FormSetting : Form
    {
        DiaChi diachi = new DiaChi();
        KhachHang khachhang = new KhachHang();
        static TaiKhoanBLL TKBLL = new TaiKhoanBLL();
        //Kết nối
        static SqlConnection conn = TKBLL.Load();

        //Lấy gtri trong Table
        static Db db = new Db();

        //static string strConnectionInfo = db.strConnection;
        static DataContext dc = new DataContext(db.strConnection);

        //Table
        static Table<DiaChi> DiaChi = dc.GetTable<DiaChi>();

        public string taikhoan { get;set; }
        public FormSetting(string tk)
        {
            InitializeComponent();
            this.taikhoan = tk;
        }
        static string email = "";

        //Hàm Load ComboBox
        private void Load_ComboBox()
        {
            cbo_gioitinh.Items.Clear();
            cbo_gioitinh.Items.Add("Nam");
            cbo_gioitinh.Items.Add("Nữ");
        }

        //Hàm Load ComboBox
        private void Load_ComboBox2()
        {

            cbo_diachi.Items.Clear();

            using (SqlConnection conn = TKBLL.Load())
            {
                conn.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.DiaChi WHERE Ma_Khach_Hang = N'{txt_makhachhang.Text}'", conn);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Lấy giá trị từ cột "Ma_loai_hang"
                        string Diachi = reader["Dia_Chi"].ToString();
                        cbo_diachi.Items.Add(Diachi);
                    }
                }
                conn.Close();
            }
        }


        private void Load_Never_Input()
        {
            txt_tentaikhoan.Enabled = false;
            lbl_tentaikhoan.Enabled = false;
            txt_trangthai.Enabled = false;
            txt_filenames.Enabled = false;
            txt_makhachhang.Enabled = false;

            txt_diachi.Text = "";
        }

        //Form load
        private void FormSetting_Load(object sender, EventArgs e)
        {
            LoadTheme();
            //Tạo Mã KH
            TaoMa();

            //Hiển thị thông tin đơn hàng
            LoadGDDonHang();

            //Load button donhang
            Button_Change();
            //Load ComboBox
            Load_ComboBox();

            //Load ra dữ liệu
            Load_Data();

            //Khi có dữ liệu
            Load_ComboBox2();

            //Load Dữ liệu k được phép chỉnh sửa
            Load_Never_Input();

            //Load Địa chỉ khách hàng
            Load_DChiKhachHang();
            email = txt_email.Text;
        }
        //Nút lưu => cập nhật lại tt
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            khachhang.Ma_Khach_Hang = txt_makhachhang.Text;
            khachhang.Ten_TK = txt_tentaikhoan.Text;
            khachhang.Ten_Khach_Hang = txt_tenkhachhang.Text;
            khachhang.Email = txt_email.Text;
            khachhang.Gioi_Tinh = cbo_gioitinh.Text;

            if (cbo_gioitinh.Text == "Nam")
            {
                khachhang.Gioi_Tinh = "1";
            }
            else if (cbo_gioitinh.Text == "Nữ")
            {
                khachhang.Gioi_Tinh = "0";
            }

            khachhang.DienThoai = txt_sdt.Text;
            khachhang.TrangThai = txt_trangthai.Text;
            khachhang.FileNames = txt_filenames.Text;

            if (khachhang.Email != email)
            {
                string get = TKBLL.CheckUpdatetKhachHang(khachhang);

                switch (get)
                {
                    case "requeid_botrong":
                        {
                            MessageBox.Show("KHÔNG ĐƯỢC ĐỂ TRỐNG BẤT KỲ THÔNG TIN NÀO!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                    case "requeid_Email":
                        {
                            MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_trungemail":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_trungemail2":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_trungemail3":
                        {
                            MessageBox.Show("EMAIL ĐÃ TỒN TẠI!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "requeid_DienThoai":
                        {
                            MessageBox.Show("SỐ ĐIỆN THOẠI SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("SỬA THÔNG TIN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormSetting_Load(null, null);
                            break;
                        }
                }
            }
            else if (khachhang.Email == email)
            {

                string get2 = TKBLL.CheckUpdatetKhachHang1(khachhang);

                switch (get2)
                {
                    case "requeid_botrong":
                        {
                            MessageBox.Show("KHÔNG ĐƯỢC ĐỂ TRỐNG BẤT KỲ THÔNG TIN NÀO!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_Email":
                        {
                            MessageBox.Show("EMAIL SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    case "requeid_DienThoai":
                        {
                            MessageBox.Show("SỐ ĐIỆN THOẠI SAI ĐỊNH DẠNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("SỬA THÔNG TIN THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            FormSetting_Load(null, null);
                            break;
                        }
                }
            }

            
        }
        static string filename = "";

        private void Load_Data()
        {
            try
            {
                using (SqlConnection conn = TKBLL.Load())
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.KhachHang\r\nWHERE Ten_TK = N'{this.taikhoan}' OR Email = N'{this.taikhoan}'", conn);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {


                        while (reader.Read())
                        {
                            // Lấy giá trị từ cột "Ma_loai_hang"
                            string MaKhachHang = reader["Ma_Khach_Hang"].ToString();
                            string TenKhachHang = reader["Ten_Khach_Hang"].ToString();
                            string TenTK = reader["Ten_TK"].ToString();
                            string Email = reader["Email"].ToString();
                            string MatKhau = reader["MatKhau"].ToString();
                            string GioiTinh = reader["Gioi_Tinh"].ToString();
                            string DienThoai = reader["DienThoai"].ToString();
                            string TrangThai = reader["TrangThai"].ToString();
                            string FileNames = reader["FileNames"].ToString();
                            filename = FileNames;

                            //Check Gtinh
                            if (GioiTinh == "True")
                            {
                                GioiTinh = "Nam";
                            }
                            else if (GioiTinh == "False")
                            {
                                GioiTinh = "Nữ";
                            }

                            //Check Tình trạng
                            if(TrangThai == "True")
                            {
                                TrangThai = "Hoạt Động";
                            }
                            else if (TrangThai == "False")
                            {
                                TrangThai = "Ngừng Hoạt Động";
                            }

                            txt_tentaikhoan.Text = TenTK;
                            txt_tenkhachhang.Text = TenKhachHang;
                            txt_makhachhang.Text = MaKhachHang;
                            txt_email.Text = Email;
                            txt_sdt.Text = MatKhau;
                            cbo_gioitinh.Text = GioiTinh;
                            txt_sdt.Text = DienThoai;
                            txt_trangthai.Text = TrangThai;
                            txt_filenames.Text = FileNames;
                            if (filename == "")
                            {
                                filename =$@"C:\Users\ADMIN\Downloads\image.png";
                            }
                            if (txt_filenames.Text != "")
                            {
                                pic_khachhang.Image = new Bitmap($@"{FileNames}");
                            }
                        }
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

        //Lấy đường dẫn ảnh
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của ảnh được chọn
                string imagePath = openFileDialog.FileName;

                // Hiển thị ảnh trong PictureBox
                pic_khachhang.ImageLocation = imagePath;

                // Hiển thị đường dẫn trong một TextBox hoặc Label, nếu cần
                txt_filenames.Text = imagePath;
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
        //THÊM ĐỊA CHỈ
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            FormAddDiaChi a = new FormAddDiaChi(this.maKH);
            a.ShowDialog();
            Load_DChiKhachHang();
            Load_ComboBox2();
        }

        //LoadGiaoDien địa chỉ khách hàng
        private void Load_DChiKhachHang()
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (SqlConnection conn = TKBLL.Load())
                {
                    conn.Open();
                    SqlCommand sqlCommand = new SqlCommand($"SELECT * FROM dbo.DiaChi WHERE Ma_Khach_Hang = N'{txt_makhachhang.Text}'", conn);
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        int count = 0;

                        while (reader.Read())
                        {

                            // Lấy giá trị từ cột "Ma_loai_hang"
                            string MaKhachHang = reader["Ma_Khach_Hang"].ToString();
                            string DiaChi = reader["Dia_Chi"].ToString();

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
                            if (filename == "")
                            {
                                pic_cmt.Image = new Bitmap($@"C:\Users\ADMIN\Downloads\image.png");
                            }
                            pic_cmt.Image = new Bitmap($@"{filename}");
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
                            count++;
                            lbl_Hoten_DanhGia.Text = "Địa Chỉ " + count;
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
                            lbl_comment.Text = DiaChi;
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

                            flowLayoutPanel1.Controls.Add(panel_CMT);
                        }
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

        //Xóa địa chỉ
        private void guna2Button3_Click(object sender, EventArgs e)
        {


            diachi.Dia_Chi = cbo_diachi.Text;
            diachi.Ma_Khach_Hang = txt_makhachhang.Text;

            var get = TKBLL.CheckDeLeTetDiaChi(diachi);

            switch (get)
            {
                case "requeid_botrong":
                    {
                        MessageBox.Show("ĐỊA CHỈ KHÔNG ĐƯỢC BỎ TRỐNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case "requeid_short":
                    {
                        MessageBox.Show("ĐỊA CHỈ CỦA BẠN KHÔNG CỤ THỂ!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                default:
                    {
                        MessageBox.Show("XÓA ĐỊA CHỈ THÀNH CÔNG!!!", "THÔNG BÁO!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormSetting_Load(null, null);
                        break;
                    }
            }
        }

        //Kiểm tra form đổi mật khẩu
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau formDoiMatKhau = new FormDoiMatKhau(this.taikhoan);
            formDoiMatKhau.ShowDialog();
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



        //Load GIao DIện đơn hàng
        private void LoadGDDonHang()
        {
            try
            {
                flowLayoutPanel2.Controls.Clear();

                conn.Open();

                //Tạo vòng lặp và lấy dữ liệu từ csdl
                SqlCommand sqlCommand = new SqlCommand($"SELECT dbo.DonHang.Ma_Don_Hang,DonHang.Ngay_Xuat_Don, dbo.DonHang.Ma_Khach_Hang,dbo.DonHang.Trang_Thai_Don, dbo.DonHang.Ma_Nhan_Vien, dbo.DonHang.Ma_Nhan_VienGH, dbo.DonHang.Thanh_Tien, Phuong_thuc_thanh_toan, DonHang.Dia_Chi FROM dbo.DonHang \r\n\tJOIN dbo.PhuongThucThanhToan ON PhuongThucThanhToan.Ma_Don_Hang = DonHang.Ma_Don_Hang\r\n\tWHERE dbo.DonHang.Ma_Khach_Hang = N'{this.maKH}'", conn);
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
                    else if (MaNhanVien != "")
                    {
                        xuly = "Đã Phê Duyệt";
                    }
                    if (TrangThaiDon == "True")
                    {
                        xuly = "HOÀN THÀNH ĐƠN HÀNG!!!";
                    }
                    else if (TrangThaiDon == "False")
                    {
                        xuly = "Đang giao hàng";
                    }

                    Guna2GradientPanel panel_Father = new Guna2GradientPanel();
                    panel_Father.Dock = DockStyle.Top;
                    panel_Father.FillColor = Color.Gray;
                    panel_Father.FillColor2 = Color.Gray;
                    panel_Father.Size = new System.Drawing.Size(1400, 250);
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
                    label_maten.Text = MaDonHang + " | " + maKH + " | " + xuly;
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
                    label_maten1.Text = "NGÀY XUẤT ĐƠN: " + NgayXuatDon;
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
                    btn_xem.Text = "XEM";
                    btn_xem.Font = new Font("SVN-Cintra", 23, FontStyle.Regular);
                    btn_xem.Height = 80;

                    // Căn giữa theo chiều dọc
                    btn_xem.Location = new Point(125,90);
                    btn_xem.FillColor = Color.FromArgb(64,64,64);
                    btn_xem.Click += (s, e) => DatHangClick(s, e, MaDonHang, ThanhTien, PhuongThucThanhToan, DiaChi, xuly); 
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
                    flowLayoutPanel2.Controls.Add(panel_Father);
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

        private void Button_Change()
        {
        }

        private void DatHangClick(object sender, EventArgs e, string madh, string thanhtien, string phuongthucthanhtoan, string diachi,string xuLy)
        {

            FormQuanLyDonHang formQuanLyDonHang = new FormQuanLyDonHang(madh, thanhtien, phuongthucthanhtoan, diachi, xuLy);
            formQuanLyDonHang.ShowDialog();
          

        }

        //Ấn vào để xem đơn đang phê duyệt
        private void btn_chopheduyet_Click(object sender, EventArgs e)
        {
            FormPheDuyetDonHang form = new FormPheDuyetDonHang(maKH);
            form.ShowDialog();
            LoadGDDonHang();

        }

        //Đã phê duyệt đơn
        private void btndapheduyet_Click(object sender, EventArgs e)
        {
            FormDaPheDuyetDonHang form = new FormDaPheDuyetDonHang(maKH);
            form.ShowDialog();
            LoadGDDonHang();
        }

        //Đang giao hàng
        private void btn_danggiao_Click(object sender, EventArgs e)
        {
            FormGiaoHangDonhang form = new FormGiaoHangDonhang(maKH);
            form.ShowDialog();
            LoadGDDonHang();

        }

        //Hoàn thành đơn hàng
        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            FormHoanThanhDonHang form = new FormHoanThanhDonHang(maKH);
            form.ShowDialog();
            LoadGDDonHang();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
        }

        

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
