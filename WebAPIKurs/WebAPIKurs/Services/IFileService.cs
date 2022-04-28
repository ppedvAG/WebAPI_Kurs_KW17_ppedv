namespace WebAPIKurs.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Upload von Dateien
        /// </summary>
        /// <param name="files"></param>
        /// <param name="subDirectory"></param>
        void UploadFile(List<IFormFile> files, string subDirectory);

        //Feature komme aus Phyton -> 3 Rückgabewerte 

        /// <summary>
        /// Download einer Zip-Datei 
        /// </summary>
        /// <param name="subDirectory"></param>
        /// <returns></returns>
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);


        /// <summary>
        /// Ausgabe des Datenvolumens anhand der Bytes
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        string SizeConverter(long bytes);
    }
}
