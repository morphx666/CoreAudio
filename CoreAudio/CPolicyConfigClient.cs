using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    [ComImport, Guid("870af99c-171d-4f9e-af0d-e63df40c2bc9")]
    internal class _CPolicyConfigClient {
    }

    public class CPolicyConfigClient {
        IPolicyConfig _policyConfigClient = (IPolicyConfig)new _CPolicyConfigClient();

        public int SetDefaultDevice(string deviceID) {
            _policyConfigClient.SetDefaultEndpoint(deviceID, ERole.eConsole);
            _policyConfigClient.SetDefaultEndpoint(deviceID, ERole.eMultimedia);
            _policyConfigClient.SetDefaultEndpoint(deviceID, ERole.eCommunications);

            return 0;
        }
    }
}
