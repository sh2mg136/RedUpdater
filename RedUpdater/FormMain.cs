using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace RedUpdater
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {

        const string REG_MAIN_KEY = "Software\\RedUpdater";
        const string REG_KEY_SOURCE_PATH = "UpdaterSrcPath";
        const string REG_KEY_DESTINATION_PATH = "UpdaterDstPath";
        const string DEFAULT_SOURCE_PATH = @"z:\RedExpress.4.0\maxx\";

        NLog.Logger Logger
        {
            get
            {
                return Program.RedLogger;
            }
        }

        public FormMain()
        {
            InitializeComponent();
            GetRedDirectory();
            Logger.Error("{0}: Some error test", DateTime.Now);
        }


        CommonTypes.ProgrammFiles GetFiles(string directoryPath)
        {
            CommonTypes.ProgrammFiles programm = new CommonTypes.ProgrammFiles();

            List<CommonTypes.MyFileInfo> localFiles = new List<CommonTypes.MyFileInfo>();

            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            if (!dirInfo.Exists)
            {
                throw new DirectoryNotFoundException(string.Concat("Каталог не найден: ", directoryPath));
            }

            var files = dirInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            var exefiles = dirInfo.GetFiles("*.exe", SearchOption.TopDirectoryOnly);
            var fff = files.Concat(exefiles);

            localFiles = (from f in fff
                          select new CommonTypes.MyFileInfo()
                          {
                              Name = f.Name,
                              Path = f.FullName,
                              Modified = f.LastWriteTime,
                              Size = f.Length,
                              Message = "Программа",
                              State = CommonTypes.FileStates.None
                          }).ToList();

            programm.Programm = localFiles;

            FileInfo[] plugins = new FileInfo[] { };

            string pluginsPath = Path.Combine(dirInfo.FullName, "Plugins");
            DirectoryInfo pDirInfo = new DirectoryInfo(pluginsPath);
            if (pDirInfo.Exists)
            {
                plugins = pDirInfo.GetFiles("*.dll", SearchOption.TopDirectoryOnly);
            }

            var oldFilesPlugins = (from p in plugins
                                   select new CommonTypes.MyFileInfo()
                                   {
                                       Name = p.Name,
                                       Path = p.FullName,
                                       Modified = p.LastWriteTime,
                                       Size = p.Length,
                                       Message = "Модуль"
                                   }).ToList();

            programm.Plugins = oldFilesPlugins;

            return programm;

            // localFiles = localFiles.Union(oldFilesPlugins).ToList();
            // return localFiles;
        }


        bool CheckDirectory(out string path)
        {
            path = string.Empty;

            DirectoryInfo dirInfo = new DirectoryInfo(@"c:\RedExpressPro\");

            if (!dirInfo.Exists)
            {
                dirInfo = new DirectoryInfo("C:\\RedExpressPro");

                if (!dirInfo.Exists)
                {
                    dirInfo = new DirectoryInfo("C:\\RedExpress");
                }

            }

            if (!dirInfo.Exists)
                return false;

            path = dirInfo.FullName;
            return true;
        }


        bool GetRedDirectory()
        {
            DirectoryInfo dirInfo = new DirectoryInfo("C:\\RedExpress");
            if (!dirInfo.Exists)
            {
                dirInfo = new DirectoryInfo("C:\\RedExpressPro");
            }

            if (!dirInfo.Exists)
                return false;

            txtDestinationPath.Text = dirInfo.FullName;
            return true;
        }


        public bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }


        private static bool IsAlreadyRunning(string exeName)
        {

            string strLoc = Assembly.GetExecutingAssembly().Location;
            FileSystemInfo fileInfo = new FileInfo(strLoc);
            string sExeName = exeName; //  fileInfo.Name;
            bool bCreatedNew;

            // Mutex mutex = new Mutex( true, "Global\\" + sExeName, out bCreatedNew );
            Mutex mutex = new Mutex(true, sExeName, out bCreatedNew);
            if (bCreatedNew)
                mutex.ReleaseMutex();

            return !bCreatedNew;
        }


        void ProcessExit(int processId)
        {
            try
            {
                Process tempProc = Process.GetProcessById(processId);
                tempProc.CloseMainWindow();
                tempProc.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        bool CloseApp()
        {
            bool isRunning = IsProcessOpen("RedExpressPro");

            bool isRunning2 = IsAlreadyRunning("RedExpressPro");

            if (!isRunning)
                return true;

            var pro = Process.GetProcesses().Where(x => x.ProcessName == "RedExpressPro").FirstOrDefault();

            if (pro == null)
                return true;

            var taskB = Task.Factory.StartNew(() => ProcessExit(pro.Id));

            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            taskB.Wait(10000);

            Cursor.Current = cursor;

            if (!taskB.IsCompleted)
            {
                return false;
            }

            pro = Process.GetProcesses().Where(x => x.ProcessName == "RedExpressPro").FirstOrDefault();

            if (pro == null)
            {
                return true;
            }

            /*
            string path = string.Empty;
            if (!CheckDirectory(out path))
                return false;
            Assembly assembly = Assembly.LoadFrom(Path.Combine(path, "RedExpressPro.exe"));
            System.IO.FileInfo fInfo = new System.IO.FileInfo(assembly.Location);
            var modules = assembly.GetModules();
            Type t = assembly.GetType("RedExpressPro.frmMain");
            Type t2 = assembly.GetType("RedExpressPro.frmMain");
            var methodInfo = t.GetMethod("CloseAll");
            if (methodInfo == null)
            {
                Debug.WriteLine("Method not found");
            }
            else
            {
                try
                {
                    // var o = Activator.CreateInstance( t, new object[] { true } );
                    var o = Activator.CreateInstance(t, new object[] { false });
                    // t.InvokeMember("CloseAll", BindingFlags.Default, 
                    // methodInfo.Invoke( o, null );
                    // DEBUG.WriteLine( r );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            /*
                Type type = Type.GetType(className, true);
                dynamic instance = Activator.CreateInstance(type);
                var response = instance.YourMethod();              
            */

            return false;

        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Trace.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm}: обновление программы", DateTime.Now));

            string updatesPath = txtSourcePath.Text.Trim();

            DirectoryInfo dirInfo = new DirectoryInfo(updatesPath);

            if (!dirInfo.Exists)
            {
                MessageBox.Show("Путь к обновлениям не найден");
                return;
            }

            gridControlMain.DataSource = null;
            gridControlPlugins.DataSource = null;

            var remoteData = new CommonTypes.ProgrammFiles();
            var localData = new CommonTypes.ProgrammFiles();

            MyFileComparer fileComparer = new MyFileComparer();

            try
            {

                remoteData = GetFiles(txtSourcePath.Text.Trim());

                localData = GetFiles(txtDestinationPath.Text.Trim());

                foreach (var f in localData.Programm)
                {
                    var src = remoteData.Programm.Where(x => x.Name == f.Name).FirstOrDefault();
                    if (src != null)
                    {
                        f.State = src.Modified > f.Modified ? CommonTypes.FileStates.Outdated : CommonTypes.FileStates.UpToDate;
                    }
                    else
                    {
                        f.State = CommonTypes.FileStates.None;
                    }
                }

                foreach (var f in localData.Plugins)
                {
                    var src = remoteData.Plugins.Where(x => x.Name == f.Name).FirstOrDefault();
                    if (src != null)
                    {
                        f.State = src.Modified > f.Modified ? CommonTypes.FileStates.Outdated : CommonTypes.FileStates.UpToDate;
                    }
                    else
                    {
                        f.State = CommonTypes.FileStates.None;
                    }
                }

                gridControlMain.DataSource = localData.Programm;
                gridControlPlugins.DataSource = localData.Plugins;

            }
            catch (DirectoryNotFoundException dex)
            {
                var msg = string.Concat("Получение списка файлов для обновления. \r\n", dex.Message);
                Logger.Error(msg);
                MessageBox.Show(msg);
                return;
            }
            catch (Exception ex)
            {
                var msg = string.Concat("Получение списка файлов для обновления. \r\n", ex.Message);
                Logger.Error(msg);
                MessageBox.Show(msg);
            }

            var outdatedFiles = localData.Programm.Where(x => x.State == CommonTypes.FileStates.Outdated).Count();
            var outdatedPlugins = localData.Plugins.Where(x => x.State == CommonTypes.FileStates.Outdated).Count();

            var newFiles = remoteData.Programm.Except(localData.Programm, fileComparer).ToList();
            newFiles.ForEach(x => Console.WriteLine(x.Name));
            var newFilesCount = newFiles.Count();

            if (outdatedFiles + outdatedPlugins + newFilesCount == 0)
            {
                Trace.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm}: Программа не нуждается в обновлении", DateTime.Now));
                MessageBox.Show("Программа не нуждается в обновлении");
                return;
            }


            if (!CloseApp())
            {
                MessageBox.Show("Похоже, программа RedExpress запущена! Завершите работу RedExpress и повторите попытку!");
                return;
            }

            var dlgres = MessageBox.Show("Обновить программу?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dlgres != System.Windows.Forms.DialogResult.OK)
                return;

            progressBarControlMain.Position = 0;
            progressBarControlMain.Properties.Step = 1;
            progressBarControlMain.Properties.Maximum = localData.Programm.Count();

            var programmFilesForUpdate = remoteData.Programm.Intersect(localData.Programm, fileComparer).ToList();

            var pluginsForUpdate = remoteData.Plugins.Intersect(localData.Plugins, fileComparer).ToList();

            string destinationFolderMain, destinationFolderPlugins;
            dirInfo = new DirectoryInfo(txtDestinationPath.Text.Trim());
            if (!dirInfo.Exists)
            {
                MessageBox.Show("Каталог программы не найден");
                return;
            }
            destinationFolderMain = dirInfo.FullName;

            dirInfo = new DirectoryInfo(Path.Combine(dirInfo.FullName, "plugins"));
            if (!dirInfo.Exists)
            {
                MessageBox.Show("Каталог модулей не найден");
                return;
            }
            destinationFolderPlugins = dirInfo.FullName;

            CommonTypes.Copir copirka = new CommonTypes.Copir();

            copirka.AddTask(programmFilesForUpdate, destinationFolderMain, "Обновление программы...");
            copirka.AddTask(pluginsForUpdate, destinationFolderPlugins, "Обновление модулей программы...");
            copirka.AddTask(newFiles, destinationFolderMain, "Добавление новых файлов \r\nиз репозитория...");

            copirka.OnStart += copirka_OnStart;
            copirka.OnProgress += copirka_OnProgress;
            copirka.OnStartTask += copirka_OnStartTask;
            copirka.OnCompleteTask += copirka_OnCompleteTask;
            copirka.OnCompleteOverall += copirka_OnCompleteOverall;
            copirka.OnError += copirka_OnError;

            // copirka.RunTasks();
            Task.Factory.StartNew(() => copirka.RunTasks());

        }

        void copirka_OnStart()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)copirka_OnStart);
                return;
            }

            gridControlMain.Enabled = false;
            gridControlPlugins.Enabled = false;
        }

        void copirka_OnError(Exception exception)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            MessageBox.Show(exception.Message);
        }

        void copirka_OnCompleteTask(CommonTypes.CopyTask obj)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
        }

        void copirka_OnStartTask(CommonTypes.CopyTask task)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action<CommonTypes.CopyTask>)copirka_OnStartTask, task);
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm("Обработка...", task.Description);
            progressBarControlMain.Position = 0;
            progressBarControlMain.Properties.Maximum = task.Files.Count();
        }


        void copirka_OnProgress(CommonTypes.MyFileInfo fileInfo)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action<CommonTypes.MyFileInfo>)copirka_OnProgress, fileInfo);
                return;
            }

            if (fileInfo != null)
            {
                int handle = -1;

                handle = gridViewMain.LocateByValue("Name", fileInfo.Name);
                if (handle >= 0)
                {
                    gridViewMain.FocusedRowHandle = handle;
                    var fi = gridViewMain.GetRow(handle) as CommonTypes.MyFileInfo;
                    if (fi != null)
                    {
                        fi.State = fileInfo.State;
                    }
                    gridViewMain.MakeRowVisible(handle);
                    gridControlMain.Update();
                }

                handle = gridViewPlugins.LocateByValue("Name", fileInfo.Name);
                if (handle >= 0)
                {
                    gridViewPlugins.FocusedRowHandle = handle;
                    var fi = gridViewPlugins.GetRow(handle) as CommonTypes.MyFileInfo;
                    if (fi != null)
                    {
                        fi.State = fileInfo.State;
                    }
                    gridViewPlugins.MakeRowVisible(handle);
                    gridControlPlugins.Update();
                }
            }

            progressBarControlMain.Increment(1);

            Application.DoEvents();

        }

        void copirka_OnCompleteOverall()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((Action)copirka_OnCompleteOverall);
                return;
            }

            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm(false);
            gridControlMain.Enabled = true;
            gridControlPlugins.Enabled = true;
            MessageBox.Show("Все операции обновления завершены!");
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            string defaultSourcePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(DEFAULT_SOURCE_PATH);
                if (dir.Exists)
                {
                    defaultSourcePath = DEFAULT_SOURCE_PATH;
                }
            }
            catch { }
            finally
            {
                Cursor.Current = cursor;
            }

            try
            {
                txtSourcePath.Text = CommonTypes.ConfigHelper.Load(REG_MAIN_KEY, REG_KEY_SOURCE_PATH, defaultSourcePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSetDestinationPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtSourcePath.Text;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSourcePath.Text = folderBrowserDialog1.SelectedPath;
                CommonTypes.ConfigHelper.Save(REG_MAIN_KEY, REG_KEY_SOURCE_PATH, folderBrowserDialog1.SelectedPath);
            }
        }


        private void gridViewMain_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "State")
            {
                if (e.CellValue != null && e.CellValue is CommonTypes.FileStates)
                {
                    if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.New)
                    {
                        e.Appearance.BackColor = Color.LightSkyBlue;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.Copied)
                    {
                        e.Appearance.BackColor = Color.Lime;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.UpToDate)
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.Outdated)
                    {
                        e.Appearance.BackColor = Color.LightCoral;
                    }
                }
            }
        }


        private void gridViewPlugins_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "State")
            {
                if (e.CellValue != null && e.CellValue is CommonTypes.FileStates)
                {
                    if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.New)
                    {
                        e.Appearance.BackColor = Color.LightSkyBlue;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.Copied)
                    {
                        e.Appearance.BackColor = Color.Lime;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.UpToDate)
                    {
                        e.Appearance.BackColor = Color.LimeGreen;
                    }
                    else if ((CommonTypes.FileStates)e.CellValue == CommonTypes.FileStates.Outdated)
                    {
                        e.Appearance.BackColor = Color.LightCoral;
                    }
                }
            }
        }

        private void btnRunRed_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(txtDestinationPath.Text, "RedExpressPro.exe");
            FileInfo finfo = new FileInfo(path);
            if (!finfo.Exists)
            {
                MessageBox.Show("Путь не найден");
                return;
            }

            bool isRunning = IsProcessOpen("RedExpressPro");

            if (isRunning)
            {
                MessageBox.Show("Программа уже запущена");
                return;
            }

            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(path);
                        System.Diagnostics.Process.Start(psi);
                    });

                //System.Threading.Thread.Sleep(1000);
                //this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSetDestinationPath_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtDestinationPath.Text;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDestinationPath.Text = folderBrowserDialog1.SelectedPath;
                CommonTypes.ConfigHelper.Save(REG_MAIN_KEY, REG_KEY_DESTINATION_PATH, folderBrowserDialog1.SelectedPath);
            }
        }

    }


    class MyFileComparer : IEqualityComparer<CommonTypes.MyFileInfo>
    {

        #region IEqualityComparer<MyFileInfo> Members

        public bool Equals(CommonTypes.MyFileInfo x, CommonTypes.MyFileInfo y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(CommonTypes.MyFileInfo obj)
        {
            return obj.Name.GetHashCode();
        }

        #endregion

    }

}