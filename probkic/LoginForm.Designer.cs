namespace probkic
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPassword = new TextBox();
            txtLogin = new TextBox();
            btnLogin = new Button();
            btnGuest = new Button();
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(405, 260);
            txtPassword.Margin = new Padding(4, 4, 4, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(127, 26);
            txtPassword.TabIndex = 1;
            // 
            // txtLogin
            // 
            txtLogin.Location = new Point(405, 199);
            txtLogin.Margin = new Padding(4, 4, 4, 4);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(127, 26);
            txtLogin.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.MediumSpringGreen;
            btnLogin.Location = new Point(320, 315);
            btnLogin.Margin = new Padding(4, 4, 4, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(152, 62);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "Авторизоваться";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnGuest
            // 
            btnGuest.BackColor = Color.MediumSpringGreen;
            btnGuest.Location = new Point(504, 315);
            btnGuest.Margin = new Padding(4, 4, 4, 4);
            btnGuest.Name = "btnGuest";
            btnGuest.Size = new Size(152, 62);
            btnGuest.TabIndex = 2;
            btnGuest.Text = "Гость";
            btnGuest.UseVisualStyleBackColor = false;
            btnGuest.Click += btnGuest_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(333, 203);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 19);
            label1.TabIndex = 3;
            label1.Text = "Логин";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(333, 263);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(58, 19);
            label2.TabIndex = 3;
            label2.Text = "Пароль";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 570);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGuest);
            Controls.Add(btnLogin);
            Controls.Add(txtLogin);
            Controls.Add(txtPassword);
            Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4, 4, 4, 4);
            Name = "LoginForm";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtPassword;
        private TextBox txtLogin;
        private Button btnLogin;
        private Button btnGuest;
        private Label label1;
        private Label label2;
    }
}
