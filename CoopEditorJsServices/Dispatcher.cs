using System;
using System.Collections.Generic;
using System.Linq;
using CoopEditorJsServices.Interfaces;

namespace CoopEditorJsServices
{
	public class Dispatcher : IDispatcher
	{
		public List<Action> pending;

		public Dispatcher()
		{
			pending = new List<Action>();
		}
		

		public void Invoke(Action fn)
		{
			lock (pending)
			{
				pending.Add(fn);

			}
		}

		public void InvokePending()
		{
			lock (pending)
			{
				foreach (var action in pending)
				{
					action();
				}

				pending.Clear();
			}
		}

		public bool IsEmpty()
		{
			lock (pending)
			{
				return pending != null && pending.Any();
			}
		}
	}
}
