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
    public class Ks123NodeType {
        public static Guid InputUndefined = new("DFF21BE0-F70F-11D0-B917-00A0C9223196");
        public static Guid Microphone = new("DFF21BE1-F70F-11D0-B917-00A0C9223196");
        public static Guid DesktopMicrophone = new("DFF21BE2-F70F-11D0-B917-00A0C9223196");
        public static Guid PersonalMicrophone = new("DFF21BE3-F70F-11D0-B917-00A0C9223196");
        public static Guid OmniDirectionalMicrophone = new("DFF21BE4-F70F-11D0-B917-00A0C9223196");
        public static Guid MicrophoneArray = new("DFF21BE5-F70F-11D0-B917-00A0C9223196");
        public static Guid ProcessingMicrophoneArray = new("DFF21BE6-F70F-11D0-B917-00A0C9223196");
        public static Guid OutputUndefined = new("DFF21CE0-F70F-11D0-B917-00A0C9223196");
        public static Guid Speaker = new("DFF21CE1-F70F-11D0-B917-00A0C9223196");
        public static Guid Headphones = new("DFF21CE2-F70F-11D0-B917-00A0C9223196");
        public static Guid HeadMountedDisplayAudio = new("DFF21CE3-F70F-11D0-B917-00A0C9223196");
        public static Guid DesktopSpeaker = new("DFF21CE4-F70F-11D0-B917-00A0C9223196");
        public static Guid RoomSpeaker = new("DFF21CE5-F70F-11D0-B917-00A0C9223196");
        public static Guid CommunicationSpeaker = new("DFF21CE6-F70F-11D0-B917-00A0C9223196");
        public static Guid LowFrequencyEffectsSpeaker = new("DFF21CE7-F70F-11D0-B917-00A0C9223196");
        public static Guid BidirectionalUndefined = new("DFF21DE0-F70F-11D0-B917-00A0C9223196");
        public static Guid Handset = new("DFF21DE1-F70F-11D0-B917-00A0C9223196");
        public static Guid Headset = new("DFF21DE2-F70F-11D0-B917-00A0C9223196");
        public static Guid SpeakerphoneNoEchoReduction = new("DFF21DE3-F70F-11D0-B917-00A0C9223196");
        public static Guid EchoSuppressingSpeakerphone = new("DFF21DE4-F70F-11D0-B917-00A0C9223196");
        public static Guid EchoCancelingSpeakerphone = new("DFF21DE5-F70F-11D0-B917-00A0C9223196");
        public static Guid TelephonyUndefined = new("DFF21EE0-F70F-11D0-B917-00A0C9223196");
        public static Guid PhoneLine = new("DFF21EE1-F70F-11D0-B917-00A0C9223196");
        public static Guid Telephone = new("DFF21EE2-F70F-11D0-B917-00A0C9223196");
        public static Guid DownLinePhone = new("DFF21EE3-F70F-11D0-B917-00A0C9223196");
        public static Guid ExternalUndefined = new("DFF21FE0-F70F-11D0-B917-00A0C9223196");
        public static Guid AnalogConnector = new("DFF21FE1-F70F-11D0-B917-00A0C9223196");
        public static Guid DigitalAudioInterface = new("DFF21FE2-F70F-11D0-B917-00A0C9223196");
        public static Guid LineConnector = new("DFF21FE3-F70F-11D0-B917-00A0C9223196");
        public static Guid LegacyAudioConnector = new("DFF21FE4-F70F-11D0-B917-00A0C9223196");
        public static Guid SpdifInterface = new("DFF21FE5-F70F-11D0-B917-00A0C9223196");
        public static Guid DaStream1394 = new("DFF21FE6-F70F-11D0-B917-00A0C9223196");
        public static Guid DvStream1394Soundtrack = new("DFF21FE7-F70F-11D0-B917-00A0C9223196");
        public static Guid EmbeddedUndefined = new("DFF220E0-F70F-11D0-B917-00A0C9223196");
        public static Guid LevelCalibrationNoiseSource = new("DFF220E1-F70F-11D0-B917-00A0C9223196");
        public static Guid EqualizationNoise = new("DFF220E2-F70F-11D0-B917-00A0C9223196");
        public static Guid CdPlayer = new("DFF220E3-F70F-11D0-B917-00A0C9223196");
        public static Guid DatIoDigitalAudioTape = new("DFF220E4-F70F-11D0-B917-00A0C9223196");
        public static Guid DccIoDigitalCompactCassette = new("DFF220E5-F70F-11D0-B917-00A0C9223196");
        public static Guid Minidisk = new("DFF220E6-F70F-11D0-B917-00A0C9223196");
        public static Guid AnalogTape = new("DFF220E7-F70F-11D0-B917-00A0C9223196");
        public static Guid Phonograph = new("DFF220E8-F70F-11D0-B917-00A0C9223196");
        public static Guid VcrAudio = new("DFF220E9-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoDiscAudio = new("DFF220EA-F70F-11D0-B917-00A0C9223196");
        public static Guid DvdAudio = new("DFF220EB-F70F-11D0-B917-00A0C9223196");
        public static Guid TvTunerAudio = new("DFF220EC-F70F-11D0-B917-00A0C9223196");
        public static Guid SatelliteReceiverAudio = new("DFF220ED-F70F-11D0-B917-00A0C9223196");
        public static Guid CableTunerAudio = new("DFF220EE-F70F-11D0-B917-00A0C9223196");
        public static Guid DssAudio = new("DFF220EF-F70F-11D0-B917-00A0C9223196");
        public static Guid RadioReceiver = new("DFF220F0-F70F-11D0-B917-00A0C9223196");
        public static Guid RadioTransmitter = new("DFF220F1-F70F-11D0-B917-00A0C9223196");
        public static Guid MultitrackRecorder = new("DFF220F2-F70F-11D0-B917-00A0C9223196");
        public static Guid Synthesizer = new("DFF220F3-F70F-11D0-B917-00A0C9223196");
        public static Guid HdmiInterface = new("D1B9CC2A-F519-417f-91C9-55FA65481001");
        public static Guid DisplayportInterface = new("E47E4031-3EA6-418d-8F9B-B73843CCBA97");
        public static Guid MidiJack = new("265E0C3F-FA39-4df3-AB04-BE01B91E299A");
        public static Guid MidiElement = new("01C6FE66-6E48-4c65-AC9B-52DB5D656C7E");
        public static Guid Swsynth = new("423274A0-8B81-11D1-A050-0000F8004788");
        public static Guid Swmidi = new("CB9BEFA0-A251-11D1-A050-0000F8004788");
        public static Guid DrmDescramble = new("FFBB6E3F-CCFE-4D84-90D9-421418B03A8E");
        public static Guid Dac = new("507AE360-C554-11D0-8A2B-00A0C9255AC1");
        public static Guid Adc = new("4D837FE0-C555-11D0-8A2B-00A0C9255AC1");
        public static Guid Src = new("9DB7B9E0-C555-11D0-8A2B-00A0C9255AC1");
        public static Guid Supermix = new("E573ADC0-C555-11D0-8A2B-00A0C9255AC1");
        public static Guid Mux = new("2CEAF780-C556-11D0-8A2B-00A0C9255AC1");
        public static Guid Demux = new("C0EB67D4-E807-11D0-958A-00C04FB925D3");
        public static Guid Sum = new("DA441A60-C556-11D0-8A2B-00A0C9255AC1");
        public static Guid Mute = new("02B223C0-C557-11D0-8A2B-00A0C9255AC1");
        public static Guid Volume = new("3A5ACC00-C557-11D0-8A2B-00A0C9255AC1");
        public static Guid Tone = new("7607E580-C557-11D0-8A2B-00A0C9255AC1");
        public static Guid Equalizer = new("9D41B4A0-C557-11D0-8A2B-00A0C9255AC1");
        public static Guid Agc = new("E88C9BA0-C557-11D0-8A2B-00A0C9255AC1");
        public static Guid NoiseSuppress = new("E07F903F-62FD-4e60-8CDD-DEA7236665B5");
        public static Guid Delay = new("144981E0-C558-11D0-8A2B-00A0C9255AC1");
        public static Guid Loudness = new("41887440-C558-11D0-8A2B-00A0C9255AC1");
        public static Guid PrologicDecoder = new("831C2C80-C558-11D0-8A2B-00A0C9255AC1");
        public static Guid StereoWide = new("A9E69800-C558-11D0-8A2B-00A0C9255AC1");
        public static Guid Reverb = new("EF0328E0-C558-11D0-8A2B-00A0C9255AC1");
        public static Guid Chorus = new("20173F20-C559-11D0-8A2B-00A0C9255AC1");
        public static Guid Effects3D = new("55515860-C559-11D0-8A2B-00A0C9255AC1");
        public static Guid ParametricEqualizer = new("19BB3A6A-CE2B-4442-87EC-6727C3CAB477");
        public static Guid UpdownMix = new("B7EDC5CF-7B63-4ee2-A100-29EE2CB6B2DE");
        public static Guid DynRangeCompressor = new("08C8A6A8-601F-4af8-8793-D905FF4CA97D");
        public static Guid DevSpecific = new("941C7AC0-C559-11D0-8A2B-00A0C9255AC1");
        public static Guid PrologicEncoder = new("8074C5B2-3C66-11D2-B45A-3078302C2030");
        public static Guid PeakMeter = new("A085651E-5F0D-4b36-A869-D195D6AB4B9E");
        public static Guid SurroundEncoder = new("8074C5B2-3C66-11D2-B45A-3078302C2030");
        public static Guid VideoStreaming = new("DFF229E1-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoInputTerminal = new("DFF229E2-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoOutputTerminal = new("DFF229E3-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoSelector = new("DFF229E4-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoProcessing = new("DFF229E5-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoCameraTerminal = new("DFF229E6-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoInputMtt = new("DFF229E7-F70F-11D0-B917-00A0C9223196");
        public static Guid VideoOutputMtt = new("DFF229E8-F70F-11D0-B917-00A0C9223196");
    }
}
