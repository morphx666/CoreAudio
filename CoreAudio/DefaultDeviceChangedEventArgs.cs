namespace CoreAudio {
    public class DefaultDeviceChangedEventArgs : DeviceNotificationEventArgs {
        public EDataFlow DataFlow { get; private set; }
        public ERole Role { get; private set; }

        public DefaultDeviceChangedEventArgs(string deviceId, EDataFlow dataFlow, ERole role) : base(deviceId) {
            DataFlow = dataFlow;
            Role = role;
        }
    }
}
