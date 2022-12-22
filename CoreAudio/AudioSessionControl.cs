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
    internal interface _IAudioSessionControl {
        void FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext);

        void FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext);

        void FireSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext);

        void FireChannelVolumeChanged(uint ChannelCount, IntPtr NewChannelVolumeArray, uint ChangedChannel, Guid EventContext);

        void FireStateChanged(AudioSessionState NewState);

        void FireSessionDisconnected(AudioSessionDisconnectReason DisconnectReason);
    }

    public class AudioSessionControl : _IAudioSessionControl, IDisposable {
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

        AudioSessionEvents? audioSessionEvents;

        internal AudioSessionControl(IAudioSessionControl realAudioSessionControl) {
            if(realAudioSessionControl is IAudioMeterInformation _meters)
                _AudioMeterInformation = new AudioMeterInformation(_meters);
            if(realAudioSessionControl is ISimpleAudioVolume _volume)
                _SimpleAudioVolume = new SimpleAudioVolume(_volume, Guid.Empty);
            _AudioSessionControl = realAudioSessionControl;

            audioSessionEvents = new AudioSessionEvents(this);
            Marshal.ThrowExceptionForHR(_AudioSessionControl.RegisterAudioSessionNotification(audioSessionEvents));
        }

        void _IAudioSessionControl.FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext) {
            OnDisplayNameChanged?.Invoke(this, NewDisplayName);
        }

        void _IAudioSessionControl.FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext) {
            OnIconPathChanged?.Invoke(this, NewIconPath);
        }

        void _IAudioSessionControl.FireSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext) {
            OnSimpleVolumeChanged?.Invoke(this, NewVolume, newMute);
        }

        void _IAudioSessionControl.FireChannelVolumeChanged(uint ChannelCount, IntPtr NewChannelVolumeArray, uint ChangedChannel, Guid EventContext) {
            float[] volume = new float[ChannelCount - 1];
            Marshal.Copy(NewChannelVolumeArray, volume, 0, (int)ChannelCount);
            OnChannelVolumeChanged?.Invoke(this, (int)ChannelCount, volume, (int)ChangedChannel);
        }

        void _IAudioSessionControl.FireStateChanged(AudioSessionState NewState) {
            OnStateChanged?.Invoke(this, NewState);
        }

        void _IAudioSessionControl.FireSessionDisconnected(AudioSessionDisconnectReason DisconnectReason) {
            OnSessionDisconnected?.Invoke(this, DisconnectReason);
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
