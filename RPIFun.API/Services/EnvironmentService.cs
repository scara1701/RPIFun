using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.ReadResult;
using Iot.Device.Common;
using RPIFun.Core;
using System.Device.I2c;
using UnitsNet;

namespace RPIFun.API.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        //bus id on raspberry pi 3
        const int busId = 1;
        //Luchtdruk op zeeniveau in gebied
        Pressure defaultSeaLevelPressure = WeatherHelper.MeanSeaLevel;

        I2cDevice i2cDevice;
        Bme280 bme280;

        public EnvironmentService()
        {
            I2cConnectionSettings i2cSettings = new(busId, Bme280.DefaultI2cAddress);
            i2cDevice = I2cDevice.Create(i2cSettings);
            bme280 = new Bme280(i2cDevice)
            {
                TemperatureSampling = Sampling.LowPower,
                PressureSampling = Sampling.UltraHighResolution,
                HumiditySampling = Sampling.Standard
            };
        }

        public EnvResult GetEnvironment()
        {
            Bme280ReadResult bme280Result = bme280.Read();
            var humidity = bme280Result.Humidity?.Percent;
            var pressure = bme280Result.Pressure?.Hectopascals;
            var temperature = bme280Result.Temperature?.DegreesCelsius;

            EnvResult envResult = new EnvResult();
            envResult.Temperature = temperature;
            envResult.Pressure = pressure;
            envResult.Humidity = humidity;

            return envResult;
        }
    }
}
