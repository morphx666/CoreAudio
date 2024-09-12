using CoreAudio.Interfaces.Undocumented;
using System;

namespace CoreAudio.Undocumented {
    class AudioPolicyConfigFactoryVariantForDownlevel : IAudioPolicyConfigFactory {
        private readonly IAudioPolicyConfigFactoryVariantForDownlevel _factory;

        internal AudioPolicyConfigFactoryVariantForDownlevel() {
            var iid = typeof(IAudioPolicyConfigFactoryVariantForDownlevel).GUID;
            Combase.RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref iid, out object factory);
            _factory = (IAudioPolicyConfigFactoryVariantForDownlevel)factory;
        }

        public int ClearAllPersistedApplicationDefaultEndpoints() {
            return _factory.ClearAllPersistedApplicationDefaultEndpoints();
        }

        public int GetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, out string deviceId) {
            return _factory.GetPersistedDefaultAudioEndpoint(processId, flow, role, out deviceId);
        }

        public int SetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, IntPtr deviceId) {
            return _factory.SetPersistedDefaultAudioEndpoint(processId, flow, role, deviceId);
        }

        public MMDevice? GetPersistedDefaultAudioEndpoint(MMDeviceEnumerator deviceEnumerator, int processId, DataFlow flow, Role role, DeviceState state = DeviceState.MaskAll) {
            MMDevice? device = null;

            int r = GetPersistedDefaultAudioEndpoint(processId, flow, role, out string deviceId);
            if(r == 0 || deviceId != "") {
                foreach(MMDevice dev in deviceEnumerator.EnumerateAudioEndPoints(flow, state)) {
                    if(deviceId.Contains(dev.ID)) {
                        device = dev;
                        break;
                    }
                }
            }
            device ??= deviceEnumerator.GetDefaultAudioEndpoint(flow, role);

            return device;
        }

        public void SetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, MMDevice device) {
            if(!string.IsNullOrWhiteSpace(device.ID)) {
                string id = $@"\\?\SWD#MMDEVAPI#{device.ID}#{{e6327cad-dcec-4949-ae8a-991e976a79d2}}";
                Combase.WindowsCreateString(id, (uint)id.Length, out IntPtr hstring);

                SetPersistedDefaultAudioEndpoint(processId, flow, role, hstring);
            }
        }
    }
}