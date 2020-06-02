using Android.Content;
using Android.Media;
using System;
using System.Runtime.InteropServices;

namespace SensorVoices
{
    public sealed class SoundPlayer
    {
        public Context context { set; get; }
        private bool isMute;
        private bool isShort;


        private MovementHandler.MovementType lastMovement;

        private SoundPlayer()
        {
        }

        private static SoundPlayer instance = null;
        public static SoundPlayer Instance
        {
            get
            {
                if (instance == null) instance = new SoundPlayer();
                return instance;
            }
        }

        public void playSound(int sound)
        {
            if (!isMute)
            {
                MediaPlayer mediaPlayer = MediaPlayer.Create(this.context, sound);
                mediaPlayer.Start();
            }
        }

        public void ToggleMute()
        {
            isMute = !isMute;
        }

        public void ToggleSound()
        {
            isShort = !isShort;
        }

        public void PlaySoundOnMovement(MovementHandler.MovementType movement)
        {
            Console.WriteLine(movement);

            if (movement == MovementHandler.MovementType.START) playSound(isShort ? Resource.Raw.Voice01_01 : Resource.Raw.Voice02_01);
            else if (movement == MovementHandler.MovementType.ROLLING && this.lastMovement != MovementHandler.MovementType.ROLLING) playSound(isShort ? Resource.Raw.Voice01_02 : Resource.Raw.Voice02_02);
            else if (movement == MovementHandler.MovementType.ROLLING && this.lastMovement == MovementHandler.MovementType.ROLLING) playSound(isShort ? Resource.Raw.Voice01_04 : Resource.Raw.Voice02_04);
            else if (movement == MovementHandler.MovementType.CHOCK && this.lastMovement != MovementHandler.MovementType.CHOCK) playSound(isShort ? Resource.Raw.Voice01_05 : Resource.Raw.Voice02_05);
            else if (movement == MovementHandler.MovementType.CHOCK && this.lastMovement == MovementHandler.MovementType.CHOCK) playSound(isShort ? Resource.Raw.Voice01_06 : Resource.Raw.Voice02_06);
            else if (movement == MovementHandler.MovementType.LINEAR && this.lastMovement != MovementHandler.MovementType.LINEAR) playSound(isShort ? Resource.Raw.Voice01_08 : Resource.Raw.Voice02_08);
            else if (movement == MovementHandler.MovementType.LINEAR && this.lastMovement != MovementHandler.MovementType.LINEAR) playSound(isShort ? Resource.Raw.Voice01_09 : Resource.Raw.Voice02_09);
            else if (movement == MovementHandler.MovementType.END) playSound(isShort ? Resource.Raw.Voice01_10 : Resource.Raw.Voice02_10);

            lastMovement = movement;
        }
    }
}