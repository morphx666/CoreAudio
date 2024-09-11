using CoreAudio.Interfaces.Undocumented;
using System;

namespace CoreAudio.Undocumented {
    class AudioPolicyConfigFactoryVariantFor21H2 : IAudioPolicyConfigFactory {
        private readonly IAudioPolicyConfigFactoryVariantFor21H2 _factory;

        internal AudioPolicyConfigFactoryVariantFor21H2() {
            var iid = typeof(IAudioPolicyConfigFactoryVariantFor21H2).GUID;
            Combase.RoGetActivationFactory("Windows.Media.Internal.AudioPolicyConfig", ref iid, out object factory);
            _factory = (IAudioPolicyConfigFactoryVariantFor21H2)factory;
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