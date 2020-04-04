using Charts.DataView;
using Microsoft.Graphics.Canvas.Geometry;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unigram.Common;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Charts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //Loaded += MainPage_Loaded;
        }
    }
}

//        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
//        {
//            await Task.Delay(1000);

//            var file = await Package.Current.InstalledLocation.GetFileAsync("test.json");
//            var text = await FileIO.ReadTextAsync(file);
//            var json = JsonConvert.DeserializeObject<TLStatsBroadcastStats>(text);

//            var obj = JsonObject.Parse(json.FollowersGraph.Json.Data);
//            var test = new Data.DoubleLinearChartData(obj);

//            chartData = test;
//            updateLineSignature();

//            var growth = Welcome.FromJson(json.FollowersGraph.Json.Data);
//            var chart = ChartData.FromWelcome(growth);
//            _chart = chart;
//            _minX = 0;
//            _maxX = 100;

//            //MinX.Value = _minX;
//            //MinX.Maximum = MaxX.Value = MaxX.Maximum = _maxX;

//            MinX.ValueChanged += MinX_ValueChanged;
//            MaxX.ValueChanged += MaxX_ValueChanged;

//            Checks.ItemsSource = chart.Columns;

//            Renderer.Invalidate();
//        }

//        private const int BOTTOM_SIGNATURE_COUNT = 6;

//        private Data.ChartData chartData;
//        private ChartBottomSignatureData currentBottomSignatures;
//        private List<ChartBottomSignatureData> bottomSignatureDate = new List<ChartBottomSignatureData>(25);

//        public int chartStart;
//        public int chartEnd;
//        public int chartWidth;
//        public float chartFullWidth;

//        protected int tmpN;
//        protected int tmpI;
//        protected int bottomSignatureOffset;

//        private void updateLineSignature()
//        {
//            chartStart = HORIZONTAL_PADDING;
//            chartEnd = (int)Renderer.ActualWidth - HORIZONTAL_PADDING;
//            chartWidth = chartEnd - chartStart;
//            chartFullWidth = chartWidth / (_maxX - _minX);

//            if (chartData == null || chartWidth == 0) return;
//            float d = chartFullWidth * chartData.oneDayPercentage;

//            float k = chartWidth / d;
//            int step = (int)(k / BOTTOM_SIGNATURE_COUNT);
//            updateDates(step);
//        }


//        private void updateDates(int step)
//        {
//            if (currentBottomSignatures == null || step >= currentBottomSignatures.stepMax || step <= currentBottomSignatures.stepMin)
//            {
//                step = step.HighestOneBit() << 1;
//                if (currentBottomSignatures != null && currentBottomSignatures.step == step)
//                {
//                    return;
//                }

//                //if (alphaBottomAnimator != null)
//                //{
//                //    alphaBottomAnimator.removeAllListeners();
//                //    alphaBottomAnimator.cancel();
//                //}

//                int stepMax = (int)(step + step * 0.2);
//                int stepMin = (int)(step - step * 0.2);


//                ChartBottomSignatureData data = new ChartBottomSignatureData(step, stepMax, stepMin);
//                data.alpha = 255;

//                if (currentBottomSignatures == null)
//                {
//                    currentBottomSignatures = data;
//                    data.alpha = 255;
//                    bottomSignatureDate.Add(data);
//                    return;
//                }

//                currentBottomSignatures = data;


//                tmpN = bottomSignatureDate.Count;
//                for (int i = 0; i < tmpN; i++)
//                {
//                    ChartBottomSignatureData a = bottomSignatureDate[i];
//                    a.fixedAlpha = a.alpha;
//                }

//                bottomSignatureDate.Add(data);
//                if (bottomSignatureDate.Count > 2)
//                {
//                    bottomSignatureDate.RemoveAt(0);
//                }

//                //    alphaBottomAnimator = createAnimator(0f, 1f, animation-> {
//                //        float alpha = (float)animation.getAnimatedValue();
//                //        for (ChartBottomSignatureData a : bottomSignatureDate)
//                //        {
//                //            if (a == data)
//                //            {
//                //                data.alpha = (int)(255 * alpha);
//                //            }
//                //            else
//                //            {
//                //                a.alpha = (int)((1f - alpha) * (a.fixedAlpha));
//                //            }
//                //        }
//                //        invalidate();
//                //    }).setDuration(200);
//                //    alphaBottomAnimator.addListener(new AnimatorListenerAdapter() {
//                //    @Override
//                //    public void onAnimationEnd(Animator animation)
//                //    {
//                //        super.onAnimationEnd(animation);
//                bottomSignatureDate.Clear();
//                bottomSignatureDate.Add(data);
//                //    }
//                //});

