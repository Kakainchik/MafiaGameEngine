using Net.Contexts.Intro;
using Net.Servers;

namespace Net.Manager.Intro
{
    public class GeneralIntroManager : IntroManager
    {
        protected override ITimerFacade StepFacade { get; }

        public GeneralIntroManager(LANServer server)
            : base(server)
        {
            var steps = new TimerStruct[]
            {
                new TimerStruct(LONG_INTERVAL.TotalMilliseconds, NameOutStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, MiddleIntroStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, EndIntroStep),
                new TimerStruct(SHORT_INTERVAL.TotalMilliseconds, EndIntro),
                new TimerStruct(AVERAGE_INTERVAL.TotalMilliseconds, Exit)
            };
            StepFacade = new TimerFacade(steps);
        }

        /// <summary>
        /// Send and receive messages about nicknames before begining the story.
        /// </summary>
        /// <param name="city">The name of city in the game.</param>
        public override void Initialize(string city)
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.NAME_IN, city));

            StepFacade.First();
        }

        private void NameOutStep()
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.NAME_OUT));
        }

        public override void StartIntroStep()
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.START));

            StepFacade.Next();
        }

        private void MiddleIntroStep()
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.MIDDLE));

            StepFacade.Next();
        }

        private void EndIntroStep()
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.END));

            StepFacade.Next();
        }

        private void EndIntro()
        {
            server.BroadcastSessionMessage(new IntroContext(IntroStep.TIP));

            StepFacade.Next();
        }

        protected override void Exit()
        {
            OnHasEnded();
        }
    }
}