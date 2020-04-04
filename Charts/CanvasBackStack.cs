using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace Unigram.Charts
{
    public class CanvasBackStack
    {
        private readonly CanvasControl _canvas;
        private readonly Stack<CanvasBackStackItem> _items;

        public CanvasBackStack(CanvasControl canvas)
        {
            _canvas = canvas;
        }
    }

    public class CanvasBackStackItem
    {
        public Matrix3x2 Transform { get; set; }
        public Rect? Clip { get; set; }

        public CanvasBackStackItem(Matrix3x2 transform)
        {
            Transform = transform;
        }
        
        public CanvasBackStackItem(Rect clip)
        {
            Clip = clip;
        }
    }
}
