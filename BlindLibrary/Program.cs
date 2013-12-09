using System;
using System.IO;
using System.Media;

namespace BlindLibrary
{
	class MainClass
	{
		public static Menu mainMenu = new Menu ();

		public static void Main (string[] args)
		{
			Console.WriteLine ("Старт программы, версия 0.01");
			prepareMenu ();
			while (1 == 1) {
				mainMenu.show ();

				var command = Console.ReadKey().KeyChar;

				foreach (var im in mainMenu.menu) {
					if (command == im.number) {
						mainMenu = im.subMenu;
						continue;
					}
				}

				if (command == '0')
					return;

				Console.WriteLine ();
			}
		}

		public static void prepareMenu()
		{
			var m1 = new ItemMenu ('1', "Загрузити новий випуск", "1.wav");
			mainMenu.addItemMenu(m1);

			var m11 = new ItemMenu ('1', "Загрузити всі нові випуски", "11.wav");
			var m12 = new ItemMenu ('2', "Вибрати нові випуски для загрузки", "12.wav");
			var m13 = new ItemMenu ('3', "Назад", "12.wav");
			var menu1 = new Menu (new System.Collections.Generic.List<ItemMenu> (){ m11, m12, m13 }, mainMenu.menu);
			m1.subMenu = menu1;

			var m2 = new ItemMenu ('2', "Видалити старі випуски", "2.wav");
			mainMenu.addItemMenu(m2);

			var m3 = new ItemMenu ('3', "Обране", "3.wav");
			mainMenu.addItemMenu(m3);

			var m4 = new ItemMenu ('4', "Вільне місце", "4.wav");
			mainMenu.addItemMenu(m4);

			var m0 = new ItemMenu ('0', "Вихід", "0.wav");
			mainMenu.addItemMenu(m0);
		}
	}
}