//                //alphaBottomAnimator.start();
//            }
//        }

//        private const int ANIM_DURATION = 400;
//        public const int HORIZONTAL_PADDING = 16;
//        private const float LINE_WIDTH = 1;
//        private const float SELECTED_LINE_WIDTH = 1.5f;
//        private const float SIGNATURE_TEXT_SIZE = 12;
//        public const int SIGNATURE_TEXT_HEIGHT = 18;
//        private const int BOTTOM_SIGNATURE_TEXT_HEIGHT = 14;
//        public const int BOTTOM_SIGNATURE_START_ALPHA = 10;
//        protected const int PICKER_PADDING = 16;
//        private const int PICKER_CAPTURE_WIDTH = 24;
//        private const int LANDSCAPE_END_PADDING = 16;
//        private const int BOTTOM_SIGNATURE_OFFSET = 10;

//        int startXIndex;
//        int endXIndex;

//        public int transitionMode = TRANSITION_MODE_NONE;
//        public TransitionParams transitionParams;

//        public const int TRANSITION_MODE_CHILD = 1;
//        public const int TRANSITION_MODE_PARENT = 2;
//        public const int TRANSITION_MODE_ALPHA_ENTER = 3;
//        public const int TRANSITION_MODE_NONE = 0;

//        Color bottomSignaturePaint;
//        float bottomSignaturePaintAlpha = 1;

//        void drawBottomSignature(CanvasDrawEventArgs canvas)
//        {
//            if (chartData == null) return;

//            tmpN = bottomSignatureDate.Count;

//            float transitionAlpha = 1f;
//            if (transitionMode == TRANSITION_MODE_PARENT)
//            {
//                transitionAlpha = 1f - transitionParams.progress;
//            }
//            else if (transitionMode == TRANSITION_MODE_CHILD)
//            {
//                transitionAlpha = transitionParams.progress;
//            }
//            else if (transitionMode == TRANSITION_MODE_ALPHA_ENTER)
//            {
//                transitionAlpha = transitionParams.progress;
//            }

//            for (tmpI = 0; tmpI < tmpN; tmpI++)
//            {
//                int resultAlpha = bottomSignatureDate[tmpI].alpha;
//                int step = bottomSignatureDate[tmpI].step;
//                if (step == 0) step = 1;

//                int start = startXIndex - bottomSignatureOffset;
//                while (start % step != 0)
//                {
//                    start--;
//                }

//                int end = endXIndex - bottomSignatureOffset;
//                while (end % step != 0 || end < chartData.x.Length - 1)
//                {
//                    end++;
//                }

//                start += bottomSignatureOffset;
//                end += bottomSignatureOffset;


//                //float offset = chartFullWidth * (pickerDelegate.pickerStart) - HORIZONTAL_PADDING;
//                float offset = chartFullWidth * (_minX) - HORIZONTAL_PADDING;

//                for (int i = start; i < end; i += step)
//                {
//                    if (i < 0 || i >= chartData.x.Length - 1) continue;
//                    float xPercentage = (float)(chartData.x[i] - chartData.x[0]) /
//                            (float)((chartData.x[chartData.x.Length - 1] - chartData.x[0]));
//                    float xPoint = xPercentage * chartFullWidth - offset;
//                    float xPointOffset = xPoint - BOTTOM_SIGNATURE_OFFSET;
//                    if (xPointOffset > 0 &&
//                            xPointOffset <= chartWidth + HORIZONTAL_PADDING)
//                    {
//                        if (xPointOffset < BOTTOM_SIGNATURE_START_ALPHA)
//                        {
//                            float a = 1f - (BOTTOM_SIGNATURE_START_ALPHA - xPointOffset) / BOTTOM_SIGNATURE_START_ALPHA;
//                            bottomSignaturePaint.A = (byte)(resultAlpha * a * bottomSignaturePaintAlpha * transitionAlpha);
//                        }
//                        else if (xPointOffset > chartWidth)
//                        {
//                            float a = 1f - (xPointOffset - chartWidth) / HORIZONTAL_PADDING;
//                            bottomSignaturePaint.A = (byte)(resultAlpha * a * bottomSignaturePaintAlpha * transitionAlpha);
//                        }
//                        else
//                        {
//                            bottomSignaturePaint.A = (byte)(resultAlpha * bottomSignaturePaintAlpha * transitionAlpha);
//                        }
//                        //canvas.drawText(chartData.getDayString(i), xPoint, getMeasuredHeight() - chartBottom + BOTTOM_SIGNATURE_TEXT_HEIGHT + 3, bottomSignaturePaint);
//                        canvas.DrawingSession.DrawText(chartData.getDayString(i), xPoint, BOTTOM_SIGNATURE_TEXT_HEIGHT + 3, bottomSignaturePaint);
//                    }
//                }
//            }
//        }

