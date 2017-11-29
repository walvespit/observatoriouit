using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;

namespace ObservatorioUIT
{
    class Motor
    {

        private Boolean isRunning;

        public enum directions
        {
            UP,
            DOWN
        };

        private GpioPin pulse;
        private GpioPin enabled;
        private GpioPin direction;

        private DispatcherTimer timer;

        public Motor(int pulse, int enabled, int direction)
        {

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;

            isRunning = false;
            var gpio = GpioController.GetDefault();

            this.pulse = gpio.OpenPin(pulse);
            this.enabled = gpio.OpenPin(enabled);
            this.direction = gpio.OpenPin(direction);

            this.pulse.SetDriveMode(GpioPinDriveMode.Output);
            this.direction.SetDriveMode(GpioPinDriveMode.Output);
            this.enabled.SetDriveMode(GpioPinDriveMode.Output);

            this.pulse.Write(GpioPinValue.Low);
            this.enabled.Write(GpioPinValue.High);
            this.direction.Write(GpioPinValue.Low);
        }

        public void step(int q)
        {
            for (int i = 0; i < q; i++)
            {
                this.pulse.Write(GpioPinValue.High);
                this.pulse.Write(GpioPinValue.Low);
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            step(1);
        }

        public void start()
        {
            isRunning = true;


            timer.Start();
        }

        public void stop()
        {
            isRunning = false;

            timer.Stop();
        }

        public void setDirection(directions dir)
        {
            if (dir == directions.UP)
            {
                this.direction.Write(GpioPinValue.High);
            }
            else
            {
                this.direction.Write(GpioPinValue.Low);
            }
        }

        public void setEnabled(Boolean enabled)
        {
            if (enabled)
            {
                this.enabled.Write(GpioPinValue.Low);
            }
            else
            {
                this.enabled.Write(GpioPinValue.High);
            }
        }
    }
}
