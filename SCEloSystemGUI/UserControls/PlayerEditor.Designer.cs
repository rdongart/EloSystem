namespace SCEloSystemGUI.UserControls
{
    partial class PlayerEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tblLOPnlPlayerEditor = new System.Windows.Forms.TableLayoutPanel();
            this.tblLoPnlCurrentImage = new System.Windows.Forms.TableLayoutPanel();
            this.picBxCurrentImage = new System.Windows.Forms.PictureBox();
            this.chckBxRemoveCurrentImage = new System.Windows.Forms.CheckBox();
            this.txtBxName = new System.Windows.Forms.TextBox();
            this.lbHeading = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tblLoPnlAliases = new System.Windows.Forms.TableLayoutPanel();
            this.txtBxAlias = new System.Windows.Forms.TextBox();
            this.btnAddAlias = new System.Windows.Forms.Button();
            this.btnRemoveAlias = new System.Windows.Forms.Button();
            this.lstViewAliases = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numUDStartRating = new System.Windows.Forms.NumericUpDown();
            this.tblLoPnlImage = new System.Windows.Forms.TableLayoutPanel();
            this.lbFileName = new System.Windows.Forms.Label();
            this.btnRemoveImage = new System.Windows.Forms.Button();
            this.txtBxIRLName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tblLoPnlBirthDate = new System.Windows.Forms.TableLayoutPanel();
            this.chkBxShowDateTimeAdder = new System.Windows.Forms.CheckBox();
            this.dateTimePickerBirthDate = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.txtBxFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.rdBtnAddNew = new System.Windows.Forms.RadioButton();
            this.rdBtnEdit = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lbCurrentImage = new System.Windows.Forms.Label();
            this.tblLOPnlPlayerEditor.SuspendLayout();
            this.tblLoPnlCurrentImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCurrentImage)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblLoPnlAliases.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDStartRating)).BeginInit();
            this.tblLoPnlImage.SuspendLayout();
            this.tblLoPnlBirthDate.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblLOPnlPlayerEditor
            // 
            this.tblLOPnlPlayerEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblLOPnlPlayerEditor.ColumnCount = 7;
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 420F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 240F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlPlayerEditor.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerEditor.Controls.Add(this.tblLoPnlCurrentImage, 4, 5);
            this.tblLOPnlPlayerEditor.Controls.Add(this.txtBxName, 4, 1);
            this.tblLOPnlPlayerEditor.Controls.Add(this.lbHeading, 0, 0);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label1, 3, 1);
            this.tblLOPnlPlayerEditor.Controls.Add(this.btnAdd, 4, 10);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tableLayoutPanel2, 3, 6);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label3, 3, 4);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label4, 3, 7);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label5, 3, 8);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label6, 3, 9);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tblLoPnlAliases, 4, 4);
            this.tblLOPnlPlayerEditor.Controls.Add(this.numUDStartRating, 4, 9);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tblLoPnlImage, 4, 6);
            this.tblLOPnlPlayerEditor.Controls.Add(this.txtBxIRLName, 4, 2);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label7, 3, 2);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label8, 3, 3);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tblLoPnlBirthDate, 4, 3);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tableLayoutPanel5, 1, 2);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label9, 0, 1);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label10, 0, 2);
            this.tblLOPnlPlayerEditor.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.tblLOPnlPlayerEditor.Controls.Add(this.label11, 0, 3);
            this.tblLOPnlPlayerEditor.Controls.Add(this.lbCurrentImage, 3, 5);
            this.tblLOPnlPlayerEditor.Location = new System.Drawing.Point(0, 0);
            this.tblLOPnlPlayerEditor.Margin = new System.Windows.Forms.Padding(0);
            this.tblLOPnlPlayerEditor.Name = "tblLOPnlPlayerEditor";
            this.tblLOPnlPlayerEditor.RowCount = 13;
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLOPnlPlayerEditor.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLOPnlPlayerEditor.Size = new System.Drawing.Size(950, 490);
            this.tblLOPnlPlayerEditor.TabIndex = 0;
            // 
            // tblLoPnlCurrentImage
            // 
            this.tblLoPnlCurrentImage.ColumnCount = 2;
            this.tblLoPnlCurrentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlCurrentImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLoPnlCurrentImage.Controls.Add(this.picBxCurrentImage, 0, 0);
            this.tblLoPnlCurrentImage.Controls.Add(this.chckBxRemoveCurrentImage, 1, 0);
            this.tblLoPnlCurrentImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlCurrentImage.Location = new System.Drawing.Point(693, 243);
            this.tblLoPnlCurrentImage.Name = "tblLoPnlCurrentImage";
            this.tblLoPnlCurrentImage.RowCount = 1;
            this.tblLoPnlCurrentImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlCurrentImage.Size = new System.Drawing.Size(234, 54);
            this.tblLoPnlCurrentImage.TabIndex = 29;
            // 
            // picBxCurrentImage
            // 
            this.picBxCurrentImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBxCurrentImage.Location = new System.Drawing.Point(0, 0);
            this.picBxCurrentImage.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.picBxCurrentImage.Name = "picBxCurrentImage";
            this.picBxCurrentImage.Size = new System.Drawing.Size(203, 54);
            this.picBxCurrentImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBxCurrentImage.TabIndex = 10;
            this.picBxCurrentImage.TabStop = false;
            // 
            // chckBxRemoveCurrentImage
            // 
            this.chckBxRemoveCurrentImage.AutoSize = true;
            this.chckBxRemoveCurrentImage.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chckBxRemoveCurrentImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chckBxRemoveCurrentImage.Enabled = false;
            this.chckBxRemoveCurrentImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chckBxRemoveCurrentImage.Location = new System.Drawing.Point(209, 18);
            this.chckBxRemoveCurrentImage.Name = "chckBxRemoveCurrentImage";
            this.chckBxRemoveCurrentImage.Size = new System.Drawing.Size(22, 33);
            this.chckBxRemoveCurrentImage.TabIndex = 11;
            this.chckBxRemoveCurrentImage.Text = " X";
            this.chckBxRemoveCurrentImage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chckBxRemoveCurrentImage.UseVisualStyleBackColor = true;
            this.chckBxRemoveCurrentImage.CheckedChanged += new System.EventHandler(this.chckBxRemoveCurrentImage_CheckedChanged);
            // 
            // txtBxName
            // 
            this.txtBxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxName.Location = new System.Drawing.Point(696, 33);
            this.txtBxName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxName.Name = "txtBxName";
            this.txtBxName.Size = new System.Drawing.Size(228, 23);
            this.txtBxName.TabIndex = 1;
            this.txtBxName.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            // 
            // lbHeading
            // 
            this.lbHeading.AutoSize = true;
            this.tblLOPnlPlayerEditor.SetColumnSpan(this.lbHeading, 5);
            this.lbHeading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHeading.Location = new System.Drawing.Point(3, 0);
            this.lbHeading.Name = "lbHeading";
            this.lbHeading.Size = new System.Drawing.Size(924, 30);
            this.lbHeading.TabIndex = 18;
            this.lbHeading.Text = "[missing]";
            this.lbHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(568, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(804, 434);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(6, 14, 6, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 32);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "A&dd to system";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnBrowse, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(565, 300);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(125, 30);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 30);
            this.label2.TabIndex = 23;
            this.label2.Text = "Image";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(59, 4);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(62, 22);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(568, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 120);
            this.label3.TabIndex = 22;
            this.label3.Text = "Aliases";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(568, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 30);
            this.label4.TabIndex = 24;
            this.label4.Text = "Country";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(568, 360);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 30);
            this.label5.TabIndex = 25;
            this.label5.Text = "Team";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(568, 390);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 30);
            this.label6.TabIndex = 26;
            this.label6.Text = "Total rating";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLoPnlAliases
            // 
            this.tblLoPnlAliases.ColumnCount = 2;
            this.tblLoPnlAliases.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlAliases.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLoPnlAliases.Controls.Add(this.txtBxAlias, 0, 1);
            this.tblLoPnlAliases.Controls.Add(this.btnAddAlias, 1, 1);
            this.tblLoPnlAliases.Controls.Add(this.btnRemoveAlias, 1, 0);
            this.tblLoPnlAliases.Controls.Add(this.lstViewAliases, 0, 0);
            this.tblLoPnlAliases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlAliases.Location = new System.Drawing.Point(693, 123);
            this.tblLoPnlAliases.Name = "tblLoPnlAliases";
            this.tblLoPnlAliases.RowCount = 2;
            this.tblLoPnlAliases.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlAliases.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tblLoPnlAliases.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlAliases.Size = new System.Drawing.Size(234, 114);
            this.tblLoPnlAliases.TabIndex = 6;
            // 
            // txtBxAlias
            // 
            this.txtBxAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxAlias.Location = new System.Drawing.Point(3, 88);
            this.txtBxAlias.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
            this.txtBxAlias.Name = "txtBxAlias";
            this.txtBxAlias.Size = new System.Drawing.Size(200, 23);
            this.txtBxAlias.TabIndex = 7;
            this.txtBxAlias.TextChanged += new System.EventHandler(this.txtBxAlias_TextChanged);
            // 
            // btnAddAlias
            // 
            this.btnAddAlias.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnAddAlias.Enabled = false;
            this.btnAddAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAlias.Location = new System.Drawing.Point(210, 91);
            this.btnAddAlias.Margin = new System.Windows.Forms.Padding(4, 4, 3, 1);
            this.btnAddAlias.Name = "btnAddAlias";
            this.btnAddAlias.Size = new System.Drawing.Size(21, 22);
            this.btnAddAlias.TabIndex = 8;
            this.btnAddAlias.Text = "+";
            this.btnAddAlias.UseVisualStyleBackColor = true;
            this.btnAddAlias.Click += new System.EventHandler(this.btnAddAlias_Click);
            // 
            // btnRemoveAlias
            // 
            this.btnRemoveAlias.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRemoveAlias.Enabled = false;
            this.btnRemoveAlias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveAlias.Location = new System.Drawing.Point(210, 58);
            this.btnRemoveAlias.Margin = new System.Windows.Forms.Padding(4, 4, 3, 4);
            this.btnRemoveAlias.Name = "btnRemoveAlias";
            this.btnRemoveAlias.Size = new System.Drawing.Size(21, 22);
            this.btnRemoveAlias.TabIndex = 16;
            this.btnRemoveAlias.Text = "-";
            this.btnRemoveAlias.UseVisualStyleBackColor = true;
            this.btnRemoveAlias.Click += new System.EventHandler(this.btnRemoveAlias_Click);
            // 
            // lstViewAliases
            // 
            this.lstViewAliases.AutoArrange = false;
            this.lstViewAliases.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstViewAliases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstViewAliases.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstViewAliases.FullRowSelect = true;
            this.lstViewAliases.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstViewAliases.HideSelection = false;
            this.lstViewAliases.Location = new System.Drawing.Point(0, 0);
            this.lstViewAliases.Margin = new System.Windows.Forms.Padding(0);
            this.lstViewAliases.MultiSelect = false;
            this.lstViewAliases.Name = "lstViewAliases";
            this.lstViewAliases.ShowGroups = false;
            this.lstViewAliases.Size = new System.Drawing.Size(206, 84);
            this.lstViewAliases.TabIndex = 15;
            this.lstViewAliases.UseCompatibleStateImageBehavior = false;
            this.lstViewAliases.View = System.Windows.Forms.View.Details;
            this.lstViewAliases.SelectedIndexChanged += new System.EventHandler(this.lstViewAliases_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 184;
            // 
            // numUDStartRating
            // 
            this.numUDStartRating.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numUDStartRating.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUDStartRating.Location = new System.Drawing.Point(696, 393);
            this.numUDStartRating.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.numUDStartRating.Name = "numUDStartRating";
            this.numUDStartRating.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numUDStartRating.Size = new System.Drawing.Size(228, 23);
            this.numUDStartRating.TabIndex = 13;
            this.numUDStartRating.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUDStartRating.ThousandsSeparator = true;
            // 
            // tblLoPnlImage
            // 
            this.tblLoPnlImage.ColumnCount = 2;
            this.tblLoPnlImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlImage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tblLoPnlImage.Controls.Add(this.lbFileName, 0, 0);
            this.tblLoPnlImage.Controls.Add(this.btnRemoveImage, 1, 0);
            this.tblLoPnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlImage.Location = new System.Drawing.Point(693, 303);
            this.tblLoPnlImage.Name = "tblLoPnlImage";
            this.tblLoPnlImage.RowCount = 1;
            this.tblLoPnlImage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlImage.Size = new System.Drawing.Size(234, 24);
            this.tblLoPnlImage.TabIndex = 8;
            // 
            // lbFileName
            // 
            this.lbFileName.AutoEllipsis = true;
            this.lbFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileName.Location = new System.Drawing.Point(0, 3);
            this.lbFileName.Margin = new System.Windows.Forms.Padding(0, 3, 6, 3);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(200, 18);
            this.lbFileName.TabIndex = 20;
            this.lbFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbFileName.TextChanged += new System.EventHandler(this.lbFileName_TextChanged);
            // 
            // btnRemoveImage
            // 
            this.btnRemoveImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRemoveImage.Enabled = false;
            this.btnRemoveImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemoveImage.Location = new System.Drawing.Point(210, 1);
            this.btnRemoveImage.Margin = new System.Windows.Forms.Padding(4, 1, 3, 1);
            this.btnRemoveImage.Name = "btnRemoveImage";
            this.btnRemoveImage.Size = new System.Drawing.Size(21, 22);
            this.btnRemoveImage.TabIndex = 17;
            this.btnRemoveImage.Text = "x";
            this.btnRemoveImage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRemoveImage.UseVisualStyleBackColor = true;
            this.btnRemoveImage.Click += new System.EventHandler(this.btnRemoveImage_Click);
            // 
            // txtBxIRLName
            // 
            this.txtBxIRLName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxIRLName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxIRLName.Location = new System.Drawing.Point(696, 63);
            this.txtBxIRLName.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxIRLName.Name = "txtBxIRLName";
            this.txtBxIRLName.Size = new System.Drawing.Size(228, 23);
            this.txtBxIRLName.TabIndex = 2;
            this.txtBxIRLName.TextChanged += new System.EventHandler(this.txtBxIRLName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(568, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 30);
            this.label7.TabIndex = 20;
            this.label7.Text = "IRL-name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(568, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 30);
            this.label8.TabIndex = 21;
            this.label8.Text = "Birth date";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblLoPnlBirthDate
            // 
            this.tblLoPnlBirthDate.ColumnCount = 2;
            this.tblLoPnlBirthDate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLoPnlBirthDate.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlBirthDate.Controls.Add(this.chkBxShowDateTimeAdder, 0, 0);
            this.tblLoPnlBirthDate.Controls.Add(this.dateTimePickerBirthDate, 1, 0);
            this.tblLoPnlBirthDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLoPnlBirthDate.Location = new System.Drawing.Point(696, 93);
            this.tblLoPnlBirthDate.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.tblLoPnlBirthDate.Name = "tblLoPnlBirthDate";
            this.tblLoPnlBirthDate.RowCount = 1;
            this.tblLoPnlBirthDate.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLoPnlBirthDate.Size = new System.Drawing.Size(228, 24);
            this.tblLoPnlBirthDate.TabIndex = 3;
            // 
            // chkBxShowDateTimeAdder
            // 
            this.chkBxShowDateTimeAdder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBxShowDateTimeAdder.Location = new System.Drawing.Point(3, 0);
            this.chkBxShowDateTimeAdder.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.chkBxShowDateTimeAdder.Name = "chkBxShowDateTimeAdder";
            this.chkBxShowDateTimeAdder.Size = new System.Drawing.Size(17, 24);
            this.chkBxShowDateTimeAdder.TabIndex = 4;
            this.chkBxShowDateTimeAdder.UseVisualStyleBackColor = true;
            this.chkBxShowDateTimeAdder.CheckedChanged += new System.EventHandler(this.chkBxShowDateTimeAdder_CheckedChanged);
            // 
            // dateTimePickerBirthDate
            // 
            this.dateTimePickerBirthDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePickerBirthDate.Location = new System.Drawing.Point(23, 0);
            this.dateTimePickerBirthDate.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.dateTimePickerBirthDate.Name = "dateTimePickerBirthDate";
            this.dateTimePickerBirthDate.Size = new System.Drawing.Size(205, 20);
            this.dateTimePickerBirthDate.TabIndex = 5;
            this.dateTimePickerBirthDate.Visible = false;
            this.dateTimePickerBirthDate.ValueChanged += new System.EventHandler(this.dateTimePickerBirthDate_ValueChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel5.Controls.Add(this.txtBxFilter, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.btnSearch, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(125, 60);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(420, 30);
            this.tableLayoutPanel5.TabIndex = 27;
            // 
            // txtBxFilter
            // 
            this.txtBxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxFilter.Enabled = false;
            this.txtBxFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBxFilter.Location = new System.Drawing.Point(6, 3);
            this.txtBxFilter.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.txtBxFilter.Name = "txtBxFilter";
            this.txtBxFilter.Size = new System.Drawing.Size(374, 23);
            this.txtBxFilter.TabIndex = 1;
            this.txtBxFilter.TextChanged += new System.EventHandler(this.txtBxName_TextChanged);
            this.txtBxFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBxFilter_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::SCEloSystemGUI.Properties.Resources.Search;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.Enabled = false;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(389, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(28, 24);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 30);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 30);
            this.label9.TabIndex = 19;
            this.label9.Text = "Select function";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(119, 30);
            this.label10.TabIndex = 19;
            this.label10.Text = "Filter";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.rdBtnAddNew, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.rdBtnEdit, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(125, 30);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(420, 30);
            this.tableLayoutPanel6.TabIndex = 28;
            // 
            // rdBtnAddNew
            // 
            this.rdBtnAddNew.AutoSize = true;
            this.rdBtnAddNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnAddNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnAddNew.Location = new System.Drawing.Point(6, 3);
            this.rdBtnAddNew.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.rdBtnAddNew.Name = "rdBtnAddNew";
            this.rdBtnAddNew.Size = new System.Drawing.Size(198, 24);
            this.rdBtnAddNew.TabIndex = 0;
            this.rdBtnAddNew.TabStop = true;
            this.rdBtnAddNew.Text = "Add new";
            this.rdBtnAddNew.UseVisualStyleBackColor = true;
            this.rdBtnAddNew.CheckedChanged += new System.EventHandler(this.rdBtnAddNew_CheckedChanged);
            // 
            // rdBtnEdit
            // 
            this.rdBtnEdit.AutoSize = true;
            this.rdBtnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdBtnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnEdit.Location = new System.Drawing.Point(216, 3);
            this.rdBtnEdit.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.rdBtnEdit.Name = "rdBtnEdit";
            this.rdBtnEdit.Size = new System.Drawing.Size(198, 24);
            this.rdBtnEdit.TabIndex = 0;
            this.rdBtnEdit.TabStop = true;
            this.rdBtnEdit.Text = "Edit";
            this.rdBtnEdit.UseVisualStyleBackColor = true;
            this.rdBtnEdit.CheckedChanged += new System.EventHandler(this.rdBtnEdit_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 90);
            this.label11.Name = "label11";
            this.tblLOPnlPlayerEditor.SetRowSpan(this.label11, 5);
            this.label11.Size = new System.Drawing.Size(119, 270);
            this.label11.TabIndex = 19;
            this.label11.Text = "Select player to edit";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbCurrentImage
            // 
            this.lbCurrentImage.AutoSize = true;
            this.lbCurrentImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCurrentImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentImage.Location = new System.Drawing.Point(568, 240);
            this.lbCurrentImage.Name = "lbCurrentImage";
            this.lbCurrentImage.Size = new System.Drawing.Size(119, 60);
            this.lbCurrentImage.TabIndex = 24;
            this.lbCurrentImage.Text = "Current Image";
            this.lbCurrentImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tblLOPnlPlayerEditor);
            this.Name = "PlayerEditor";
            this.Size = new System.Drawing.Size(950, 490);
            this.tblLOPnlPlayerEditor.ResumeLayout(false);
            this.tblLOPnlPlayerEditor.PerformLayout();
            this.tblLoPnlCurrentImage.ResumeLayout(false);
            this.tblLoPnlCurrentImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBxCurrentImage)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblLoPnlAliases.ResumeLayout(false);
            this.tblLoPnlAliases.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUDStartRating)).EndInit();
            this.tblLoPnlImage.ResumeLayout(false);
            this.tblLoPnlBirthDate.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblLOPnlPlayerEditor;
        private System.Windows.Forms.TextBox txtBxName;
        private System.Windows.Forms.Label lbHeading;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlAliases;
        private System.Windows.Forms.TextBox txtBxAlias;
        private System.Windows.Forms.Button btnAddAlias;
        private System.Windows.Forms.Button btnRemoveAlias;
        private System.Windows.Forms.ListView lstViewAliases;
        private System.Windows.Forms.NumericUpDown numUDStartRating;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlImage;
        private System.Windows.Forms.Button btnRemoveImage;
        private System.Windows.Forms.TextBox txtBxIRLName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlBirthDate;
        private System.Windows.Forms.CheckBox chkBxShowDateTimeAdder;
        private System.Windows.Forms.DateTimePicker dateTimePickerBirthDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TextBox txtBxFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.RadioButton rdBtnAddNew;
        private System.Windows.Forms.RadioButton rdBtnEdit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbCurrentImage;
        private System.Windows.Forms.TableLayoutPanel tblLoPnlCurrentImage;
        private System.Windows.Forms.PictureBox picBxCurrentImage;
        private System.Windows.Forms.CheckBox chckBxRemoveCurrentImage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
