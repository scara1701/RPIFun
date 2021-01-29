using System.Device.Gpio;

namespace RPIFun.API.Services
{
    public class LEDService : ILEDService
    {
        private const int _Gpio_Pin_Num = 10;
        public GpioController _gpioController;

        public LEDService()
        {
            _gpioController = new GpioController(PinNumberingScheme.Board);
        }

        public bool HitLEDSwitch()
        {
            if (_gpioController == null) return false;

            _gpioController.OpenPin(_Gpio_Pin_Num, PinMode.Output);


            PinValue pinValue = _gpioController.Read(_Gpio_Pin_Num);
            if (pinValue == PinValue.High)
            {
                _gpioController.Write(_Gpio_Pin_Num, PinValue.Low);
                return false;
            }
            else
            {
                _gpioController.Write(_Gpio_Pin_Num, PinValue.High);
                return true;
            }

        }

        public bool IsLEDON()
        {
            if (_gpioController == null) return false;

            _gpioController.OpenPin(_Gpio_Pin_Num, PinMode.Output);

            PinValue pinValue = _gpioController.Read(_Gpio_Pin_Num);
            if (pinValue == PinValue.High)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
