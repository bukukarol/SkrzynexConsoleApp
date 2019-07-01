using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;


namespace SkrzynexConsoleApp.Infrastructure
{
    public class GPIOHandler
    {
        private readonly IGpioPin _line1;
        private readonly IGpioPin _line2;
        private readonly IGpioPin _line3;
        private readonly IGpioPin _line4;
        
        public GPIOHandler()
        {

            _line1 = Pi.Gpio[BcmPin.Gpio02];
            _line2 = Pi.Gpio[BcmPin.Gpio03];
            _line3 = Pi.Gpio[BcmPin.Gpio04];
            _line4 = Pi.Gpio[BcmPin.Gpio17];
            

            _line1.PinMode = GpioPinDriveMode.Output;
            _line2.PinMode = GpioPinDriveMode.Output;
            _line3.PinMode = GpioPinDriveMode.Output;
            _line4.PinMode = GpioPinDriveMode.Output;
        }

        public bool GetLineStatus(int lineNr)
        {
            switch (lineNr)
            {
                case 1:
                    return !_line1.Read();
                case 2:
                    return !_line2.Read();
                case 3:
                    return !_line3.Read();
                case 4:
                    return !_line4.Read();
                default: 
                    throw new Exception($"Not implemented line {lineNr}");
            }
        }
        public void ClearAllLines()
        {
            //_line1.InputPullMode = GpioPinResistorPullMode.PullUp;
            //_line2.InputPullMode = GpioPinResistorPullMode.PullUp;
            //_line3.InputPullMode = GpioPinResistorPullMode.PullUp;
            //_line4.InputPullMode = GpioPinResistorPullMode.PullUp;

            _line1.Write(GpioPinValue.High); // by default high made relay off
            _line2.Write(GpioPinValue.High);
            _line3.Write(GpioPinValue.High);
            _line4.Write(GpioPinValue.High);
        }

        public void UpdateSprinklesLinesStatues(bool line1, bool line2, bool line3, bool line4)
        {
            UpdateLineStatus(_line1, line1);
            UpdateLineStatus(_line2, line2);
            UpdateLineStatus(_line3, line3);
            UpdateLineStatus(_line4, line4);
        }
        
        private void UpdateLineStatus(IGpioPin line, bool newStatus)
        {
            var oldStatus = line.Read(); // on when false , off when true
            if(oldStatus==newStatus) line.Write(!newStatus); 
        }

        public void TurnOnAllLines()
        {
            UpdateLineStatus(_line1, true);
            UpdateLineStatus(_line2, true);
            UpdateLineStatus(_line3, true);
            UpdateLineStatus(_line4, true);
        }
    }
}
