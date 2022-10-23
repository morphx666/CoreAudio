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
    public class Part : IDisposable {
        IPart _Part;

        AudioVolumeLevel? _AudioVolumeLevel;
        AudioMute? _AudioMute;
        AudioPeakMeter? _AudioPeakMeter;
        AudioLoudness? _AudioLoudness;

        public delegate void PartNotificationDelegate(object sender);

        public event PartNotificationDelegate? OnPartNotification;

        ControlChangeNotify? _AudioVolumeLevelChangeNotification;
        ControlChangeNotify? _AudioMuteChangeNotification;
        ControlChangeNotify? _AudioPeakMeterChangeNotification;
        ControlChangeNotify? _AudioLoudnessChangeNotification;

        PartsList? partsListIncoming;
        PartsList? partsListOutgoing;

        internal Part(IPart part) {
            _Part = part;
        }

        internal void FireNotification(uint dwSenderProcessId, ref Guid pguidEventContext) {
            OnPartNotification?.Invoke(this);
        }

        void GetAudioVolumeLevel() {
            _Part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioVolumeLevel, out var result);
            if(result is IAudioVolumeLevel level) {
                _AudioVolumeLevel = new AudioVolumeLevel(level);
                _AudioVolumeLevelChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(_Part.RegisterControlChangeCallback(ref RefIId.IIdIAudioVolumeLevel,
                    _AudioVolumeLevelChangeNotification));
            }
        }

        void GetAudioMute() {
            _Part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioMute, out var result);
            if(result is IAudioMute mute) {
                _AudioMute = new AudioMute(mute);
                _AudioMuteChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(
                    _Part.RegisterControlChangeCallback(ref RefIId.IIdIAudioMute, _AudioMuteChangeNotification));
            }
        }

        void GetAudioPeakMeter() {
            _Part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioPeakMeter, out var result);
            if(result is IAudioPeakMeter meter) {
                _AudioPeakMeter = new AudioPeakMeter(meter);
                _AudioPeakMeterChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(_Part.RegisterControlChangeCallback(ref RefIId.IIdIAudioPeakMeter,
                    _AudioPeakMeterChangeNotification));
            }
        }

        void GetAudioLoudness() {
            _Part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioLoudness, out var result);
            if(result is IAudioLoudness loudness) {
                _AudioLoudness = new AudioLoudness(loudness);
                _AudioLoudnessChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(_Part.RegisterControlChangeCallback(ref RefIId.IIdIAudioLoudness,
                    _AudioLoudnessChangeNotification));
            }
        }

        public string GetName {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetName(out string name));
                return name;
            }
        }

        public int GetLocalId {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetLocalId(out var id));
                return id;
            }
        }

        public string GetGlobalId {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetGlobalId(out string id));
                return id;
            }
        }

        public PartType GetPartType {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetPartType(out var type));
                return type;
            }
        }

        public Guid GetSubType {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetSubType(out var type));
                return type;
            }
        }

        public string GetSubTypeName {
            get {
                string result;

                result = FindSubTypeIn(GetSubType, typeof(KsNodeType));
                if(result != "") return result;

                result = FindSubTypeIn(GetSubType, typeof(KsCategory));
                if(result != "") return result;

                return "UNDEFINED";
            }
        }

        string FindSubTypeIn(Guid findGuid, Type inClass) {
            foreach(var field in inClass.GetFields()) {
                string name = field.Name;
                var temp = (Guid)field.GetValue(null);
                if(temp == findGuid) {
                    return name;
                }
            }

            return "";
        }

        public int GetControlInterfaceCount {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetControlInterfaceCount(out var count));
                return count;
            }
        }

        public ControlInterface GetControlInterface(int index) {
            Marshal.ThrowExceptionForHR(_Part.GetControlInterface(index, out IControlInterface controlInterface));
            return new ControlInterface(controlInterface);
        }

        public PartsList? EnumPartsIncoming {
            get {
                if(partsListIncoming == null) {
                    _Part.EnumPartsIncoming(out var partsList);
                    if(partsList != null) partsListIncoming = new PartsList(partsList);
                }

                return partsListIncoming;
            }
        }

        public PartsList? EnumPartsOutgoing {
            get {
                if(partsListOutgoing == null) {
                    _Part.EnumPartsOutgoing(out var partsList);
                    if(partsList != null) partsListOutgoing = new PartsList(partsList);
                }

                return partsListOutgoing;
            }
        }

        public DeviceTopology GetTopologyObject {
            get {
                Marshal.ThrowExceptionForHR(_Part.GetTopologyObject(out IDeviceTopology deviceTopology));
                return new DeviceTopology(deviceTopology);
            }
        }

        public AudioVolumeLevel? AudioVolumeLevel {
            get {
                if(_AudioVolumeLevel == null)
                    GetAudioVolumeLevel();

                return _AudioVolumeLevel;
            }
        }

        public AudioMute? AudioMute {
            get {
                if(_AudioMute == null)
                    GetAudioMute();

                return _AudioMute;
            }
        }

        public AudioPeakMeter? AudioPeakMeter {
            get {
                if(_AudioPeakMeter == null)
                    GetAudioPeakMeter();

                return _AudioPeakMeter;
            }
        }

        public AudioLoudness? AudioLoudness {
            get {
                if(_AudioLoudness == null)
                    GetAudioLoudness();

                return _AudioLoudness;
            }
        }

        #region IDisposable Members

        public void Dispose() {
            DisposeCtrlChangeNotify(ref _AudioLoudnessChangeNotification);
            DisposeCtrlChangeNotify(ref _AudioMuteChangeNotification);
            DisposeCtrlChangeNotify(ref _AudioPeakMeterChangeNotification);
            DisposeCtrlChangeNotify(ref _AudioVolumeLevelChangeNotification);
            OnPartNotification = null;
        }

        void DisposeCtrlChangeNotify(ref ControlChangeNotify? obj) {
            if(obj != null) {
                try {
                    if(obj.IsAllocated) {
                        Marshal.ThrowExceptionForHR(_Part.UnregisterControlChangeCallback(obj));
                        obj.Dispose();
                    }
                } catch {
                    // ignored
                }

                obj = null;
            }
        }

        ~Part() {
            Dispose();
        }

        #endregion
    }
}