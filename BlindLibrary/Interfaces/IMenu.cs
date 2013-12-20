using System;
using System.Collections.Generic;


namespace BlindLibrary
{
	public interface IMenu
	{
		Menu Init(string shcemeFile);
		
		void Show();
	
		void GoToChild(MenuItem item);
		
		void GoToParent();
		
		void Clear();
		
		void Execute(int index);
	}
}

