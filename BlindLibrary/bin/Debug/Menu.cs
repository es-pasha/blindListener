using System;
using System.IO;
using System.Collections.Generic;
using System.Media;

namespace BlindLibrary
{
	public class Menu : IMenu
	{
		public List<MenuItem> menu;
		public List<MenuItem> parent;

		public Menu()
		{
			menu = new List<MenuItem> ();
		}

		public Menu(List<MenuItem> _menu, List<MenuItem> _parent)
		{
			menu = _menu;
			parent = _parent;
		}

		public void addItemMenu(MenuItem itemMenu)
		{
			menu.Add (itemMenu);
		}
	
		public void Init(string shcemeFile)
		{
			
		}
		
		public void Show()
		{
			foreach (var item in menu) {
				Console.WriteLine ("{0} - {1}", item.number, item.description.Replace("і", "i"));
				if (File.Exists ("Resources/sounds/" + item.fileAudio)) {
					if (File.Exists ("Resources/sounds/n" + item.number + ".wav"))
						using (var file = new FileStream ("Resources/sounds/n" + item.number + ".wav", FileMode.Open, FileAccess.Read)) {
							var player = new SoundPlayer (file);
							player.PlaySync ();
							System.Threading.Thread.Sleep (100);
						}

					using (var file = new FileStream ("Resources/sounds/" + item.fileAudio, FileMode.Open, FileAccess.Read)) {
						var player = new SoundPlayer (file);
						player.PlaySync ();
						System.Threading.Thread.Sleep (500);
					}
				}
			}
		}
		
		public List<MenuItem> GoToChild(MenuItem item)
		{
			return new List<MenuItem>();
		}
		
		
	}

	public class MenuItem
	{
		public char number;
		public string description;
		public string fileAudio;
		public Menu subMenu;

		public MenuItem()
		{
		}

		public MenuItem(char _number, string _description, string _fileAudio)
		{
			number = _number;
			description = _description;
			fileAudio = _fileAudio;
		}
	}
}

