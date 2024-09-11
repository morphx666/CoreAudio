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
    }
}