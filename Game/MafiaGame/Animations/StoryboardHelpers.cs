using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace MafiaGame.Animations
{
    /// <summary>
    /// Animation helpers for <see cref="Storyboard"/>
    /// </summary>
    public static class StoryboardHelpers
    {
        /// <summary>
        /// Adds a slide from right animation to the soryboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelarationRatio">The rate of deceleration</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float decelarationRatio = 0.9F)
        {
            //Create the margin animate from right
            var Animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(offset, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelarationRatio
            };
            //Set the target property name
            Storyboard.SetTargetProperty(Animation, new PropertyPath("Margin"));

            storyboard.Children.Add(Animation);
        }

        /// <summary>
        /// Adds a slide to left animation to the soryboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelarationRatio">The rate of deceleration</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelarationRatio = 0.9F)
        {
            //Create the margin animate from right
            var Animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, offset, 0),
                DecelerationRatio = decelarationRatio
            };
            //Set the target property name
            Storyboard.SetTargetProperty(Animation, new PropertyPath("Margin"));

            storyboard.Children.Add(Animation);
        }

        /// <summary>
        /// Adds fade in animation to the soryboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            //Create the margin animate from right
            var Animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,
            };
            //Set the target property name
            Storyboard.SetTargetProperty(Animation, new PropertyPath("Opacity"));

            storyboard.Children.Add(Animation);
        }

        /// <summary>
        /// Adds fade out animation to the soryboard
        /// </summary>
        /// <param name="storyboard">The storyboard to add the animation to</param>
        /// <param name="seconds">The time the animation will take</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            //Create the margin animate from right
            var Animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 2,
            };
            //Set the target property name
            Storyboard.SetTargetProperty(Animation, new PropertyPath("Opacity"));

            storyboard.Children.Add(Animation);
        }
    }
}