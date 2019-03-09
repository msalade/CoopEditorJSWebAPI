using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoopEditorJsServices.Interfaces;
using CoopEditorJSEnitites;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CoopEditorJsServices.Middleware
{
	public class EditorWebSocketMiddleware
	{
		private readonly RequestDelegate _next;
		private Room _globalRoom;
		private HashSet<Room> _privateRooms;
		private IWebSocketsService _webSocketsService = new WebSocketsService();
		private IRoomService _roomService = new RoomService();

		public EditorWebSocketMiddleware(RequestDelegate next)
		{
			_next = next;
			_globalRoom = new Room("Global room");
			_globalRoom.UsersList = new HashSet<User>();
			_privateRooms = new HashSet<Room>();
		}

		public async Task Invoke(HttpContext context)
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
				var buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Message()));
				var segment = new ArraySegment<byte>(buffer);
				Task sendTask = currentSocket.SendAsync(segment, WebSocketMessageType.Text, true, requesdToken);
				sendTask.Wait();
			}

			while (currentSocket.State == WebSocketState.Open && !requesdToken.IsCancellationRequested)
			{
				try
				{
					string response = await _webSocketsService.ExtractMessage(currentSocket, requesdToken);

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

		private async Task<Task> AcceptNewUser(WebSocket socket, string socketId)
		{
			User newUser = new User()
			{
				WebSocket = socket,
			};

			_globalRoom.UsersList.Add(newUser);
			return Task.CompletedTask;
		}
	}
}
