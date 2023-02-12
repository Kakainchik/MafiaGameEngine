using GameLogic.Cycles.History;
using GameLogic.Extensions;
using GameLogic.Model;
using GameLogic.Roles;

namespace GameLogic.Cycles
{
    public class MorningCycle : GameCycle
    {
        private IList<DeathDetail> result = new List<DeathDetail>();
        private IEnumerable<Player> mementoAlive;

        public int TownPlayerNumber { get; }
        public int MafiaPlayerNumber { get; }
        public int CultusPlayerNumber { get; }
        public int UndeadPlayerNumber { get; }
        public int SerialKPlayerNumber { get; }
        public int WitchPlayerNumber { get; }
        public int TerroristPlayerNumber { get; }

        public int NeutralPlayerNumber => SerialKPlayerNumber 
            + WitchPlayerNumber 
            + TerroristPlayerNumber;

        public MorningCycle(List<Player> alivePlayers, IEnumerable<Player> mementoAlive)
            : base(alivePlayers)
        {
            TownPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.TOWN);
            MafiaPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.MAFIA);
            CultusPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.CULTUS);
            UndeadPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.UNDEAD);
            SerialKPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.SERIAL_KILLER);
            WitchPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.WITCH);
            TerroristPlayerNumber = alivePlayers.Count(p => p.Role.GetTeam() == Team.TERRORIST);
            this.mementoAlive = mementoAlive;
        }

        internal event EventHandler<MorningMemento> MorningEnded;

        public IEnumerable<DeathDetail> NoteDeaths()
        {
            var deadP = mementoAlive.Where(p => p.DeathReason != null);

            foreach(var d in deadP)
            {
                result.Add(ResolveDeath(d));
            }

            MorningMemento memento = new MorningMemento
            {
                deaths = result
            };
            MorningEnded?.Invoke(this, memento);

            return result;
        }

        public bool TryResolveWinner(out Team? result)
        {
            //Everyone is dead
            if(alivePlayers.Count == 0)
            {
                //If there was a terrorist at previous night
                if(mementoAlive.Any(p => p.Role is TerroristRole))
                {
                    result = Team.TERRORIST;
                    return true;
                }

                result = null;
                return true;
            }

            //Town wins when remains alone
            if(TownPlayerNumber == alivePlayers.Count)
            {
                result = Team.TOWN;
                return true;
            }

            //Check others who stays alone
            if(MafiaPlayerNumber == alivePlayers.Count)
            {
                result = Team.MAFIA;
                return true;
            }
            if(CultusPlayerNumber == alivePlayers.Count)
            {
                result = Team.CULTUS;
                return true;
            }
            if(UndeadPlayerNumber == alivePlayers.Count)
            {
                result = Team.UNDEAD;
                return true;
            }

            //Check alone neutrals
            if(SerialKPlayerNumber == alivePlayers.Count)
            {
                result = Team.SERIAL_KILLER;
                return true;
            }
            if(WitchPlayerNumber == alivePlayers.Count)
            {
                result = Team.WITCH;
                return true;
            }
            if(TerroristPlayerNumber == alivePlayers.Count)
            {
                result = Team.TERRORIST;
                return true;
            }

            //Check one-versus-one
            if(alivePlayers.Count == 2)
            {
                //Order is important because it means priority of winners
                if(alivePlayers.Any(p => p.Role.GetTeam() == Team.SERIAL_KILLER))
                {
                    result = Team.SERIAL_KILLER;
                }
                else if(alivePlayers.Any(p => p.Role.GetTeam() == Team.MAFIA))
                {
                    result = Team.MAFIA;
                }
                else if(alivePlayers.Any(p => p.Role.GetTeam() == Team.CULTUS))
                {
                    result = Team.CULTUS;
                }
                else if(alivePlayers.Any(p => p.Role.GetTeam() == Team.WITCH))
                {
                    result = Team.WITCH;
                }
                else
                {
                    result = Team.TOWN;
                }

                return true;
            }

            result = default;
            return false;
        }

        private DeathDetail ResolveDeath(Player victim)
        {
            if(victim.DeathReason == victim)
                return new DeathDetail(victim, DeathReason.SUICIDE);
            else switch(victim.DeathReason)
                {
                    case MafiaRole _:
                        {
                            return new DeathDetail(victim, DeathReason.BY_MAFIA);
                        }
                    case VigilanteRole _:
                        {
                            return new DeathDetail(victim, DeathReason.BY_VIGILANTE);
                        }
                    case SerialKillerRole _:
                        {
                            return new DeathDetail(victim, DeathReason.BY_SERIAL_KILLER);
                        }
                    case TerroristRole _:
                        {
                            return new DeathDetail(victim, DeathReason.BY_TERRORIST);
                        }
                    default:
                        throw new Exception("Invalid death reason.");
                }
        }
    }
}