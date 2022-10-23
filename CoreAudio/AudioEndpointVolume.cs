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

using System;
using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {

    public class AudioEndpointVolume : IDisposable {
        IAudioEndpointVolume _AudioEndPointVolume;
        AudioEndpointVolumeChannels _Channels;
        AudioEndpointVolumeStepInformation _StepInformation;
        AudioEndPointVolumeVolumeRange _VolumeRange;
        EEndpointHardwareSupport _HardwareSupport;
        AudioEndpointVolumeCallback? _CallBack;
        public event AudioEndpointVolumeNotificationDelegate? OnVolumeNotification;

        public AudioEndPointVolumeVolumeRange VolumeRange => _VolumeRange;

        public EEndpointHardwareSupport HardwareSupport => _HardwareSupport;

        public AudioEndpointVolumeStepInformation StepInformation => _StepInformation;

        public AudioEndpointVolumeChannels Channels => _Channels;

        public float MasterVolumeLevel {
            get {
                Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMasterVolumeLevel(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
        }

        public float MasterVolumeLevelScalar {
            get {
                Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMasterVolumeLevelScalar(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
        }

        public bool Mute {
            get {
                Marshal.ThrowExceptionForHR(_AudioEndPointVolume.GetMute(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(_AudioEndPointVolume.SetMute(value, Guid.Empty));
        }

        public void VolumeStepUp() {
            Marshal.ThrowExceptionForHR(_AudioEndPointVolume.VolumeStepUp(Guid.Empty));
        }

        public void VolumeStepDown() {
            Marshal.ThrowExceptionForHR(_AudioEndPointVolume.VolumeStepDown(Guid.Empty));
        }

        internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume) {
            _AudioEndPointVolume = realEndpointVolume;
            _Channels = new AudioEndpointVolumeChannels(_AudioEndPointVolume);
            _StepInformation = new AudioEndpointVolumeStepInformation(_AudioEndPointVolume);
            Marshal.ThrowExceptionForHR(_AudioEndPointVolume.QueryHardwareSupport(out var hardwareSupp));
            _HardwareSupport = (EEndpointHardwareSupport)hardwareSupp;
            _VolumeRange = new AudioEndPointVolumeVolumeRange(_AudioEndPointVolume);
            _CallBack = new AudioEndpointVolumeCallback(this);
            Marshal.ThrowExceptionForHR(_AudioEndPointVolume.RegisterControlChangeNotify(_CallBack));
        }
        internal void FireNotification(AudioVolumeNotificationData notificationData) {
            OnVolumeNotification?.Invoke(notificationData);
        }

        #region IDisposable Members

        public void Dispose() {
            if(_CallBack != null) {
                Marshal.ThrowExceptionForHR(_AudioEndPointVolume.UnregisterControlChangeNotify(_CallBack));
                _CallBack = null;
            }
        }

        ~AudioEndpointVolume() {
            Dispose();
        }

        #endregion

    }
}
