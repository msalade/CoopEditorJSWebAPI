using System.Threading.Tasks;
using CoopEditorJSEnitites.Messages;

namespace CoopEditorJsServices.Interfaces
{
	public interface IMessageHandler<in TMessage> where TMessage : BaseMessage
	{
		void Handle(TMessage message);
	}
}
