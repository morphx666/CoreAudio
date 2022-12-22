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
    public class MMDevice : IDisposable {
        #region Variables

        readonly IMMDevice realDevice;
        PropertyStore? propertyStore;
        AudioMeterInformation? audioMeterInformation;
        AudioEndpointVolume? audioEndPointVolume;
        AudioSessionManager2? audioSessionManager2;
        DeviceTopology? deviceTopology;
        private Guid eventContext;
        #endregion

        #region Init

        void GetPropertyInformation() {
            Marshal.ThrowExceptionForHR(realDevice.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore));
            this.propertyStore = new PropertyStore(propertyStore);
        }

        void GetAudioSessionManager2() {
            Marshal.ThrowExceptionForHR(realDevice.Activate(ref RefIId.IIdIAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out var result));
            audioSessionManager2 = new AudioSessionManager2((IAudioSessionManager2)result, eventContext);
        }

        void GetAudioMeterInformation() {
            Marshal.ThrowExceptionForHR(realDevice.Activate(ref RefIId.IIdIAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out var result));
            audioMeterInformation = new AudioMeterInformation((IAudioMeterInformation)result);
        }

        void GetAudioEndpointVolume() {
            Marshal.ThrowExceptionForHR(realDevice.Activate(ref RefIId.IIdIAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out var result));
            audioEndPointVolume = new AudioEndpointVolume((IAudioEndpointVolume)result, eventContext);
        }

        void GetDeviceTopology() {
            Marshal.ThrowExceptionForHR(realDevice.Activate(ref RefIId.IIdIDeviceTopology, CLSCTX.ALL, IntPtr.Zero, out var result));
            deviceTopology = new DeviceTopology((IDeviceTopology)result);
        }

        public void Dispose() {
            audioEndPointVolume?.Dispose();
            audioSessionManager2?.Dispose();
        }

        ~MMDevice() {
            Dispose();
        }

        #endregion

        #region Properties

        public AudioSessionManager2? AudioSessionManager2 {
            get {
                if(audioSessionManager2 == null) GetAudioSessionManager2();
                return audioSessionManager2;
            }
        }

        public AudioMeterInformation? AudioMeterInformation {
            get {
                if(audioMeterInformation == null) GetAudioMeterInformation();
                return audioMeterInformation;
            }
        }

        public AudioEndpointVolume? AudioEndpointVolume {
            get {
                if(audioEndPointVolume == null) GetAudioEndpointVolume();
                return audioEndPointVolume;
            }
        }

        public PropertyStore? Properties {
            get {
                if(propertyStore == null) GetPropertyInformation();
                return propertyStore;
            }
        }

        public DeviceTopology? DeviceTopology {
            get {
                if(deviceTopology == null) GetDeviceTopology();
                return deviceTopology;
            }
        }

        [Obsolete("FriendlyName is deprecated, please use " + nameof(DeviceFriendlyName) + " instead.")]
        public string FriendlyName {
            get {
                return DeviceFriendlyName;
            }
        }

        public string DeviceInterfaceFriendlyName {
            get {
                if(propertyStore == null)
                    GetPropertyInformation();
                if(propertyStore?.Contains(PKey.DeviceInterfaceFriendlyName) ?? false) {
                    return (string?)propertyStore?[PKey.DeviceInterfaceFriendlyName]?.Value ?? "";
                }

                return "Unknown";
            }
        }

        public string DeviceFriendlyName {
            get {
                if(propertyStore == null)
                    GetPropertyInformation();
                if(propertyStore?.Contains(PKey.DeviceFriendlyName) ?? false) {
                    return (string?)propertyStore?[PKey.DeviceFriendlyName]?.Value ?? "";
                }

                return "Unknown";
            }
        }

        public string IconPath {
            get {
                if(propertyStore == null) GetPropertyInformation();
                if(propertyStore?.Contains(PKey.DeviceClassIconPath) ?? false)
                    return (string?)propertyStore[PKey.DeviceClassIconPath]?.Value ?? "";
                return "";
            }
        }

        public string ID {
            get {
                Marshal.ThrowExceptionForHR(realDevice.GetId(out string result));
                return result;
            }
        }

        public DataFlow DataFlow {
            get {
                var ep = (IMMEndpoint)realDevice;
                ep.GetDataFlow(out var result);
                return result;
            }
        }

        public DeviceState State {
            get {
                Marshal.ThrowExceptionForHR(realDevice.GetState(out var result));
                return result;

            }
        }

        internal IMMDevice ReadDevice => realDevice;

        public bool Selected {
            get => new MMDeviceEnumerator(eventContext).GetDefaultAudioEndpoint(DataFlow, Role.Multimedia).ID == ID;
            set {
                if(value) {
                    new CPolicyConfigVistaClient().SetDefaultDevice(ID);
                }
            }
        }

        #endregion

        #region Constructor
        internal MMDevice(IMMDevice realDevice, Guid eventContext) {
            this.realDevice = realDevice;
            this.eventContext = eventContext;
        }
        #endregion
    }
}
