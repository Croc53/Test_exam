using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace probkic
{
    public partial class FormMain : Form
    {
        private string userRole;
        private string userFIO;

        public FormMain(string role, string fio)
        {
            InitializeComponent();
            this.userRole = role;
            this.userFIO = fio;
            dataGridViewOrders.CellDoubleClick += dataGridViewOrders_CellDoubleClick;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = $"Пользователь: {userFIO} ({userRole})";
            ConfigureInterfaceByRole();
            ShowProducts();
        }

        private void ConfigureInterfaceByRole()
        {
            switch (userRole)
            {
                case "Гость":
                    btnOrders.Enabled = false;
                    btnOrders.Text = "Заказы (недоступно)";
                    break;
                case "Авторизированный клиент":
                    btnOrders.Enabled = true;
                    btnOrders.Text = "Мои заказы";
                    break;
                case "Менеджер":
                case "Администратор":
                    btnOrders.Enabled = true;
                    btnOrders.Text = "Все заказы";
                    break;
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ShowProducts();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            ShowOrders();
        }

        private void ShowProducts()
        {
            lblTitle.Text = "Каталог товаров";
            dataGridViewOrders.Visible = false;
            panelProductsView.Visible = true;
            LoadProducts();
        }

        private void ShowOrders()
        {
            lblTitle.Text = "Список заказов";
            panelProductsView.Visible = false;
            dataGridViewOrders.Visible = true;
            ConfigureDataGridView();
            LoadOrders();
        }

        private void ConfigureDataGridView()
        {
            dataGridViewOrders.AutoGenerateColumns = false;
            dataGridViewOrders.Columns.Clear();
            dataGridViewOrders.BackgroundColor = SystemColors.Window;
            dataGridViewOrders.BorderStyle = BorderStyle.Fixed3D;
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.MultiSelect = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;

            if (userRole == "Авторизированный клиент")
            {
                CreateClientColumns();
            }
            else if (userRole == "Менеджер" || userRole == "Администратор")
            {
                CreateManagerColumns();
            }
        }

        private void CreateClientColumns()
        {
            dataGridViewOrders.Columns.Add(CreateColumn("Номер заказа", "OrderID", 80));
            dataGridViewOrders.Columns.Add(CreateColumn("Артикулы", "articul", 150));
            dataGridViewOrders.Columns.Add(CreateColumn("Дата заказа", "data_order", 100));
            dataGridViewOrders.Columns.Add(CreateColumn("Дата доставки", "data_del", 100));
            dataGridViewOrders.Columns.Add(CreateColumn("Пункт выдачи", "address", 120));
            dataGridViewOrders.Columns.Add(CreateColumn("Код", "Code", 80));
            dataGridViewOrders.Columns.Add(CreateColumn("Статус", "status", 100));
        }

        private void CreateManagerColumns()
        {
            dataGridViewOrders.Columns.Add(CreateColumn("Номер заказа", "OrderID", 80));
            dataGridViewOrders.Columns.Add(CreateColumn("Клиент", "FIO", 120));
            dataGridViewOrders.Columns.Add(CreateColumn("Артикулы", "articul", 150));
            dataGridViewOrders.Columns.Add(CreateColumn("Дата заказа", "data_order", 100));
            dataGridViewOrders.Columns.Add(CreateColumn("Дата доставки", "data_del", 100));
            dataGridViewOrders.Columns.Add(CreateColumn("Пункт выдачи", "address", 120));
            dataGridViewOrders.Columns.Add(CreateColumn("Код", "Code", 80));
            dataGridViewOrders.Columns.Add(CreateColumn("Статус", "status", 100));
        }

        private DataGridViewTextBoxColumn CreateColumn(string headerText, string dataPropertyName, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = dataPropertyName,
                Width = width,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.Automatic
            };
        }

        private void LoadOrders()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "";

                    if (userRole == "Авторизированный клиент")
                    {
                        sql = @"
                            SELECT 
                                o.OrderID,
                                o.articul,
                                o.data_order,
                                o.data_del,
                                p.adress as address,
                                o.Code,
                                o.status
                            FROM Order1 o
                            INNER JOIN Point p ON o.post_id = p.PintID
                            WHERE o.user_sys_id = @userID
                            ORDER BY o.data_order DESC";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userID", GetCurrentUserID());
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewOrders.DataSource = dt;
                    }
                    else if (userRole == "Менеджер" || userRole == "Администратор")
                    {
                        sql = @"
                            SELECT 
                                o.OrderID,
                                u.FIO,
                                o.articul,
                                o.data_order,
                                o.data_del,
                                p.adress as address,
                                o.Code,
                                o.status
                            FROM Order1 o
                            INNER JOIN Point p ON o.post_id = p.PintID
                            INNER JOIN user_syst u ON o.user_sys_id = u.UserID
                            ORDER BY o.data_order DESC";

                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewOrders.DataSource = dt;
                    }
                    else
                    {
                        dataGridViewOrders.DataSource = new DataTable();
                    }

                    ApplyDataGridViewStyles();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки заказов: " + ex.Message);
                }
            }
        }

        private void ApplyDataGridViewStyles()
        {
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            dataGridViewOrders.RowsDefaultCellStyle.BackColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void dataGridViewOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var orderId = dataGridViewOrders.Rows[e.RowIndex].Cells["OrderID"].Value?.ToString();
                var status = dataGridViewOrders.Rows[e.RowIndex].Cells["status"].Value?.ToString();
                MessageBox.Show($"Заказ №{orderId}\nСтатус: {status}", "Информация о заказе");
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            ItemID,
                            Type,
                            description,
                            Factory,
                            Price,
                            sale,
                            count_item,
                            photo_link
                        FROM items";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    flowLayoutProducts.Controls.Clear();

                    while (reader.Read())
                    {
                        CreateProductCard(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки товаров: " + ex.Message);
                }
            }
        }

        private void CreateProductCard(SqlDataReader reader)
        {
            Panel productCard = new Panel();
            productCard.Size = new Size(350, 180);
            productCard.BorderStyle = BorderStyle.FixedSingle;
            productCard.Margin = new Padding(10);
            productCard.AutoSize = false;

            int sale = Convert.ToInt32(reader["sale"]);
            int stock = Convert.ToInt32(reader["count_item"]);
            decimal price = Convert.ToDecimal(reader["Price"]);
            decimal finalPrice = price * (100 - sale) / 100;

            if (sale > 15)
                productCard.BackColor = ColorTranslator.FromHtml("#2E8B57");
            else if (stock == 0)
                productCard.BackColor = Color.LightBlue;

            PictureBox productImage = new PictureBox();
            productImage.Size = new Size(120, 120);
            productImage.Location = new Point(10, 30);
            productImage.SizeMode = PictureBoxSizeMode.Zoom;
            productImage.BorderStyle = BorderStyle.FixedSingle;

            string imageRef = reader["photo_link"]?.ToString();
            LoadProductImage(productImage, imageRef);

            Panel infoPanel = new Panel();
            infoPanel.Location = new Point(140, 10);
            infoPanel.Size = new Size(200, 160);
            infoPanel.AutoSize = false;

            Label lblCategory = new Label();
            lblCategory.Text = reader["Type"].ToString();
            lblCategory.Location = new Point(0, 0);
            lblCategory.Size = new Size(190, 20);
            lblCategory.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblCategory.AutoSize = false;

            Label lblName = new Label();
            lblName.Text = reader["description"].ToString();
            lblName.Location = new Point(0, 20);
            lblName.Size = new Size(190, 40);
            lblName.AutoSize = false;
            lblName.Font = new Font("Segoe UI", 8);

            Label lblFactory = new Label();
            lblFactory.Text = $"Произв.: {reader["Factory"]}";
            lblFactory.Location = new Point(0, 60);
            lblFactory.Size = new Size(190, 20);
            lblFactory.Font = new Font("Segoe UI", 8);
            lblFactory.AutoSize = false;

            Label lblPrice = new Label();
            if (sale > 0)
            {
                lblPrice.Text = $"{price:N2} ₽ → {finalPrice:N2} ₽";
                lblPrice.ForeColor = Color.Red;
            }
            else
            {
                lblPrice.Text = $"{price:N2} ₽";
                lblPrice.ForeColor = Color.Black;
            }
            lblPrice.Location = new Point(0, 80);
            lblPrice.Size = new Size(190, 20);
            lblPrice.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblPrice.AutoSize = false;

            Label lblStock = new Label();
            lblStock.Text = $"На складе: {stock} шт.";
            lblStock.Location = new Point(0, 100);
            lblStock.Size = new Size(190, 20);
            lblStock.Font = new Font("Segoe UI", 8);
            lblStock.AutoSize = false;

            Panel discountPanel = new Panel();
            discountPanel.Location = new Point(310, 10);
            discountPanel.Size = new Size(30, 30);

            if (sale > 0)
            {
                discountPanel.BackColor = sale > 15 ? Color.Red : Color.Gold;
                discountPanel.Visible = true;
            }
            else
            {
                discountPanel.Visible = false;
            }

            Label lblDiscount = new Label();
            lblDiscount.Text = $"{sale}%";
            lblDiscount.Location = new Point(5, 5);
            lblDiscount.Size = new Size(20, 20);
            lblDiscount.TextAlign = ContentAlignment.MiddleCenter;
            lblDiscount.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            lblDiscount.ForeColor = sale > 15 ? Color.White : Color.Black;

            discountPanel.Controls.Add(lblDiscount);
            infoPanel.Controls.AddRange(new Control[] { lblCategory, lblName, lblFactory, lblPrice, lblStock });
            productCard.Controls.AddRange(new Control[] { productImage, infoPanel, discountPanel });
            flowLayoutProducts.Controls.Add(productCard);
        }

        private void LoadProductImage(PictureBox pictureBox, string imageReference)
        {
            try
            {
                pictureBox.Image = null;

                if (string.IsNullOrEmpty(imageReference))
                {
                    SetDefaultImage(pictureBox);
                    return;
                }

                imageReference = imageReference.Trim();
                string correctedFileName = imageReference
                    .Replace("ipg", "jpg")
                    .Replace("ipe", "jpg")
                    .Replace("ipa", "jpg");

                string cDriveImagesPath = Path.Combine(@"C:\Images", correctedFileName);
                string debugImagesPath = Path.Combine(Application.StartupPath, "Images", correctedFileName);

                if (File.Exists(cDriveImagesPath))
                {
                    using (FileStream stream = new FileStream(cDriveImagesPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(stream);
                    }
                }
                else if (File.Exists(debugImagesPath))
                {
                    using (FileStream stream = new FileStream(debugImagesPath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    SetDefaultImage(pictureBox);
                }
            }
            catch (Exception)
            {
                SetDefaultImage(pictureBox);
            }
        }

        private void SetDefaultImage(PictureBox pictureBox)
        {
            try
            {
                Bitmap defaultImage = new Bitmap(100, 100);
                using (Graphics g = Graphics.FromImage(defaultImage))
                {
                    g.Clear(Color.LightGray);
                    using (Font font = new Font("Arial", 10))
                    using (Brush brush = new SolidBrush(Color.DarkGray))
                    {
                        g.DrawString("No Image", font, brush, 10, 40);
                    }
                }
                pictureBox.Image = defaultImage;
            }
            catch
            {
                pictureBox.Image = null;
            }
        }

        private int GetCurrentUserID()
        {
            using (SqlConnection conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT UserID FROM user_syst WHERE FIO = @fio";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@fio", userFIO);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
                catch
                {
                    return 0;
                }
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}