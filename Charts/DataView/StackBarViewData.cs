﻿using Charts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unigram.Common;
using Windows.UI;

namespace Charts.DataView
{
    public class StackBarViewData : LineViewData
    {

        public readonly Paint unselectedPaint = new Paint();
        public Color blendColor;

        public StackBarViewData(ChartData.Line line)
            : base(line)
        {
            paint.StrokeWidth = 1;
            //paint.setStyle(Paint.Style.STROKE);
            //unselectedPaint.setStyle(Paint.Style.STROKE);
            //paint.setAntiAlias(false);
        }

        //public void updateColors()
        //{
        //    super.updateColors();
        //    blendColor = Extensions.blendARGB(Theme.getColor(Theme.key_windowBackgroundWhite), lineColor, 0.3f);
        //}
    }
}
