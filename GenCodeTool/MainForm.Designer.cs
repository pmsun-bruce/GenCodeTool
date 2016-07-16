namespace NFramework.GenCodeTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SettingPanel = new System.Windows.Forms.Panel();
            this.ChooseTargetFolderBtn = new System.Windows.Forms.Button();
            this.ChooseReferenceFolderBtn = new System.Windows.Forms.Button();
            this.ProjectDisplayNameLbl = new System.Windows.Forms.Label();
            this.GenTargetPathLbl = new System.Windows.Forms.Label();
            this.ReferenceFolderLbl = new System.Windows.Forms.Label();
            this.NamespaceLbl = new System.Windows.Forms.Label();
            this.ProjectNameLbl = new System.Windows.Forms.Label();
            this.ClearTargetLbl = new System.Windows.Forms.Label();
            this.SettingNameLbl = new System.Windows.Forms.Label();
            this.GenSettingNameTxt = new System.Windows.Forms.TextBox();
            this.SaveSettingBtn = new System.Windows.Forms.Button();
            this.IsClearTargetFolderChk = new System.Windows.Forms.CheckBox();
            this.GenTargetFolderTxt = new System.Windows.Forms.TextBox();
            this.CodeInfoGetterLbl = new System.Windows.Forms.Label();
            this.CodeTemplateCbx = new System.Windows.Forms.ComboBox();
            this.CodeTemplateBSource = new System.Windows.Forms.BindingSource(this.components);
            this.ConnectionStringLbl = new System.Windows.Forms.Label();
            this.CodeTemplateLbl = new System.Windows.Forms.Label();
            this.GenSettingLbl = new System.Windows.Forms.Label();
            this.DBTypeLbl = new System.Windows.Forms.Label();
            this.GenSettingCbx = new System.Windows.Forms.ComboBox();
            this.GenSettingBSource = new System.Windows.Forms.BindingSource(this.components);
            this.CodeInfoGetterCbx = new System.Windows.Forms.ComboBox();
            this.CodeInfoGetterBSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReferenceFolderTxt = new System.Windows.Forms.TextBox();
            this.NamespaceTxt = new System.Windows.Forms.TextBox();
            this.ProjectDisplayNameTxt = new System.Windows.Forms.TextBox();
            this.ProjectNameTxt = new System.Windows.Forms.TextBox();
            this.DBInfoGetterCbx = new System.Windows.Forms.ComboBox();
            this.DBInfoGetterBSource = new System.Windows.Forms.BindingSource(this.components);
            this.ConnectionStringTxt = new System.Windows.Forms.TextBox();
            this.ConnectDbBtn = new System.Windows.Forms.Button();
            this.StartGenBtn = new System.Windows.Forms.Button();
            this.TablePanel = new System.Windows.Forms.Panel();
            this.ColumnInfoGrid = new System.Windows.Forms.DataGridView();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isPKDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isFKDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fKNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKTableNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fKColumnNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isAutoNumDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.sqlTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxLengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precisionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scaleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isNullableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isUniqueDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.maxValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isGenSearchResultDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isGenSearchConditionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isGenInputDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnInfoBSource = new System.Windows.Forms.BindingSource(this.components);
            this.ColumnTitlePanel = new System.Windows.Forms.Panel();
            this.ColumnInfoLbl = new System.Windows.Forms.Label();
            this.TableInfoGrid = new System.Windows.Forms.DataGridView();
            this.isGenDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.namespaceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isGenUIDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TableInfoBSource = new System.Windows.Forms.BindingSource(this.components);
            this.TableTitlePancel = new System.Windows.Forms.Panel();
            this.TableInfoLbl = new System.Windows.Forms.Label();
            this.ReferenceFbd = new System.Windows.Forms.FolderBrowserDialog();
            this.GenTargetFbd = new System.Windows.Forms.FolderBrowserDialog();
            this.SettingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CodeTemplateBSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenSettingBSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodeInfoGetterBSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBInfoGetterBSource)).BeginInit();
            this.TablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnInfoGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnInfoBSource)).BeginInit();
            this.ColumnTitlePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableInfoGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableInfoBSource)).BeginInit();
            this.TableTitlePancel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingPanel
            // 
            this.SettingPanel.Controls.Add(this.ChooseTargetFolderBtn);
            this.SettingPanel.Controls.Add(this.ChooseReferenceFolderBtn);
            this.SettingPanel.Controls.Add(this.ProjectDisplayNameLbl);
            this.SettingPanel.Controls.Add(this.GenTargetPathLbl);
            this.SettingPanel.Controls.Add(this.ReferenceFolderLbl);
            this.SettingPanel.Controls.Add(this.NamespaceLbl);
            this.SettingPanel.Controls.Add(this.ProjectNameLbl);
            this.SettingPanel.Controls.Add(this.ClearTargetLbl);
            this.SettingPanel.Controls.Add(this.SettingNameLbl);
            this.SettingPanel.Controls.Add(this.GenSettingNameTxt);
            this.SettingPanel.Controls.Add(this.SaveSettingBtn);
            this.SettingPanel.Controls.Add(this.IsClearTargetFolderChk);
            this.SettingPanel.Controls.Add(this.GenTargetFolderTxt);
            this.SettingPanel.Controls.Add(this.CodeInfoGetterLbl);
            this.SettingPanel.Controls.Add(this.CodeTemplateCbx);
            this.SettingPanel.Controls.Add(this.ConnectionStringLbl);
            this.SettingPanel.Controls.Add(this.CodeTemplateLbl);
            this.SettingPanel.Controls.Add(this.GenSettingLbl);
            this.SettingPanel.Controls.Add(this.DBTypeLbl);
            this.SettingPanel.Controls.Add(this.GenSettingCbx);
            this.SettingPanel.Controls.Add(this.CodeInfoGetterCbx);
            this.SettingPanel.Controls.Add(this.ReferenceFolderTxt);
            this.SettingPanel.Controls.Add(this.NamespaceTxt);
            this.SettingPanel.Controls.Add(this.ProjectDisplayNameTxt);
            this.SettingPanel.Controls.Add(this.ProjectNameTxt);
            this.SettingPanel.Controls.Add(this.DBInfoGetterCbx);
            this.SettingPanel.Controls.Add(this.ConnectionStringTxt);
            this.SettingPanel.Controls.Add(this.ConnectDbBtn);
            this.SettingPanel.Controls.Add(this.StartGenBtn);
            this.SettingPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SettingPanel.Location = new System.Drawing.Point(0, 0);
            this.SettingPanel.Name = "SettingPanel";
            this.SettingPanel.Size = new System.Drawing.Size(301, 592);
            this.SettingPanel.TabIndex = 0;
            // 
            // ChooseTargetFolderBtn
            // 
            this.ChooseTargetFolderBtn.Location = new System.Drawing.Point(255, 278);
            this.ChooseTargetFolderBtn.Name = "ChooseTargetFolderBtn";
            this.ChooseTargetFolderBtn.Size = new System.Drawing.Size(40, 23);
            this.ChooseTargetFolderBtn.TabIndex = 30;
            this.ChooseTargetFolderBtn.Text = "浏览";
            this.ChooseTargetFolderBtn.UseVisualStyleBackColor = true;
            this.ChooseTargetFolderBtn.Click += new System.EventHandler(this.ChooseTargetFolderBtn_Click);
            // 
            // ChooseReferenceFolderBtn
            // 
            this.ChooseReferenceFolderBtn.Location = new System.Drawing.Point(255, 251);
            this.ChooseReferenceFolderBtn.Name = "ChooseReferenceFolderBtn";
            this.ChooseReferenceFolderBtn.Size = new System.Drawing.Size(40, 23);
            this.ChooseReferenceFolderBtn.TabIndex = 29;
            this.ChooseReferenceFolderBtn.Text = "浏览";
            this.ChooseReferenceFolderBtn.UseVisualStyleBackColor = true;
            this.ChooseReferenceFolderBtn.Click += new System.EventHandler(this.ChooseReferenceFolderBtn_Click);
            // 
            // ProjectDisplayNameLbl
            // 
            this.ProjectDisplayNameLbl.AutoSize = true;
            this.ProjectDisplayNameLbl.Location = new System.Drawing.Point(4, 203);
            this.ProjectDisplayNameLbl.Name = "ProjectDisplayNameLbl";
            this.ProjectDisplayNameLbl.Size = new System.Drawing.Size(53, 12);
            this.ProjectDisplayNameLbl.TabIndex = 28;
            this.ProjectDisplayNameLbl.Text = "项目说明";
            // 
            // GenTargetPathLbl
            // 
            this.GenTargetPathLbl.AutoSize = true;
            this.GenTargetPathLbl.Location = new System.Drawing.Point(2, 283);
            this.GenTargetPathLbl.Name = "GenTargetPathLbl";
            this.GenTargetPathLbl.Size = new System.Drawing.Size(53, 12);
            this.GenTargetPathLbl.TabIndex = 27;
            this.GenTargetPathLbl.Text = "生成路径";
            // 
            // ReferenceFolderLbl
            // 
            this.ReferenceFolderLbl.AutoSize = true;
            this.ReferenceFolderLbl.Location = new System.Drawing.Point(3, 256);
            this.ReferenceFolderLbl.Name = "ReferenceFolderLbl";
            this.ReferenceFolderLbl.Size = new System.Drawing.Size(53, 12);
            this.ReferenceFolderLbl.TabIndex = 26;
            this.ReferenceFolderLbl.Text = "引用路径";
            // 
            // NamespaceLbl
            // 
            this.NamespaceLbl.AutoSize = true;
            this.NamespaceLbl.Location = new System.Drawing.Point(4, 230);
            this.NamespaceLbl.Name = "NamespaceLbl";
            this.NamespaceLbl.Size = new System.Drawing.Size(53, 12);
            this.NamespaceLbl.TabIndex = 25;
            this.NamespaceLbl.Text = "命名空间";
            // 
            // ProjectNameLbl
            // 
            this.ProjectNameLbl.AutoSize = true;
            this.ProjectNameLbl.Location = new System.Drawing.Point(3, 176);
            this.ProjectNameLbl.Name = "ProjectNameLbl";
            this.ProjectNameLbl.Size = new System.Drawing.Size(53, 12);
            this.ProjectNameLbl.TabIndex = 24;
            this.ProjectNameLbl.Text = "项目名称";
            // 
            // ClearTargetLbl
            // 
            this.ClearTargetLbl.AutoSize = true;
            this.ClearTargetLbl.Location = new System.Drawing.Point(4, 306);
            this.ClearTargetLbl.Name = "ClearTargetLbl";
            this.ClearTargetLbl.Size = new System.Drawing.Size(53, 24);
            this.ClearTargetLbl.TabIndex = 23;
            this.ClearTargetLbl.Text = "是否清空\r\n目标目录";
            // 
            // SettingNameLbl
            // 
            this.SettingNameLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SettingNameLbl.AutoSize = true;
            this.SettingNameLbl.Location = new System.Drawing.Point(4, 529);
            this.SettingNameLbl.Name = "SettingNameLbl";
            this.SettingNameLbl.Size = new System.Drawing.Size(53, 12);
            this.SettingNameLbl.TabIndex = 22;
            this.SettingNameLbl.Text = "设置名称";
            // 
            // GenSettingNameTxt
            // 
            this.GenSettingNameTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenSettingNameTxt.BackColor = System.Drawing.SystemColors.Window;
            this.GenSettingNameTxt.Location = new System.Drawing.Point(79, 526);
            this.GenSettingNameTxt.Name = "GenSettingNameTxt";
            this.GenSettingNameTxt.Size = new System.Drawing.Size(138, 21);
            this.GenSettingNameTxt.TabIndex = 21;
            // 
            // SaveSettingBtn
            // 
            this.SaveSettingBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveSettingBtn.Location = new System.Drawing.Point(223, 526);
            this.SaveSettingBtn.Name = "SaveSettingBtn";
            this.SaveSettingBtn.Size = new System.Drawing.Size(72, 23);
            this.SaveSettingBtn.TabIndex = 20;
            this.SaveSettingBtn.Text = "保存配置";
            this.SaveSettingBtn.UseVisualStyleBackColor = true;
            this.SaveSettingBtn.Click += new System.EventHandler(this.SaveSettingBtn_Click);
            // 
            // IsClearTargetFolderChk
            // 
            this.IsClearTargetFolderChk.AutoSize = true;
            this.IsClearTargetFolderChk.Location = new System.Drawing.Point(79, 306);
            this.IsClearTargetFolderChk.Name = "IsClearTargetFolderChk";
            this.IsClearTargetFolderChk.Size = new System.Drawing.Size(15, 14);
            this.IsClearTargetFolderChk.TabIndex = 19;
            this.IsClearTargetFolderChk.UseVisualStyleBackColor = true;
            // 
            // GenTargetFolderTxt
            // 
            this.GenTargetFolderTxt.BackColor = System.Drawing.SystemColors.Window;
            this.GenTargetFolderTxt.Location = new System.Drawing.Point(79, 279);
            this.GenTargetFolderTxt.Name = "GenTargetFolderTxt";
            this.GenTargetFolderTxt.Size = new System.Drawing.Size(170, 21);
            this.GenTargetFolderTxt.TabIndex = 18;
            // 
            // CodeInfoGetterLbl
            // 
            this.CodeInfoGetterLbl.AutoSize = true;
            this.CodeInfoGetterLbl.Location = new System.Drawing.Point(4, 124);
            this.CodeInfoGetterLbl.Name = "CodeInfoGetterLbl";
            this.CodeInfoGetterLbl.Size = new System.Drawing.Size(53, 12);
            this.CodeInfoGetterLbl.TabIndex = 17;
            this.CodeInfoGetterLbl.Text = "程序语言";
            // 
            // CodeTemplateCbx
            // 
            this.CodeTemplateCbx.DataSource = this.CodeTemplateBSource;
            this.CodeTemplateCbx.FormattingEnabled = true;
            this.CodeTemplateCbx.Location = new System.Drawing.Point(79, 146);
            this.CodeTemplateCbx.Name = "CodeTemplateCbx";
            this.CodeTemplateCbx.Size = new System.Drawing.Size(216, 20);
            this.CodeTemplateCbx.TabIndex = 16;
            // 
            // ConnectionStringLbl
            // 
            this.ConnectionStringLbl.AutoSize = true;
            this.ConnectionStringLbl.Location = new System.Drawing.Point(3, 68);
            this.ConnectionStringLbl.Name = "ConnectionStringLbl";
            this.ConnectionStringLbl.Size = new System.Drawing.Size(65, 12);
            this.ConnectionStringLbl.TabIndex = 15;
            this.ConnectionStringLbl.Text = "连接字符串";
            // 
            // CodeTemplateLbl
            // 
            this.CodeTemplateLbl.AutoSize = true;
            this.CodeTemplateLbl.Location = new System.Drawing.Point(4, 150);
            this.CodeTemplateLbl.Name = "CodeTemplateLbl";
            this.CodeTemplateLbl.Size = new System.Drawing.Size(65, 12);
            this.CodeTemplateLbl.TabIndex = 14;
            this.CodeTemplateLbl.Text = "使用的模板";
            // 
            // GenSettingLbl
            // 
            this.GenSettingLbl.AutoSize = true;
            this.GenSettingLbl.Location = new System.Drawing.Point(3, 16);
            this.GenSettingLbl.Name = "GenSettingLbl";
            this.GenSettingLbl.Size = new System.Drawing.Size(53, 12);
            this.GenSettingLbl.TabIndex = 13;
            this.GenSettingLbl.Text = "已有配置";
            // 
            // DBTypeLbl
            // 
            this.DBTypeLbl.AutoSize = true;
            this.DBTypeLbl.Location = new System.Drawing.Point(4, 42);
            this.DBTypeLbl.Name = "DBTypeLbl";
            this.DBTypeLbl.Size = new System.Drawing.Size(65, 12);
            this.DBTypeLbl.TabIndex = 12;
            this.DBTypeLbl.Text = "数据库类型";
            // 
            // GenSettingCbx
            // 
            this.GenSettingCbx.DataSource = this.GenSettingBSource;
            this.GenSettingCbx.DisplayMember = "SettingName";
            this.GenSettingCbx.FormattingEnabled = true;
            this.GenSettingCbx.Location = new System.Drawing.Point(79, 12);
            this.GenSettingCbx.Name = "GenSettingCbx";
            this.GenSettingCbx.Size = new System.Drawing.Size(216, 20);
            this.GenSettingCbx.TabIndex = 11;
            this.GenSettingCbx.ValueMember = "SettingName";
            this.GenSettingCbx.SelectedIndexChanged += new System.EventHandler(this.GenSettingCbx_SelectedIndexChanged);
            // 
            // GenSettingBSource
            // 
            this.GenSettingBSource.DataSource = typeof(NFramework.GenCodeTool.Entity.GenSetting);
            // 
            // CodeInfoGetterCbx
            // 
            this.CodeInfoGetterCbx.DataSource = this.CodeInfoGetterBSource;
            this.CodeInfoGetterCbx.DisplayMember = "GetterName";
            this.CodeInfoGetterCbx.FormattingEnabled = true;
            this.CodeInfoGetterCbx.Location = new System.Drawing.Point(79, 120);
            this.CodeInfoGetterCbx.Name = "CodeInfoGetterCbx";
            this.CodeInfoGetterCbx.Size = new System.Drawing.Size(216, 20);
            this.CodeInfoGetterCbx.TabIndex = 10;
            this.CodeInfoGetterCbx.ValueMember = "GetterName";
            // 
            // CodeInfoGetterBSource
            // 
            this.CodeInfoGetterBSource.DataSource = typeof(NFramework.GenCodeTool.CodeInfoGetter.ICodeInfoGetter);
            // 
            // ReferenceFolderTxt
            // 
            this.ReferenceFolderTxt.BackColor = System.Drawing.SystemColors.Window;
            this.ReferenceFolderTxt.Location = new System.Drawing.Point(79, 252);
            this.ReferenceFolderTxt.Name = "ReferenceFolderTxt";
            this.ReferenceFolderTxt.Size = new System.Drawing.Size(170, 21);
            this.ReferenceFolderTxt.TabIndex = 9;
            // 
            // NamespaceTxt
            // 
            this.NamespaceTxt.BackColor = System.Drawing.SystemColors.Window;
            this.NamespaceTxt.Location = new System.Drawing.Point(79, 226);
            this.NamespaceTxt.Name = "NamespaceTxt";
            this.NamespaceTxt.Size = new System.Drawing.Size(216, 21);
            this.NamespaceTxt.TabIndex = 8;
            // 
            // ProjectDisplayNameTxt
            // 
            this.ProjectDisplayNameTxt.BackColor = System.Drawing.SystemColors.Window;
            this.ProjectDisplayNameTxt.Location = new System.Drawing.Point(79, 199);
            this.ProjectDisplayNameTxt.Name = "ProjectDisplayNameTxt";
            this.ProjectDisplayNameTxt.Size = new System.Drawing.Size(216, 21);
            this.ProjectDisplayNameTxt.TabIndex = 7;
            // 
            // ProjectNameTxt
            // 
            this.ProjectNameTxt.BackColor = System.Drawing.SystemColors.Window;
            this.ProjectNameTxt.Location = new System.Drawing.Point(79, 172);
            this.ProjectNameTxt.Name = "ProjectNameTxt";
            this.ProjectNameTxt.Size = new System.Drawing.Size(216, 21);
            this.ProjectNameTxt.TabIndex = 6;
            // 
            // DBInfoGetterCbx
            // 
            this.DBInfoGetterCbx.DataSource = this.DBInfoGetterBSource;
            this.DBInfoGetterCbx.DisplayMember = "GetterName";
            this.DBInfoGetterCbx.FormattingEnabled = true;
            this.DBInfoGetterCbx.Location = new System.Drawing.Point(79, 38);
            this.DBInfoGetterCbx.Name = "DBInfoGetterCbx";
            this.DBInfoGetterCbx.Size = new System.Drawing.Size(216, 20);
            this.DBInfoGetterCbx.TabIndex = 5;
            this.DBInfoGetterCbx.ValueMember = "GetterName";
            // 
            // DBInfoGetterBSource
            // 
            this.DBInfoGetterBSource.DataSource = typeof(NFramework.GenCodeTool.DBInfoGetter.IDBInfoGetter);
            // 
            // ConnectionStringTxt
            // 
            this.ConnectionStringTxt.BackColor = System.Drawing.SystemColors.Window;
            this.ConnectionStringTxt.Location = new System.Drawing.Point(79, 64);
            this.ConnectionStringTxt.Name = "ConnectionStringTxt";
            this.ConnectionStringTxt.Size = new System.Drawing.Size(216, 21);
            this.ConnectionStringTxt.TabIndex = 4;
            // 
            // ConnectDbBtn
            // 
            this.ConnectDbBtn.Location = new System.Drawing.Point(181, 91);
            this.ConnectDbBtn.Name = "ConnectDbBtn";
            this.ConnectDbBtn.Size = new System.Drawing.Size(114, 23);
            this.ConnectDbBtn.TabIndex = 3;
            this.ConnectDbBtn.Text = "连接数据库";
            this.ConnectDbBtn.UseVisualStyleBackColor = true;
            this.ConnectDbBtn.Click += new System.EventHandler(this.ConnectDbBtn_Click);
            // 
            // StartGenBtn
            // 
            this.StartGenBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StartGenBtn.Location = new System.Drawing.Point(0, 562);
            this.StartGenBtn.Name = "StartGenBtn";
            this.StartGenBtn.Size = new System.Drawing.Size(301, 30);
            this.StartGenBtn.TabIndex = 1;
            this.StartGenBtn.Text = "开始生成";
            this.StartGenBtn.UseVisualStyleBackColor = true;
            this.StartGenBtn.Click += new System.EventHandler(this.StartGenBtn_Click);
            // 
            // TablePanel
            // 
            this.TablePanel.Controls.Add(this.ColumnInfoGrid);
            this.TablePanel.Controls.Add(this.ColumnTitlePanel);
            this.TablePanel.Controls.Add(this.TableInfoGrid);
            this.TablePanel.Controls.Add(this.TableTitlePancel);
            this.TablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablePanel.Location = new System.Drawing.Point(301, 0);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.Size = new System.Drawing.Size(649, 592);
            this.TablePanel.TabIndex = 1;
            // 
            // ColumnInfoGrid
            // 
            this.ColumnInfoGrid.AllowUserToAddRows = false;
            this.ColumnInfoGrid.AllowUserToDeleteRows = false;
            this.ColumnInfoGrid.AutoGenerateColumns = false;
            this.ColumnInfoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ColumnInfoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn1,
            this.commentDataGridViewTextBoxColumn1,
            this.colIdDataGridViewTextBoxColumn,
            this.isPKDataGridViewCheckBoxColumn,
            this.isFKDataGridViewCheckBoxColumn,
            this.fKNameDataGridViewTextBoxColumn,
            this.fKTableNameDataGridViewTextBoxColumn,
            this.fKColumnNameDataGridViewTextBoxColumn,
            this.isAutoNumDataGridViewCheckBoxColumn,
            this.sqlTypeDataGridViewTextBoxColumn,
            this.maxLengthDataGridViewTextBoxColumn,
            this.precisionDataGridViewTextBoxColumn,
            this.scaleDataGridViewTextBoxColumn,
            this.defaultValueDataGridViewTextBoxColumn,
            this.isNullableDataGridViewCheckBoxColumn,
            this.isUniqueDataGridViewCheckBoxColumn,
            this.maxValueDataGridViewTextBoxColumn,
            this.minValueDataGridViewTextBoxColumn,
            this.isGenSearchResultDataGridViewCheckBoxColumn,
            this.isGenSearchConditionDataGridViewCheckBoxColumn,
            this.isGenInputDataGridViewCheckBoxColumn});
            this.ColumnInfoGrid.DataSource = this.ColumnInfoBSource;
            this.ColumnInfoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ColumnInfoGrid.Location = new System.Drawing.Point(0, 280);
            this.ColumnInfoGrid.Name = "ColumnInfoGrid";
            this.ColumnInfoGrid.RowTemplate.Height = 23;
            this.ColumnInfoGrid.Size = new System.Drawing.Size(649, 312);
            this.ColumnInfoGrid.TabIndex = 5;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.Frozen = true;
            this.nameDataGridViewTextBoxColumn1.HeaderText = "字段名称";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn1.Width = 150;
            // 
            // commentDataGridViewTextBoxColumn1
            // 
            this.commentDataGridViewTextBoxColumn1.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn1.HeaderText = "备注";
            this.commentDataGridViewTextBoxColumn1.Name = "commentDataGridViewTextBoxColumn1";
            this.commentDataGridViewTextBoxColumn1.Width = 150;
            // 
            // colIdDataGridViewTextBoxColumn
            // 
            this.colIdDataGridViewTextBoxColumn.DataPropertyName = "ColId";
            this.colIdDataGridViewTextBoxColumn.HeaderText = "字段序号";
            this.colIdDataGridViewTextBoxColumn.Name = "colIdDataGridViewTextBoxColumn";
            this.colIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.colIdDataGridViewTextBoxColumn.Width = 80;
            // 
            // isPKDataGridViewCheckBoxColumn
            // 
            this.isPKDataGridViewCheckBoxColumn.DataPropertyName = "IsPK";
            this.isPKDataGridViewCheckBoxColumn.HeaderText = "是否主键";
            this.isPKDataGridViewCheckBoxColumn.Name = "isPKDataGridViewCheckBoxColumn";
            this.isPKDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isPKDataGridViewCheckBoxColumn.Width = 70;
            // 
            // isFKDataGridViewCheckBoxColumn
            // 
            this.isFKDataGridViewCheckBoxColumn.DataPropertyName = "IsFK";
            this.isFKDataGridViewCheckBoxColumn.HeaderText = "是否外键";
            this.isFKDataGridViewCheckBoxColumn.Name = "isFKDataGridViewCheckBoxColumn";
            this.isFKDataGridViewCheckBoxColumn.Width = 70;
            // 
            // fKNameDataGridViewTextBoxColumn
            // 
            this.fKNameDataGridViewTextBoxColumn.DataPropertyName = "FKName";
            this.fKNameDataGridViewTextBoxColumn.HeaderText = "外键名称";
            this.fKNameDataGridViewTextBoxColumn.Name = "fKNameDataGridViewTextBoxColumn";
            this.fKNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // fKTableNameDataGridViewTextBoxColumn
            // 
            this.fKTableNameDataGridViewTextBoxColumn.DataPropertyName = "FKTableName";
            this.fKTableNameDataGridViewTextBoxColumn.HeaderText = "外键对应表名";
            this.fKTableNameDataGridViewTextBoxColumn.Name = "fKTableNameDataGridViewTextBoxColumn";
            this.fKTableNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // fKColumnNameDataGridViewTextBoxColumn
            // 
            this.fKColumnNameDataGridViewTextBoxColumn.DataPropertyName = "FKColumnName";
            this.fKColumnNameDataGridViewTextBoxColumn.HeaderText = "外键对应字段";
            this.fKColumnNameDataGridViewTextBoxColumn.Name = "fKColumnNameDataGridViewTextBoxColumn";
            this.fKColumnNameDataGridViewTextBoxColumn.Width = 150;
            // 
            // isAutoNumDataGridViewCheckBoxColumn
            // 
            this.isAutoNumDataGridViewCheckBoxColumn.DataPropertyName = "IsAutoNum";
            this.isAutoNumDataGridViewCheckBoxColumn.HeaderText = "是否自增长";
            this.isAutoNumDataGridViewCheckBoxColumn.Name = "isAutoNumDataGridViewCheckBoxColumn";
            this.isAutoNumDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isAutoNumDataGridViewCheckBoxColumn.Width = 70;
            // 
            // sqlTypeDataGridViewTextBoxColumn
            // 
            this.sqlTypeDataGridViewTextBoxColumn.DataPropertyName = "SqlType";
            this.sqlTypeDataGridViewTextBoxColumn.HeaderText = "数据类型";
            this.sqlTypeDataGridViewTextBoxColumn.Name = "sqlTypeDataGridViewTextBoxColumn";
            this.sqlTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.sqlTypeDataGridViewTextBoxColumn.Width = 80;
            // 
            // maxLengthDataGridViewTextBoxColumn
            // 
            this.maxLengthDataGridViewTextBoxColumn.DataPropertyName = "MaxLength";
            this.maxLengthDataGridViewTextBoxColumn.HeaderText = "最大长度";
            this.maxLengthDataGridViewTextBoxColumn.Name = "maxLengthDataGridViewTextBoxColumn";
            this.maxLengthDataGridViewTextBoxColumn.Width = 80;
            // 
            // precisionDataGridViewTextBoxColumn
            // 
            this.precisionDataGridViewTextBoxColumn.DataPropertyName = "Precision";
            this.precisionDataGridViewTextBoxColumn.HeaderText = "有效位数";
            this.precisionDataGridViewTextBoxColumn.Name = "precisionDataGridViewTextBoxColumn";
            this.precisionDataGridViewTextBoxColumn.Width = 80;
            // 
            // scaleDataGridViewTextBoxColumn
            // 
            this.scaleDataGridViewTextBoxColumn.DataPropertyName = "Scale";
            this.scaleDataGridViewTextBoxColumn.HeaderText = "有效小数位";
            this.scaleDataGridViewTextBoxColumn.Name = "scaleDataGridViewTextBoxColumn";
            this.scaleDataGridViewTextBoxColumn.Width = 120;
            // 
            // defaultValueDataGridViewTextBoxColumn
            // 
            this.defaultValueDataGridViewTextBoxColumn.DataPropertyName = "DefaultValue";
            this.defaultValueDataGridViewTextBoxColumn.HeaderText = "默认值";
            this.defaultValueDataGridViewTextBoxColumn.Name = "defaultValueDataGridViewTextBoxColumn";
            // 
            // isNullableDataGridViewCheckBoxColumn
            // 
            this.isNullableDataGridViewCheckBoxColumn.DataPropertyName = "IsNullable";
            this.isNullableDataGridViewCheckBoxColumn.HeaderText = "是否可为空";
            this.isNullableDataGridViewCheckBoxColumn.Name = "isNullableDataGridViewCheckBoxColumn";
            this.isNullableDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isNullableDataGridViewCheckBoxColumn.Width = 120;
            // 
            // isUniqueDataGridViewCheckBoxColumn
            // 
            this.isUniqueDataGridViewCheckBoxColumn.DataPropertyName = "IsUnique";
            this.isUniqueDataGridViewCheckBoxColumn.HeaderText = "是否唯一";
            this.isUniqueDataGridViewCheckBoxColumn.Name = "isUniqueDataGridViewCheckBoxColumn";
            this.isUniqueDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isUniqueDataGridViewCheckBoxColumn.Width = 70;
            // 
            // maxValueDataGridViewTextBoxColumn
            // 
            this.maxValueDataGridViewTextBoxColumn.DataPropertyName = "MaxValue";
            this.maxValueDataGridViewTextBoxColumn.HeaderText = "最大值";
            this.maxValueDataGridViewTextBoxColumn.Name = "maxValueDataGridViewTextBoxColumn";
            // 
            // minValueDataGridViewTextBoxColumn
            // 
            this.minValueDataGridViewTextBoxColumn.DataPropertyName = "MinValue";
            this.minValueDataGridViewTextBoxColumn.HeaderText = "最小值";
            this.minValueDataGridViewTextBoxColumn.Name = "minValueDataGridViewTextBoxColumn";
            // 
            // isGenSearchResultDataGridViewCheckBoxColumn
            // 
            this.isGenSearchResultDataGridViewCheckBoxColumn.DataPropertyName = "IsGenSearchResult";
            this.isGenSearchResultDataGridViewCheckBoxColumn.HeaderText = "是否在查询结果中显示";
            this.isGenSearchResultDataGridViewCheckBoxColumn.Name = "isGenSearchResultDataGridViewCheckBoxColumn";
            this.isGenSearchResultDataGridViewCheckBoxColumn.Width = 200;
            // 
            // isGenSearchConditionDataGridViewCheckBoxColumn
            // 
            this.isGenSearchConditionDataGridViewCheckBoxColumn.DataPropertyName = "IsGenSearchCondition";
            this.isGenSearchConditionDataGridViewCheckBoxColumn.HeaderText = "是否最为查询条件";
            this.isGenSearchConditionDataGridViewCheckBoxColumn.Name = "isGenSearchConditionDataGridViewCheckBoxColumn";
            this.isGenSearchConditionDataGridViewCheckBoxColumn.Width = 150;
            // 
            // isGenInputDataGridViewCheckBoxColumn
            // 
            this.isGenInputDataGridViewCheckBoxColumn.DataPropertyName = "IsGenInput";
            this.isGenInputDataGridViewCheckBoxColumn.HeaderText = "是否作为输入项";
            this.isGenInputDataGridViewCheckBoxColumn.Name = "isGenInputDataGridViewCheckBoxColumn";
            this.isGenInputDataGridViewCheckBoxColumn.Width = 150;
            // 
            // ColumnInfoBSource
            // 
            this.ColumnInfoBSource.DataSource = typeof(NFramework.GenCodeTool.Entity.ColumnInfo);
            // 
            // ColumnTitlePanel
            // 
            this.ColumnTitlePanel.Controls.Add(this.ColumnInfoLbl);
            this.ColumnTitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ColumnTitlePanel.Location = new System.Drawing.Point(0, 255);
            this.ColumnTitlePanel.Name = "ColumnTitlePanel";
            this.ColumnTitlePanel.Size = new System.Drawing.Size(649, 25);
            this.ColumnTitlePanel.TabIndex = 4;
            // 
            // ColumnInfoLbl
            // 
            this.ColumnInfoLbl.AutoSize = true;
            this.ColumnInfoLbl.Location = new System.Drawing.Point(7, 6);
            this.ColumnInfoLbl.Name = "ColumnInfoLbl";
            this.ColumnInfoLbl.Size = new System.Drawing.Size(41, 12);
            this.ColumnInfoLbl.TabIndex = 1;
            this.ColumnInfoLbl.Text = "表字段";
            // 
            // TableInfoGrid
            // 
            this.TableInfoGrid.AllowUserToAddRows = false;
            this.TableInfoGrid.AllowUserToDeleteRows = false;
            this.TableInfoGrid.AutoGenerateColumns = false;
            this.TableInfoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableInfoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.isGenDataGridViewCheckBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.namespaceDataGridViewTextBoxColumn,
            this.commentDataGridViewTextBoxColumn,
            this.isGenUIDataGridViewCheckBoxColumn});
            this.TableInfoGrid.DataSource = this.TableInfoBSource;
            this.TableInfoGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableInfoGrid.Location = new System.Drawing.Point(0, 25);
            this.TableInfoGrid.Name = "TableInfoGrid";
            this.TableInfoGrid.RowTemplate.Height = 23;
            this.TableInfoGrid.Size = new System.Drawing.Size(649, 230);
            this.TableInfoGrid.TabIndex = 3;
            this.TableInfoGrid.SelectionChanged += new System.EventHandler(this.TableInfoGrid_SelectionChanged);
            // 
            // isGenDataGridViewCheckBoxColumn
            // 
            this.isGenDataGridViewCheckBoxColumn.DataPropertyName = "IsGen";
            this.isGenDataGridViewCheckBoxColumn.HeaderText = "选择生成";
            this.isGenDataGridViewCheckBoxColumn.Name = "isGenDataGridViewCheckBoxColumn";
            this.isGenDataGridViewCheckBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.isGenDataGridViewCheckBoxColumn.Width = 70;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "表名";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            this.nameDataGridViewTextBoxColumn.Width = 150;
            // 
            // namespaceDataGridViewTextBoxColumn
            // 
            this.namespaceDataGridViewTextBoxColumn.DataPropertyName = "Namespace";
            this.namespaceDataGridViewTextBoxColumn.HeaderText = "命名空间";
            this.namespaceDataGridViewTextBoxColumn.Name = "namespaceDataGridViewTextBoxColumn";
            this.namespaceDataGridViewTextBoxColumn.Width = 150;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "Comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "备注";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.Width = 150;
            // 
            // isGenUIDataGridViewCheckBoxColumn
            // 
            this.isGenUIDataGridViewCheckBoxColumn.DataPropertyName = "IsGenUI";
            this.isGenUIDataGridViewCheckBoxColumn.HeaderText = "是否生成界面";
            this.isGenUIDataGridViewCheckBoxColumn.Name = "isGenUIDataGridViewCheckBoxColumn";
            this.isGenUIDataGridViewCheckBoxColumn.Width = 90;
            // 
            // TableInfoBSource
            // 
            this.TableInfoBSource.DataSource = typeof(NFramework.GenCodeTool.Entity.TableInfo);
            // 
            // TableTitlePancel
            // 
            this.TableTitlePancel.Controls.Add(this.TableInfoLbl);
            this.TableTitlePancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableTitlePancel.Location = new System.Drawing.Point(0, 0);
            this.TableTitlePancel.Name = "TableTitlePancel";
            this.TableTitlePancel.Size = new System.Drawing.Size(649, 25);
            this.TableTitlePancel.TabIndex = 2;
            // 
            // TableInfoLbl
            // 
            this.TableInfoLbl.AutoSize = true;
            this.TableInfoLbl.Location = new System.Drawing.Point(7, 6);
            this.TableInfoLbl.Name = "TableInfoLbl";
            this.TableInfoLbl.Size = new System.Drawing.Size(41, 12);
            this.TableInfoLbl.TabIndex = 0;
            this.TableInfoLbl.Text = "所有表";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 592);
            this.Controls.Add(this.TablePanel);
            this.Controls.Add(this.SettingPanel);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SettingPanel.ResumeLayout(false);
            this.SettingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CodeTemplateBSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GenSettingBSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CodeInfoGetterBSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DBInfoGetterBSource)).EndInit();
            this.TablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ColumnInfoGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ColumnInfoBSource)).EndInit();
            this.ColumnTitlePanel.ResumeLayout(false);
            this.ColumnTitlePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableInfoGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TableInfoBSource)).EndInit();
            this.TableTitlePancel.ResumeLayout(false);
            this.TableTitlePancel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SettingPanel;
        private System.Windows.Forms.Panel TablePanel;
        private System.Windows.Forms.Panel TableTitlePancel;
        private System.Windows.Forms.Button ConnectDbBtn;
        private System.Windows.Forms.Button StartGenBtn;
        private System.Windows.Forms.TextBox ProjectNameTxt;
        private System.Windows.Forms.ComboBox DBInfoGetterCbx;
        private System.Windows.Forms.TextBox ConnectionStringTxt;
        private System.Windows.Forms.TextBox ProjectDisplayNameTxt;
        private System.Windows.Forms.TextBox NamespaceTxt;
        private System.Windows.Forms.TextBox ReferenceFolderTxt;
        private System.Windows.Forms.DataGridView ColumnInfoGrid;
        private System.Windows.Forms.Panel ColumnTitlePanel;
        private System.Windows.Forms.Label ColumnInfoLbl;
        private System.Windows.Forms.Label TableInfoLbl;
        private System.Windows.Forms.ComboBox CodeInfoGetterCbx;
        private System.Windows.Forms.ComboBox GenSettingCbx;
        private System.Windows.Forms.Label DBTypeLbl;
        private System.Windows.Forms.Label GenSettingLbl;
        private System.Windows.Forms.Label CodeTemplateLbl;
        private System.Windows.Forms.Label ConnectionStringLbl;
        private System.Windows.Forms.ComboBox CodeTemplateCbx;
        private System.Windows.Forms.Label CodeInfoGetterLbl;
        private System.Windows.Forms.BindingSource GenSettingBSource;
        private System.Windows.Forms.BindingSource DBInfoGetterBSource;
        private System.Windows.Forms.BindingSource CodeInfoGetterBSource;
        private System.Windows.Forms.BindingSource CodeTemplateBSource;
        private System.Windows.Forms.BindingSource TableInfoBSource;
        private System.Windows.Forms.BindingSource ColumnInfoBSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn rColumnListDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox GenTargetFolderTxt;
        private System.Windows.Forms.CheckBox IsClearTargetFolderChk;
        private System.Windows.Forms.Button SaveSettingBtn;
        private System.Windows.Forms.TextBox GenSettingNameTxt;
        private System.Windows.Forms.Label SettingNameLbl;
        private System.Windows.Forms.Label ClearTargetLbl;
        private System.Windows.Forms.DataGridView TableInfoGrid;
        private System.Windows.Forms.Label ReferenceFolderLbl;
        private System.Windows.Forms.Label NamespaceLbl;
        private System.Windows.Forms.Label ProjectNameLbl;
        private System.Windows.Forms.Label GenTargetPathLbl;
        private System.Windows.Forms.Label ProjectDisplayNameLbl;
        private System.Windows.Forms.Button ChooseTargetFolderBtn;
        private System.Windows.Forms.Button ChooseReferenceFolderBtn;
        private System.Windows.Forms.FolderBrowserDialog ReferenceFbd;
        private System.Windows.Forms.FolderBrowserDialog GenTargetFbd;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isPKDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isFKDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fKNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fKTableNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fKColumnNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isAutoNumDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sqlTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precisionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn scaleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isNullableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isUniqueDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minValueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isGenSearchResultDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isGenSearchConditionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isGenInputDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isGenDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn namespaceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isGenUIDataGridViewCheckBoxColumn;
    }
}

