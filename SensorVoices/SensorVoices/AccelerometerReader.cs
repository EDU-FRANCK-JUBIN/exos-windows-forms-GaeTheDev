using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using Xamarin.Essentials;

namespace SensorVoices
{
    public sealed class AccelerometerReader
    {
        private SensorSpeed speed = SensorSpeed.UI;
        private MovementHandler movementHandler;

        private AccelerometerReader()
        {
            Accelerometer.ReadingChanged += AccelerometerSensor_ReadingChenged;
            this.movementHandler = new MovementHandler();
        }

        private static AccelerometerReader instance = null;
        public static AccelerometerReader Instance
        {
            get
            {
                if (instance == null) instance = new AccelerometerReader();
                return instance;
            }
        }

        private void AccelerometerSensor_ReadingChenged(object sender, AccelerometerChangedEventArgs e)
        {
            Console.WriteLine("changement");
            var data = e.Reading;
            Task.Run(() =>
            {
                this.movementHandler.AccelerationChanged(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z);
                MainThread.BeginInvokeOnMainThread(() => SoundPlayer.Instance.PlaySoundOnMovement(this.movementHandler.GetMovement));
            });
        }

        public void ToggleAccelerometerSensor(Context context)
        {
            try
            {
                if (Accelerometer.IsMonitoring) Accelerometer.Stop();
                else Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException ex)
            {
                Toast.MakeText(context, "Accelerometer not supported", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, "Unknow error", ToastLength.Short).Show();
            }
        }
    }
}