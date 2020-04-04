using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Unigram.Charts.Data
{
    public class StackLinearChartData : ChartData
    {
        int[] ySum;
        SegmentTree ySumSegmentTree;

        public int[][] simplifiedY;
        public int simplifiedSize;

        public StackLinearChartData(JsonObject jsonObject)
                : base(jsonObject)
        {
            int n = lines[0].y.Length;
            int k = lines.Count;

            ySum = new int[n];
            for (int i = 0; i < n; i++)
            {
                ySum[i] = 0;
                for (int j = 0; j < k; j++)
                {
                    ySum[i] += lines[j].y[i];
                }
            }
            ySumSegmentTree = new SegmentTree(ySum);
        }

        //public StackLinearChartData(ChartData data, long d)
        //{
        //    int index = Arrays.binarySearch(data.x, d);
        //    int startIndex = index - 4;
        //    int endIndex = index + 4;

        //    if (startIndex < 0)
        //    {
        //        endIndex += -startIndex;
        //        startIndex = 0;
        //    }
        //    if (endIndex > data.x.length - 1)
        //    {
        //        startIndex -= endIndex - data.x.length;
        //        endIndex = data.x.length - 1;
        //    }

        //    if (startIndex < 0)
        //    {
        //        startIndex = 0;
        //    }

        //    int n = endIndex - startIndex + 1;

        //    x = new long[n];
        //    xPercentage = new float[n];
        //    lines = new ArrayList<>();

        //    for (int i = 0; i < data.lines.size(); i++)
        //    {
        //        Line line = new Line();
        //        line.y = new int[n];
        //        line.id = data.lines.get(i).id;
        //        line.name = data.lines.get(i).name;
        //        line.colorKey = data.lines.get(i).colorKey;
        //        line.color = data.lines.get(i).color;
        //        line.colorDark = data.lines.get(i).colorDark;
        //        lines.add(line);
        //    }
        //    int i = 0;
        //    for (int j = startIndex; j <= endIndex; j++)
        //    {
        //        x[i] = data.x[j];

        //        for (int k = 0; k < lines.size(); k++)
        //        {
        //            Line line = lines.get(k);
        //            line.y[i] = data.lines.get(k).y[j];
        //        }
        //        i++;
        //    }

        //    timeStep = 86400000L;
        //    measure();
        //}

        protected override void measure()
        {
            base.measure();
            simplifiedSize = 0;
            int n = xPercentage.Length;
            int nl = lines.Count;
            int step = (int)Math.Max(1, Math.Round(n / 140f));
            int maxSize = n / step;
            simplifiedY = new int[nl][];

            for (int i = 0; i < nl; i++)
            {
                simplifiedY[i] = new int[maxSize];
            }

            int[] max = new int[nl];

            for (int i = 0; i < n; i++)
            {
                for (int k = 0; k < nl; k++)
                {
                    ChartData.Line line = lines[k];
                    if (line.y[i] > max[k]) max[k] = line.y[i];
                }
                if (i % step == 0)
                {
                    for (int k = 0; k < nl; k++)
                    {
                        simplifiedY[k][simplifiedSize] = max[k];
                        max[k] = 0;
                    }
                    simplifiedSize++;
                    if (simplifiedSize >= maxSize)
                    {
                        break;
                    }
                }
            }
        }
    }
}
