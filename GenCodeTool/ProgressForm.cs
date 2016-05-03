using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using NFramework.GenCodeTool.Entity;
using NFramework.GenCodeTool.Handler;
using NFramework.GenCodeTool.Resources.Lan;

namespace NFramework.GenCodeTool
{
    public partial class ProgressForm : Form
    {
        #region Fields & Properties

        /// <summary>
        /// 进度条增长委托
        /// </summary>
        private delegate void ProgressIncreaseHandle();
        /// <summary>
        /// 完成生成委托；包括报错和正常完成两种情况
        /// </summary>
        private delegate void FinishHandle();

        /// <summary>
        /// 需要生成的项目信息
        /// </summary>
        public ProjectInfo WaitGenProjectInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 是否完成
        /// </summary>
        private bool Complete
        {
            get;
            set;
        }

        /// <summary>
        /// 是否报错
        /// </summary>
        private bool IsError
        {
            get;
            set;
        }

        /// <summary>
        /// 显示生成日志的开始Index
        /// </summary>
        private int ShowStart
        {
            get;
            set;
        }

        #endregion

        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.GenBar.Style = ProgressBarStyle.Continuous;
            this.GenBar.Value = 0;
            this.GenBar.Maximum = WaitGenProjectInfo.MaxGenCount = GenCodeHandler.CalculateGenCount(WaitGenProjectInfo.TemplatePath, WaitGenProjectInfo.GenTableInfoList.Count);
            this.Complete = false;
            this.IsError = false;

            ThreadStart genStartThread = new ThreadStart(StartGen);
            Thread genThread = new Thread(genStartThread);
            genThread.Start();

            ThreadStart procStartThread = new ThreadStart(StartProcess);
            Thread procThread = new Thread(procStartThread);
            procThread.Start();
        }

        private void FinishBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 开始显示进度
        /// </summary>
        private void StartProcess()
        {
            while (this.ShowStart != this.GenBar.Maximum && !this.IsError)
            {
                IncreaseProcess();
                Thread.Sleep(100);
            }

            if (!this.IsError)
            {
                MessageBox.Show(GenCodeToolResource.GenSucc, GenCodeToolResource.InfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 推动进度显示和生成日志显示
        /// </summary>
        public void IncreaseProcess()
        {
            if (this.InvokeRequired)
            {
                ProgressIncreaseHandle procIncrease = new ProgressIncreaseHandle(IncreaseProcess);
                this.Invoke(procIncrease);
            }
            else
            {
                int maxCount = WaitGenProjectInfo.CurrGenFileList.Count;
                this.GenBar.Value = maxCount;
                for (int i = ShowStart; i < maxCount; i++)
                {
                    this.GenLogTxt.Text += WaitGenProjectInfo.CurrGenFileList[i] + System.Environment.NewLine;
                    this.GenLogTxt.SelectionStart = this.GenLogTxt.Text.Length;
                    this.GenLogTxt.ScrollToCaret();
                }

                ShowStart = maxCount;

                if (this.Complete)
                {
                    this.FinishBtn.Enabled = true;

                    if (this.ShowStart == this.GenBar.Maximum)
                    {
                        this.GenLogTxt.Text += GenCodeToolResource.GenSucc;
                        this.GenLogTxt.SelectionStart = this.GenLogTxt.Text.Length;
                        this.GenLogTxt.ScrollToCaret();
                    }
                }
            }
        }

        /// <summary>
        /// 开始生成
        /// </summary>
        private void StartGen()
        {
            try
            {
                GenCodeHandler.GenCode(WaitGenProjectInfo);
            }
            catch (Exception ex)
            {
                this.IsError = true;
                MessageBox.Show(GenCodeToolResource.GenFaild + System.Environment.NewLine + ex.Message, GenCodeToolResource.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Complete = true;
                FinishHandle finish = new FinishHandle(OnFinish);
                this.Invoke(finish);
            }
        }

        /// <summary>
        /// 完成生成
        /// </summary>
        private void OnFinish()
        {
            this.FinishBtn.Enabled = true;
        }

    }
}
