using System;
using System.Windows;
using System.Windows.Controls;

namespace TouchKeyboardSample.Extensions
{
    public static class FrameworkElementExtensions
    {
        /// <summary>
        /// Causes the element to be scrolled to the top of the viewable area.
        /// </summary>
        /// <param name="frameworkElement">
        /// <see cref="FrameworkElement"/> that will be scrolled to the top of 
        /// the viewable area.
        /// </param>
        public static void BringIntoTopOfView(this FrameworkElement frameworkElement)
        {
            if (frameworkElement == null)
            {
                throw new ArgumentNullException("frameworkElement");
            }

            var parentScrollViewer = frameworkElement.FindParent<ScrollViewer>();
            if (parentScrollViewer != null)
            {
                parentScrollViewer.ScrollToBottom();
                frameworkElement.BringIntoView();
            }
        }
    }
}
