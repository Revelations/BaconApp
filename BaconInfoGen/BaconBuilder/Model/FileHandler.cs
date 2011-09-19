using System;
using System.Collections.Generic;
using System.IO;

namespace BaconBuilder.Model
{
	public class FileHandler
	{
		/// <summary>
		/// The safe limit for files. If the HTML files are this large, consider splitting it up into smaller files.
		/// </summary>
		private const int MaximumSafeFileSize = 1000000;
        /// <summary>
        /// 
        /// </summary>
        private readonly string _ext;
        /// <summary>
        /// A collection of file contents.
        /// </summary>
		private readonly Dictionary<string, IEnumerable<string>> _contents;
		private readonly Dictionary<string, DateTime> _files;

		public FileHandler(string ext)
		{
			_ext = ext;
			_files = new Dictionary<string, DateTime>();
			_contents = new Dictionary<string, IEnumerable<string>>();
		}

        /// <summary>
        /// Gets a usable key to use for retrieving contents from memory.
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
		private static string GetKey(FileSystemInfo fileInfo)
		{
			if (fileInfo == null) throw new ArgumentNullException("fileInfo");
			return fileInfo.FullName;
		}

		/// <summary>
		/// Puts contents into the collection of file contents.
		/// 
		/// Must be called before calling <see cref="SaveFile(System.IO.FileInfo)"/> to update the file contents.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="contents"></param>
		public void UpdateFileContentInMemory(FileInfo info, IEnumerable<string> contents)
		{
			string key = GetKey(info);

			_contents[key] = contents;
		}

		/// <summary>
		/// Checks whether the file is in memory.
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public bool IsFileInMemory(FileInfo info)
		{
			string key = GetKey(info);

			return _files.ContainsKey(key);
		}

		/// <summary>
		/// Checks whether the file source has been modified.
		/// 
		/// It first checks whether the file has been recorded previously, then compares the modification (UTC) time.
		/// </summary>
		/// <param name="info">The file info that allows for a uniform file pathing convention.</param>
		/// <returns>Whether the file has been modified by another program.</returns>
		public bool HasFileBeenModified(FileInfo info)
		{
			string path = GetKey(info);

			return IsFileInMemory(info) && File.GetLastWriteTimeUtc(path).CompareTo(_files[path]) != 0;
		}

		/// <summary>
		/// Loads and returns the content of the file into memory. Will not load files larger than the safe limit.
		/// </summary>
		/// <seealso cref="MaximumSafeFileSize">The safe limit</seealso>
		/// <param name="info">The file info that allows for a uniform file pathing convention.</param>
		/// <returns></returns>
		public void LoadFile(FileInfo info)
		{
			if (info.Extension.Equals(_ext))
			{
				var path = GetKey(info);
				var bytes = info.Length;

				if (bytes > MaximumSafeFileSize)
				{
					throw new IOException(
						string.Format("File to load is larger than safe limit of {0:n0} bytes. [file: Path={1}, Size={2:n0} bytes]",
						              MaximumSafeFileSize, path, bytes)
						);
				}

				using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
				using (var reader = new StreamReader(stream))
				{
					var content = new List<string>();
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						content.Add(line);
					}

					_files[path] = File.GetLastWriteTimeUtc(path);
					UpdateFileContentInMemory(info, content);
				}
			}
		}

        public IEnumerable<string> LoadDirectory(DirectoryInfo dI)
        {
            List<string> content = new List<string>();

            foreach (FileInfo fileInfo in dI.GetFiles())
            {
                    LoadFile(fileInfo);
                    content.Add(fileInfo.Name);
            }

            return content;
        }

		/// <summary>
		/// Saves the contents in memory to disk and updates the last write time (UTC).
		/// </summary>
		/// <param name="info">The file info that allows for a uniform file pathing convention.</param>
		public void SaveFile(FileInfo info)
		{
			string path = GetKey(info);

			using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
			using (var writer = new StreamWriter(stream))
			{
				foreach (string line in _contents[path])
				{
					writer.WriteLine(line);
				}
			}

			_files[path] = File.GetLastWriteTimeUtc(path);
		}

		/// <summary>
		/// Updates the contents in memory, saves it to disk and updates the last write time.
		/// </summary>
		/// <param name="info"></param>
		/// <param name="contents"></param>
		public void SaveFile(FileInfo info, IEnumerable<string> contents)
		{
			UpdateFileContentInMemory(info, contents); // update the file contents in memory
			SaveFile(info); // writes the memory to disk
		}

		/// <summary>
		/// Gets the file from memory if it is loaded, otherwise null
		/// </summary>
		/// <param name="fileInfo"></param>
		/// <returns></returns>
		public IEnumerable<string> GetFileFromMemory(FileInfo fileInfo)
		{
			string key = GetKey(fileInfo);
			return _contents.ContainsKey(key) ? _contents[key] : null;
		}
	}
}