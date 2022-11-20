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
        IPart part;
        readonly AudioVolumeLevel? audioVolumeLevel;
        AudioMute? audioMute;
        AudioPeakMeter? audioPeakMeter;
        AudioLoudness? audioLoudness;

        public delegate void PartNotificationDelegate(object sender);

        public event PartNotificationDelegate? OnPartNotification;

        ControlChangeNotify? _AudioVolumeLevelChangeNotification;
        ControlChangeNotify? _AudioMuteChangeNotification;
        ControlChangeNotify? _AudioPeakMeterChangeNotification;
        ControlChangeNotify? _AudioLoudnessChangeNotification;

        PartsList? partsListIncoming;
        PartsList? partsListOutgoing;

        internal Part(IPart part) {
            this.part = part;
        }

        internal void FireNotification(uint dwSenderProcessId, ref Guid pguidEventContext) {
            OnPartNotification?.Invoke(this);
        }

        private AudioVolumeLevel? GetAudioVolumeLevel() {
            return audioVolumeLevel;
        }

        void GetAudioVolumeLevel(AudioVolumeLevel? audioVolumeLevel) {
            part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioVolumeLevel, out var result);
            if(result is IAudioVolumeLevel level) {
                audioVolumeLevel = new AudioVolumeLevel(level);
                _AudioVolumeLevelChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(part.RegisterControlChangeCallback(ref RefIId.IIdIAudioVolumeLevel,
                    _AudioVolumeLevelChangeNotification));
            }
        }

        void GetAudioMute() {
            part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioMute, out var result);
            if(result is IAudioMute mute) {
                audioMute = new AudioMute(mute);
                _AudioMuteChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(
                    part.RegisterControlChangeCallback(ref RefIId.IIdIAudioMute, _AudioMuteChangeNotification));
            }
        }

        void GetAudioPeakMeter() {
            part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioPeakMeter, out var result);
            if(result is IAudioPeakMeter meter) {
                audioPeakMeter = new AudioPeakMeter(meter);
                _AudioPeakMeterChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(part.RegisterControlChangeCallback(ref RefIId.IIdIAudioPeakMeter,
                    _AudioPeakMeterChangeNotification));
            }
        }

        void GetAudioLoudness() {
            part.Activate(CLSCTX.ALL, ref RefIId.IIdIAudioLoudness, out var result);
            if(result is IAudioLoudness loudness) {
                audioLoudness = new AudioLoudness(loudness);
                _AudioLoudnessChangeNotification = new ControlChangeNotify(this);
                Marshal.ThrowExceptionForHR(part.RegisterControlChangeCallback(ref RefIId.IIdIAudioLoudness,
                    _AudioLoudnessChangeNotification));
            }
        }

        public string Name {
            get {
                Marshal.ThrowExceptionForHR(part.GetName(out string name));
                return name;
            }
        }

        public int LocalId {
            get {
                Marshal.ThrowExceptionForHR(part.GetLocalId(out var id));
                return id;
            }
        }

        public string GlobalId {
            get {
                Marshal.ThrowExceptionForHR(part.GetGlobalId(out string id));
                return id;
            }
        }

        public PartType PartType {
            get {
                Marshal.ThrowExceptionForHR(part.GetPartType(out var type));
                return type;
            }
        }

        public Guid SubType {
            get {
                Marshal.ThrowExceptionForHR(part.GetSubType(out var type));
                return type;
            }
        }

        public string SubTypeName {
            get {
                string result;

                result = FindSubTypeIn(SubType, typeof(KsNodeType));
                if(result != "") return result;

                result = FindSubTypeIn(SubType, typeof(KsCategory));
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

        public int ControlInterfaceCount {
            get {
                Marshal.ThrowExceptionForHR(part.GetControlInterfaceCount(out var count));
                return count;
            }
        }

        public ControlInterface ControlInterface(int index) {
            Marshal.ThrowExceptionForHR(part.GetControlInterface(index, out IControlInterface controlInterface));
            return new ControlInterface(controlInterface);
        }

        public PartsList? EnumPartsIncoming {
            get {
                if(partsListIncoming == null) {
                    part.EnumPartsIncoming(out var partsList);
                    if(partsList != null) partsListIncoming = new PartsList(partsList);
                }

                return partsListIncoming;
            }
        }

        public PartsList? EnumPartsOutgoing {
            get {
                if(partsListOutgoing == null) {
                    part.EnumPartsOutgoing(out var partsList);
                    if(partsList != null) partsListOutgoing = new PartsList(partsList);
                }

                return partsListOutgoing;
            }
        }

        public DeviceTopology TopologyObject {
            get {
                Marshal.ThrowExceptionForHR(part.GetTopologyObject(out IDeviceTopology deviceTopology));
                return new DeviceTopology(deviceTopology);
            }
        }

        public AudioVolumeLevel? AudioVolumeLevel {
            get {
                if(audioVolumeLevel == null)
                    GetAudioVolumeLevel(GetAudioVolumeLevel());

                return audioVolumeLevel;
            }
        }

        public AudioMute? AudioMute {
            get {
                if(audioMute == null)
                    GetAudioMute();

                return audioMute;
            }
        }

        public AudioPeakMeter? AudioPeakMeter {
            get {
                if(audioPeakMeter == null)
                    GetAudioPeakMeter();

                return audioPeakMeter;
            }
        }

        public AudioLoudness? AudioLoudness {
            get {
                if(audioLoudness == null)
                    GetAudioLoudness();

                return audioLoudness;
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
                        Marshal.ThrowExceptionForHR(part.UnregisterControlChangeCallback(obj));
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