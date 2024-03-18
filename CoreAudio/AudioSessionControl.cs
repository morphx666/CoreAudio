/*
  LICENSE
  -------
  Copyright (C) 2007-2010 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/
/* Updated by John de Jong (2020/04/02) */

using System;
using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    internal interface IIAudioSessionControl {
        void FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string newDisplayName, ref Guid eventContext);

        void FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string newIconPath, ref Guid eventContext);

        void FireSimpleVolumeChanged(float newVolume, bool newMute, ref Guid eventContext);

        void FireChannelVolumeChanged(uint channelCount, IntPtr newChannelVolumeArray, uint changedChannel, ref Guid eventContext);

        void FireStateChanged(AudioSessionState newState);

        void FireSessionDisconnected(AudioSessionDisconnectReason disconnectReason);
    }

    public class AudioSessionControl : IIAudioSessionControl, IDisposable {
        internal IAudioSessionControl _AudioSessionControl;
        internal AudioMeterInformation? _AudioMeterInformation;
        internal SimpleAudioVolume? _SimpleAudioVolume;

        #region events
        public delegate void DisplayNameChangedDelegate(object sender, string newDisplayName);
        public event DisplayNameChangedDelegate? OnDisplayNameChanged;

        public delegate void IconPathChangedDelegate(object sender, string newIconPath);
        public event IconPathChangedDelegate? OnIconPathChanged;

        public delegate void SimpleVolumeChangedDelegate(object sender, float newVolume, bool newMute);
        public event SimpleVolumeChangedDelegate? OnSimpleVolumeChanged;

        public delegate void ChannelVolumeChangedDelegate(object sender, int channelCount, float[] newVolume, int changedChannel);
        public event ChannelVolumeChangedDelegate? OnChannelVolumeChanged;

        public delegate void StateChangedDelegate(object sender, AudioSessionState newState);
        public event StateChangedDelegate? OnStateChanged;

        public delegate void SessionDisconnectedDelegate(object sender, AudioSessionDisconnectReason disconnectReason);
        public event SessionDisconnectedDelegate? OnSessionDisconnected;
        #endregion

        readonly AudioSessionEvents? audioSessionEvents;

        internal AudioSessionControl(IAudioSessionControl realAudioSessionControl, ref Guid eventContext) {
            if(realAudioSessionControl is IAudioMeterInformation _meters)
                _AudioMeterInformation = new AudioMeterInformation(_meters);
            if(realAudioSessionControl is ISimpleAudioVolume _volume)
                _SimpleAudioVolume = new SimpleAudioVolume(_volume, ref eventContext);
            _AudioSessionControl = realAudioSessionControl;

            audioSessionEvents = new AudioSessionEvents(this);
            Marshal.ThrowExceptionForHR(_AudioSessionControl.RegisterAudioSessionNotification(audioSessionEvents));
        }

        void IIAudioSessionControl.FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string newDisplayName, ref Guid eventContext) {
            OnDisplayNameChanged?.Invoke(this, newDisplayName);
        }

        void IIAudioSessionControl.FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string newIconPath, ref Guid eventContext) {
            OnIconPathChanged?.Invoke(this, newIconPath);
        }

        void IIAudioSessionControl.FireSimpleVolumeChanged(float newVolume, bool newMute, ref Guid eventContext) {
            OnSimpleVolumeChanged?.Invoke(this, newVolume, newMute);
        }

        void IIAudioSessionControl.FireChannelVolumeChanged(uint channelCount, IntPtr newChannelVolumeArray, uint changedChannel, ref Guid eventContext) {
            float[] volume = new float[channelCount - 1];
            Marshal.Copy(newChannelVolumeArray, volume, 0, (int)channelCount);
            OnChannelVolumeChanged?.Invoke(this, (int)channelCount, volume, (int)changedChannel);
        }

        void IIAudioSessionControl.FireStateChanged(AudioSessionState newState) {
            OnStateChanged?.Invoke(this, newState);
        }

        void IIAudioSessionControl.FireSessionDisconnected(AudioSessionDisconnectReason disconnectReason) {
            OnSessionDisconnected?.Invoke(this, disconnectReason);
        }

        public AudioMeterInformation? AudioMeterInformation => _AudioMeterInformation;

        public SimpleAudioVolume? SimpleAudioVolume => _SimpleAudioVolume;

        public AudioSessionState State {
            get {
                Marshal.ThrowExceptionForHR(_AudioSessionControl.GetState(out var res));
                return res;
            }
        }

        public string DisplayName {
            get {
                Marshal.ThrowExceptionForHR(_AudioSessionControl.GetDisplayName(out string str));
                return str;
            }
        }

        public string IconPath {
            get {
                Marshal.ThrowExceptionForHR(_AudioSessionControl.GetIconPath(out string str));
                return str;
            }
        }

        public void Dispose() {
            if(audioSessionEvents != null)
                Marshal.ThrowExceptionForHR(_AudioSessionControl.UnregisterAudioSessionNotification(audioSessionEvents));
        }

        ~AudioSessionControl() {
            Dispose();
        }

    }
}
