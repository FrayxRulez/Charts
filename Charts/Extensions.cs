using System;
using System.Globalization;
using Windows.Foundation;
using Windows.UI;

namespace Unigram.Common
{
    public static class Extensions
    {
        public static int centerX(this Rect rect)
        {
            return (int)(rect.Left + rect.Right) >> 1;
        }

        /**
         * @return the vertical center of the rectangle. If the computed value
         *         is fractional, this method returns the largest integer that is
         *         less than the computed value.
         */
        public static int centerY(this Rect rect)
        {
            return (int)(rect.Top + rect.Bottom) >> 1;
        }

        public static int HighestOneBit(this int number)
        {
            return (int)Math.Pow(2, Convert.ToString(number, 2).Length - 1);
        }

        public static Color ToColor(this int color)
        {
            return Color.FromArgb(0xFF, (byte)((color >> 16) & 0xFF), (byte)((color >> 8) & 0xFF), (byte)(color & 0xFF));
        }

        public static Color ToColor(this string color)
        {
            color = color.Trim('#');
            if (int.TryParse(color, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexValue))
            {
                byte r = (byte)((hexValue & 0x00ff0000) >> 16);
                byte g = (byte)((hexValue & 0x0000ff00) >> 8);
                byte b = (byte)(hexValue & 0x000000ff);

                return Color.FromArgb(255, r, g, b);
            }

            return default;
        }

        public static int ToValue(this Color color)
        {
            return (color.R << 16) + (color.G << 8) + color.B;
        }

        public static int ToTimestamp(this DateTime dateTime)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime.SpecifyKind(dtDateTime, DateTimeKind.Utc);

            return (int)(dateTime.ToUniversalTime() - dtDateTime).TotalSeconds;
        }
    }
}
