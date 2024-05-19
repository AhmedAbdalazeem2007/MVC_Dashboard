namespace E_Commerece_PL.Helper
{
    public static class DocumentSeetings
    {
        public static string UploadFile(IFormFile file, string foldername)
        {
            string folder_path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\", foldername);
            string file_name = $"{Guid.NewGuid()}{file.FileName}";
            string file_path = Path.Combine(folder_path, file_name);
            using var fs = new FileStream(file_path, FileMode.Create);
            file.CopyTo(fs);
            return file_name;
        }

        public static void Deletefile(string filename, string foldername)
        {
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\", filename);
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
        }
    }
}
