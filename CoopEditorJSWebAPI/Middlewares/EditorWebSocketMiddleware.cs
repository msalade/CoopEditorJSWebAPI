using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites.Messages;
using CoopEditorJSWebAPI.Configuration;
using Microsoft.AspNetCore.Http;

namespace CoopEditorJsServices.Middleware
{
	public sealed class EditorWebSocketMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebSocketsService _webSocketsService;
		private readonly IMessageService _messageService;
		private readonly IDispatcher _dispatcher;
		private readonly IMessageHandler<BaseMessage> _messageHandler;
		private readonly IRoomService _roomService;

		public EditorWebSocketMiddleware(RequestDelegate next)
		{
			_next = next;
			_webSocketsService = DependencyInjectionConfiguration.GetContainer().GetInstance<IWebSocketsService>();
			_messageService = DependencyInjectionConfiguration.GetContainer().GetInstance<IMessageService>();
			_dispatcher = DependencyInjectionConfiguration.GetContainer().GetInstance<IDispatcher>();
			_messageHandler = DependencyInjectionConfiguration.GetContainer().GetInstance<IMessageHandler<BaseMessage>>();
			_roomService = DependencyInjectionConfiguration.GetContainer().GetInstance<IRoomService>();
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (!context.WebSockets.IsWebSocketRequest)
			{
				await _next.Invoke(context);
				return;
			}

			CancellationToken requesdToken = context.RequestAborted;
			WebSocket currentSocket = await context.WebSockets.AcceptWebSocketAsync();

			_roomService.AddNewUser(Guid.NewGuid().ToString(), currentSocket);

			while (currentSocket.State == WebSocketState.Open && !requesdToken.IsCancellationRequested)
			{
				try
				{
					string response = await _webSocketsService.ExtractMessage(currentSocket, requesdToken);
					var message = _messageService.ParseMessage(response);
					_messageHandler.Handle(message);


					if (!_dispatcher.IsEmpty())
						_dispatcher.InvokePending();
				}
				catch (WebSocketException ex)
				{
					_messageHandler.Handle(new ErrorMessage(ex.Message));
				}
				catch (Exception ex)
				{
					_messageHandler.Handle(new ErrorMessage(ex.Message));
				}
			}
		}
	}
}
