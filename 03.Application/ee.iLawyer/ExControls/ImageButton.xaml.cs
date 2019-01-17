using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ee.iLawyer.ExControls
{
    public partial class ImageButton : Button
    {
        public DrawingImage DrawingImage
        {
            get { return (DrawingImage)GetValue(DrawingImageProperty); }
            set { SetValue(DrawingImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DrawingImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DrawingImageProperty =
            DependencyProperty.Register("DrawingImage", typeof(DrawingImage), typeof(ImageButton), new PropertyMetadata(new DrawingImage(), OnPropertyChanged));

        public Brush DefaultFillBrush
        {
            get { return (Brush)GetValue(DefaultFillBrushProperty); }
            set { SetValue(DefaultFillBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultFillBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultFillBrushProperty =
            DependencyProperty.Register("DefaultFillBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(Brushes.DarkGray));

        public Brush MouseOverBrush
        {
            get { return (Brush)GetValue(MouseOverBrushProperty); }
            set { SetValue(MouseOverBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseOverBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBrushProperty =
            DependencyProperty.Register("MouseOverBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(Brushes.DeepSkyBlue));

        public Brush IsPressedBrush
        {
            get { return (Brush)GetValue(IsPressedBrushProperty); }
            set { SetValue(IsPressedBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPressedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPressedBrushProperty =
            DependencyProperty.Register("IsPressedBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(Brushes.DodgerBlue));

        public Brush IsEnabledBrush
        {
            get { return (Brush)GetValue(IsEnabledBrushProperty); }
            set { SetValue(IsEnabledBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEnabledBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledBrushProperty =
            DependencyProperty.Register("IsEnabledBrush", typeof(Brush), typeof(ImageButton), new PropertyMetadata(Brushes.LightGray));



        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);

            var drawing = (e.NewValue as DrawingImage).Drawing;

            if (drawing is DrawingGroup)
            {
                var drawingGroup = drawing as DrawingGroup;

                if (drawingGroup.Children != null)
                {
                    foreach (var item in drawingGroup.Children)
                    {
                        var brush = (item as GeometryDrawing).Brush;
                    }
                }
            }
            if (drawing is GeometryDrawing)
            {
                var geometryDrawing = drawing as GeometryDrawing;

                var brush = (geometryDrawing as GeometryDrawing).Brush;


            }

        }
        public DrawingImage MouseOverDrawingImage
        {
            get
            {
                var cloneDrawingImage = DrawingImage.Clone();
                var drawing = cloneDrawingImage.Drawing;

                if (drawing is DrawingGroup)
                {
                    var drawingGroup = drawing as DrawingGroup;

                    if (drawingGroup.Children != null)
                    {
                        foreach (var item in drawingGroup.Children)
                        {
                            (item as GeometryDrawing).Brush=MouseOverBrush;
                        }
                    }
                }
                if (drawing is GeometryDrawing)
                {
                    var geometryDrawing = drawing as GeometryDrawing;

                   geometryDrawing.Brush = MouseOverBrush;
                }

                return cloneDrawingImage;
            }

        }


        public ImageButton()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
