using Charts.Data;
using Charts.DataView;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Charts
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        private async void test_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);

            var file = await Package.Current.InstalledLocation.GetFileAsync("test.json");
            var text = await FileIO.ReadTextAsync(file);
            var json = JsonConvert.DeserializeObject<TLStatsBroadcastStats>(text);

            var obj = JsonObject.Parse(json.FollowersGraph.Json.Data);
            var data = new StackBarChartData(obj);

            test.setData(data);
            test.onCheckChanged();

            Checks.ItemsSource = test.lines;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox check && check.DataContext is LineViewData column)
            {
                column.enabled = check.IsChecked == true;
                test.onCheckChanged();
            }
        }
    }

    public class Animator
    {
        protected readonly List<AnimatorUpdateListener> _listeners = new List<AnimatorUpdateListener>();
        protected readonly List<AnimatorUpdateListener> _updateListeners = new List<AnimatorUpdateListener>();

        internal void cancel()
        {
            //throw new NotImplementedException();
        }

        internal virtual void start()
        {
            foreach (var l in _listeners.Union(_updateListeners))
            {
                l.Action(this);
            }
        }

        internal void addUpdateListener(AnimatorUpdateListener l)
        {
            _updateListeners.Add(l);
        }

        internal void addListener(AnimatorUpdateListener l)
        {
            _listeners.Add(l);
        }

        internal void removeAllListeners()
        {
            _updateListeners.Clear();
            _listeners.Clear();
        }

        internal virtual object getAnimatedValue()
        {
            return null;
        }

    }

    public class AnimatorSet : Animator
    {
        private readonly List<Animator> _animators = new List<Animator>();

        internal void playTogether(params Animator[] valueAnimator)
        {
            foreach (var animator in valueAnimator)
            {
                _animators.Add(animator);
            }
        }

        internal override void start()
        {
            base.start();

            foreach (var animator in _animators)
            {
                animator.start();
            }
        }
    }

    public class ValueAnimator : Animator
    {
        private readonly float _f1;
        private readonly float _f2;

        private readonly DispatcherTimer _timer;

        private DateTime _last;
        private TimeSpan _progress;
        private TimeSpan _duration;

        private FastOutSlowInInterpolator _interpolator;

        public ValueAnimator(float f1, float f2)
        {
            _f1 = f1;
            _f2 = f2;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000d / 30d);
            _timer.Tick += OnTick;
        }

        //internal override void start()
        //{
        //    _last = DateTime.Now;
        //    _timer.Start();
        //}

        private void OnTick(object sender, object e)
        {
            var diff = DateTime.Now - _last;
            var progress = _progress + diff;

            var maximum = _f2 - _f1;
            var value = maximum * (float)(progress / _duration);

            //if (_interpolator != null)
            //{
            //    value = _interpolator.getInterpolation(value);
            //}

            if (value >= _f2)
            {
                _timer.Stop();

                foreach (var l in _updateListeners.Union(_listeners))
                {
                    l.Action(this);
                }
            }
            else
            {
                foreach (var l in _updateListeners)
                {
                    l.Action(this);
                }
            }

            _last = DateTime.Now;
            _progress = progress;
        }

        internal static ValueAnimator ofFloat(float f1, float f2)
        {
            return new ValueAnimator(f1, f2);
        }

        internal ValueAnimator setDuration(int duration)
        {
            _duration = TimeSpan.FromMilliseconds(duration);
            return this;
        }

        internal Animator setInterpolator(FastOutSlowInInterpolator interpolator)
        {
            _interpolator = interpolator;
            return this;
        }

        internal bool isRunning()
        {
            return false;
        }

        internal override object getAnimatedValue()
        {
            //var maximum = _f2 - _f1;
            //var value = maximum * (float)(_progress / _duration);

            //return value;
            return _f2;
        }
    }

    public class AnimatorUpdateListener
    {
        private Action<Animator> _update;
        private Action<Animator> _end;

        public AnimatorUpdateListener(Action<Animator> update = null, Action<Animator> end = null)
        {
            _update = update;
            _end = end;
        }

        public Action<Animator> Action => _update ?? _end;
    }
}
