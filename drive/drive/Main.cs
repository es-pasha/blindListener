using System;
using System.IO;
using System.Data.Linq;
using System.Collections.Generic;
using System.Timers;
using System.Media;

namespace drive
{
	class MainClass
	{
		private static Timer _timer = null;
		private static DrivesDetector _detector = null;
		private static ContentManager _manager = null;
		private static Settings _settings = null;
		
		public static void Main (string[] args)
		{
			_settings = new Settings();
			
			
			_detector = new DrivesDetector();
			_manager = new ContentManager(_settings.CONTENT_PATH);
				
			_timer = new Timer();
			_timer.Interval = 500;
			_timer.Elapsed += Handle_timerElapsed;
			_timer.Start();

			Console.ReadKey();
			
		}

		static void Handle_timerElapsed (object sender, ElapsedEventArgs e)
		{
			try
			{
				var drive = _detector.GetConnectedDrive();
				if (drive != null)
				{
					Play(_settings.SOUND_START_COPY);
					
					if(drive.TotalSize < _manager.GetDirectorySize())
					{
						Play(_settings.SOUND_LOW_SIZE);
					}
					
					_manager.CopyTo(drive);
					
					Play(_settings.SOUND_FINISH_COPY);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);	
			}
		}
	

		private static void Play(string file, int delay = 100)
		{
			try
			{
				using (var stream = new FileStream (file, FileMode.Open, FileAccess.Read)) 
				{
					var player = new SoundPlayer (stream);
					player.PlaySync ();
					System.Threading.Thread.Sleep (delay);
				}
			}
			catch(Exception ex)
			{
				
			}
		}
		
	}
}