//        private void MinX_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
//        {
//            _minX = (float)e.NewValue / 100f;
//            Renderer.Invalidate();
//        }

//        private void MaxX_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
//        {
//            _maxX = (float)e.NewValue / 100f;
//            Renderer.Invalidate();
//        }

//        private ChartData _chart;
//        private float _minX;
//        private float _maxX = 1.01f;

//        private void CanvasControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
//        {
//            var chart = _chart;
//            if (chart == null)
//            {
//                return;
//            }

//            var length = chart.Axis.Values.Length;
//            var vector = sender.Size.ToVector2();

//            var xMin = float.MaxValue;
//            var xMax = float.MinValue;
//            var yMin = float.MaxValue;
//            var yMax = float.MinValue;

//            var paths = new List<Tuple<CanvasPathBuilder, Color>>();
//            foreach (var column in chart.Columns)
//            {
//                if (column.IsHidden)
//                {
//                    continue;
//                }

//                CanvasPathBuilder pathBuilder = null;
//                for (int i = 0; i < length; i++)
//                {
//                    //var x = (float)i / x0.Values.Length * vector.X;
//                    var x = (chart.Axis.Values[i] - chart.Axis.Min) / chart.Axis.Length * vector.X;
//                    var y = (column.Values[i] - chart.Min) / chart.Length * vector.Y;
//                    y = vector.Y - y;

//                    if (pathBuilder == null)
//                    {
//                        pathBuilder = new CanvasPathBuilder(sender);
//                        pathBuilder.BeginFigure(x, y);
//                    }
//                    else
//                    {
//                        pathBuilder.AddLine(x, y);
//                    }

//                    var offset = (float)i / (float)length;
//                    if (offset >= _minX && offset <= _maxX)
//                    {
//                        xMin = Math.Min(xMin, x);
//                        xMax = Math.Max(xMax, x);
//                        yMin = Math.Min(yMin, y);
//                        yMax = Math.Max(yMax, y);
//                    }
//                }

//                pathBuilder.EndFigure(CanvasFigureLoop.Open);
//                paths.Add(Tuple.Create(pathBuilder, column.Color));
//            }

//            xMin = vector.X * _minX;
//            xMax = vector.X * _maxX;

//            var xLength = xMax - xMin;
//            var yLength = yMax - yMin;
//            var xScale = vector.X / xLength;
//            var yScale = vector.Y / yLength;

//            var matrix = Matrix3x2.CreateScale(xScale, yScale);
//            matrix.Translation = new Vector2(-(xMin * xScale), -(yMin * yScale));

//            //var rect = CanvasGeometry.CreateRectangle(sender, minX, minY, maxX - minX, maxY - minY);
//            //var group = CanvasGeometry.CreateGroup(sender, new[] { geometry, rect });

//            //geometry = geometry.Transform(matrix);
//            //args.DrawingSession.Transform = matrix;
//            //args.DrawingSession.DrawGeometry(geometry, Windows.UI.Colors.Blue, 6);
//            var translation = Matrix3x2.CreateTranslation(-xMin, -yMin);
//            var scale = Matrix3x2.CreateScale(vector.X / xLength, vector.Y / yLength);

//            args.DrawingSession.Transform = matrix;

//            foreach (var pathBuilder in paths)
//            {
//                var geometry = CanvasGeometry.CreatePath(pathBuilder.Item1);
//                args.DrawingSession.DrawGeometry(geometry, pathBuilder.Item2, 6, new CanvasStrokeStyle
//                {
//                    StartCap = CanvasCapStyle.Round,
//                    EndCap = CanvasCapStyle.Round,
//                    LineJoin = CanvasLineJoin.Round,
//                    TransformBehavior = CanvasStrokeTransformBehavior.Fixed
//                });
//            }

