using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace TechMart
{
    public partial class Form1 : Form
    {
        private readonly List<Product> _products = new();      // dữ liệu gốc
        private readonly BindingList<Product> _view = new();    // dữ liệu hiển thị (sau khi lọc)
        private readonly List<Category> _categories = new()
        {
            new Category(1, "Điện thoại"),
            new Category(2, "Laptop"),
            new Category(3, "Phụ kiện")
        };
        private int _nextId = 1;
        private bool _loading;
        private string? _currentImagePath;

        public Form1()
        {
            InitializeComponent();

            // ComboBox danh mục
            cboCategory.DataSource = _categories;
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "Id";

            // Data binding: BindingList<T> -> BindingSource -> DataGridView
            bindingSource1.DataSource = _view;

            // Sự kiện
            dgvProducts.SelectionChanged += Dgv_SelectionChanged;
            txtSearch.TextChanged += (s, e) => RefreshView();
            btnChooseImage.Click += BtnChooseImage_Click;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ResetForm();
            btnExport.Click += (s, e) => ExportCsv();
            mnuExport.Click += (s, e) => ExportCsv();
            mnuExit.Click += (s, e) => Close();

            // Dữ liệu mẫu
            AddProduct("iPhone 15", 1, 22_000_000m, 10);
            AddProduct("Laptop Dell XPS", 2, 30_000_000m, 5);
            AddProduct("Tai nghe Sony", 3, 2_500_000m, 25);

            RefreshView();
            ResetForm();
        }

        // ================= DỮ LIỆU =================

        private Product AddProduct(string name, int categoryId, decimal price, int qty, string? image = null)
        {
            var p = new Product
            {
                ProductId = $"SP{_nextId:000}",
                ProductName = name.Trim(),
                CategoryId = categoryId,
                CategoryName = _categories.First(c => c.Id == categoryId).Name,
                UnitPrice = price,
                Quantity = qty,
                ImagePath = image
            };
            _nextId++;
            _products.Add(p);
            return p;
        }

        /// <summary>Nạp lại lưới theo ô tìm kiếm (Live search) và cập nhật StatusStrip.</summary>
        private void RefreshView(Product? select = null)
        {
            _loading = true;
            string kw = txtSearch.Text.Trim();

            _view.RaiseListChangedEvents = false;
            _view.Clear();
            foreach (var p in _products)
                if (kw.Length == 0 || p.ProductName.Contains(kw, StringComparison.OrdinalIgnoreCase))
                    _view.Add(p);
            _view.RaiseListChangedEvents = true;
            _view.ResetBindings();

            _loading = false;
            lblTotal.Text = $"Tổng số sản phẩm: {_products.Count}";

            dgvProducts.ClearSelection();
            if (select != null)
            {
                int idx = _view.IndexOf(select);
                if (idx >= 0)
                {
                    dgvProducts.Rows[idx].Selected = true;
                    dgvProducts.CurrentCell = dgvProducts.Rows[idx].Cells[0];
                }
            }
        }

        private Product? GetSelected() =>
            dgvProducts.SelectedRows.Count > 0 ? dgvProducts.SelectedRows[0].DataBoundItem as Product : null;

        // Click dòng trên Grid -> nạp ngược lên các ô nhập
        private void Dgv_SelectionChanged(object? sender, EventArgs e)
        {
            if (_loading) return;
            var p = GetSelected();
            if (p == null) return;

            errorProvider.Clear();
            txtProductId.Text = p.ProductId;
            txtProductName.Text = p.ProductName;
            cboCategory.SelectedValue = p.CategoryId;
            txtUnitPrice.Text = p.UnitPrice.ToString("0", CultureInfo.InvariantCulture);
            txtQuantity.Text = p.Quantity.ToString();
            _currentImagePath = p.ImagePath;
            SetImage(p.ImagePath);
        }

        private void ResetForm()
        {
            errorProvider.Clear();
            txtProductId.Text = $"SP{_nextId:000}";
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            if (cboCategory.Items.Count > 0) cboCategory.SelectedIndex = 0;
            _currentImagePath = null;
            SetImage(null);
            dgvProducts.ClearSelection();
            txtProductName.Focus();
        }

        private void SetImage(string? path)
        {
            var old = picAvatar.Image;
            picAvatar.Image = null;
            old?.Dispose();

            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return;
            try
            {
                // Đọc vào bộ nhớ để không khóa file ảnh trên đĩa
                using var ms = new MemoryStream(File.ReadAllBytes(path));
                using var img = Image.FromStream(ms);
                picAvatar.Image = new Bitmap(img);
            }
            catch
            {
                MessageBox.Show("Không đọc được file ảnh này.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= NHẬP LIỆU + VALIDATION =================

        private bool ValidateInput(out decimal price, out int qty)
        {
            errorProvider.Clear();
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider.SetError(txtProductName, "Tên sản phẩm không được để trống");
                ok = false;
            }

            string rawPrice = txtUnitPrice.Text.Replace(",", "").Replace(".", "").Replace(" ", "");
            if (!decimal.TryParse(rawPrice, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out price) || price <= 0)
            {
                errorProvider.SetError(txtUnitPrice, "Đơn giá phải là số lớn hơn 0");
                ok = false;
            }

            if (!int.TryParse(txtQuantity.Text.Trim(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out qty) || qty < 0)
            {
                errorProvider.SetError(txtQuantity, "Số lượng phải là số nguyên từ 0 trở lên");
                ok = false;
            }

            return ok;
        }

        // ================= CÁC NÚT CHỨC NĂNG =================

        private void BtnChooseImage_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh sản phẩm",
                Filter = "Tệp ảnh (*.png;*.jpg;*.jpeg;*.bmp;*.gif)|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Tất cả tệp (*.*)|*.*"
            };
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                _currentImagePath = ofd.FileName;
                SetImage(_currentImagePath);
            }
        }

        private void BtnAdd_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(out decimal price, out int qty)) return;

            var cat = (Category)cboCategory.SelectedItem!;
            AddProduct(txtProductName.Text, cat.Id, price, qty, _currentImagePath);

            RefreshView();
            ResetForm();
        }

        private void BtnUpdate_Click(object? sender, EventArgs e)
        {
            var sel = GetSelected();
            if (sel == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trên bảng để cập nhật.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!ValidateInput(out decimal price, out int qty)) return;

            var cat = (Category)cboCategory.SelectedItem!;
            sel.ProductName = txtProductName.Text.Trim();
            sel.CategoryId = cat.Id;
            sel.CategoryName = cat.Name;
            sel.UnitPrice = price;
            sel.Quantity = qty;
            sel.ImagePath = _currentImagePath;

            RefreshView(sel);
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            var sel = GetSelected();
            if (sel == null)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var r = MessageBox.Show($"Bạn có chắc muốn xóa sản phẩm \"{sel.ProductName}\"?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;

            _products.Remove(sel);
            RefreshView();
            ResetForm();
        }

        private void ExportCsv()
        {
            using var sfd = new SaveFileDialog
            {
                Title = "Xuất danh sách sản phẩm",
                Filter = "CSV (*.csv)|*.csv",
                FileName = "DanhSachSanPham.csv"
            };
            if (sfd.ShowDialog(this) != DialogResult.OK) return;

            var sb = new StringBuilder();
            sb.AppendLine("Mã SP,Tên SP,Danh mục,Đơn giá,Số lượng");
            foreach (var p in _products)
            {
                sb.AppendLine(string.Join(",",
                    Csv(p.ProductId), Csv(p.ProductName), Csv(p.CategoryName),
                    p.UnitPrice.ToString("0", CultureInfo.InvariantCulture), p.Quantity));
            }

            // UTF-8 có BOM để Excel đọc đúng tiếng Việt
            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
            MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Csv(string s) =>
            s.Contains(',') || s.Contains('"') || s.Contains('\n')
                ? "\"" + s.Replace("\"", "\"\"") + "\""
                : s;
    }
}
