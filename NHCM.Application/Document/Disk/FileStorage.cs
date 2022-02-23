using NHCM.Application.Document.Disk.Cropper;
using NHCM.Application.Document.Disk.Cropper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NHCM.Application.Document.Disk.FileManager
{
    public class FileStorage
    {
        public async System.Threading.Tasks.Task<string> CreateAsync(Stream file, string fileextension, string path)
        {
            var filename = GenerateFileName(fileextension);
            var filepath = path + filename;
            using (var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return filename;
        }

        public async System.Threading.Tasks.Task<Stream> GetAsync(string filepath)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(filepath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return memory;
        }

        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private string GenerateFileName(string extension)
        {
            // e.g.: e2384d8f-f24e-47c8-b1d0-d2c286dd9c1b-490x240.png
            return string.Format("{0}{1}", Guid.NewGuid(), extension);
        }


        public async System.Threading.Tasks.Task<string> Crop(CropRequest cropmodel, string path)
        {
            //// extract original image ID and generate a new filename for the cropped result
            var extension = Path.GetExtension(cropmodel.imgUrl);
            var croppedId = GenerateFileName(extension);
            var result = new object();
            try
            {
                // load the original picture and resample it to the scaled values
                Stream imgstream = await GetAsync(path + cropmodel.imgUrl);

                var img = System.Drawing.Image.FromStream(imgstream);

                var bitmap = ImageUtils.Resize(img, (int)cropmodel.imgW, (int)cropmodel.imgH);
                System.Drawing.Image croppedBitmap = ImageUtils.Crop(bitmap, cropmodel.imgX1, cropmodel.imgY1, (int)cropmodel.cropW, (int)cropmodel.cropH);
                croppedBitmap.Save(path + croppedId);
                File.Delete(path + cropmodel.imgUrl);

                return croppedId;
            }
            catch (Exception e)
            {
                return e.Message + "//" + e.StackTrace;
            }
        }

        private void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);
                FileStream fsIn = new FileStream(inputFile, FileMode.Open);
                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);
                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
              
            }
        }
        ///<summary>
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        private void DecryptFile(string inputFile, string outputFile)
        {
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);
                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);
                FileStream fsOut = new FileStream(outputFile, FileMode.Create);
                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);
                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }
        }
    }
}