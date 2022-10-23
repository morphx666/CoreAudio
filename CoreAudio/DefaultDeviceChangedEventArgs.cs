namespace CoreAudio {
    public class DefaultDeviceChangedEventArgs : DeviceNotificationEventArgs {
        public DataFlow DataFlow { get; private set; }
        public Role Role { get; private set; }

        public DefaultDeviceChangedEventArgs(string deviceId, DataFlow dataFlow, Role role) : base(deviceId) {
            DataFlow = dataFlow;
            Role = role;
        }
    }
}
