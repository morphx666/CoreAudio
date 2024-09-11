using System;

// Original code from the EarTrumpet project
// https://github.com/File-New-Project/EarTrumpet

namespace CoreAudio.Interfaces.Undocumented {
    public interface IAudioPolicyConfigFactory {
        int SetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, IntPtr deviceId);
        int GetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, out string deviceId);
        int ClearAllPersistedApplicationDefaultEndpoints();
    }
}
