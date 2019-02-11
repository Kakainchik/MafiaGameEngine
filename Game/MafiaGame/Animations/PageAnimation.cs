using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaGame.Animations
{
    /// <summary>
    /// Styles of page animations for appearing\disapperaring
    /// </summary>
    public enum PageAnimation
    {
        /// <summary>
        /// No animation takes place
        /// </summary>
        None = 0,

        /// <summary>
        /// The page slides in and fades in from the right
        /// </summary>
        SlideAndFadeInFromRight = 1,

        /// <summary>
        /// The page slides out and fades out to the left
        /// </summary>
        SlideAndFadeOutToLeft = 2
    }
}
