using CoreAudio.Interfaces.Undocumented;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace CoreAudio.Undocumented {
    public class AudioPolicyConfigFactory {
        public static IAudioPolicyConfigFactory Create() {

            int osBuildNumber = int.Parse(RuntimeInformation.OSDescription.Split('.').Last());

            if(osBuildNumber >= 21390) { // Windows 10 21H2
                return new AudioPolicyConfigFactoryVariantFor21H2();
            } else {
                return new AudioPolicyConfigFactoryVariantForDownlevel();
            }
        }
    }

    public static class Combase {
        [DllImport("combase.dll", PreserveSig = false)]
        public static extern void RoGetActivationFactory(
            [MarshalAs(UnmanagedType.HString)] string activatableClassId,
            [In] ref Guid iid,
            [Out, MarshalAs(UnmanagedType.IInspectable)] out Object factory);

        [DllImport("combase.dll", PreserveSig = false)]
        public static extern void WindowsCreateString(
            [MarshalAs(UnmanagedType.LPWStr)] string src,
            [In] uint length,
            [Out] out IntPtr hstring);
    }
}