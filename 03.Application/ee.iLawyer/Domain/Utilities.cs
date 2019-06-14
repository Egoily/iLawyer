using System;
using System.Windows;
using System.Windows.Media;

namespace ee.iLawyer.Domain
{
    public static class Utilities
    {
        public static FrameworkElement FindVisualAncestor(this Visual visual, System.Type ancestorType)
        {
            while ((visual != null && !ancestorType.IsInstanceOfType(visual)))
            {
                visual = (Visual)VisualTreeHelper.GetParent(visual);
            }

            return (FrameworkElement)visual;
        }


        public static bool IsMovementBigEnough(Point initialMousePosition, Point currentPosition)
        {
            return (Math.Abs(currentPosition.X - initialMousePosition.X) >= SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(currentPosition.Y - initialMousePosition.Y) >= SystemParameters.MinimumVerticalDragDistance);
        }

        public static T FindName<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            if (null != obj)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T && (child as T).Name.Equals(name))
                        return (T)child;
                    else
                    {
                        T childOfChild = FindName<T>(child, name);
                        if (childOfChild != null && childOfChild is T && (childOfChild as T).Name.Equals(name))
                            return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
