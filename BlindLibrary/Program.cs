using System;
using System.IO;
using System.Media;
using System.Threading;

namespace BlindLibrary
{
	class MainClass
	{
		public static Menu mainMenu = new Menu ();

		public static void Main (string[] args)
		{
			Menu menu = new Menu();
			menu.Init("input.txt");		
			while(true)
			{
				menu.Show();
				var key = Console.ReadKey().KeyChar - 48;
				
				if (key == 0)
				{
					menu.GoToParent();
				}
				else
				{
					menu.Execute(key);
				}
				menu.Clear();
			}
		}
	}
}
