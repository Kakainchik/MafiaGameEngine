using GameLogic.Model;
using Net.Models;
using System.Text.Json.Serialization;

namespace Net.Contexts.Game;

/*The complex context that provides all info of the entire game 
 * with its history*/

[Serializable]
public class EndGameContext : Context
{
    public Team? Winner { get; }
    public EndGamePlayerState[] Users { get; }
    public EndGameHistory[] Cycles { get; }

    [JsonConstructor]
    public EndGameContext(Team? winner,
        EndGamePlayerState[] users,
        EndGameHistory[] cycles)
    {
        Winner = winner;
        Users = users;
        Cycles = cycles;
    }
}

[Serializable]
public class EndGamePlayerState
{
    public ulong Id { get; }
    public string Nickname { get; }
    public RGB NColor { get; }
    public RoleSignature Role { get; }
    public bool IsAlive { get; }

    [JsonConstructor]
    public EndGamePlayerState(ulong id,
        string nickname,
        RGB nColor,
        RoleSignature role,
        bool isAlive)
    {
        Id = id;
        Nickname = nickname;
        NColor = nColor;
        Role = role;
        IsAlive = isAlive;
    }
}

[Serializable]
public class EndGameHistory
{
    public int Turn { get; }
    public string? DayUsernameElected { get; }
    public int DayVotesCount { get; }
    public string? LynchLastMessage { get; }
    public EndGameNightH[] NightActions { get; }
    public string[] MorningDeathsUsername { get; }

    [JsonConstructor]
    public EndGameHistory(int turn,
        string? dayUsernameElected,
        int dayVotesCount,
        string? lynchLastMessage,
        EndGameNightH[] nightActions,
        string[] morningDeathsUsername)
    {
        Turn = turn;
        DayUsernameElected = dayUsernameElected;
        DayVotesCount = dayVotesCount;
        LynchLastMessage = lynchLastMessage;
        NightActions = nightActions;
        MorningDeathsUsername = morningDeathsUsername;
    }
}

[Serializable]
public class EndGameNightH
{
    public string Executor { get; }
    public RoleSignature ERole { get; }
    public string Primary { get; }
    public bool Success { get; }
    public string? Secondary { get; }

    [JsonConstructor]
    public EndGameNightH(string executor,
        RoleSignature eRole,
        string primary,
        bool success,
        string? secondary = null)
    {
        Executor = executor;
        ERole = eRole;
        Primary = primary;
        Secondary = secondary;
        Success = success;
    }
}