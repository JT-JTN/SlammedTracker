using MudBlazor;

namespace ST.Ui.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;
        public event EventHandler<NotificationEventArgs>? NotificationAdded;

        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Add(string message, Severity severity)
        {
            _snackbar.Add(message, severity);
            OnNotificationAdded(new NotificationEventArgs { Message = message });
        }

        protected virtual void OnNotificationAdded(NotificationEventArgs e)
        {
            NotificationAdded?.Invoke(this, e);
        }
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; set; } = string.Empty;
    }
}
