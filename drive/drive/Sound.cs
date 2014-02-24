using System;
using System.Media;
using System.IO;

namespace drive
{
	public class Sound
	{
		public static void Play(string file, int delay = 100)
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
				Console.WriteLine(ex.Message);
			}
		}
	}
}

