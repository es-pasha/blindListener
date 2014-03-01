using System;
using System.IO;
//using System.Data.Linq;
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
		public static Settings settings = null;
		
		public static void Main (string[] args)
		{
			settings = new Settings();
			
			
			_detector = new DrivesDetector();
			_manager = new ContentManager(settings.CONTENT_PATH);

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
					Sound.Play(settings.SOUND_START_COPY);
					
					if(drive.TotalSize < _manager.GetDirectorySize())
					{
						Sound.Play(settings.SOUND_LOW_SIZE);
					}
					
					_manager.CopyTo(drive);
					
					Sound.Play(settings.SOUND_FINISH_COPY);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);	
			}
		}
	}
}
