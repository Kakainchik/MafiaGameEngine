using GameLogic.Model;
using Net.Models;

namespace Net.Contexts.Game;

//The complex context that provides all info of the entire game
//with its history

[Serializable]
public class EndGameContext : Context
{
    public Team? Winner { get; }
    public EndGamePlayerState[] Users { get; }
    public EndGameHistory[] Cycles { get; }

    public EndGameContext(Team? winner,
        EndGamePlayerState[] users,
        EndGameHistory[] history)
    {
        Winner = winner;
        Users = users;
        Cycles = history;
    }
}

[Serializable]
public class EndGamePlayerState
{
    public Guid Id { get; }
    public string Nickname { get; }
    public RGB NColor { get; }
    public RoleSignature Role { get; }
    public bool IsAlive { get; }

    public EndGamePlayerState(Guid id,
        string nickname,
        RGB color,
        RoleSignature role,
        bool isAlive)
    {
        Id = id;
        Nickname = nickname;
        NColor = color;
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