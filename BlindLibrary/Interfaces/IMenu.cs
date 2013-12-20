using System;
using System.Collections.Generic;

namespace BlindLibrary
{
	public interface IMenu
	{
		void Init(string shcemeFile);
		
		void Show();
	
		List<MenuItem> GoToChild(MenuItem item);
		
		void Clear();
		
		void Execute(MenuItem item);
	}
}

