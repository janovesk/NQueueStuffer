using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NQueueStuffer.UI
{
	[Serializable]
	public class NqsSettingItem
	{
		public string AssemblyPath { get; set; }
		public string SelectedTypeName { get; set; }
		public string QueueName { get; set; }
		public string MessageContent { get; set; }
		public bool? IsLocked { get; set; }

		internal Type SelectedType { get; set; }


	}
}
