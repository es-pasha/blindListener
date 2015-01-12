using System;
using System.IO;
using System.Collections.Generic;

namespace drive
{
	public class DrivesDetector
	{
		private List<string> lastDrives = null;
		private DriveInfo CurrentDrive = null;
		private List<DriveInfo> drives = null;
			
		public DrivesDetector ()
		{
			lastDrives = new List<string>();
			drives = new List<DriveInfo>();
		}
		
		public DriveInfo GetConnectedDrive()
		{
			drives = GetDrives();
			CurrentDrive = null;
			foreach(var drive in drives){
				if (!lastDrives.Contains(drive.Name)){
					CurrentDrive = drive;
					Print(drive);	
					break;
				}
			}
			
			RewriteLastDrives();
			
			return CurrentDrive;
		}
		
		private List<DriveInfo> GetDrives()
		{
			var temp_drives = new List<DriveInfo>();
			
			foreach (var d in DriveInfo.GetDrives()) {
				if (d.IsReady) {
					if (d.DriveType == DriveType.Removable 
						&& (d.DriveFormat.ToLower ().Contains ("fat") 
							|| d.DriveFormat.ToLower ().Contains ("ntfs") 
							|| d.DriveFormat.ToLower ().Contains ("fuseblk"))) {
						temp_drives.Add (d);
					}
				}
			}

			temp_drives.Reverse ();
			return temp_drives;
		}
		
		private void RewriteLastDrives(){
			lastDrives.Clear();
			foreach(var drive in drives){
				lastDrives.Add(drive.Name);
			}			
		}
		
		public void Print(DriveInfo d)
		{
			Console.WriteLine("--------------------------------");
			Console.WriteLine("\t" + d.Name);
			Console.WriteLine("\t" + d.DriveType);
			Console.WriteLine("\t" + d.DriveFormat);
			Console.WriteLine("--------------------------------");
		}		
		
		
		
	}
}

