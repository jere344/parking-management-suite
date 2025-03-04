using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace paymentterminal.Services;

public class InactivityService : INotifyPropertyChanged
{
    private readonly DispatcherTimer _timer;
    private TimeSpan _remainingTime;
    private readonly TimeSpan _timeout = TimeSpan.FromMinutes(1);

    public Func<bool> IsTicking { get; set; } = () => true;


    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler InactivityTimeout;

    public TimeSpan RemainingTime
    {
        get => _remainingTime;
        private set
        {
            _remainingTime = value;
            OnPropertyChanged(nameof(RemainingTime));
            OnPropertyChanged(nameof(IsCountdownVisible));
        }
    }

    // Expose a visibility property for the countdown (visible if less than 30 seconds left)
    public Visibility IsCountdownVisible => (_remainingTime.TotalSeconds < 30) ? Visibility.Visible : Visibility.Collapsed;

    public InactivityService()
    {
        _remainingTime = _timeout;
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += TimerTick;
        _timer.Start();

        // Subscribe to global input events
        InputManager.Current.PreProcessInput += OnInputEvent;

        Debug.WriteLine("InactivityService initialized");
    }

    private void OnInputEvent(object sender, PreProcessInputEventArgs e)
    {
        ResetTimer();
    }

    private void TimerTick(object sender, EventArgs e)
    {
        if (!IsTicking()) return;

        RemainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));

        if (_remainingTime <= TimeSpan.Zero)
        {
            _timer.Stop();
            InactivityTimeout?.Invoke(this, EventArgs.Empty);
            ResetTimer(); // reset after triggering timeout
            _timer.Start();
        }
    }

    public void ResetTimer()
    {
        _remainingTime = _timeout;
        RemainingTime = _remainingTime;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
