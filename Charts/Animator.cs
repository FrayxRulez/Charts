using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Unigram.Charts
{
    public class Animator
    {
        protected readonly List<AnimatorUpdateListener> _listeners = new List<AnimatorUpdateListener>();
        protected readonly List<AnimatorUpdateListener> _updateListeners = new List<AnimatorUpdateListener>();

        protected readonly object _listenersLock = new object();

        internal virtual void cancel()
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
            lock (_listenersLock)
            {
                _updateListeners.Add(l);
            }
        }

        internal void addListener(AnimatorUpdateListener l)
        {
            lock (_listenersLock)
            {
                _listeners.Add(l);
            }
        }

        internal void removeAllListeners()
        {
            lock (_listenersLock)
            {
                _updateListeners.Clear();
                _listeners.Clear();
            }
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

        internal override void cancel()
        {
            base.cancel();

            foreach (var animator in _animators)
            {
                animator.cancel();
            }
        }
    }

    public class ValueAnimator : Animator
    {
        private readonly float _f1;
        private readonly float _f2;

        private readonly Timer _timer;

        private int _begin;
        private int _duration = 300;

        private float _result;

        private FastOutSlowInInterpolator _interpolator;

        public ValueAnimator(float f1, float f2)
        {
            _f1 = f1;
            _f2 = f2;

            _timer = new Timer(OnTick, null, Timeout.Infinite, Timeout.Infinite);
            //_timer.Interval = TimeSpan.FromMilliseconds(1000d / 30d);
            //_timer.Tick += OnTick;
        }

        internal override void start()
        {
            if (_f1 == _f2)
            {
                return;
            }

            _begin = Environment.TickCount;
            _timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(1000d / 30d));
        }

        internal override void cancel()
        {
            _begin = Timeout.Infinite;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void OnTick(object sender)
        {
            var tick = Environment.TickCount;
            if (tick >= _begin + _duration)
            {
                System.Diagnostics.Debug.WriteLine($"_f1: {_f1}; _f2: {_f2}; _result: {_result} -> timeout");

                _result = _f2;

                complete();
                return;
            }

            var diff = tick - _begin;
            var perc = (float)diff / _duration;

            if (_interpolator != null)
            {
                perc = _interpolator.getInterpolation(perc);
            }

            var maximum = Math.Max(_f2, _f1) - Math.Min(_f1, _f2);
            var value = maximum * (_f2 > _f1 ? perc : 1 - perc);

            _result = _f2 > _f1 ? Math.Min(_f2, value) : Math.Max(_f2, value);

            System.Diagnostics.Debug.WriteLine($"_f1: {_f1}; _f2: {_f2}; _result: {_result}");

            if ((_f2 > _f1 && _result >= _f2) || (_f2 < _f1 && _result <= _f2))
            {
                complete();
            }
            else
            {
                lock (_listenersLock)
                {
                    foreach (var l in _updateListeners)
                    {
                        l.Action(this);
                    }
                }
            }
        }

        private void complete()
        {
            _begin = Timeout.Infinite;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);

            lock (_listenersLock)
            {
                foreach (var l in _updateListeners.Union(_listeners))
                {
                    l.Action(this);
                }
            }
        }

        internal static ValueAnimator ofFloat(float f1, float f2)
        {
            return new ValueAnimator(f1, f2);
        }

        internal ValueAnimator setDuration(int duration)
        {
            _duration = duration;
            return this;
        }

        internal Animator setInterpolator(FastOutSlowInInterpolator interpolator)
        {
            _interpolator = interpolator;
            return this;
        }

        internal bool isRunning()
        {
            return _begin != Timeout.Infinite;
        }

        internal override object getAnimatedValue()
        {
            return _result;
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
