using System;

namespace CoopEditorJsServices.Interfaces
{
	public interface IMessageBusProvider
	{
		IObservable<T> Listen<T>();
		void Send<T>(T message);
	}
}
