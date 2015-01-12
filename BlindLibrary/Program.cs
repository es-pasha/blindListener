using System;
using System.IO;
using System.Media;
using System.Threading;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.DeviceNotify;

namespace BlindLibrary
{
	class MainClass
	{
		public static Menu mainMenu = new Menu ();
		public static UsbDeviceFinder MyUsbFinder;
		public static IDeviceNotifier UsbDeviceNotifier;
		
		public static void Main (string[] args)
		{
			//Find your vendor id etc by listing all available USB devices
			MyUsbFinder = new UsbDeviceFinder(0x2341, 0x0001);
			UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
			
			
			/*
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
			*/
			Console.ReadKey();
		}
	
		public UsbDevice MyUsbDevice;


		private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
		{
		    if (e.Object.ToString().Split('\n')[1].Contains("0x2341"))
		    {
		        if (e.EventType == EventType.DeviceArrival)
		        {
		            //Connect();
					Console.WriteLine("Connect!");
		        }
		        else if(e.EventType == EventType.DeviceRemoveComplete)
		        {
		            //ResetConnection();
					Console.WriteLine("Disconnect!");
		        }
		    }
		}
	}
}
