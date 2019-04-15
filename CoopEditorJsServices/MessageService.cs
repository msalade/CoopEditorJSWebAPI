using System;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using CoopEditorJSEnitites.Messages;
using Newtonsoft.Json;

namespace CoopEditorJsServices
{
	public class MessageService : IMessageService
	{
		public dynamic DeserializeMessage(dynamic message)
		{
			try
			{
				var deserializeObject = JsonConvert.DeserializeObject(message);

				switch (deserializeObject?.GetType().ToString())
				{
					case "ChatMessage":
						return (ChatMessage) deserializeObject;
					case "CodeMessage":
						return (CodeMessage) deserializeObject;
					case "ControllMessage":
						return (ControllMessage) deserializeObject;
					case "ErrorMessage":
						return (ErrorMessage) deserializeObject;
					case "JHSMessage":
						return (JHSMessage) deserializeObject;
					default:
						return (BaseMessage) deserializeObject;
				}
			}
			catch (Exception e)
			{
				return new ErrorMessage(e.Message);
			}
		}

		public string ParseMessage(dynamic message)
		{
			string stringMessage;

			try
			{
				stringMessage = JsonConvert.SerializeObject(message);
			}
			catch (Exception e)
			{
				stringMessage = JsonConvert.SerializeObject(new ErrorMessage(e.Message));
			}

			return stringMessage;
		}
	}
}
