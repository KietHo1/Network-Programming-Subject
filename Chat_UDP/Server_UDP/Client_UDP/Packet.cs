using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_UDP_P
{
    public enum DataIdentifier
    {
        Message,
        LogIn,
        LogOut,
        Null
    }

    public class Packet
    {
        #region Private Members
        private DataIdentifier dataIdentifier;
        private string name;
        private string message;
        #endregion

        #region Public Properties
        public DataIdentifier ChatDataIdentifier
        {
            get { return dataIdentifier; }
            set { dataIdentifier = value; }
        }

        public string ChatName
        {
            get { return name; }
            set { name = value; }
        }

        public string ChatMessage
        {
            get { return message; }
            set { message = value; }
        }
        #endregion

        #region Methods

        public Packet()
        {
            this.dataIdentifier = DataIdentifier.Null;
            this.message = null;
            this.name = null;
        }

        public Packet(byte[] dataStream)
        {
            this.dataIdentifier = (DataIdentifier)BitConverter.ToInt32(dataStream, 0);
            int nameLength = BitConverter.ToInt32(dataStream, 4);
            int msgLength = BitConverter.ToInt32(dataStream, 8);

            if (nameLength > 0)
                this.name = Encoding.UTF8.GetString(dataStream, 12, nameLength);
            else
                this.name = null;

            if (msgLength > 0)
                this.message = Encoding.UTF8.GetString(dataStream, 12 + nameLength, msgLength);
            else
                this.message = null;
        }

        public byte[] GetDataStream()
        {
            List<byte> dataStream = new List<byte>();

            dataStream.AddRange(BitConverter.GetBytes((int)this.dataIdentifier));

            if (this.name != null)
                dataStream.AddRange(BitConverter.GetBytes(this.name.Length));
            else
                dataStream.AddRange(BitConverter.GetBytes(0));

            if (this.message != null)
                dataStream.AddRange(BitConverter.GetBytes(this.message.Length));
            else
                dataStream.AddRange(BitConverter.GetBytes(0));

            if (this.name != null)
                dataStream.AddRange(Encoding.UTF8.GetBytes(this.name));

            if (this.message != null)
                dataStream.AddRange(Encoding.UTF8.GetBytes(this.message));

            return dataStream.ToArray();
        }

        #endregion
    }
}
