using System;
using System.Windows;
using System.Windows.Media;

namespace TouchKeyboardSample.Extensions
{
    public static class DependencyObjectExtensions
    {
        /// <summary>
        /// Returns the visual parent of the <see cref="DependencyObject"/>
        /// that is of type T, or null if no parent exists.
        /// </summary>
        /// <typeparam name="T">
        /// Type of parent to return.
        /// </typeparam>
        /// <param name="dependencyObject">
        /// This <see cref="DependencyObject"/> reference.
        /// </param>
        /// <returns>
        /// The visual parent of the <see cref="DependencyObject"/>
        /// that is of type T, or null if no parent exists.
        /// </returns>
        public static T FindParent<T>(this DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }

            T parent;

            do
            {
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

                if (dependencyObject == null) return null;

                parent = dependencyObject as T;

            } while (parent == null);

            return parent;
        }
    }
}
