using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MafiaGame.Animations
{
    /// <summary>
    /// Helpers to animate pages in specific ways
    /// </summary>
    public static class PageAnimations
    {
        /// <summary>
        /// Slides a page in from the right
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeInFromRight(this Page page, float seconds)
        {
            //Create the story board
            var sb = new Storyboard();

            //Add slide from right
            sb.AddSlideFromRight(seconds, page.WindowWidth);

            //Add fade in animation
            sb.AddFadeIn(seconds);

            sb.Begin(page);
            
            //Make page visible
            page.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }

        /// <summary>
        /// Slides a page out to the left
        /// </summary>
        /// <param name="page">The page to animate</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <returns></returns>
        public static async Task SlideAndFadeOutToLeft(this Page page, float seconds)
        {
            //Create the story board
            var sb = new Storyboard();

            //Add slide from right
            sb.AddSlideToLeft(seconds, page.WindowWidth);

            //Add fade in animation
            sb.AddFadeOut(seconds);

            sb.Begin(page);

            //Make page visible
            page.Visibility = Visibility.Visible;

            await Task.Delay((int)(seconds * 1000));
        }
    }
}
