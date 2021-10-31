using System;
using System.Timers;

namespace BlazorWithIdentity.Client.Services.Implementations.ToastNotification
{
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private  Timer CountDown;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (CountDown.Enabled)
            {
                CountDown.Stop();
                CountDown.Start();
            }
            else
            {
                CountDown.Start();
            }
        }

        private void SetCountdown()
        {
            if (CountDown == null)
            {
                CountDown = new Timer(5000);
                CountDown.Elapsed += HideToast;
                CountDown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            CountDown?.Dispose();
        }
    }
}
