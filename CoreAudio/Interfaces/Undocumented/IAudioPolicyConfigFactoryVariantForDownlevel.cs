﻿using System;
using System.Runtime.InteropServices;

// Original code from the EarTrumpet project
// https://github.com/File-New-Project/EarTrumpet

namespace CoreAudio.Interfaces.Undocumented {
    [Guid("2a59116d-6c4f-45e0-a74f-707e3fef9258")]
    [InterfaceType(ComInterfaceType.InterfaceIsIInspectable)]
    public interface IAudioPolicyConfigFactoryVariantForDownlevel {
        int __incomplete__add_CtxVolumeChange();
        int __incomplete__remove_CtxVolumeChanged();
        int __incomplete__add_RingerVibrateStateChanged();
        int __incomplete__remove_RingerVibrateStateChange();
        int __incomplete__SetVolumeGroupGainForId();
        int __incomplete__GetVolumeGroupGainForId();
        int __incomplete__GetActiveVolumeGroupForEndpointId();
        int __incomplete__GetVolumeGroupsForEndpoint();
        int __incomplete__GetCurrentVolumeContext();
        int __incomplete__SetVolumeGroupMuteForId();
        int __incomplete__GetVolumeGroupMuteForId();
        int __incomplete__SetRingerVibrateState();
        int __incomplete__GetRingerVibrateState();
        int __incomplete__SetPreferredChatApplication();
        int __incomplete__ResetPreferredChatApplication();
        int __incomplete__GetPreferredChatApplication();
        int __incomplete__GetCurrentChatApplications();
        int __incomplete__add_ChatContextChanged();
        int __incomplete__remove_ChatContextChanged();
        [PreserveSig]
        int SetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, IntPtr deviceId);
        [PreserveSig]
        int GetPersistedDefaultAudioEndpoint(int processId, DataFlow flow, Role role, [Out, MarshalAs(UnmanagedType.HString)] out string deviceId);
        [PreserveSig]
        int ClearAllPersistedApplicationDefaultEndpoints();
    }
}
