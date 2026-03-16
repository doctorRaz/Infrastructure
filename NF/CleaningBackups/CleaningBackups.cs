using System;
using System.IO;
using System.Text.RegularExpressions;

namespace dRz.Cleaner
{
    public class CleaningBackups

    {

        // Регулярное выражение для файлов вида "Имя(число).Расширение"
        private static Regex digitsInBracketsAtEnd = new Regex(@"\(\d+\)\.\w+$");

        public static void Cleaning(string directoryPath)
        {

            try
            {
                // EnumerateFiles лениво проходит файлы рекурсивно
                foreach (string? filePath in Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(filePath);
                    string extension = Path.GetExtension(filePath);

                    if (extension.Equals(".bak", StringComparison.OrdinalIgnoreCase) || digitsInBracketsAtEnd.IsMatch(fileName))
                    {
                        try
                        {
                            File.Delete(filePath);

                        }
                        catch/* (Exception ex)*/
                        {

                        }
                    }
                }

            }
            catch /*(Exception ex)*/
            {

            }

        }
    }
}