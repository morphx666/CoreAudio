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
    public class AudioSessionControl2 : IIAudioSessionControl, IDisposable {
        readonly IAudioSessionControl2 audioSessionControl2;
        readonly AudioMeterInformation? audioMeterInformation;
        readonly SimpleAudioVolume? simpleAudioVolume;

        #region events

        public delegate void DisplayNameChangedDelegate(object sender, string newDisplayName);

        public event DisplayNameChangedDelegate? OnDisplayNameChanged;

        public delegate void IconPathChangedDelegate(object sender, string newIconPath);

        public event IconPathChangedDelegate? OnIconPathChanged;

        public delegate void SimpleVolumeChangedDelegate(object sender, float newVolume, bool newMute);

        public event SimpleVolumeChangedDelegate? OnSimpleVolumeChanged;

        public delegate void ChannelVolumeChangedDelegate(object sender, int channelCount, float[] newVolume,
            int changedChannel);

        public event ChannelVolumeChangedDelegate? OnChannelVolumeChanged;

        public delegate void StateChangedDelegate(object sender, AudioSessionState newState);

        public event StateChangedDelegate? OnStateChanged;

        public delegate void SessionDisconnectedDelegate(object sender, AudioSessionDisconnectReason disconnectReason);

        public event SessionDisconnectedDelegate? OnSessionDisconnected;

        internal Guid eventContext;

        #endregion

        AudioSessionEvents? audioSessionEvents;

        internal AudioSessionControl2(IAudioSessionControl2 realAudioSessionControl2, ref Guid eventContext) {
            audioSessionControl2 = realAudioSessionControl2;
            this.eventContext = eventContext;
            if(audioSessionControl2 is IAudioMeterInformation meters)
                audioMeterInformation = new AudioMeterInformation(meters);

            if(audioSessionControl2 is ISimpleAudioVolume volume)
                simpleAudioVolume = new SimpleAudioVolume(volume, ref eventContext);

            audioSessionEvents = new AudioSessionEvents(this);
            Marshal.ThrowExceptionForHR(audioSessionControl2.RegisterAudioSessionNotification(audioSessionEvents));
        }

        void IIAudioSessionControl.FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string newDisplayName, ref Guid eventContext) {
            if(eventContext != this.eventContext)
                OnDisplayNameChanged?.Invoke(this, newDisplayName);
        }

        void IIAudioSessionControl.FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string newIconPath, ref Guid eventContext) {
            if(eventContext != this.eventContext)
                OnIconPathChanged?.Invoke(this, newIconPath);
        }

        void IIAudioSessionControl.FireSimpleVolumeChanged(float NewVolume, bool newMute, ref Guid eventContext) {
            if(eventContext != this.eventContext)
                OnSimpleVolumeChanged?.Invoke(this, NewVolume, newMute);
        }

        void IIAudioSessionControl.FireChannelVolumeChanged(uint channelCount, IntPtr newChannelVolumeArray, uint changedChannel, ref Guid eventContext) {
            if(eventContext == this.eventContext) return;
            float[] volume = new float[channelCount];
            Marshal.Copy(newChannelVolumeArray, volume, 0, (int)channelCount);
            OnChannelVolumeChanged?.Invoke(this, (int)channelCount, volume, (int)changedChannel);
        }

        void IIAudioSessionControl.FireStateChanged(AudioSessionState newState) {
            OnStateChanged?.Invoke(this, newState);
        }

        void IIAudioSessionControl.FireSessionDisconnected(AudioSessionDisconnectReason disconnectReason) {
            OnSessionDisconnected?.Invoke(this, disconnectReason);
        }

        public void Dispose() {
            if(audioSessionEvents != null) {
                try {
                    Marshal.ThrowExceptionForHR(
                        audioSessionControl2.UnregisterAudioSessionNotification(audioSessionEvents));
                    audioSessionEvents = null;
                } catch {
                    // ignored
                }
            }
        }

        public AudioMeterInformation? AudioMeterInformation => audioMeterInformation;

        public SimpleAudioVolume? SimpleAudioVolume => simpleAudioVolume;

        public AudioSessionState State {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetState(out var res));
                return res;
            }
        }

        public string DisplayName {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetDisplayName(out var str));
                return str;
            }
        }

        public string IconPath {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetIconPath(out var str));
                return str;
            }
        }

        public string SessionIdentifier {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetSessionIdentifier(out var str));
                return str;
            }
        }

        public string SessionInstanceIdentifier {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetSessionInstanceIdentifier(out var str));
                return str;
            }
        }

        public uint ProcessID {
            get {
                Marshal.ThrowExceptionForHR(audioSessionControl2.GetProcessId(out var pid));
                return pid;
            }
        }

        public bool IsSystemSoundsSession => (audioSessionControl2.IsSystemSoundsSession() == 0);

        ~AudioSessionControl2() {
            Dispose();
        }
    }
}