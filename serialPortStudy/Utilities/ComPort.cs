using System;
using System.IO.Ports;

namespace serialPortStudy.Utilities
{
    internal class ComPort
    {
        #region Fields
        private SerialPort _serialPort;
        #endregion

        #region Constructor
        public ComPort(string szPortName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            this._serialPort = new SerialPort
            {
                PortName = szPortName,
                BaudRate = baudRate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = stopBits
            };
        }
        #endregion

        #region Method
        public bool Open()
        {
            try
            {
                if (!this._serialPort.IsOpen)
                {
                    this._serialPort.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void Close()
        {
            try
            {
                if (this._serialPort.IsOpen)
                {
                    this._serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void writeStr(string szData)
        {
            try
            {
                if (this._serialPort.IsOpen)
                {
                    this._serialPort.Write(szData);
                }
                else
                {
                    Console.WriteLine("Port is not open");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string readStr()
        {
            try
            {
                if (this._serialPort.IsOpen)
                {
                    return this._serialPort.ReadExisting();
                }
                else
                {
                    Console.WriteLine("Port is not open");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }
        #endregion

        #region Set or get properties
        public string PortName
        {
            get => _serialPort.PortName;
            set => _serialPort.PortName = value;
        }

        public int BaudRate
        {
            get => _serialPort.BaudRate;
            set => _serialPort.BaudRate = value;
        }

        public Parity Parity
        {
            get => _serialPort.Parity;
            set => _serialPort.Parity = value;
        }

        public int DataBits
        {
            get => _serialPort.DataBits;
            set => _serialPort.DataBits = value;
        }

        public StopBits StopBits
        {
            get => _serialPort.StopBits;
            set => _serialPort.StopBits = value;
        }
        #endregion
    }
}
