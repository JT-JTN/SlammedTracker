using MudBlazor;
using System;
using System.Collections.Generic;

namespace ST.Ui.Services
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;
        public event EventHandler<SnackbarEventArgs>? SnackbarAdded;

        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void Add(string message, Severity severity)
        {
            _snackbar.Add(message, severity);
            OnSnackbarAdded(new SnackbarEventArgs { Message = message });
        }

        protected virtual void OnSnackbarAdded(SnackbarEventArgs e)
        {
            SnackbarAdded?.Invoke(this, e);
        }
    }

    public class SnackbarEventArgs : EventArgs
    {
        public string Message { get; set; } = string.Empty;
    }
}
