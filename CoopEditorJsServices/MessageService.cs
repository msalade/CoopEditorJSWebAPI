using System;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using CoopEditorJSEnitites.Messages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoopEditorJsServices
{
	public class MessageService : IMessageService
	{
		public dynamic DeserializeMessage(string message)
		{
			try
            {
                dynamic jMessage = JObject.Parse(message);
				
				switch (jMessage.type.ToString())
				{
					case "Chat":
                        return JsonConvert.DeserializeObject<ChatMessage>(message);
                    case "Code":
						return JsonConvert.DeserializeObject<CodeMessage>(message);
                    case "Controll":
						return JsonConvert.DeserializeObject<ControllMessage>(message);
                    case "Error":
						return JsonConvert.DeserializeObject<ErrorMessage>(message);
                    case "JHS":
						return JsonConvert.DeserializeObject<JHSMessage>(message);
                    default:
						return JsonConvert.DeserializeObject<BaseMessage>(message);
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
