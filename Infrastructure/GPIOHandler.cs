using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace SkrzynexConsoleApp.Infrastructure
{
    public class GPIOHandler
    {
        private GpioPin _line1;
        private GpioPin _line2;
        private GpioPin _line3;
        private GpioPin _line4;

        public GPIOHandler()
        {
            _line1 = Pi.Gpio.Pin17;
            _line2 = Pi.Gpio.Pin27;
            _line3 = Pi.Gpio.Pin20;
            _line4 = Pi.Gpio.Pin21;

            _line1.PinMode = GpioPinDriveMode.Input;
            _line2.PinMode = GpioPinDriveMode.Input;
            _line3.PinMode = GpioPinDriveMode.Input;
            _line4.PinMode = GpioPinDriveMode.Input;
        }

        public void ClearAllLines()
        {
            _line1.InputPullMode = GpioPinResistorPullMode.PullUp;
            _line2.InputPullMode = GpioPinResistorPullMode.PullUp; 
            _line3.InputPullMode = GpioPinResistorPullMode.PullUp;
            _line3.InputPullMode = GpioPinResistorPullMode.PullUp;
        }

        public void UpdateSprinklesLinesStatues(bool line1, bool line2, bool line3, bool line4)
        {
            UpdateLineStatus(_line1,line1);
            UpdateLineStatus(_line2, line2);
            UpdateLineStatus(_line3, line3);
            UpdateLineStatus(_line4, line4);
        }
        
        private void UpdateLineStatus(GpioPin line, bool newStatus)
        {
            line.InputPullMode = newStatus ? GpioPinResistorPullMode.PullDown : GpioPinResistorPullMode.PullUp;
        }
    }
}
