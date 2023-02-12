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

/* Created by Xavier Flix (2010/11/18) */

using System;
using CoreAudio.Interfaces;

namespace CoreAudio {
    public class RefIId {
        public static Guid IIdIAudioCaptureClient = typeof(IAudioCaptureClient).GUID;
        //public static Guid IIdIAudioClock
        //public static Guid IIdIAudioRenderClient
        //public static Guid IIdIAudioSessionControl
        //public static Guid IIdIAudioStreamVolume
        //public static Guid IIdIChannelAudioVolume
        //public static Guid IIdIMFTrustedOutput

        public static Guid IIdISimpleAudioVolume = typeof(ISimpleAudioVolume).GUID;
        public static Guid IIdIAudioVolumeLevel = typeof(IAudioVolumeLevel).GUID;
        public static Guid IIdIAudioMute = typeof(IAudioMute).GUID;
        public static Guid IIdIAudioPeakMeter = typeof(IAudioPeakMeter).GUID;
        public static Guid IIdIAudioLoudness = typeof(IAudioLoudness).GUID;

        public static Guid IIdIAudioMeterInformation = typeof(IAudioMeterInformation).GUID;
        public static Guid IIdIAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
        public static Guid IIdIAudioSessionManager2 = typeof(IAudioSessionManager2).GUID;
        public static Guid IIdIDeviceTopology = typeof(IDeviceTopology).GUID;

        public static Guid IIdIPart = typeof(IPart).GUID;
        public static Guid IIdIConnector = typeof(IConnector).GUID;
    }
}
