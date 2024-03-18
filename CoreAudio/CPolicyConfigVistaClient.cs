﻿using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    [ComImport, Guid("294935CE-F637-4E7C-A41B-AB255460B862")]
    internal class CCPolicyConfigVistaClient {
    }

    public class CPolicyConfigVistaClient {
        IPolicyConfigVista policyConfigVistaClient = (IPolicyConfigVista)new CCPolicyConfigVistaClient();

        public int SetDefaultDevice(string deviceID) {
            policyConfigVistaClient.SetDefaultEndpoint(deviceID, Role.Console);
            policyConfigVistaClient.SetDefaultEndpoint(deviceID, Role.Multimedia);
            policyConfigVistaClient.SetDefaultEndpoint(deviceID, Role.Communications);

            return 0;
        }
    }
}
