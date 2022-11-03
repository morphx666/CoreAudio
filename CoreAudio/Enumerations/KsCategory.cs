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

namespace CoreAudio {
    public class KsCategory {
        public static Guid MicrophoneArrayProcessor = new("830a44f2-a32d-476b-be97-42845673b35a");
        public static Guid Audio = new("6994AD04-93EF-11D0-A3CC-00A0C9223196");
        public static Guid Video = new("6994AD05-93EF-11D0-A3CC-00A0C9223196");
        public static Guid Realtime = new("EB115FFC-10C8-4964-831D-6DCB02E6F23F");
        public static Guid Text = new("6994AD06-93EF-11D0-A3CC-00A0C9223196");
        public static Guid Network = new("67C9CC3C-69C4-11D2-8759-00A0C9223196");
        public static Guid Topology = new("DDA54A40-1E4C-11D1-A050-405705C10000");
        public static Guid Virtual = new("3503EAC4-1F26-11D1-8AB0-00A0C9223196");
        public static Guid AcousticEchoCancel = new("BF963D80-C559-11D0-8A2B-00A0C9255AC1");
        public static Guid Sysaudio = new("A7C7A5B1-5AF3-11D1-9CED-00A024BF0407");
        public static Guid Wdmaud = new("3E227E76-690D-11D2-8161-0000F8775BF1");
        public static Guid AudioGfx = new("9BAF9572-340C-11D3-ABDC-00A0C90AB16F");
        public static Guid AudioSplitter = new("9EA331FA-B91B-45F8-9285-BD2BC77AFCDE");
        public static Guid AudioDevice = new("FBF6F530-07B9-11D2-A71E-0000F8004788");
        public static Guid PreferredWaveoutDevice = new("D6C5066E-72C1-11D2-9755-0000F8004788");
        public static Guid PreferredWaveinDevice = new("D6C50671-72C1-11D2-9755-0000F8004788");
        public static Guid PreferredMidioutDevice = new("D6C50674-72C1-11D2-9755-0000F8004788");
        public static Guid WdmaudUsePinName = new("47A4FA20-A251-11D1-A050-0000F8004788");
        public static Guid EscalantePlatformDriver = new("74F3AEA8-9768-11D1-8E07-00A0C95EC22E");
        public static Guid Tvtuner = new("A799A800-A46D-11D0-A18C-00A02401DCD4");
        public static Guid Crossbar = new("A799A801-A46D-11D0-A18C-00A02401DCD4");
        public static Guid Tvaudio = new("A799A802-A46D-11D0-A18C-00A02401DCD4");
        public static Guid Vpmux = new("A799A803-A46D-11D0-A18C-00A02401DCD4");
        public static Guid Vbicodec = new("07DAD660-22F1-11D1-A9F4-00C04FBBDE8F");
        public static Guid Encoder = new("19689BF6-C384-48FD-AD51-90E58C79F70B");
        public static Guid Multiplexer = new("7A5DE1D3-01A1-452c-B481-4FA2B96271E8");
    }
}
