using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RadioExt_Helper.utility
{
    /// <summary>
    /// Provides methods to safely enumerate files in directories, handling restricted access gracefully.
    /// </summary>
    public class FileEnumerator
    {
        /// <summary>
        /// Enumerates directories in the specified path, handling restricted access and other exceptions.
        /// </summary>
        /// <param name="path">The directory path to search for directories.</param>
        /// <param name="searchPattern">The search string to match against the names of directories in <paramref name="path"/>. Defaults to "*".</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or all subdirectories. Defaults to <see cref="SearchOption.TopDirectoryOnly"/>.</param>
        /// <returns>An enumerable collection of directory names in the specified directory.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="searchOption"/> is not a valid <see cref="SearchOption"/> value.</exception>
        public static IEnumerable<string> SafeEnumerateDirectories(string path, string searchPattern = "*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var directories = new List<string>();
            try
            {
                directories.AddRange(Directory.EnumerateDirectories(path, searchPattern, searchOption));
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine($"Access to the path '{path}' is denied.");
            }
            catch (PathTooLongException)
            {
                Debug.WriteLine($"The path '{path}' is too long.");
            }
            catch (DirectoryNotFoundException)
            {
                Debug.WriteLine($"The path '{path}' is not found.");
            }
            catch (SecurityException)
            {
                Debug.WriteLine($"You do not have permission to access the path '{path}'.");
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"An I/O error occurred while accessing the path '{path}': {ex.Message}");
            }

            return directories;
        }

        /// <summary>
        /// Enumerates files in the specified path, handling restricted access and other exceptions.
        /// </summary>
        /// <param name="path">The directory path to search for files.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path"/>. Defaults to "*.*".</param>
        /// <param name="searchOption">Specifies whether to search the current directory, or all subdirectories. Defaults to <see cref="SearchOption.TopDirectoryOnly"/>.</param>
        /// <returns>An enumerable collection of file names in the specified directory.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="path"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="searchOption"/> is not a valid <see cref="SearchOption"/> value.</exception>
        public static IEnumerable<string> SafeEnumerateFiles(string path, string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var files = new List<string>();
            try
            {
                files.AddRange(Directory.EnumerateFiles(path, searchPattern, searchOption));
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine($"Access to the path '{path}' is denied.");
            }
            catch (PathTooLongException)
            {
                Debug.WriteLine($"The path '{path}' is too long.");
            }
            catch (DirectoryNotFoundException)
            {
                Debug.WriteLine($"The path '{path}' is not found.");
            }
            catch (SecurityException)
            {
                Debug.WriteLine($"You do not have permission to access the path '{path}'.");
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"An I/O error occurred while accessing the path '{path}': {ex.Message}");
            }

            return files;
        }
    }

    //public class Program
    //{
    //    public static void Main()
    //    {
    //        var fileEnumerator = new FileEnumerator();
    //        var path = @"C:\SomeDirectory"; // Change this to your path
    //        var files = fileEnumerator.SafeEnumerateFiles(path, "*.*", SearchOption.AllDirectories);

    //        foreach (var file in files)
    //        {
    //            Console.WriteLine(file);
    //        }
    //    }
    //}

}
