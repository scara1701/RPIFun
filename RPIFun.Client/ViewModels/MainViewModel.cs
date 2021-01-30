using Microsoft.Toolkit.Mvvm.Input;
using RPIFun.Client.Services;
using RPIFun.Core;
using System;
using System.Windows.Input;

namespace RPIFun.Client.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string temperature;
        public string Temperature
        {
            get => temperature;
            set
            {
                SetProperty(ref temperature, value);
            }
        }

        private string pressure;
        public string Pressure
        {
            get => pressure;
            set
            {
                SetProperty(ref pressure, value);
            }
        }

        private string humidity;
        public string Humidity
        {
            get => humidity;
            set
            {
                SetProperty(ref humidity, value);
            }
        }

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
                SetProperty(ref buttonsEnabled, value);
            }
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
            set
            {
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

        public ICommand GetEnvironmentCommand { get; }

        public MainViewModel()
        {
            GetLEDStatusCommand = new RelayCommand(GetLEDStatus);
            SwitchLEDCommand = new RelayCommand(SwitchLED);
            GetEnvironmentCommand = new RelayCommand(GetEnvironment);
        }

        private async void GetEnvironment()
        {
            IsBusy = true;
            StatusMessage = "Getting Environment, please wait...";
            try
            {
                EnvService envService = new EnvService(ServerAddress, ServerPort);
                EnvResult envResult = await envService.getEnvironment();
                StatusMessage = "Environment succesfully retrieved.";
                //Temperature = envResult.Temperature.ToString();
                //Humidity = envResult.Humidity.ToString();
                //Pressure = envResult.Pressure.ToString();

                Temperature = String.Format("{0:0.00}\u00b0C", envResult.Temperature);
                Humidity = String.Format("{0:0.00} %", envResult.Humidity);
                Pressure = String.Format("{0:0.00} hPa", envResult.Pressure);
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
                else
                {
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
