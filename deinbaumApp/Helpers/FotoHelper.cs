using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Helpers
{
    /// <summary>
    /// Hilfsklasse um auf die Kamera des Devices zuzugreifen
    /// Umwandlung in Byte Array
    /// </summary>
    public static class FotoHelper
    {
        /// <summary>
        /// Foto aufnehmen mittels Media Picker und Rueckgabe als Byte-Array
        /// </summary>
        /// <returns>Foto als byte[]</returns>
        public async static Task<byte[]> DoCapturePhoto()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                var photoBytes = await LoadPhotoAsync(photo);

                return photoBytes;
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Foto erfassen fehlgeschlagen", $"{ex.Message}", "Ok");
                return null;
            }
        }

        private async static Task<byte[]> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return null;
            }

            // save the file into local storage
            var newFile = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }

            // convert into byte Array
            byte[] fileContent = null;
            using (var fs = new System.IO.FileStream(newFile, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            using (var binaryReader = new System.IO.BinaryReader(fs))
            {
                long byteLength = new System.IO.FileInfo(newFile).Length;
                fileContent = binaryReader.ReadBytes((Int32)byteLength);
            }
            return fileContent;
        }
    }
}
