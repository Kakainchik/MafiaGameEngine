using GameRemoteServer.Entities;
using GameRemoteServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GameRemoteServer.Hubs
{
    [Authorize]
    public class LobbyHub : Hub<ILobbyClient>
    {
        private readonly IHallService hallService;
        private readonly IUserService userService;

        public LobbyHub(IHallService hallService, IUserService userService)
        {
            this.hallService = hallService;
            this.userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task JoinLobby(long lobbyId)
        {
            //Get authorized user that sure is not null
            long userId = long.Parse(Context.User!.FindFirst("Id")!.Value);
            User user = await userService.GetUserById(userId);

            if(hallService.JoinLobby(lobbyId, user, out bool isHost))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, lobbyId.ToString());
                await Clients.OthersInGroup(lobbyId.ToString()).JoinAsync();

                if(isHost)
                {

                }
            }
        }

        public async Task UpdateMaxPlayer(long lobbyId, int capacity)
        {

            hallService.UpdateMaxCapacity(lobbyId, capacity);
        }
    }
}