using Microsoft.Toolkit.Mvvm.Input;
using RPIFun.Client.Services;
using System;
using System.Windows.Input;

namespace RPIFun.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool lEDStatus;

        public bool LEDStatus
        {
            get => lEDStatus;
            set
            {
                SetProperty(ref lEDStatus, value);
            }
        }
        private bool buttonsEnabled;
        public bool ButtonsEnabled
        {
            get => buttonsEnabled;
            set
            {
                SetProperty(ref buttonsEnabled, value);            }
        }

        private string serverAddress;

        public string ServerAddress
        {
            get => serverAddress;
            set
            {
                SetProperty(ref serverAddress, value);
                SetButtons();
            }
        }
        private string serverPort;
        public string ServerPort
        {
            get => serverPort;
            set { 
                SetProperty(ref serverPort, value);
                SetButtons();
            }
        }


        private void SetButtons()
        {
            if (String.IsNullOrEmpty(ServerAddress) || String.IsNullOrEmpty(ServerPort))
            {
                ButtonsEnabled = false;
            }
            else
            {
                ButtonsEnabled = true;
            }
        }

        public ICommand GetLEDStatusCommand { get; }
        public ICommand SwitchLEDCommand { get; }

        public MainViewModel()
        {
            GetLEDStatusCommand = new RelayCommand(GetLEDStatus);
            SwitchLEDCommand = new RelayCommand(SwitchLED);
        }

        private async void SwitchLED()
        {
            IsBusy = true;
            StatusMessage = "Hitting LED switch, please wait...";
            try
            {
                LEDService lEDService = new LEDService(ServerAddress, ServerPort);
                LEDStatus = await lEDService.SwitchLEDStatus();
                if (LEDStatus)
                {
                    StatusMessage = "LED was turned on.";
                }
                else{
                    StatusMessage = "LED was turned off.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void GetLEDStatus()
        {
            IsBusy = true;
            StatusMessage = "Getting LED status, please wait...";
            try
            {
                LEDService lEDService = new LEDService(ServerAddress, ServerPort);
                LEDStatus = await lEDService.GetLEDStatus();
                StatusMessage = "LED status succesfully retrieved.";
                if (LEDStatus)
                {
                    StatusMessage += " LED is On.";
                }
                else
                {
                    StatusMessage += " LED is Off.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
