using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Media;
using Xamarin.Essentials;

namespace SensorVoices
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        ToggleButton shortBtn, longBtn;
        Button voice1Btn, voice2Btn, voice3Btn, voice4Btn, voice5Btn, voice6Btn, voice7Btn, voice8Btn, voice9Btn, voice10Btn;
        ToggleButton muteBtn;
        Button vibrationBtn;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SoundPlayer.Instance.context = this;
            AccelerometerReader.Instance.ToggleAccelerometerSensor(this);

            shortBtn = FindViewById<ToggleButton>(Resource.Id.shortBtn);
            longBtn = FindViewById<ToggleButton>(Resource.Id.longBtn);

            voice1Btn = FindViewById<Button>(Resource.Id.voice1);
            voice2Btn = FindViewById<Button>(Resource.Id.voice2);
            voice3Btn = FindViewById<Button>(Resource.Id.voice3);
            voice4Btn = FindViewById<Button>(Resource.Id.voice4);
            voice5Btn = FindViewById<Button>(Resource.Id.voice5);
            voice6Btn = FindViewById<Button>(Resource.Id.voice6);
            voice7Btn = FindViewById<Button>(Resource.Id.voice7);
            voice8Btn = FindViewById<Button>(Resource.Id.voice8);
            voice9Btn = FindViewById<Button>(Resource.Id.voice9);
            voice10Btn = FindViewById<Button>(Resource.Id.voice10);

            muteBtn = FindViewById<ToggleButton>(Resource.Id.mute);
            vibrationBtn = FindViewById<Button>(Resource.Id.vibration);

            shortBtn.Click += delegate
            {
                longBtn.Checked = !shortBtn.Checked;
            };

            longBtn.Click += delegate
            {
                shortBtn.Checked = !longBtn.Checked;
            };

            voice1Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_01 : Resource.Raw.Voice02_01); };
            voice2Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_02 : Resource.Raw.Voice02_02); };
            voice3Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_03 : Resource.Raw.Voice02_03); };
            voice4Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_04 : Resource.Raw.Voice02_04); };
            voice5Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_05 : Resource.Raw.Voice02_05); };
            voice6Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_06 : Resource.Raw.Voice02_06); };
            voice7Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_07 : Resource.Raw.Voice02_07); };
            voice8Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_08 : Resource.Raw.Voice02_08); };
            voice9Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_09 : Resource.Raw.Voice02_09); };
            voice10Btn.Click += delegate { SoundPlayer.Instance.playSound(shortBtn.Checked ? Resource.Raw.Voice01_10 : Resource.Raw.Voice02_10); };

            vibrationBtn.Click += delegate
            {
                try
                {
                    Vibration.Vibrate();
                    // explicite vibration for vm !
                    Toast.MakeText(this, "Vibration", ToastLength.Short).Show();
                } 
                catch (FeatureNotSupportedException ex)
                {
                    Toast.MakeText(this, "Vibration not supported", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Unkown error while vibrating", ToastLength.Short).Show();
                }
            };

            muteBtn.Click += delegate { SoundPlayer.Instance.ToggleMute(); };

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
