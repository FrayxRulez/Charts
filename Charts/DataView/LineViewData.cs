using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Charts.DataView
{
    public class LineViewData
    {

        public readonly Data.ChartData.Line line;
        public /*readonly*/ Color bottomLinePaint;
        public /*readonly*/ Color paint;
        public CanvasStrokeStyle style;
        public readonly Color selectionPaint;

        public CanvasPathBuilder bottomLinePath;
        public CanvasPathBuilder chartPath;
        //public readonly Path chartPathPicker = new Path();
        public ValueAnimator animatorIn;
        public ValueAnimator animatorOut;
        public int linesPathBottomSize;

        public float[] linesPath;
        public float[] linesPathBottom;

        public int lineColor;

        public bool enabled = true;

        public float alpha = 1f;

        // !!!
        public float getStrokeWidth() => 2;


        public LineViewData(Data.ChartData.Line line)
        {
            this.line = line;

            //paint.setStrokeWidth(AndroidUtilities.dpf2(2));
            //paint.setStyle(Paint.Style.STROKE);
            //if (!BaseChartView.USE_LINES)
            //{
            //    paint.setStrokeJoin(Paint.Join.ROUND);
            //}
            //paint.setColor(line.color);
            paint = line.color;

            style = new CanvasStrokeStyle();

            //bottomLinePaint.setStrokeWidth(AndroidUtilities.dpf2(1));
            //bottomLinePaint.setStyle(Paint.Style.STROKE);
            //bottomLinePaint.setColor(line.color);
            bottomLinePaint = line.color;

            //selectionPaint.setStrokeWidth(AndroidUtilities.dpf2(10));
            //selectionPaint.setStyle(Paint.Style.STROKE);
            //selectionPaint.setStrokeCap(Paint.Cap.ROUND);
            //selectionPaint.setColor(line.color);
            selectionPaint = line.color;


            linesPath = new float[line.y.Length << 2];
            linesPathBottom = new float[line.y.Length << 2];
        }

        public void updateColors()
        {
            //if (line.colorKey != null && Theme.hasThemeKey(line.colorKey))
            //{
            //    lineColor = Theme.getColor(line.colorKey);
            //}
            //else
            //{
            //    int color = Theme.getColor(Theme.key_windowBackgroundWhite);
            //    bool darkBackground = ColorUtils.calculateLuminance(color) < 0.5f;
            //    lineColor = darkBackground ? line.colorDark : line.color;
            //}
            //paint.setColor(lineColor);
            //bottomLinePaint.setColor(lineColor);
            //selectionPaint.setColor(lineColor);
        }
    }
}
