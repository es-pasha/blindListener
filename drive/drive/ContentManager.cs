using System;
using System.IO;
using System.Media;
using System.Timers;

namespace drive
{
	public class ContentManager
	{
		private string _path = "";
		private bool complated = true;
		private Timer _timer = null;
		
		public ContentManager (string src_path)
		{
			_path = src_path;
			_timer = new Timer();
			_timer.Interval = 30 * 1000;
			_timer.Elapsed += delegate {
				if (!complated){
					Sound.Play(MainClass.settings.SOUND_CONTINUE, 100);	
				}
			};
			_timer.Start();
		}
		
		public bool CopyTo(DriveInfo drive)
		{
			try
			{
				if (!string.IsNullOrEmpty(_path))
				{
					complated = false;
					var fileName = Path.GetFileName(_path); 
					CopyDirectory(_path, string.Format("{0}/{1}", drive.RootDirectory.FullName, fileName));
					complated = true;
					return true;
				}
			}
			catch(Exception ex){ }
			
			return false;
		}
		
		private void CopyDirectory(string strSource, string strDestination)
		{
		    if (!Directory.Exists(strDestination))
		    {
		        Directory.CreateDirectory(strDestination);
		    }
			
		    DirectoryInfo dirInfo = new DirectoryInfo(strSource);
		    FileInfo[] files = dirInfo.GetFiles();
			
		    foreach(FileInfo tempfile in files )
		    {
		        tempfile.CopyTo(Path.Combine(strDestination,tempfile.Name), true);
		    }
			
		    DirectoryInfo[] dirctororys = dirInfo.GetDirectories();
		    foreach(DirectoryInfo tempdir in dirctororys )
		    {
		        CopyDirectory(Path.Combine(strSource, tempdir.Name), Path.Combine(strDestination, tempdir.Name));
		    }
		
		}
	
		public long GetDirectorySize(string path)
		{
			long size = 0;
			var files = new DirectoryInfo(path).GetFiles();
			foreach (var file in files)
			{
				size += file.Length;
			}
			return size;
		}
		
		public long GetDirectorySize()
		{
			return GetDirectorySize(_path);
		}
	}
}

