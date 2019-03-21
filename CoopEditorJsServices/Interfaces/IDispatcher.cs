using System;

namespace CoopEditorJsServices.Interfaces
{
	public interface IDispatcher
	{
		void Invoke(Action fn);
		void InvokePending();
		bool IsEmpty();
	}
}
