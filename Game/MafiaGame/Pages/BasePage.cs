using MafiaGame.Animations;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MafiaGame.Pages
{
    /// <summary>
    /// A base page for all pages to gain base funcrionality
    /// </summary>
    public class BasePage : Page
    {
        #region Properties

        /// <summary>
        /// The animation the play when the page is first loaded
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeInFromRight;

        /// <summary>
        /// The animation the play when the page is unloaded
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeOutToLeft;

        /// <summary>
        /// The time any slide animation takes to complete
        /// </summary>
        public float SlideSeconds { get; set; } = 0.8F;
        
        #endregion
        
        //Constructor
        public BasePage()
        {
            //If we are animating in, hide to begin with
            if(this.PageLoadAnimation != PageAnimation.None)
                this.Visibility = Visibility.Collapsed;
            //Listen out for the page loading
            this.Loaded += BasePage_Loaded;
        }

        #region Animation Load\Unload

        /// <summary>
        /// Once the page is loaded
        /// </summary>
        private async void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }

        public async Task AnimateIn()
        {
            if(this.PageLoadAnimation == PageAnimation.None) return;
            
            switch(this.PageLoadAnimation)
            {
                case PageAnimation.SlideAndFadeInFromRight:
                    {
                        //Start the animation
                        await this.SlideAndFadeInFromRight(SlideSeconds);
                        break;
                    }
            }
        }

        /// <summary>
        /// Animate the page out
        /// </summary>
        public async Task AnimateOut()
        {
            if(this.PageUnloadAnimation == PageAnimation.None) return;

            switch(this.PageUnloadAnimation)
            {
                case PageAnimation.SlideAndFadeOutToLeft:
                    {
                        //Start the animation
                        await this.SlideAndFadeOutToLeft(SlideSeconds);
                        break;
                    }
            }
        }

        #endregion

    }
}