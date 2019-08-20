using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace TestingSystem.XamarinForms.Infrastructure
{
    public class Timer
    {
        private readonly TimeSpan timespan;
        private readonly Func<bool> callback;
        private CancellationTokenSource cancellation;

        public Timer(TimeSpan timespan, Func<bool> callback)
        {
            this.timespan = timespan;
            this.callback = callback;
            this.cancellation = new CancellationTokenSource();
        }

        ~Timer()
        {
            Stop();
        }

        public void Start()
        {
            CancellationTokenSource cts = this.cancellation;
            Device.StartTimer(this.timespan, () => {
                if (cts.IsCancellationRequested)
                    return false;
                return this.callback();
            });
        }

        public void Stop()
        {
            Interlocked.Exchange(ref this.cancellation, new CancellationTokenSource()).Cancel();
        }
    }
}
