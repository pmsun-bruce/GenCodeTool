using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NFramework.GenCodeTool.CodeInfoGetter;
using NFramework.GenCodeTool.DBInfoGetter;
using NFramework.GenCodeTool.Entity;
using NFramework.GenCodeTool.Handler;
using NFramework.GenCodeTool.Resources.Lan;

namespace NFramework.GenCodeTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DBInfoGetterBSource.DataSource = DBInfoGetterFactory.DBInfoGetterPool;
            CodeInfoGetterBSource.DataSource = CodeInfoGetterFactory.CodeInfoGetterPool;
            GenSettingBSource.DataSource = GenCodeHandler.GenSettingPool;
            Dictionary<string, string> tmplPool = GenCodeHandler.CodeTemplatePool;

            if (GenCodeHandler.GenSettingPool.Count > 0)
            {
                LoadGenSetting(GenCodeHandler.GenSettingPool[0]);
            }

            if (tmplPool.Count > 0)
            {
                CodeTemplateBSource.DataSource = tmplPool.Keys;
            }
            
            #region Control Display

            this.ConnectDbBtn.Text = GenCodeToolResource.ConnectDB;
            this.StartGenBtn.Text = GenCodeToolResource.StartGen;
            this.SaveSettingBtn.Text = GenCodeToolResource.SaveGenSetting;

            #endregion
        }

        private void ConnectDbBtn_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;

            if (string.IsNullOrWhiteSpace(this.ConnectionStringTxt.Text))
            {
                errMsg += GenCodeToolResource.Error_NoConnectionString + System.Environment.NewLine;
            }

            if (DBInfoGetterCbx.SelectedItem == null)
            {
                errMsg += GenCodeToolResource.Error_NoDBInfoGetter + System.Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(GenCodeToolResource.ConnectDBFaild + System.Environment.NewLine + errMsg, GenCodeToolResource.WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TableInfoBSource.DataSource = GenCodeHandler.GetTableInfoList(this.ConnectionStringTxt.Text, (IDBInfoGetter)this.DBInfoGetterCbx.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GenCodeToolResource.ConnectDBFaild + System.Environment.NewLine + ex.Message, GenCodeToolResource.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveSettingBtn_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;

            if (string.IsNullOrWhiteSpace(this.GenSettingNameTxt.Text))
            {
                MessageBox.Show(GenCodeToolResource.Error_NoSettingName, GenCodeToolResource.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (GenCodeHandler.GenSettingPool.Count(s => this.GenSettingNameTxt.Text.Equals(s.SettingName)) > 0)
            {
                MessageBox.Show(GenCodeToolResource.Error_NoSettingName, GenCodeToolResource.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                MessageBox.Show(GenCodeToolResource.SettingSaveFaild + System.Environment.NewLine + errMsg, GenCodeToolResource.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GenSetting genSetting = new GenSetting();
            genSetting.SettingName = this.GenSettingNameTxt.Text;
            genSetting.ConnectionString = this.ConnectionStringTxt.Text;
            genSetting.GenTargetPath = this.GenTargetFolderTxt.Text;
            genSetting.Namespace = this.NamespaceTxt.Text;
            genSetting.IsClearTargetFolder = this.IsClearTargetFolderChk.Checked;
            genSetting.ReferenceRootFolder = this.ReferenceFolderTxt.Text;
            genSetting.TemplatePath = CodeTemplateCbx.SelectedValue.ToString();
            genSetting.DBType = DBInfoGetterCbx.SelectedValue.ToString();
            genSetting.CodeType = CodeInfoGetterCbx.SelectedValue.ToString();

            try
            {
                GenCodeHandler.SaveGenSetting(genSetting);
                MessageBox.Show(GenCodeToolResource.SettingSaveSucc, GenCodeToolResource.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(GenCodeToolResource.SettingSaveFaild + System.Environment.NewLine + ex.Message, GenCodeToolResource.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void StartGenBtn_Click(object sender, EventArgs e)
        {
            ProjectInfo projectInfo = new ProjectInfo();
            projectInfo.ConnectionString = this.ConnectionStringTxt.Text;
            projectInfo.Name = this.ProjectNameTxt.Text;
            projectInfo.DisplayName = this.ProjectDisplayNameTxt.Text;
            projectInfo.Namespace = this.NamespaceTxt.Text;
            projectInfo.IsClearTargetFolder = this.IsClearTargetFolderChk.Checked;
            projectInfo.ReferenceRootFolder = this.ReferenceFolderTxt.Text;

            if (CodeTemplateCbx.SelectedValue != null)
            {
                projectInfo.TemplatePath = GenCodeHandler.CodeTemplatePool[CodeTemplateCbx.SelectedValue.ToString()];
            }

            projectInfo.GenTargetPath = this.GenTargetFolderTxt.Text;
            projectInfo.DBInfoGetter = (IDBInfoGetter)DBInfoGetterCbx.SelectedItem;
            projectInfo.CodeInfoGetter = (ICodeInfoGetter)CodeInfoGetterCbx.SelectedItem;
            IList<TableInfo> tableInfoList = (IList<TableInfo>)TableInfoBSource.DataSource;
            projectInfo.AllDBTableInfoList = tableInfoList;

            foreach (TableInfo tableInfo in tableInfoList)
            {
                if (!tableInfo.IsGen)
                {
                    continue;
                }

                projectInfo.AddTableInfo(tableInfo);
            }

            string errorMsg = GenCodeHandler.ValidatePreGen(projectInfo);

            if (!string.IsNullOrWhiteSpace(errorMsg))
            {
                MessageBox.Show(GenCodeToolResource.GenFaild + System.Environment.NewLine + errorMsg, GenCodeToolResource.WarningTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ProgressForm progressFrm = new ProgressForm();
            progressFrm.WaitGenProjectInfo = projectInfo;
            progressFrm.ShowDialog();
        }

        private void TableInfoGrid_SelectionChanged(object sender, EventArgs e)
        {
            TableInfo selectedTableInfo = (TableInfo)this.TableInfoGrid.CurrentRow.DataBoundItem;
            GenCodeHandler.FillColumnInfoList(this.ConnectionStringTxt.Text, (IDBInfoGetter)DBInfoGetterCbx.SelectedItem, (ICodeInfoGetter)CodeInfoGetterCbx.SelectedItem, selectedTableInfo);
            ColumnInfoBSource.DataSource = selectedTableInfo.ColumnList;
        }

        private void GenSettingCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenSetting selectedSetting = (GenSetting)GenSettingCbx.SelectedItem;
            LoadGenSetting(selectedSetting);
        }

        private void ChooseReferenceFolderBtn_Click(object sender, EventArgs e)
        {
            if (ReferenceFbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.ReferenceFolderTxt.Text = ReferenceFbd.SelectedPath;
            }
        }

        private void ChooseTargetFolderBtn_Click(object sender, EventArgs e)
        {
            if (GenTargetFbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.GenTargetFolderTxt.Text = GenTargetFbd.SelectedPath;
            }
        }

        /// <summary>
        /// 加载配置信息
        /// </summary>
        /// <param name="genSetting">配置信息对象</param>
        private void LoadGenSetting(GenSetting genSetting)
        {
            if (genSetting == null)
            {
                return;
            }

            this.ConnectionStringTxt.Text = genSetting.ConnectionString;
            this.IsClearTargetFolderChk.Checked = genSetting.IsClearTargetFolder;
            this.NamespaceTxt.Text = genSetting.Namespace;
            this.GenTargetFolderTxt.Text = genSetting.GenTargetPath;
            this.ReferenceFolderTxt.Text = genSetting.ReferenceRootFolder;
            this.CodeTemplateCbx.SelectedItem = genSetting.TemplatePath;

            if (!string.IsNullOrEmpty(genSetting.DBType))
            {
                this.DBInfoGetterCbx.SelectedValue = genSetting.DBType;
            }

            if (!string.IsNullOrEmpty(genSetting.CodeType))
            {
                this.CodeInfoGetterCbx.SelectedValue = genSetting.CodeType;
            }
        }
    }
}
