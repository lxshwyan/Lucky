using AutoUpdateInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 参照喜科堂编写
/// </summary>
namespace AutoUpdate
{
    public partial class FrmUpdate : Form
    {
        private UpdateManager manager = new UpdateManager();


        /// <summary>
        /// 是否更新（根据更新版本对比）
        /// </summary>
        public bool IsUpdate
        {
            get
            {
                return manager.IsUpdate;
            }
        }
        #region 任意拖动窗体
        public const int WM_NCLBUTTONDOWN = 0xA1; //消息:左键点击
        public const int HT_CAPTION = 0x2; //标题栏
        //引入Win32 API函数
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam); //发送消息
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture(); //释放鼠标捕捉

        /// <summary>
        /// 鼠标按下触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picShow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); //释放鼠标捕捉
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); //发送左键点击的消息至该窗体(标题栏)
            }
        }
        #endregion
        public FrmUpdate()
        {
            InitializeComponent();

        }
        private void Init()
        {
            try
            {
                if (!IsUpdate)
                {
                    MessageBox.Show("当前已经是最新版本不需要更新");
                    StartMainApp();
                    this.Close();

                }
                List<UpdateFile> filelist = manager.NewUpdateInfo.FileList;

                this.btnFinish.Enabled = false;
                foreach (var item in filelist)
                {
                    string[] file = new string[] { item.FileName, item.FileContentLength, item.FileVer, item.FileDownProcess };
                    this.lvUpdateList.Items.Add(new ListViewItem(file));
                }
                this.lblVersion.Text = manager.LastUpdateInfo.UpdateVersion;
                this.lblUpdateTime.Text = manager.LastUpdateInfo.UpdateTime;
                manager.showUpdateProgress = ShowUpdateProgress;
            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + "更新失败" + ex.Message);

            }
        }

        private void ShowUpdateProgress(int fileIndex, int finishedPercent)
        {
            this.lvUpdateList.Items[fileIndex].SubItems[3].Text = finishedPercent + "%";
            this.pbDownLoadFile.Value = finishedPercent;
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认取消升级吗？", "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }
        /// <summary>
        /// 完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblTips.Text = "正在解压文件，请稍后...";
                manager.CopyFiles();
                manager.UnZip(); 
                StartMainApp();
                this.Close();  

            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + ex.Message);

            }
        }
        /// <summary>
        /// 启动主进程
        /// </summary>
        private void StartMainApp()
        {
            try
            { 
                System.Diagnostics.Process.Start(manager.NewUpdateInfo.ApplicationName);   
            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + ex.Message);

            }
        }
        /// <summary>
        /// 杀掉主线程
        /// </summary>
        /// <returns></returns>
        private void KillMainAppRun()
        {
            string mainAppExe = manager.NewUpdateInfo.ApplicationName;

            Process[] allProcess = Process.GetProcesses();

            foreach (Process p in allProcess)
            {
                if (p.ProcessName == mainAppExe)
                {
                    p.Kill();
                }
            }

        }
        /// <summary>
        /// 下一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            this.btnNext.Enabled = false;
            try
            {
                this.KillMainAppRun();
                this.lblUpdateStatus.Text = "正在下载更新文件，请稍后...";
                this.lblTips.Text = "点击“取消”可以结束下载";
                this.picClose.Enabled = false;
                this.manager.DownLoadFiles();
                this.lblTips.Text = "全部文件已经下载到本地，请点击“完成”结束升级";
                this.btnCancel.Enabled = true;
                this.btnFinish.Enabled = true;
            }
            catch (Exception ex)
            {
                cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + "更新失败" + ex.Message);

            }
        }

        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            Init();

        }
    }
}
