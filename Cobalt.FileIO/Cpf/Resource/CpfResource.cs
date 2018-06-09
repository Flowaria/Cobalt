using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.FileIO.Cpf.Resource
{
    public enum CpfResourceType
    {
        IconZip = 1,
        Template = 2,
        MapConfig = 3
    }

    public class CpfResource
    {
        protected CpfResourceType _type;

        public string FileName { get; set; }
        public byte[] Raw { get; set; }
        public byte[] Data
        {
            get
            {
                if (Raw != null && Raw.Length > 0)
                {
                    using (var os = new MemoryStream())
                    {
                        using (var ms = new MemoryStream(Raw))
                        {
                            using (var ds = new DeflateStream(ms, CompressionMode.Decompress))
                            {
                                ds.CopyTo(os);
                            }
                        }
                        return os.ToArray();
                    }
                }
                throw new ArgumentOutOfRangeException("data is empty");
            }

            set
            {
                if (value != null && value.Length > 0)
                {
                    using (var os = new MemoryStream())
                    {
                        using (var ms = new MemoryStream(value))
                        {
                            using (var ds = new DeflateStream(ms, CompressionMode.Compress))
                            {
                                ds.CopyTo(os);
                            }
                        }
                        Raw = os.ToArray();
                    }
                }
                throw new ArgumentOutOfRangeException("byte array cannot be null or length 0");
            }
        }
        public int Length
        {
            get
            {
                return Raw.Length;
            }
        }


        //type:<byte> - length 1
		//filename:<null terminate string (utf-8)> - length unlimited

        //resource_length:<uint>  - length 4
		//resource:<deflate compressed raw> - legnth=resource_length

        protected CpfResource()
        {
            
        }

        public virtual bool ToFile(string directory)
        {
            try
            {
                using (var fs = new FileStream(Path.Combine(directory, FileName), FileMode.Create))
                {
                    var buffer = Data;
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
