using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{

    public class MyFileInfo
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Modified { get; set; }
        public long Size { get; set; }
        public string Message { get; set; }
        public FileStates State { get; set; }

        public string DestinationPath { get; set; }
        public void SetDestinationPath(string destinationFolder)
        {
            this.DestinationPath = System.IO.Path.Combine(destinationFolder, this.Name ?? string.Empty);
        }
    }


    public enum FileStates
    {
        None = 0,
        /// <summary>
        /// Файл скопирован
        /// </summary>
        Copied,
        /// <summary>
        /// Новый файл
        /// </summary>
        New,
        /// <summary>
        /// Файл актуальной версии
        /// </summary>
        UpToDate,
        /// <summary>
        /// Устаревший файл
        /// </summary>
        Outdated
    }


}
