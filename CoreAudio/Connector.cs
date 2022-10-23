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

using System;
using System.Runtime.InteropServices;
using CoreAudio.Interfaces;

namespace CoreAudio {
    public class Connector {
        IConnector connector;
        Part? part;

        internal Connector(IConnector connector) {
            this.connector = connector;
        }

        public ConnectorType ConnectorType {
            get {
                Marshal.ThrowExceptionForHR(connector.GetType(out var type));
                return type;
            }
        }

        public DataFlow DataFlow {
            get {
                Marshal.ThrowExceptionForHR(connector.GetDataFlow(out var flow));
                return flow;
            }
        }

        public void ConnectTo(Connector connectTo) {
            Marshal.ThrowExceptionForHR(connector.ConnectTo((IConnector)connectTo));
        }

        public void Disconnect() {
            Marshal.ThrowExceptionForHR(connector.Disconnect());
        }

        public bool IsConnected {
            get {
                Marshal.ThrowExceptionForHR(connector.IsConnected(out var result));
                return result;
            }
        }

        public Connector ConnectedTo {
            get {
                Marshal.ThrowExceptionForHR(connector.GetConnectedTo(out IConnector connectedTo));
                return new Connector(connectedTo);
            }
        }

        public string ConnectorIdConnectedTo {
            get {
                Marshal.ThrowExceptionForHR(connector.GetConnectorIdConnectedTo(out string id));
                return id;
            }
        }

        public string DeviceIdConnectedTo {
            get {
                Marshal.ThrowExceptionForHR(connector.GetDeviceIdConnectedTo(out string id));
                return id;
            }
        }

        public Part? Part {
            get {
                if(part == null) {
                    var pUnk = Marshal.GetIUnknownForObject(connector);

                    var res = Marshal.QueryInterface(pUnk, ref RefIId.IIdIPart, out var ppv);
                    if(ppv != IntPtr.Zero)
                        part = new Part((IPart)Marshal.GetObjectForIUnknown(ppv));
                    else
                        part = null;
                }
                return part;
            }
        }
    }
}
