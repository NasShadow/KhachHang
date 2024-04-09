namespace Login
{
    partial class FormAddDiaChi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.txt_makhachhang = new System.Windows.Forms.TextBox();
            this.txtLon = new System.Windows.Forms.TextBox();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.btn_luu = new Guna.UI2.WinForms.Guna2Button();
            this.lbl_maloai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txt_diachi = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.guna2CustomGradientPanel1.SuspendLayout();
            this.guna2GradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.txt_makhachhang);
            this.guna2CustomGradientPanel1.Controls.Add(this.txtLon);
            this.guna2CustomGradientPanel1.Controls.Add(this.txtLat);
            this.guna2CustomGradientPanel1.Controls.Add(this.btn_luu);
            this.guna2CustomGradientPanel1.Controls.Add(this.lbl_maloai);
            this.guna2CustomGradientPanel1.Controls.Add(this.txt_diachi);
            this.guna2CustomGradientPanel1.Controls.Add(this.guna2GradientPanel1);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(60, 60);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(1800, 960);
            this.guna2CustomGradientPanel1.TabIndex = 42;
            // 
            // txt_makhachhang
            // 
            this.txt_makhachhang.Location = new System.Drawing.Point(191, 841);
            this.txt_makhachhang.Name = "txt_makhachhang";
            this.txt_makhachhang.Size = new System.Drawing.Size(100, 20);
            this.txt_makhachhang.TabIndex = 16;
            this.txt_makhachhang.Visible = false;
            // 
            // txtLon
            // 
            this.txtLon.Location = new System.Drawing.Point(68, 867);
            this.txtLon.Name = "txtLon";
            this.txtLon.Size = new System.Drawing.Size(100, 20);
            this.txtLon.TabIndex = 15;
            this.txtLon.Visible = false;
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(68, 841);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 14;
            this.txtLat.Visible = false;
            // 
            // btn_luu
            // 
            this.btn_luu.BackColor = System.Drawing.Color.White;
            this.btn_luu.BorderRadius = 5;
            this.btn_luu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_luu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_luu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_luu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_luu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_luu.Font = new System.Drawing.Font("SVN-Cintra", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_luu.ForeColor = System.Drawing.Color.White;
            this.btn_luu.Location = new System.Drawing.Point(1539, 849);
            this.btn_luu.Name = "btn_luu";
            this.btn_luu.Size = new System.Drawing.Size(236, 69);
            this.btn_luu.TabIndex = 13;
            this.btn_luu.Text = "Xác Nhận";
            this.btn_luu.Click += new System.EventHandler(this.btn_luu_Click);
            // 
            // lbl_maloai
            // 
            this.lbl_maloai.BackColor = System.Drawing.Color.White;
            this.lbl_maloai.Font = new System.Drawing.Font("SVN-Appleberry", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_maloai.Location = new System.Drawing.Point(912, 846);
            this.lbl_maloai.Name = "lbl_maloai";
            this.lbl_maloai.Size = new System.Drawing.Size(204, 62);
            this.lbl_maloai.TabIndex = 6;
            this.lbl_maloai.Text = "Tên Địa Chỉ";
            // 
            // txt_diachi
            // 
            this.txt_diachi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_diachi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_diachi.DefaultText = "";
            this.txt_diachi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txt_diachi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txt_diachi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_diachi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txt_diachi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_diachi.Font = new System.Drawing.Font("Segoe UI Semibold", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_diachi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txt_diachi.Location = new System.Drawing.Point(1124, 863);
            this.txt_diachi.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.txt_diachi.Name = "txt_diachi";
            this.txt_diachi.PasswordChar = '\0';
            this.txt_diachi.PlaceholderText = "";
            this.txt_diachi.SelectedText = "";
            this.txt_diachi.Size = new System.Drawing.Size(377, 45);
            this.txt_diachi.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txt_diachi.TabIndex = 5;
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.Controls.Add(this.webView21);
            this.guna2GradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(1800, 816);
            this.guna2GradientPanel1.TabIndex = 1;
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 0);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(1800, 816);
            this.webView21.TabIndex = 0;
            this.webView21.ZoomFactor = 1D;
            this.webView21.SourceChanged += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs>(this.webView21_SourceChanged);
            this.webView21.Click += new System.EventHandler(this.webView21_Click);
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.FillColor = System.Drawing.Color.Red;
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(1883, 18);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(20, 20);
            this.guna2CirclePictureBox1.TabIndex = 43;
            this.guna2CirclePictureBox1.TabStop = false;
            this.guna2CirclePictureBox1.Click += new System.EventHandler(this.guna2CirclePictureBox1_Click);
            // 
            // FormAddDiaChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.guna2CirclePictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAddDiaChi";
            this.Padding = new System.Windows.Forms.Padding(60);
            this.Text = "FormAddDiaChi";
            this.Load += new System.EventHandler(this.FormAddDiaChi_Load);
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            this.guna2CustomGradientPanel1.PerformLayout();
            this.guna2GradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private System.Windows.Forms.TextBox txt_makhachhang;
        private System.Windows.Forms.TextBox txtLon;
        private System.Windows.Forms.TextBox txtLat;
        private Guna.UI2.WinForms.Guna2Button btn_luu;
        private Guna.UI2.WinForms.Guna2HtmlLabel lbl_maloai;
        private Guna.UI2.WinForms.Guna2TextBox txt_diachi;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
    }
}