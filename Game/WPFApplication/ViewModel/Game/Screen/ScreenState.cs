using WPFApplication.Core;
using System;
using System.Windows.Documents;
using Net.Contexts;
using Net.Clients;

namespace WPFApplication.ViewModel
{
    public abstract class ScreenState : ObservableObject, IFlowStory
    {
        private FlowDocument storyLog = new FlowDocument(new Paragraph());

        protected IClient client;

        protected Paragraph StoryParagraph => (Paragraph)StoryLog.Blocks.FirstBlock;

        public FlowDocument StoryLog
        {
            get => storyLog;
            set
            {
                storyLog = value;
                OnPropertyChanged(nameof(StoryLog));
            }
        }

        public ScreenState(IClient client)
        {
            this.client = client;
        }

        public event EventHandler<bool>? FadeRequested;

        public abstract void HandleContext(Context c);

        public void StoryNewLine()
        {
            StoryParagraph.Inlines.Add(new LineBreak());
        }

        public void StoryRun(Run line)
        {
            StoryParagraph.Inlines.Add(line);
        }

        public void StoryClear()
        {
            StoryParagraph.Inlines.Clear();
        }

        protected virtual void OnFadeRequested(bool args)
        {
            FadeRequested?.Invoke(this, args);
        }
    }
}