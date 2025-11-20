namespace probkic
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUserInfo;
        private Button btnLogout;
        private Button btnProducts;
        private Button btnOrders;
        private Panel mainPanel;
        private DataGridView dataGridViewOrders;
        private Label lblTitle;
        private Panel panelProductsView;
        private FlowLayoutPanel flowLayoutProducts;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblUserInfo = new Label();
            btnLogout = new Button();
            btnProducts = new Button();
            btnOrders = new Button();
            mainPanel = new Panel();
            panelProductsView = new Panel();
            flowLayoutProducts = new FlowLayoutPanel();
            dataGridViewOrders = new DataGridView();
            lblTitle = new Label();
            mainPanel.SuspendLayout();
            panelProductsView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).BeginInit();
            SuspendLayout();
            // 
            // lblUserInfo
            // 
            lblUserInfo.AutoSize = true;
            lblUserInfo.Location = new Point(600, 20);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(0, 15);
            lblUserInfo.TabIndex = 0;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.MediumSpringGreen;
            btnLogout.Location = new Point(600, 50);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(111, 47);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "Выход";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnProducts
            // 
            btnProducts.BackColor = Color.MediumSpringGreen;
            btnProducts.Location = new Point(50, 50);
            btnProducts.Name = "btnProducts";
            btnProducts.Size = new Size(124, 47);
            btnProducts.TabIndex = 2;
            btnProducts.Text = "Товары";
            btnProducts.UseVisualStyleBackColor = false;
            btnProducts.Click += btnProducts_Click;
            // 
            // btnOrders
            // 
            btnOrders.BackColor = Color.MediumSpringGreen;
            btnOrders.Location = new Point(180, 50);
            btnOrders.Name = "btnOrders";
            btnOrders.Size = new Size(126, 47);
            btnOrders.TabIndex = 3;
            btnOrders.Text = "Заказы";
            btnOrders.UseVisualStyleBackColor = false;
            btnOrders.Click += btnOrders_Click;
            // 
            // mainPanel
            // 
            mainPanel.BorderStyle = BorderStyle.FixedSingle;
            mainPanel.Controls.Add(panelProductsView);
            mainPanel.Controls.Add(dataGridViewOrders);
            mainPanel.Location = new Point(50, 130);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(800, 450);
            mainPanel.TabIndex = 5;
            // 
            // panelProductsView
            // 
            panelProductsView.AutoScroll = true;
            panelProductsView.Controls.Add(flowLayoutProducts);
            panelProductsView.Dock = DockStyle.Fill;
            panelProductsView.Location = new Point(0, 0);
            panelProductsView.Name = "panelProductsView";
            panelProductsView.Size = new Size(798, 448);
            panelProductsView.TabIndex = 0;
            // 
            // flowLayoutProducts
            // 
            flowLayoutProducts.AutoScroll = true;
            flowLayoutProducts.Dock = DockStyle.Fill;
            flowLayoutProducts.Location = new Point(0, 0);
            flowLayoutProducts.Name = "flowLayoutProducts";
            flowLayoutProducts.Size = new Size(798, 448);
            flowLayoutProducts.TabIndex = 0;
            // 
            // dataGridViewOrders
            // 
            dataGridViewOrders.Dock = DockStyle.Fill;
            dataGridViewOrders.Location = new Point(0, 0);
            dataGridViewOrders.Name = "dataGridViewOrders";
            dataGridViewOrders.Size = new Size(798, 448);
            dataGridViewOrders.TabIndex = 1;
            dataGridViewOrders.Visible = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(50, 100);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0, 25);
            lblTitle.TabIndex = 4;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(908, 622);
            Controls.Add(lblTitle);
            Controls.Add(mainPanel);
            Controls.Add(btnOrders);
            Controls.Add(btnProducts);
            Controls.Add(btnLogout);
            Controls.Add(lblUserInfo);
            Name = "FormMain";
            Text = "Главная страница";
            Load += FormMain_Load;
            mainPanel.ResumeLayout(false);
            panelProductsView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}