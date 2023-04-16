using GameLogic;
using GameLogic.Interfaces;
using Net.Contexts.Day;
using Net.Contexts.Game;
using Net.Servers;
using System.Timers;

namespace Net.Manager.Day
{
    public class MajorityDayManager : DayManager
    {
        private IVotable? gonnaElected;

        protected override ITimerFacade StepFacade { get; }

        public MajorityDayManager(LANServer server, Game game, IEnumerable<Player> players)
            : base(server, game, players)
        {
            var steps = new TimerStruct[]
            {
                new TimerStruct(AVERAGE_INTERVAL.TotalMilliseconds, StartBallot),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, EndDay),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, Exit)
            };
            StepFacade = new TimerFacade(steps);
        }

        public override void StartDay()
        {
            var msg = new DayContext(DayStep.START_DAY, game.Day);
            server.BroadcastSessionMessage(msg);

            var tmsg = new TimerContext(AVERAGE_INTERVAL, true);
            server.BroadcastSessionMessage(tmsg);

            StepFacade.First();
        }

        private void StartBallot()
        {
            //As game just started there is not elections
            if(game.Day == 1)
            {
                var fmsg = new DayContext(DayStep.FIRST_DAY_CASE);
                server.BroadcastSessionMessage(fmsg);

                //Next turn
                StepFacade.Exit();
            }
            else
            {
                Cycle.IsBallotBegan = true;

                var message = new DayContext(DayStep.START_BALLOT);
                server.BroadcastSessionMessage(message);
            }
        }

        public override void SendVoteFromTo(ulong voterId, ulong targetId)
        {
            Player voter = players.First(c => c.Id.Equals(voterId));
            Player target = players.First(c => c.Id.Equals(targetId));

            //Get previous voter's target before voting
            var previousT = voter.VoteTarget;
            if(Cycle.VoteFor(voter, target))
            {
                VoteTarget? previous = null;

                if(Cycle.IsItPenguin(previousT))
                {
                    //Do not put the id as non-lynchable object has no one
                    previous = new VoteTarget(null, previousT.Votes);
                }
                else if(previousT != null)
                {
                    Player t = (Player)previousT;
                    previous = new VoteTarget(t.Id, t.Votes);
                }

                var message = new ReceiveVoteContext(voter.Id,
                    target.Id,
                    target.Votes,
                    previous);
                server.BroadcastSessionMessage(message);

                TryGetResult();
            }
        }

        public override void SendVoteForNonLynch(ulong voterId)
        {
            Player voter = players.First(c => c.Id.Equals(voterId));

            //Get previous voter's target before voting
            var previousT = voter.VoteTarget;
            if(Cycle.VoteForNonLynch(voter))
            {
                VoteTarget? previous = null;
                if(previousT != null)
                {
                    Player t = (Player)previousT;
                    previous = new VoteTarget(t.Id, t.Votes);
                }

                //Context for non-lynch
                var message = new ReceiveVoteContext(voter.Id,
                    new VoteTarget(null, Cycle.NonLynchVotes),
                    previous);
                server.BroadcastSessionMessage(message);

                TryGetResult();
            }
        }

        public override void Unvote(ulong voterId)
        {
            Player voter = players.First(c => c.Id.Equals(voterId));

            //Get previous voter's target before voting
            var previousT = voter.VoteTarget;
            Cycle.Unvote(voter);

            VoteTarget? previous = null;
            if(Cycle.IsItPenguin(previousT))
            {
                //Do not put the name as non-lynchable object has no one
                previous = new VoteTarget(null, previousT.Votes);
            }
            else if(previousT != null)
            {
                Player t = (Player)previousT;
                previous = new VoteTarget(t.Id, t.Votes);
            }

            //Context for unvote
            var message = new ReceiveVoteContext(voter.Id,
                    null,
                    previous);
            server.BroadcastSessionMessage(message);

            TryGetResult();
        }

        private void TryGetResult()
        {
            IVotable result;
            if(Cycle.TryGetElectionResult(out result))
            {
                if(gonnaElected == result) return;

                gonnaElected = result;

                var tmsg = new TimerContext(LONG_INTERVAL, true);
                server.BroadcastSessionMessage(tmsg);

                ResetTimer(LONG_INTERVAL);

                //Send warning about election
                ulong? userId = (result as Player)?.Id ?? null;
                var pmsg = new WarningVoteContext(userId, true);
                server.BroadcastSessionMessage(pmsg);
            }
            else
            {
                gonnaElected = null;

                var tmsg = new TimerContext(TimeSpan.Zero, false);
                server.BroadcastSessionMessage(tmsg);

                electionTimer.Stop();
            }
        }

        private void EndDay()
        {
            var result = Cycle.GetElectionResult();
            gonnaElected = null;

            //Send result to all players
            var emsg = new ElectionResultContext((result as Player)?.Id ?? null);
            server.BroadcastSessionMessage(emsg);

            //Dispose votes
            Cycle.ConfirmElectionResult(result);
            Cycle.ClearVotes();

            StepFacade.Next();
        }

        protected override void Exit()
        {
            //Next turn
            OnHasEnded();
        }

        private void ResetTimer(TimeSpan interval)
        {
            electionTimer.Close();
            electionTimer.Elapsed -= ElectionTimer_Elapsed;

            electionTimer.Interval = interval.TotalMilliseconds + 1_000D;

            electionTimer.Elapsed += ElectionTimer_Elapsed;
            electionTimer.Start();
        }

        private void ElectionTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            //Election has ended, get final result
            electionTimer.Stop();
            electionTimer.Close();

            //Disallow voting
            Cycle.IsBallotBegan = false;

            //Send command to stop ballot
            var message = new DayContext(DayStep.END_BALLOT);
            server.BroadcastSessionMessage(message);

            StepFacade.Next();
        }

        #region IDisposable
#nullable disable warnings

        protected override void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    electionTimer.Stop();
                    electionTimer.Dispose();
                }

                players = null;
                gonnaElected = null;
                electionTimer = null;
                game = null;

                base.Dispose(disposing);
            }
        }

#nullable restore warnings
        #endregion
    }
}