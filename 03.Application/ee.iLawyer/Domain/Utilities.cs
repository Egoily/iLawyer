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
    }
}
