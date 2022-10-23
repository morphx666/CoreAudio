using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    [ComImport, Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
    internal class _CPolicyConfigClient {
    }

    public class CPolicyConfigClient {
        IPolicyConfig policyConfigClient = (IPolicyConfig)new _CPolicyConfigClient();

        public int SetDefaultDevice(string deviceID) {
            policyConfigClient.SetDefaultEndpoint(deviceID, Role.Console);
            policyConfigClient.SetDefaultEndpoint(deviceID, Role.Multimedia);
            policyConfigClient.SetDefaultEndpoint(deviceID, Role.Communications);

            return 0;
        }
    }
}
