using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using CoopEditorJSWebAPI.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoopEditorJsServices.Middleware
{
	public sealed class EditorWebSocketMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IWebSocketsService _webSocketsService;
		private readonly IRoomService _roomService;
		private Room _globalRoom;
		private HashSet<Room> _privateRooms;
		private Message globalMessage = new Message();

		public EditorWebSocketMiddleware(RequestDelegate next)
		{
			_next = next;
			_globalRoom = new Room("Global room");
			_globalRoom.UsersList = new HashSet<User>();
			_privateRooms = new HashSet<Room>();
			_webSocketsService = DependencyInjectionConfiguration.GetContainer().GetInstance<IWebSocketsService>();
			_roomService = DependencyInjectionConfiguration.GetContainer().GetInstance<IRoomService>();
		}

		private async Task<Task> AcceptNewUser(WebSocket socket, string socketId)
		{
			User newUser = new User()
			{
				WebSocket = socket,
			};

			_globalRoom.UsersList.Add(newUser);
			return Task.CompletedTask;
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
			var socketId = Guid.NewGuid().ToString();

			await AcceptNewUser(currentSocket, socketId);

			lock (currentSocket)
			{
				var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(globalMessage));
				var segment = new ArraySegment<byte>(buffer);
				Task sendTask = currentSocket.SendAsync(segment, WebSocketMessageType.Text, true, requesdToken);
				sendTask.Wait();
			}

			while (currentSocket.State == WebSocketState.Open && !requesdToken.IsCancellationRequested)
			{
				try
				{
					string response = await _webSocketsService.ExtractMessage(currentSocket, requesdToken);
					var msg = JsonConvert.DeserializeObject<Message>(response);
					if (msg != null)
					{
						globalMessage = msg;
					}

					_webSocketsService.HandleMessage(response, socketId, _globalRoom, requesdToken);
				}
				catch (WebSocketException ex)
				{
					break;
				}
				catch (Exception ex)
				{
					break;
				}
			}
		}
	}
}
