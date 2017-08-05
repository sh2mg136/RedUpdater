using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedDeploy
{
    public partial class FormMain : Form
    {

        const string REG_MAIN_KEY = "Software\\RedUpdater";
        const string REG_KEY_SOURCE_PATH = "SrcPath";
        const string REG_KEY_DESTINATION_PATH = "DstPath";

        List<CommonTypes.MyFileInfo> _sourceFiles;
        List<CommonTypes.MyFileInfo> _sourcePlugins;


        public FormMain()
        {
            InitializeComponent();
        }


        private void Form1_Load( object sender, EventArgs e )
        {

            try
            {
                txtPathMainSource.Text = CommonTypes.ConfigHelper.Load( REG_MAIN_KEY,
                    REG_KEY_SOURCE_PATH,
                    Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) );

                txtPathMainDestination.Text = CommonTypes.ConfigHelper.Load( REG_MAIN_KEY,
                    REG_KEY_DESTINATION_PATH,
                    Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ) );
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message );
            }

        }


        List<CommonTypes.MyFileInfo> GetProgrammFiles( string path )
        {
            DirectoryInfo pDirInfo = new DirectoryInfo( path );
            List<CommonTypes.MyFileInfo> myfiles = new List<CommonTypes.MyFileInfo>();

            if (pDirInfo.Exists)
            {

                var dllFiles = pDirInfo.GetFiles( "*.dll", SearchOption.TopDirectoryOnly );
                var exefiles = pDirInfo.GetFiles( "Red*.exe", SearchOption.TopDirectoryOnly );
                var fff = dllFiles.Concat( exefiles );

                myfiles = ( from f in fff
                            select new CommonTypes.MyFileInfo()
                            {
                                Name = f.Name,
                                Path = f.FullName,
                                Modified = f.LastWriteTime,
                                Size = f.Length
                            } ).ToList();
            }

            return myfiles;
        }


        List<CommonTypes.MyFileInfo> GetPlugins( string path )
        {
            string pPath = Path.Combine( path, "plugins" );
            DirectoryInfo pDirInfo = new DirectoryInfo( pPath );
            List<CommonTypes.MyFileInfo> myfiles = new List<CommonTypes.MyFileInfo>();

            if (!pDirInfo.Exists)
            {
                pDirInfo.Create();
                Application.DoEvents();
                System.Threading.Thread.Sleep( 1000 );
                Application.DoEvents();
            }

            if (pDirInfo.Exists)
            {
                var dllFiles = pDirInfo.GetFiles( "*.dll", SearchOption.AllDirectories );

                myfiles = ( from f in dllFiles
                            select new CommonTypes.MyFileInfo()
                            {
                                Name = f.Name,
                                Path = f.FullName,
                                Modified = f.LastWriteTime,
                                Size = f.Length
                            } ).ToList();
            }

            return myfiles;
        }


        private void btnCompare_Click( object sender, EventArgs e )
        {

            if (string.IsNullOrWhiteSpace( txtPathMainSource.Text ))
            {
                MessageBox.Show( "Не задана директория-источник" );
                return;
            }

            if (string.IsNullOrWhiteSpace( txtPathMainDestination.Text ))
            {
                MessageBox.Show( "Не задана папка назначения" );
                return;
            }

            _sourceFiles = GetProgrammFiles( txtPathMainSource.Text.Trim() );
            _sourcePlugins = GetPlugins( txtPathMainSource.Text.Trim() );

            gridControl2.DataSource = _sourcePlugins;

            var destinationPath = txtPathMainDestination.Text.Trim();

            if (string.IsNullOrEmpty( destinationPath ))
            {
                MessageBox.Show( "ERROR!" );
                return;
            }

            DirectoryInfo dirInfo = new DirectoryInfo( destinationPath );

            CommonTypes.Copir copirka = new CommonTypes.Copir();

            copirka.OnStart += coper_OnStart;
            copirka.OnStartTask += coper_OnStartTask;
            copirka.OnProgress += coper_OnProgress;
            copirka.OnError += coper_OnError;
            copirka.OnCompleteTask += coper_OnComplete;
            copirka.OnCompleteOverall += coper_OnCompleteOverall;

            if (dirInfo.Exists)
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = _sourceFiles.Count;
                progressBar1.Step = 1;

                copirka.AddTask( _sourceFiles, destinationPath, "Обновление файлов программы" );

                dirInfo = new DirectoryInfo( Path.Combine( destinationPath, "plugins" ) );
                if (dirInfo.Exists)
                {
                    copirka.AddTask( _sourcePlugins, dirInfo.FullName, "Обновление плагинов" );
                }

                //var task1 = new System.Threading.Tasks.Task( () => coper.Copy( _sourceFiles, destinationPath, progressBar1 ) );
                //var task2 = task1.ContinueWith( ( arg ) => coper.Copy( _sourcePlugins, destinationPathPlugins, progressBar2 ) );
                //task1.Start();

                progressBar1.Minimum = 0;
                progressBar1.Maximum = copirka.TotalCount;
                progressBar1.Step = 1;

                //gridControl2.Enabled = false;
                //copirka.RunTasks();
                //gridControl2.Enabled = true;

                var task1 = new System.Threading.Tasks.Task( () =>
                    {
                        copirka.RunTasks();
                    } );

                task1.Start();

            }

        }


        void coper_OnStart()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm( "Обработка...", "Чуточку терпения" );
            gridControl2.Enabled = false;
        }

        void coper_OnStartTask( CommonTypes.CopyTask task )
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowDefaultWaitForm( "Обработка...", task.Description );
        }

        void coper_OnError( Exception exception )
        {
            Debug.WriteLine( exception.Message );
        }

        void coper_OnComplete( CommonTypes.CopyTask task )
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm( false );
        }

        void coper_OnProgress( CommonTypes.MyFileInfo fileInfo )
        {
            this.Invoke( (Action)( () =>
                {

                    if (fileInfo != null)
                    {
                        var f = _sourcePlugins.Where( x => x.Name == fileInfo.Name ).FirstOrDefault();

                        if (f != null)
                        {
                            int handle = gridView2.LocateByValue( "Name", f.Name );
                            gridView2.FocusedRowHandle = handle;
                            gridView2.MakeRowVisible( handle );
                            gridControl2.Update();
                        }

                    }

                    progressBar1.PerformStep();

                } ) );
        }

        void coper_OnCompleteOverall()
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm( false );
            gridControl2.Enabled = true;
        }


        private void gridView2_CustomDrawCell( object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e )
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
                }
            }
        }


        private void btnSetPathMainSource_Click( object sender, EventArgs e )
        {
            folderBrowserDialog1.SelectedPath = txtPathMainSource.Text;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPathMainSource.Text = folderBrowserDialog1.SelectedPath;
                CommonTypes.ConfigHelper.Save( REG_MAIN_KEY, REG_KEY_SOURCE_PATH, folderBrowserDialog1.SelectedPath );
            }
        }


        private void btnSetDestinationPath_Click( object sender, EventArgs e )
        {
            folderBrowserDialog1.SelectedPath = txtPathMainDestination.Text;
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtPathMainDestination.Text = folderBrowserDialog1.SelectedPath;
                CommonTypes.ConfigHelper.Save( REG_MAIN_KEY, REG_KEY_DESTINATION_PATH, folderBrowserDialog1.SelectedPath );
            }
        }


    }


}
