using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{

    public class Copir
    {

        public Copir()
        {
            Tasks = new List<CopyTask>();
        }

        List<CopyTask> Tasks { get; set; }

        public event Action OnStart;
        public event Action<CopyTask> OnStartTask;
        public event Action<CommonTypes.MyFileInfo> OnProgress;
        public event Action<Exception> OnError;
        public event Action<CopyTask> OnCompleteTask;
        public event Action OnCompleteOverall;


        public void AddTask(List<CommonTypes.MyFileInfo> files, string destinationFolderPath, string description)
        {
            this.Tasks.Add(new CopyTask()
            {
                Files = files,
                DestinationPath = destinationFolderPath,
                Description = description
            });
        }


        public int TotalCount
        {
            get
            {
                return this.Tasks.Sum(x => x.Files.Count());
            }
        }


        public void RunTasks()
        {

            var @eventStart = OnStart;
            if (@eventStart != null)
            {
                @eventStart();
            }

            foreach (var tsk in Tasks)
            {
                Copy(tsk);
            }

            var @eventCompleteOverall = OnCompleteOverall;
            if (@eventCompleteOverall != null)
            {
                @eventCompleteOverall();
            }

        }


        public void Copy(List<CommonTypes.MyFileInfo> files, string destinationPath, string description)
        {
            CopyTask copyTask = new CopyTask()
            {
                Files = files,
                DestinationPath = destinationPath,
                Description = description
            };

            Copy(copyTask);
        }


        private void CopyFile(MyFileInfo file)
        {
            if (string.IsNullOrWhiteSpace(file.DestinationPath))
                throw new InvalidOperationException("Путь назначения не указан");

            var fileInfo = new FileInfo(file.DestinationPath);

            System.Diagnostics.Trace.WriteLine(string.Format("{0:yyyy-MM-dd HH:mm} copy: {1}  ->  {2}",
                DateTime.Now,
                file.Name,
                file.DestinationPath));

            if (fileInfo.Exists)
            {
                if (fileInfo.LastWriteTime < file.Modified)
                {
                    File.Copy(file.Path, file.DestinationPath, true);
                    file.State = CommonTypes.FileStates.Copied;
                }
                else
                {
                    file.State = CommonTypes.FileStates.UpToDate;
                }
            }
            else
            {
                File.Copy(file.Path, file.DestinationPath, true);
                file.State = CommonTypes.FileStates.New;
            }

            var @event = OnProgress;
            if (@event != null)
            {
                @event(file);
            }
        }


        public void Copy(CopyTask copyTask)
        {

            var @eventStartTask = OnStartTask;
            if (@eventStartTask != null)
            {
                @eventStartTask(copyTask);
            }

            try
            {
                copyTask.Files.ForEach(x => x.SetDestinationPath(copyTask.DestinationPath));
                ParallelOptions options = new ParallelOptions() { MaxDegreeOfParallelism = 3 };
                Parallel.ForEach(copyTask.Files, options, (Action<MyFileInfo>)CopyFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);

                var @event = OnError;
                if (@event != null)
                {
                    @event(ex);
                }

                var @eventP = OnProgress;
                if (@eventP != null)
                {
                    @eventP(null);
                }
            }
            finally
            {
                var @event = OnCompleteTask;
                if (@event != null)
                {
                    @event(copyTask);
                }
            }

        }


    }


}
