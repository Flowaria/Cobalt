using Cobalt.FileIO.Cpf.Resource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.FileIO.Cpf.Reader
{
    public partial class CpfReader
    {
        private bool IsLittleEndian;
        private FileStream Stream;

        public Cpf Cpf { get; private set; }

        public CpfReader(string filename)
        {
            if(File.Exists(filename))
            {
                try
                {
                    Stream = File.OpenRead(filename);
                    if(ReadByte() == 203 && ReadByte() == 254) //CB FE
                    {
                        
                        switch(ReadUShort())
                        {
                            case 1:
                                Cpf = ReadV1();
                            break;
                                
                            default:
                                throw new ArgumentException("Not Cobalt Project file (invalid version)");
                        }
                    }
                    throw new ArgumentException("Not Cobalt Project file (header wrong)");
                }
                catch
                {
                    Stream.Dispose();
                }
                finally
                {

                }
            }
            throw new FileNotFoundException();
        }

        private string ReadString()
        {
            using (var ms = new MemoryStream())
            {
                int bt;
                while ((bt = ReadByte()) != 0)
                {
                    ms.WriteByte((byte)bt);
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        private int ReadByte()
        {
            return Stream.ReadByte();
        }

        private int ReadInt()
        {
            var buffer = ReadBytes(4);
            return BitConverter.ToInt32(buffer, 0);
        }

        private ushort ReadUShort()
        {
            var buffer = ReadBytes(2);
            return BitConverter.ToUInt16(buffer,0);
        }

        private byte[] ReadBytes(int count, bool endian_associate=true)
        {
            if(Stream.Position+count <= Stream.Length)
            {
                byte[] buffer = new byte[count];
                Stream.Read(buffer, 0, count);
                if (endian_associate && BitConverter.IsLittleEndian != IsLittleEndian)
                    Array.Reverse(buffer);

                return buffer;
            }
            throw new ArgumentNullException("Not Avaliable");
        }
    }
}
