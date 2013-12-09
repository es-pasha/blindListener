using System;
using System.IO;
using System.Collections.Generic;
using System.Media;

namespace BlindLibrary
{
	public class Menu
	{
		public List<ItemMenu> menu;
		public List<ItemMenu> parent;

		public Menu()
		{
			menu = new List<ItemMenu> ();
		}

		public Menu(List<ItemMenu> _menu, List<ItemMenu> _parent)
		{
			menu = _menu;
			parent = _parent;
		}

		public void addItemMenu(ItemMenu itemMenu)
		{
			menu.Add (itemMenu);
		}

		public void show()
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
	}

	public class ItemMenu
	{
		public char number;
		public string description;
		public string fileAudio;
		public Menu subMenu;

		public ItemMenu()
		{
		}

		public ItemMenu(char _number, string _description, string _fileAudio)
		{
			number = _number;
			description = _description;
			fileAudio = _fileAudio;
		}
	}
}

