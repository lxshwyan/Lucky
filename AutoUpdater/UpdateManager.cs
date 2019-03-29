/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：AutoUpdate
*文件名： UpdateManager
*创建人： Lxsh
*创建时间：2019/3/28 15:52:07
*描述
*=======================================================================
*修改标记
*修改时间：2019/3/28 15:52:07
*修改人：Lxsh
*描述：
************************************************************************/
using AutoUpdateInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;


namespace AutoUpdate
{
    public class UpdateManager
    {
        public UpdateManager()
        {
            this.LastUpdateInfo = new UpdateInfo();
            this.NewUpdateInfo = new UpdateInfo();
            this.GetLastUpdateInfo();
            this.GetNewUpdateInfo();
        }

        #region 属性
      
        /// <summary>
        /// 上次更新信息
        /// </summary>
        public UpdateInfo LastUpdateInfo { get; set; }
        /// <summary>
        /// 当前更新信息
        /// </summary>
        public UpdateInfo NewUpdateInfo { get; set; }
        /// <summary>
        /// 是否更新（根据更新版本号判断）
        /// </summary>
        public bool IsUpdate
        {
            get
            {
                return CheckForUpdate();
            }
        }
        /// <summary>
        /// 临时文件路径
        /// </summary>
        public string TempFilePath
        {
            get
            {
                string newTempPath = Environment.GetEnvironmentVariable("Temp") + "\\" + this.LastUpdateInfo.ApplicationName + "\\updateFiles";
                if (!Directory.Exists(newTempPath))
                {
                    Directory.CreateDirectory(newTempPath);
                }
                return newTempPath;
            }
        }

        /// <summary>
        /// 用于显示更新进度的委托
        /// </summary>
        public Action<int, int> showUpdateProgress;
        #endregion

        #region 方法
        /// <summary>
        /// 从本地读取最后一次更新的信息
        /// </summary>
        private void GetLastUpdateInfo()
        {
             this.LastUpdateInfo.FileList = new List<UpdateFile>();
            //用这个适合快速查找，
            FileStream fileStream = new FileStream("UpdateList.xml", FileMode.Open);
            XmlTextReader xmlTextReader = new XmlTextReader(fileStream);
           
            while (xmlTextReader.Read())
            {
                switch (xmlTextReader.Name)
                {
                    case "URLAddress":
                        this.LastUpdateInfo.UpdateFileUrl = xmlTextReader.GetAttribute("URL");
                        break;
                    case "Version":
                        this.LastUpdateInfo.UpdateVersion = xmlTextReader.GetAttribute("Num");
                        break;
                    case "UpdateTime":
                        this.LastUpdateInfo.UpdateTime = xmlTextReader.GetAttribute("Date");
                        break;
                    case "EntryPoint":
                        this.LastUpdateInfo.ApplicationName = xmlTextReader.GetAttribute("ApplicationID");
                        break;
                    case "UpdateFile":
                        string ver = xmlTextReader.GetAttribute("Ver");
                        string fileName = xmlTextReader.GetAttribute("FileName");
                        string contentLenth = xmlTextReader.GetAttribute("ContentLenth");
                        this.LastUpdateInfo.FileList.Add(new UpdateFile()
                        {

                            FileVer = ver,
                            FileName = fileName,
                            FileContentLength = contentLenth,
                            FileDownProcess = "0"
                        });
                        break;
                    default:
                        break;
                }
            }
            xmlTextReader.Close();
            fileStream.Close();
        }

