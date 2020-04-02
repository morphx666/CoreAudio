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
using CoreAudio.Interfaces;
using System.Runtime.InteropServices;

namespace CoreAudio
{
    public class AudioSessionControl2 
        : _IAudioSessionControl
    {
        IAudioSessionControl2 _AudioSessionControl2;
        internal AudioMeterInformation _AudioMeterInformation;
        internal SimpleAudioVolume _SimpleAudioVolume;
        //internal AudioVolumeLevel  _AudioVolumeLevel;

        #region events
        public delegate void DisplayNameChangedDelegate(object sender, string newDisplayName);
        public event DisplayNameChangedDelegate OnDisplayNameChanged;

        public delegate void IconPathChangedDelegate(object sender, string newIconPath);
        public event IconPathChangedDelegate OnIconPathChanged;

        public delegate void SimpleVolumeChangedDelegate(object sender, Single newVolume, Boolean newMute);
        public event SimpleVolumeChangedDelegate OnSimpleVolumeChanged;

        public delegate void ChannelVolumeChangedDelegate(object sender, int channelCount, Single[] newVolume, int changedChannel);
        public event ChannelVolumeChangedDelegate OnChannelVolumeChanged;

        public delegate void StateChangedDelegate(object sender, AudioSessionState newState);
        public event StateChangedDelegate OnStateChanged;

        public delegate void SessionDisconnectedDelegate(object sender, AudioSessionDisconnectReason disconnectReason);
        public event SessionDisconnectedDelegate OnSessionDisconnected;
        #endregion

        private AudioSessionEvents _AudioSessionEvents;

        internal AudioSessionControl2(IAudioSessionControl2 realAudioSessionControl2)
        {
            _AudioSessionControl2 = realAudioSessionControl2;

            if (_AudioSessionControl2 is IAudioMeterInformation _meters) _AudioMeterInformation = new AudioMeterInformation(_meters);

            if (_AudioSessionControl2 is ISimpleAudioVolume _volume1) _SimpleAudioVolume = new SimpleAudioVolume(_volume1);

            //IAudioVolumeLevel _volume2 = _AudioSessionControl2 as IAudioVolumeLevel;
            //if (_volume2 != null) _AudioVolumeLevel = new AudioVolumeLevel(_volume2);

            _AudioSessionEvents = new AudioSessionEvents(this);
            Marshal.ThrowExceptionForHR(_AudioSessionControl2.RegisterAudioSessionNotification(_AudioSessionEvents));
        }

        public void FireDisplayNameChanged([MarshalAs(UnmanagedType.LPWStr)] string NewDisplayName, Guid EventContext)
        {
            OnDisplayNameChanged?.Invoke(this, NewDisplayName);
        }

        public void FireOnIconPathChanged([MarshalAs(UnmanagedType.LPWStr)] string NewIconPath, Guid EventContext)
        {
            OnIconPathChanged?.Invoke(this, NewIconPath);
        }

        public void FireSimpleVolumeChanged(float NewVolume, bool newMute, Guid EventContext)
        {
            OnSimpleVolumeChanged?.Invoke(this, NewVolume, newMute);
        }

        public void FireChannelVolumeChanged(UInt32 ChannelCount, IntPtr NewChannelVolumeArray, UInt32 ChangedChannel, Guid EventContext)
        {
            float[] volume = new float[ChannelCount];
            Marshal.Copy(NewChannelVolumeArray, volume, 0, (int)ChannelCount);
            OnChannelVolumeChanged?.Invoke(this, (int)ChannelCount, (Single[])volume, (int)ChangedChannel);
        }

        public void FireStateChanged(AudioSessionState NewState)
        {
            OnStateChanged?.Invoke(this, NewState);
        }

        public void FireSessionDisconnected(AudioSessionDisconnectReason DisconnectReason)
        {
            OnSessionDisconnected?.Invoke(this, DisconnectReason);
        }

        public AudioMeterInformation AudioMeterInformation
        {
            get
            {
                return _AudioMeterInformation;
            }
        }

        public SimpleAudioVolume SimpleAudioVolume
        {
            get
            {
                return _SimpleAudioVolume;
            }
        }

        //public AudioVolumeLevel AudioVolumeLevel {
        //    get {
        //        return _AudioVolumeLevel;
        //    }
        //}

        public AudioSessionState State
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetState(out AudioSessionState res));
                return res;
            }
        }

        public string DisplayName
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetDisplayName(out string str));
                return str;
            }
        }

        public string IconPath
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetIconPath(out string str));
                return str;
            }
        }

        public string GetSessionIdentifier
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetSessionIdentifier(out string str));
                return str;
            }
        }

        public string GetSessionInstanceIdentifier
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetSessionInstanceIdentifier(out string str));
                return str;
            }
        }

        public uint GetProcessID
        {
            get
            {
                Marshal.ThrowExceptionForHR(_AudioSessionControl2.GetProcessId(out uint pid));
                return pid;
            }
        }

        public bool IsSystemSoundsSession
        {
            get
            {
                return (_AudioSessionControl2.IsSystemSoundsSession() == 0);
            }

        }

        public void Dispose()
        {
            if(_AudioSessionEvents != null)
                try {
                    Marshal.ThrowExceptionForHR(_AudioSessionControl2.UnregisterAudioSessionNotification(_AudioSessionEvents));
                    _AudioSessionEvents = null;
                } catch { }
        }

        ~AudioSessionControl2() {
            Dispose();
        }
    }
}
