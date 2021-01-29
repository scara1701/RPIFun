using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace RPIFun.Client.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private bool isBusy;
        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        private string statusMessage;
        public string StatusMessage
        {
            get => statusMessage;
            set => SetProperty(ref statusMessage, value);
        }
    }
}