//            args.DrawingSession.Transform = Matrix3x2.Identity;

//            for (int i = 1; i <= 6; i++)
//            {
//                args.DrawingSession.DrawLine(0, i * vector.Y / 6, vector.X, i * vector.Y / 6, Colors.Gray, 4);
//                args.DrawingSession.DrawText($"{yMax / 6 * i}", 0, i * vector.Y / 6, Colors.Gray);
//            }

//            updateLineSignature();
//            drawBottomSignature(args);
//        }

//        private void CheckBox_Checked(object sender, RoutedEventArgs e)
//        {
//            if (sender is CheckBox check && check.DataContext is Column column)
//            {
//                column.IsHidden = !check.IsChecked == true;
//            }

//            Renderer.Invalidate();
//        }
//    }

public class TLStatsDateRangeDays
{
    public int MinDate { get; set; }
    public int MaxDate { get; set; }
}

public class TLStatsAbsValueAndPrev
{
    public double Current { get; set; }
    public double Previous { get; set; }
}

public class TLStatsPercentValue
{
    public double Part { get; set; }
    public double Total { get; set; }
}

public class TLStatsGraphAsync : TLStatsGraphBase
{
    public string Token { get; set; }
}

public class TLStatsGraphError : TLStatsGraphBase
{
    public string Error { get; set; }
}

public class TLStatsGraph : TLStatsGraphBase
{
    [Flags]
    public enum Flag : Int32
    {
        ZoomToken = (1 << 0),
    }

    public bool HasZoomToken { get { return Flags.HasFlag(Flag.ZoomToken); } set { Flags = value ? (Flags | Flag.ZoomToken) : (Flags & ~Flag.ZoomToken); } }

    public Flag Flags { get; set; }
    public TLDataJSON Json { get; set; }
    public string ZoomToken { get; set; }
}

public abstract class TLStatsGraphBase
{

}

public class TLMessageInteractionCounters
{
    public int MsgId { get; set; }
    public int Views { get; set; }
    public int Forwards { get; set; }
}

public class TLStatsBroadcastStats
{
    public TLStatsDateRangeDays Period { get; set; }
    public TLStatsAbsValueAndPrev Followers { get; set; }
    public TLStatsAbsValueAndPrev ViewsPerPost { get; set; }
    public TLStatsAbsValueAndPrev SharesPerPost { get; set; }
    public TLStatsPercentValue EnabledNotifications { get; set; }
    public TLStatsGraph GrowthGraph { get; set; }
    public TLStatsGraph FollowersGraph { get; set; }
    public TLStatsGraph MuteGraph { get; set; }
    public TLStatsGraph TopHoursGraph { get; set; }
    public TLStatsGraph InteractionsGraph { get; set; }
    public TLStatsGraph IvInteractionsGrapg { get; set; }
    public TLStatsGraph ViewsBySourceGraph { get; set; }
    public TLStatsGraph NewFollowersBySourceGraph { get; set; }
    public TLStatsGraph LanguagesGraph { get; set; }
    public List<TLMessageInteractionCounters> RecentMessageInteractions { get; set; }
}

public class TLDataJSON
{
    public string Data { get; set; }
}












//    public partial class ChartData
//    {
//        public Column Axis { get; private set; }
//        public List<Column> Columns { get; private set; }

//        public static ChartData FromWelcome(Welcome welcome)
//        {
//            var data = new ChartData();
//            data.Columns = new List<Column>();

//            foreach (var i in welcome.Columns)
//            {
//                var key = i[0].String;
//                var column = new Column();
//                var values = new List<float>();

//                var type = welcome.Types[key];

//                for (int j = 1; j < i.Length; j++)
//                {
//                    if (i[j].Integer is long value)
//                    {
//                        values.Add(value);

//                        column.Min = Math.Min(column.Min, value);
//                        column.Max = Math.Max(column.Max, value);

//                        if (type != ColumnType.X)
//                        {
//                            data.Min = Math.Min(data.Min, value);
//                            data.Max = Math.Max(data.Max, value);
//                        }
//                    }
//                }

//                Color color = Windows.UI.Colors.Red;
//                if (welcome.Colors.TryGetValue(key, out string colorString))
//                {
//                    var split = colorString.Split('#');
//                    if (int.TryParse(split[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexValue))
//                    {
//                        byte r = (byte)((hexValue & 0x00ff0000) >> 16);
//                        byte g = (byte)((hexValue & 0x0000ff00) >> 8);
//                        byte b = (byte)(hexValue & 0x000000ff);

