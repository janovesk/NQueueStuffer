using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace NQueueStuffer.UI
{
	public class NqsSettingsLoader
	{
		private string _fileName = null;
		public NqsSettingsLoader(string fileName)
		{
			_fileName = fileName;
		}

		public List<NqsSettingItem> Load()
		{
			var res = new List<NqsSettingItem>();
			using (var file = new StreamReader(_fileName))
			{
				using (XmlReader reader = XmlReader.Create(file))
				{
					var serilizer = new XmlSerializer(typeof (List<NqsSettingItem>));
					if (serilizer.CanDeserialize(reader))
					{
						res = serilizer.Deserialize(reader) as List<NqsSettingItem>;
					}
				}
			}
			return res;
		}

		public void Save(List<NqsSettingItem> items)
		{
			using (var file = new StreamWriter(_fileName))
			{
				using (XmlWriter writer = XmlWriter.Create(file))
				{
					var serilizer = new XmlSerializer(typeof (List<NqsSettingItem>));
					serilizer.Serialize(writer, items);
					writer.Flush();
				}
				file.Flush();
			}
		}

	}
}
