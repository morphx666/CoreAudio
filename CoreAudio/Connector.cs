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
using CoreAudio.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace CoreAudio
{
    public class Connector
    {
        private IConnector _Connector;
        private Part _Part;

        internal Connector(IConnector connector)
        {
            _Connector = connector;
        }

        public ConnectorType GetConnectorType
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.GetType(out ConnectorType type));
                return type;
            }
        }

        public EDataFlow GetDataFlow
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.GetDataFlow(out EDataFlow flow));
                return flow;
            }
        }

        public void ConnecTo(Connector connectTo)
        {
            Marshal.ThrowExceptionForHR(_Connector.ConnectTo((IConnector)connectTo));
        }

        public void Disconnect()
        {
            Marshal.ThrowExceptionForHR(_Connector.Disconnect());
        }

        public bool IsConnected
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.IsConnected(out bool result));
                return result;
            }
        }

        public Connector GetConnectedTo
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.GetConnectedTo(out IConnector connectedTo));
                return new Connector(connectedTo);
            }
        }

        public string GetConnectorIdConnectedTo
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.GetConnectorIdConnectedTo(out string id));
                return id;
            }
        }

        public string GetDeviceIdConnectedTo
        {
            get
            {
                Marshal.ThrowExceptionForHR(_Connector.GetDeviceIdConnectedTo(out string id));
                return id;
            }
        }

        public Part GetPart
        {
            get
            {
                if (_Part == null)
                {
                    IntPtr pUnk = Marshal.GetIUnknownForObject(_Connector);

                    int res = Marshal.QueryInterface(pUnk, ref IIDs.IID_IPart, out IntPtr ppv);
                    if (ppv != IntPtr.Zero)
                        _Part = new Part((IPart)Marshal.GetObjectForIUnknown(ppv));
                    else
                        _Part = null;
                }
                return _Part;
            }
        }
    }
}
