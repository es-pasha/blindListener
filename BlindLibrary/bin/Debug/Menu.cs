using System;
using System.IO;
using System.Collections.Generic;
using System.Media;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace BlindLibrary
{
	public class Menu : IMenu
	{
		public List<MenuItem> _menu;
		public Stack<List<MenuItem>> _parents;
		
		#region Public methods
		public Menu()
		{
			_menu = new List<MenuItem> ();
			_parents = new Stack<List<MenuItem>>();
		}
	
		public Menu Init(string shcemeFile)
		{
			if (!File.Exists(shcemeFile))
			{
				throw new Exception("Shceme file not exsits!");
			}
			
			_menu = Parse( JArray.Parse( File.ReadAllText( shcemeFile ) ) );
			
			return this;
		}
		
		public void Show()
		{
			for (var i = 0; i < _menu.Count; i++)
			{
				Console.WriteLine ("{0} - {1}", i + 1, _menu[i].title);
				if (File.Exists ("Resources/sounds/" + _menu[i].sound)) 
				{
					if (File.Exists (string.Format("Resources/sounds/n{0}.wav", i + 1)))
						this.Play(string.Format("Resources/sounds/n{0}.wav", i + 1));

					this.Play("Resources/sounds/" + _menu[i].sound, 500);
				}
			}
			
			if (_parents.Count > 0)
			{
				Console.WriteLine ("0 - Back");
				this.Play("Resources/sounds/back.wav");
			}
			
		}
		
		public void GoToChild(MenuItem item)
		{
			if (item.subMenu.Count > 0)
			{
				_parents.Push(_menu);
				_menu = item.subMenu;
			}
		}
		
		public void GoToParent()
		{
			if (_parents.Count > 0)
			{
				_menu = _parents.Pop();
			}
			
		}
		
		public void Clear()
		{
			Console.Clear();
		}
	
		public void Execute(int index)
		{
			if (--index < _menu.Count && index > -1)
			{
				if (_menu[index].subMenu.Count > 0)
				{
					this.GoToChild(_menu[index]);
					return;
				}
				
				Type type = typeof(Package);
	            MethodInfo method = type.GetMethod(_menu[index].method);
	            Package package = new Package();
	            method.Invoke(package, null);
			}
		}
		#endregion
		
		
		#region Private methods
		private List<MenuItem> Parse(JArray json)
		{
			var menu = new List<MenuItem>();			
			foreach (var item in json)
			{
				var menuItem = new MenuItem();
				menuItem.title = item["title"].ToString();
				menuItem.sound = item["sound"].ToString();
				menuItem.method = item["method"].ToString();
				
				var buff = JArray.Parse(item["subMenu"].ToString());
				menuItem.subMenu = Parse(buff);
				
				menu.Add(menuItem);
			}
			return menu;
		}
		
		private void Play(string file, int delay = 100)
		{
			using (var stream = new FileStream (file, FileMode.Open, FileAccess.Read)) 
			{
				var player = new SoundPlayer (stream);
				player.PlaySync ();
				System.Threading.Thread.Sleep (delay);
			}
		}
		#endregion
	}

	public class MenuItem
	{
		public string title;
		public string sound;
		public string method;
		public List<MenuItem> subMenu;

		public MenuItem(){}
	}
}

