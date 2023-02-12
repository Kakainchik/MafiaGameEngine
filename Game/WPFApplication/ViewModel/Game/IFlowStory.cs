using System.Windows.Documents;

namespace WPFApplication.ViewModel
{
    public interface IFlowStory
    {
        void StoryNewLine();
        void StoryRun(Run line);
        void StoryClear();
    }
}