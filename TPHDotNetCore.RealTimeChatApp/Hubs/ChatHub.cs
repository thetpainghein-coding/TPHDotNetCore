﻿using Microsoft.AspNetCore.SignalR;

namespace TPHDotNetCore.RealTimeChatApp.Hubs
{
	public class ChatHub : Hub
	{
		public async Task ServerReceiveMessage(string user, string message)
		{
			await Clients.All.SendAsync("ClientReceiveMessage", user, message);
		}
	}
}
