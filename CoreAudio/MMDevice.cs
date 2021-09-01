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

namespace CoreAudio
{
    public class MMDevice
    {
        #region Variables

        readonly IMMDevice _RealDevice;
        PropertyStore? _PropertyStore;
        AudioMeterInformation? _AudioMeterInformation;
        AudioEndpointVolume? _AudioEndpointVolume;
        AudioSessionManager2? _AudioSessionManager2;
        DeviceTopology? _DeviceTopology;

        #endregion

        #region Init

        void GetPropertyInformation()
        {
            Marshal.ThrowExceptionForHR(_RealDevice.OpenPropertyStore(EStgmAccess.STGM_READ, out IPropertyStore propertyStore));
            _PropertyStore = new PropertyStore(propertyStore);
        }

        void GetAudioSessionManager2()
        {
            Marshal.ThrowExceptionForHR(_RealDevice.Activate(ref IIDs.IID_IAudioSessionManager2, CLSCTX.ALL, IntPtr.Zero, out var result));
            _AudioSessionManager2 = new AudioSessionManager2((IAudioSessionManager2)result);
        }

        void GetAudioMeterInformation()
        {
            Marshal.ThrowExceptionForHR(_RealDevice.Activate(ref IIDs.IID_IAudioMeterInformation, CLSCTX.ALL, IntPtr.Zero, out var result));
            _AudioMeterInformation = new AudioMeterInformation((IAudioMeterInformation)result);
        }

        void GetAudioEndpointVolume()
        {
            Marshal.ThrowExceptionForHR(_RealDevice.Activate(ref IIDs.IID_IAudioEndpointVolume, CLSCTX.ALL, IntPtr.Zero, out var result));
            _AudioEndpointVolume = new AudioEndpointVolume((IAudioEndpointVolume)result);
        }

        void GetDeviceTopology()
        {
            Marshal.ThrowExceptionForHR(_RealDevice.Activate(ref IIDs.IID_IDeviceTopology, CLSCTX.ALL, IntPtr.Zero, out var result));
            _DeviceTopology = new DeviceTopology((IDeviceTopology)result);
        }

        #endregion

        #region Properties

        public AudioSessionManager2? AudioSessionManager2
        {
            get
            {
                if (_AudioSessionManager2 == null) GetAudioSessionManager2();
                return _AudioSessionManager2;
            }
        }

        public AudioMeterInformation? AudioMeterInformation
        {
            get
            {
                if (_AudioMeterInformation == null) GetAudioMeterInformation();
                return _AudioMeterInformation;
            }
        }

        public AudioEndpointVolume? AudioEndpointVolume
        {
            get
            {
                if (_AudioEndpointVolume == null) GetAudioEndpointVolume();
                return _AudioEndpointVolume;
            }
        }

        public PropertyStore? Properties
        {
            get
            {
                if (_PropertyStore == null) GetPropertyInformation();
                return _PropertyStore;
            }
        }

        public DeviceTopology? DeviceTopology
        {
            get
            {
                if (_DeviceTopology == null) GetDeviceTopology();
                return _DeviceTopology;
            }
        }

        public string FriendlyName
        {
            get
            {
                if (_PropertyStore == null) 
                    GetPropertyInformation();
                if (_PropertyStore?.Contains(PKEY.PKEY_DeviceInterface_FriendlyName) ?? false) {
                    return (string?)_PropertyStore?[PKEY.PKEY_DeviceInterface_FriendlyName]?.Value ?? "";
                }

                return "Unknown";
            }
        }

        public string IconPath
        {
            get
            {
                if (_PropertyStore == null) GetPropertyInformation();
                if (_PropertyStore?.Contains(PKEY.PKEY_DeviceClass_IconPath) ?? false)
                    return (string)_PropertyStore[PKEY.PKEY_DeviceClass_IconPath].Value;
                return "";
            }
        }

        public string ID
        {
            get
            {
                Marshal.ThrowExceptionForHR(_RealDevice.GetId(out string result));
                return result;
            }
        }

        public EDataFlow DataFlow
        {
            get
            {
                var ep = (IMMEndpoint)_RealDevice;
                ep.GetDataFlow(out var result);
                return result;
            }
        }

        public DEVICE_STATE State
        {
            get
            {
                Marshal.ThrowExceptionForHR(_RealDevice.GetState(out var result));
                return result;

            }
        }

        internal IMMDevice ReadDevice => _RealDevice;

        public bool Selected
        {
            get => new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow, ERole.eMultimedia).ID == ID;
            set
            {
                if (value)
                {
                    new CPolicyConfigVistaClient().SetDefaultDevice(ID);
                }
            }
        }

        #endregion

        #region Constructor
        internal MMDevice(IMMDevice realDevice)
        {
            _RealDevice = realDevice;
        }
        #endregion

    }
}
