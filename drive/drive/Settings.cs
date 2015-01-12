using System;
using System.Xml;

namespace drive
{
	public class Settings
	{
		public string CONTENT_PATH = "";
		public string SOUND_START_COPY = "";
		public string SOUND_FINISH_COPY = "";
		public string SOUND_LOW_SIZE = "";
		public string SOUND_CONTINUE = "";
		
		public Settings (string path = "config.xml")
		{
			XmlTextReader reader = new XmlTextReader (path);
			while (reader.Read())
			{
                if (reader.NodeType == XmlNodeType.Element) 
				{
					switch(reader.Name)
					{
						case "ContentPath":{
							reader.Read();
							CONTENT_PATH = reader.Value;
							break;
						}		
						case "SoundStartCopy":{
							reader.Read();
							SOUND_START_COPY = reader.Value;
							break;							
						}
						case "SoundFinishCopy":{
							reader.Read();
							SOUND_FINISH_COPY = reader.Value;
							break;							
						}						
						case "SoundLowSize":{
							reader.Read();
							SOUND_LOW_SIZE = reader.Value;
							break;							
						}
						case "SoundContinue":{
							reader.Read();
							SOUND_CONTINUE = reader.Value;
							break;							
						}						
					}
				}
			}
		}
	}
}

