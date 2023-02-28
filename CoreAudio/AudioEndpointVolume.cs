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
        IAudioEndpointVolume audioEndPointVolume;
        AudioEndpointVolumeChannels channels;
        AudioEndpointVolumeStepInformation stepInformation;
        AudioEndPointVolumeVolumeRange volumeRange;
        EEndpointHardwareSupport hardwareSupport;
        AudioEndpointVolumeCallback? callBack;
        public readonly Guid eventContext;
        public event AudioEndpointVolumeNotificationDelegate? OnVolumeNotification;

        public AudioEndPointVolumeVolumeRange VolumeRange => volumeRange;

        public EEndpointHardwareSupport HardwareSupport => hardwareSupport;

        public AudioEndpointVolumeStepInformation StepInformation => stepInformation;

        public AudioEndpointVolumeChannels Channels => channels;

        public float MasterVolumeLevel {
            get {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMasterVolumeLevel(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMasterVolumeLevel(value, eventContext));
        }

        public float MasterVolumeLevelScalar {
            get {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMasterVolumeLevelScalar(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMasterVolumeLevelScalar(value, eventContext));
        }

        public bool Mute {
            get {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.GetMute(out var result));
                return result;
            }
            set => Marshal.ThrowExceptionForHR(audioEndPointVolume.SetMute(value, eventContext));
        }

        public void VolumeStepUp() {
            Marshal.ThrowExceptionForHR(audioEndPointVolume.VolumeStepUp(eventContext));
        }
      
        public void VolumeStepDown() {
            Marshal.ThrowExceptionForHR(audioEndPointVolume.VolumeStepDown(eventContext));
        }

        internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume, Guid eventContext) {
            audioEndPointVolume = realEndpointVolume;
            this.eventContext = eventContext;
            channels = new AudioEndpointVolumeChannels(audioEndPointVolume);
            stepInformation = new AudioEndpointVolumeStepInformation(audioEndPointVolume);
            Marshal.ThrowExceptionForHR(audioEndPointVolume.QueryHardwareSupport(out var hardwareSupp));
            hardwareSupport = (EEndpointHardwareSupport)hardwareSupp;
            volumeRange = new AudioEndPointVolumeVolumeRange(audioEndPointVolume);
            callBack = new AudioEndpointVolumeCallback(this);
            Marshal.ThrowExceptionForHR(audioEndPointVolume.RegisterControlChangeNotify(callBack));
        }
        internal void FireNotification(AudioVolumeNotificationData notificationData) {
            OnVolumeNotification?.Invoke(notificationData);
        }

        #region IDisposable Members

        public void Dispose() {
            if(callBack != null) {
                Marshal.ThrowExceptionForHR(audioEndPointVolume.UnregisterControlChangeNotify(callBack));
                callBack = null;
            }
        }

        ~AudioEndpointVolume() {
            Dispose();
        }

        #endregion

    }
}