        /// <summary>
        /// 从服务器上面获取最新的更新XML文件
        /// </summary>
        private void GetNewUpdateInfo()
        {
            try
            {
                string UpdaterUrl = this.LastUpdateInfo.UpdateFileUrl + "/UpdateList.xml";
                string newXmlTempPath = TempFilePath + "\\UpdateList.xml";
                WebRequest req = WebRequest.Create(UpdaterUrl);
                req.Timeout = 2000;
                WebResponse res = req.GetResponse();
                if (res.ContentLength > 0)
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(UpdaterUrl, newXmlTempPath);

                    FileStream fileStream = new FileStream(newXmlTempPath, FileMode.Open);
                    XmlTextReader xmlTextReader = new XmlTextReader(fileStream);
                    this.NewUpdateInfo.FileList = new List<UpdateFile>();
                    while (xmlTextReader.Read())
                    {
                        switch (xmlTextReader.Name)
                        {
                            case "Version":
                                this.NewUpdateInfo.UpdateVersion = xmlTextReader.GetAttribute("Num");
                                break;
                            case "UpdateTime":
                                this.NewUpdateInfo.UpdateTime = xmlTextReader.GetAttribute("Date");
                                break;
                            case "EntryPoint":
                                this.NewUpdateInfo.ApplicationName = xmlTextReader.GetAttribute("ApplicationID");
                                break;
                            case "UpdateFile":
                                string ver = xmlTextReader.GetAttribute("Ver");
                                string fileName = xmlTextReader.GetAttribute("FileName");
                                string contentLenth = xmlTextReader.GetAttribute("ContentLength");
                                this.NewUpdateInfo.FileList.Add(new UpdateFile()
                                {

                                    FileVer = ver,
                                    FileName = fileName,
                                    FileContentLength = contentLenth,
                                    FileDownProcess = "0"
                                });
                                break;
                            default:
                                break;
                        }
                    }
                    xmlTextReader.Close();
                    fileStream.Close();
                }
                else
                {
                    cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + "文件更新服务器连接异常");
                }

            }
            catch (Exception ex)
            {
                     cGlobe_Log.Error(cGlobe_Log.GetMethodInfo() + "文件更新服务器连接异常:"+ex.Message);
            }
                

        }

        /// <summary>
        /// 检查更新文件
        /// </summary>
        /// <param name="bMainVer">是否根据主版本号决定子项更新与否</param>
        /// <returns></returns>
        public bool CheckForUpdate()
        {
            if (this.NewUpdateInfo.FileList==null)
            {
                return false;
            }
            List<UpdateFile> updateFileList = new List<UpdateFile>();
              ArrayList oldFileAl = new ArrayList();
            foreach (var item in this.LastUpdateInfo.FileList)
            {
                oldFileAl.Add(item.FileName);
                oldFileAl.Add(item.FileVer);
            }
            foreach (var item in this.NewUpdateInfo.FileList)
            {
               int pos = oldFileAl.IndexOf(item.FileName);
                if (pos == -1)
                {
                    updateFileList.Add(item);
                }
                else if (pos > -1 && Common.CompareVersion(item.FileVer, oldFileAl[pos + 1].ToString()) > 0 ? true : false)
                {
                    updateFileList.Add(item);
                }
            }
            if ( CheckMainVerForUpdate()&&updateFileList.Count>0)
            {
                this.NewUpdateInfo.FileList = updateFileList;
                 return true;
            }
            return false;
        }
        /// <summary>
        /// 根据主版本号号检查文件下面文件是否更新
        /// </summary>
        /// <returns></returns>
        private bool CheckMainVerForUpdate()
        {
            string LastVer = this.LastUpdateInfo.UpdateVersion;
            string NewVer = this.NewUpdateInfo.UpdateVersion;
            return Common.CompareVersion(NewVer, LastVer) > 0 ? true : false;
        }
        /// <summary>
        /// 下载更新包
        /// </summary>
        public void DownLoadFiles()
        {
            List<UpdateFile> fileList = this.NewUpdateInfo.FileList;
            for (int i = 0; i < fileList.Count; i++)
            {
                string fileName = fileList[i].FileName;
                string fileUrl = this.LastUpdateInfo.UpdateFileUrl + "/" + fileName; //当前需要下载的文件URL
                WebRequest webRequest = WebRequest.Create(fileUrl);  //根据文件的url创建请求对象
                WebResponse webResponse = webRequest.GetResponse(); //根据请求对象创建响应对象
                Stream stream = webResponse.GetResponseStream();//通过响应对象返回数据流对象
                StreamReader reader = new StreamReader(stream);  //用数据流对象作为参数常见流读取对象

                long fileLength = webResponse.ContentLength; //获取接收数据长度
                byte[] bufferbyte = new byte[fileLength];    //获取字节数组
                int allByte = bufferbyte.Length;  //表示需下载的总字节数
                float total = (float)allByte / 1024;
                int startByte = 0;
                while (fileLength > 0)
                {
                    Application.DoEvents();//该语句表示允许在同一个线程中同时处理其他事件（跨线程访问可视化控件）
                    int downloadByte = stream.Read(bufferbyte, startByte, allByte); //开始读取字节流 
                    if (downloadByte == 0) break;
                    startByte += downloadByte;  //累计已经下载字节数 
                    allByte -= downloadByte;//未下载的字节数  
                    float part = (float)startByte / 1024;
                    int percent = Convert.ToInt32((part / total) * 100);
                    showUpdateProgress?.Invoke(i, percent);
                }
                string newFileName = this.TempFilePath + "\\" + fileName;
                FileStream fs = new FileStream(newFileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(bufferbyte, 0, bufferbyte.Length);
                stream.Close();
                reader.Close();
                fs.Close();
            }
        }
        /// <summary>
        /// 复制文件
        /// </summary>
        public void CopyFiles()
        {
            string  objPath=Application.StartupPath;
            string sourcePath = TempFilePath;
            Common.CopyFile(sourcePath,objPath);
        }
        /// <summary>
        /// 解压文件
        /// </summary>
        public void UnZip()
        {
            string  strPath=Application.StartupPath;
            for (int i = 0; i < this.NewUpdateInfo.FileList.Count; i++)
            {
                if (this.NewUpdateInfo.FileList[i].FileName.EndsWith("zip"))
                {
                    ZipClass.UnZip(TempFilePath + "\\" +this.NewUpdateInfo.FileList[i].FileName, strPath);
                
                    File.Delete(strPath + "\\" + this.NewUpdateInfo.FileList[i].FileName);
                }
               
            }
             System.IO.Directory.Delete(TempFilePath, true);
        }
     


        #endregion
    }
}