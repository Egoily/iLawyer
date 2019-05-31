using ee.iLawyer.Models;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ee.iLawyer.Domain
{
    public class DragDropHelper
    {
        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDragSourceChanged));

        // format
        private DataFormat format = null/* TODO Change to default(_) if this is not a reference type */;
        // source and target
        private Point initialMousePosition;
        private object draggedData;
        private Window topWindow;

        // singleton
        private static DragDropHelper m_instance;
        private static DragDropHelper Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new DragDropHelper();
                }

                return m_instance;
            }
        }

        public static bool GetIsDragSource(DependencyObject obj)
        {
            return System.Convert.ToBoolean(obj.GetValue(IsDragSourceProperty));
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }

        private static void IsDragSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dragSource = obj as FrameworkElement;

            if (dragSource != null)
            {
                if (object.Equals(e.NewValue, true))
                {
                    dragSource.PreviewMouseLeftButtonDown += Instance.DragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp += Instance.DragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove += Instance.DragSource_PreviewMouseMove;
                }
                else
                {
                    dragSource.PreviewMouseLeftButtonDown -= Instance.DragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp -= Instance.DragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove -= Instance.DragSource_PreviewMouseMove;
                }
            }
        }

        // DragSource
        private void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Int32 i = -1;
            Visual visual = e.OriginalSource as Visual;
            this.initialMousePosition = e.GetPosition(this.topWindow);
            String RPItemTypeName = String.Empty;

            this.topWindow = (Window)((Visual)sender).FindVisualAncestor(typeof(Window));
            this.draggedData = ((FrameworkElement)sender).DataContext;
            if (this.draggedData != null)
            {
                this.format = DataFormats.GetDataFormat(typeof(Appointment).FullName);
            }
        }

        public void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.draggedData != null && Utilities.IsMovementBigEnough(this.initialMousePosition, e.GetPosition(this.topWindow)))
            {
                DataObject data = new DataObject(this.format.Name, this.draggedData);
                try
                {
                    DragDropEffects effects = DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in DragDropHelper: " + ex.Message);
                }
                finally
                {
                    this.draggedData = null;
                }
            }
        }

        private void DragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.draggedData = null;
        }
    }
}