//                        color = Color.FromArgb(255, r, g, b);
//                    }
//                }

//                column.Key = key;
//                column.Values = values.ToArray();
//                column.Color = color;

//                if (welcome.Names.TryGetValue(key, out string name))
//                {
//                    column.Name = name;
//                }

//                if (type == ColumnType.X)
//                {
//                    data.Axis = column;
//                }
//                else
//                {
//                    data.Columns.Add(column);
//                }
//            }

//            return data;
//        }

//        public float Min { get; set; }
//        public float Max { get; set; }

//        public float Length => Max - Min;

//    }

//    public partial class Welcome
//    {
//        [JsonProperty("title")]
//        public string Title { get; set; }

//        [JsonProperty("columns")]
//        public ColumnValue[][] Columns { get; set; }

//        [JsonProperty("types")]
//        public Dictionary<string, ColumnType> Types { get; set; }

//        [JsonProperty("names")]
//        public Dictionary<string, string> Names { get; set; }

//        [JsonProperty("colors")]
//        public Dictionary<string, string> Colors { get; set; }

//        [JsonProperty("hidden")]
//        public object[] Hidden { get; set; }

//        [JsonProperty("subchart")]
//        public Subchart Subchart { get; set; }

//        [JsonProperty("strokeWidth")]
//        public long StrokeWidth { get; set; }

//        [JsonProperty("xTickFormatter")]
//        public string XTickFormatter { get; set; }

//        [JsonProperty("xTooltipFormatter")]
//        public string XTooltipFormatter { get; set; }

//        [JsonProperty("xRangeFormatter")]
//        public string XRangeFormatter { get; set; }

//        [JsonProperty("yTooltipFormatter")]
//        public string YTooltipFormatter { get; set; }
//    }

//    public partial class Subchart
//    {
//        [JsonProperty("show")]
//        public bool Show { get; set; }

//        [JsonProperty("defaultZoom")]
//        public long[] DefaultZoom { get; set; }
//    }

//    public partial class Column
//    {
//        public string Key { get; set; }
//        public float[] Values { get; set; }

//        public float Max { get; set; } = float.MinValue;
//        public float Min { get; set; } = float.MaxValue;

//        public float Length => Max - Min;

//        public string Name { get; set; }
//        public Color Color { get; set; }
//        public ColumnType Type { get; set; }

//        public bool IsHidden { get; set; }
//    }

//    public partial struct ColumnValue
//    {
//        public long? Integer;
//        public string String;

//        public static implicit operator ColumnValue(long Integer) => new ColumnValue { Integer = Integer };
//        public static implicit operator ColumnValue(string String) => new ColumnValue { String = String };
//    }

//    public partial class Welcome
//    {
//        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, Converter.Settings);
//    }

//    public static class Serialize
//    {
//        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, Converter.Settings);
//    }

//    internal static class Converter
//    {
//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters =
//            {
//                ColumnConverter.Singleton,
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//        };
//    }

//    internal class ColumnConverter : JsonConverter
//    {
//        public override bool CanConvert(Type t) => t == typeof(ColumnValue) || t == typeof(ColumnValue?);

//        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
//        {
//            switch (reader.TokenType)
//            {
//                case JsonToken.Integer:
//                    var integerValue = serializer.Deserialize<long>(reader);
//                    return new ColumnValue { Integer = integerValue };
//                case JsonToken.String:
//                case JsonToken.Date:
//                    var stringValue = serializer.Deserialize<string>(reader);
//                    return new ColumnValue { String = stringValue };
//            }
//            throw new Exception("Cannot unmarshal type Column");
//        }

//        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
//        {
//            var value = (ColumnValue)untypedValue;
//            if (value.Integer != null)
//            {
//                serializer.Serialize(writer, value.Integer.Value);
//                return;
//            }
//            if (value.String != null)
//            {
//                serializer.Serialize(writer, value.String);
//                return;
//            }
//            throw new Exception("Cannot marshal type Column");
//        }

//        public static readonly ColumnConverter Singleton = new ColumnConverter();
//    }

//    public enum ColumnType
//    {
//        X,
//        Line,
//        Area,
//        Bar,
//        Step
//    }
//}
