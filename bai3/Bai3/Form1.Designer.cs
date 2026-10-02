namespace TechMart
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ----- Khai báo control -----
            menuStrip1 = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuExport = new ToolStripMenuItem();
            mnuExit = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblTotal = new ToolStripStatusLabel();

            tlpMain = new TableLayoutPanel();
            grpInput = new GroupBox();
            tlpInput = new TableLayoutPanel();
            lblId = new Label();
            lblName = new Label();
            lblCategory = new Label();
            lblPrice = new Label();
            lblQty = new Label();
            lblImage = new Label();
            txtProductId = new TextBox();
            txtProductName = new TextBox();
            cboCategory = new ComboBox();
            txtUnitPrice = new TextBox();
            txtQuantity = new TextBox();
            picAvatar = new PictureBox();
            btnChooseImage = new Button();
            flowButtons = new FlowLayoutPanel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            tlpRight = new TableLayoutPanel();
            tlpSearch = new TableLayoutPanel();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnExport = new Button();
            dgvProducts = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();

            errorProvider = new ErrorProvider(components);
            bindingSource1 = new BindingSource(components);

            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();

            // ----- menuStrip1 -----
            menuStrip1.Items.Add(mnuFile);
            mnuFile.Text = "&File";
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuExport, new ToolStripSeparator(), mnuExit });
            mnuExport.Text = "Export CSV";
            mnuExport.ShortcutKeys = Keys.Control | Keys.E;
            mnuExit.Text = "Exit";
            mnuExit.ShortcutKeys = Keys.Control | Keys.X;

            // ----- statusStrip1 -----
            statusStrip1.Items.Add(lblTotal);
            lblTotal.Text = "Tổng số sản phẩm: 0";

            // ----- tlpMain: 2 cột 35% - 65% -----
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.ColumnCount = 2;
            tlpMain.RowCount = 1;
            tlpMain.Padding = new Padding(8);
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(grpInput, 0, 0);
            tlpMain.Controls.Add(tlpRight, 1, 0);

            // ----- grpInput + tlpInput (khung nhập liệu, cột trái) -----
            grpInput.Text = "Thông tin sản phẩm";
            grpInput.Dock = DockStyle.Fill;
            grpInput.Padding = new Padding(8);
            grpInput.Controls.Add(tlpInput);

            tlpInput.Dock = DockStyle.Fill;
            tlpInput.ColumnCount = 2;
            tlpInput.RowCount = 8;
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            for (int i = 0; i < 5; i++) tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInput.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpInput.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // nhãn
            SetLabel(lblId, "Mã SP:");
            SetLabel(lblName, "Tên SP:");
            SetLabel(lblCategory, "Danh mục:");
            SetLabel(lblPrice, "Đơn giá:");
            SetLabel(lblQty, "Số lượng:");
            SetLabel(lblImage, "Ảnh:");
            lblImage.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // ô nhập (chừa lề phải 26px cho icon ErrorProvider)
            foreach (Control c in new Control[] { txtProductId, txtProductName, cboCategory, txtUnitPrice, txtQuantity })
            {
                c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                c.Margin = new Padding(3, 5, 26, 5);
            }
            txtProductId.Name = "txtProductId";
            txtProductId.ReadOnly = true;
            txtProductName.Name = "txtProductName";
            txtUnitPrice.Name = "txtUnitPrice";
            txtQuantity.Name = "txtQuantity";
            cboCategory.Name = "cboCategory";
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // ảnh đại diện
            picAvatar.Name = "picAvatar";
            picAvatar.Dock = DockStyle.Fill;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.MinimumSize = new Size(100, 100);
            picAvatar.Margin = new Padding(3, 3, 26, 3);

            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Text = "Chọn ảnh...";
            btnChooseImage.AutoSize = true;
            btnChooseImage.Anchor = AnchorStyles.Left;

            // nút hành động
            flowButtons.Dock = DockStyle.Fill;
            flowButtons.AutoSize = true;
            flowButtons.WrapContents = true;
            SetButton(btnAdd, "btnAdd", "Thêm mới");
            SetButton(btnUpdate, "btnUpdate", "Cập nhật");
            SetButton(btnDelete, "btnDelete", "Xóa");
            SetButton(btnClear, "btnClear", "Làm mới");
            flowButtons.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            // đặt vào lưới nhập
            tlpInput.Controls.Add(lblId, 0, 0); tlpInput.Controls.Add(txtProductId, 1, 0);
            tlpInput.Controls.Add(lblName, 0, 1); tlpInput.Controls.Add(txtProductName, 1, 1);
            tlpInput.Controls.Add(lblCategory, 0, 2); tlpInput.Controls.Add(cboCategory, 1, 2);
            tlpInput.Controls.Add(lblPrice, 0, 3); tlpInput.Controls.Add(txtUnitPrice, 1, 3);
            tlpInput.Controls.Add(lblQty, 0, 4); tlpInput.Controls.Add(txtQuantity, 1, 4);
            tlpInput.Controls.Add(lblImage, 0, 5); tlpInput.Controls.Add(picAvatar, 1, 5);
            tlpInput.Controls.Add(btnChooseImage, 1, 6);
            tlpInput.Controls.Add(flowButtons, 0, 7);
            tlpInput.SetColumnSpan(flowButtons, 2);

            // ----- tlpRight: thanh tìm kiếm + DataGridView (cột phải) -----
            tlpRight.Dock = DockStyle.Fill;
            tlpRight.ColumnCount = 1;
            tlpRight.RowCount = 2;
            tlpRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRight.Controls.Add(tlpSearch, 0, 0);
            tlpRight.Controls.Add(dgvProducts, 0, 1);

            tlpSearch.Dock = DockStyle.Fill;
            tlpSearch.AutoSize = true;
            tlpSearch.ColumnCount = 3;
            tlpSearch.RowCount = 1;
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpSearch.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpSearch.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            lblSearch.Text = "Tìm kiếm theo tên:";
            lblSearch.AutoSize = true;
            lblSearch.Anchor = AnchorStyles.Left;
            txtSearch.Name = "txtSearch";
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SetButton(btnExport, "btnExport", "Xuất CSV");
            tlpSearch.Controls.Add(lblSearch, 0, 0);
            tlpSearch.Controls.Add(txtSearch, 1, 0);
            tlpSearch.Controls.Add(btnExport, 2, 0);

            // ----- dgvProducts -----
            dgvProducts.Name = "dgvProducts";
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.RowHeadersVisible = false;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProducts.BackgroundColor = SystemColors.Window;
            dgvProducts.DataSource = bindingSource1;

            colId.HeaderText = "Mã SP"; colId.DataPropertyName = "ProductId"; colId.FillWeight = 15;
            colName.HeaderText = "Tên SP"; colName.DataPropertyName = "ProductName"; colName.FillWeight = 35;
            colCategory.HeaderText = "Danh Mục"; colCategory.DataPropertyName = "CategoryName"; colCategory.FillWeight = 20;
            colPrice.HeaderText = "Đơn Giá (VNĐ)"; colPrice.DataPropertyName = "UnitPrice"; colPrice.FillWeight = 20;
            colPrice.DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "N0",
                FormatProvider = System.Globalization.CultureInfo.InvariantCulture,
                Alignment = DataGridViewContentAlignment.MiddleRight
            };
            colQty.HeaderText = "Số Lượng"; colQty.DataPropertyName = "Quantity"; colQty.FillWeight = 10;
            colQty.DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight };
            dgvProducts.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colCategory, colPrice, colQty });

            // ----- errorProvider -----
            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            // ----- Form1 -----
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            ClientSize = new Size(1150, 680);
            MinimumSize = new Size(900, 520);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TechMart Product Manager";
            Controls.Add(tlpMain);        // Fill thêm trước
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;

            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void SetLabel(Label l, string text)
        {
            l.Text = text;
            l.AutoSize = true;
            l.Anchor = AnchorStyles.Left;
            l.Margin = new Padding(3, 8, 3, 8);
        }

        private static void SetButton(Button b, string name, string text)
        {
            b.Name = name;
            b.Text = text;
            b.AutoSize = true;
            b.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            b.Padding = new Padding(6, 2, 6, 2);
            b.Margin = new Padding(3, 6, 3, 3);
        }

        // ----- Khai báo biến control -----
        private MenuStrip menuStrip1;
        private ToolStripMenuItem mnuFile, mnuExport, mnuExit;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblTotal;
        private TableLayoutPanel tlpMain, tlpInput, tlpRight, tlpSearch;
        private GroupBox grpInput;
        private Label lblId, lblName, lblCategory, lblPrice, lblQty, lblImage, lblSearch;
        private TextBox txtProductId, txtProductName, txtUnitPrice, txtQuantity, txtSearch;
        private ComboBox cboCategory;
        private PictureBox picAvatar;
        private Button btnChooseImage, btnAdd, btnUpdate, btnDelete, btnClear, btnExport;
        private FlowLayoutPanel flowButtons;
        private DataGridView dgvProducts;
        private DataGridViewTextBoxColumn colId, colName, colCategory, colPrice, colQty;
        private ErrorProvider errorProvider;
        private BindingSource bindingSource1;
    }
}
