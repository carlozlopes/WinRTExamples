﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuadraticEaseType.cs" company="Jeremy Likness">
//   Copyright (c) Jeremy Likness
// </copyright>
// <summary>
//   The quadratic ease type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AnimationsExample
{
    using System;

    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>
    /// The bounce ease type.
    /// </summary>
    public class QuadraticEaseType : BaseAnimationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationsExample.QuadraticEaseType"/> class. 
        /// </summary>
        public QuadraticEaseType()
        {
            this.Description = "Quadratic Ease";
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public override sealed string Description { get; protected set; }

        /// <summary>
        /// The start animation.
        /// </summary>
        /// <param name="target">
        /// The target.
        /// </param>
        /// <returns>
        /// The <see cref="Storyboard"/>.
        /// </returns>
        public override Storyboard GenerateAnimation(FrameworkElement target)
        {
            var ease = new QuadraticEase
            {
                EasingMode = EasingMode.EaseInOut
            };

            var doubleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 800,
                Duration = TimeSpan.FromSeconds(5),
                EasingFunction = ease
            };
            Storyboard.SetTarget(doubleAnimation, target);
            Storyboard.SetTargetProperty(
                doubleAnimation,
                "(FrameworkElement.RenderTransform).(TranslateTransform.X)");
            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimation);
            return storyboard;
        }
    }
}