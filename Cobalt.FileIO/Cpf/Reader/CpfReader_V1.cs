using Cobalt.FileIO.Cpf.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.FileIO.Cpf.Reader
{
    //v1 Reader
    public partial class CpfReader
    {
        private Cpf ReadV1()
        {
            var rescount = ReadUShort();
            for(int i = 0 ; i<rescount ; i++)
            {
                var type = ReadByte();
                var filename = ReadString();
                var length = ReadInt();
                var raw = ReadBytes(length, false);

                if (length > 0)
                {
                    switch (type)
                    {
                        case (int)CpfResourceType.IconZip:
                            var ires = new CpfResourceIconZip();
                            ires.FileName = filename;
                            ires.Raw = raw;
                            break;
                        case (int)CpfResourceType.Template:
                            var tres = new CpfResourceTemplate();
                            tres.FileName = filename;
                            tres.Raw = raw;
                            break;
                        case (int)CpfResourceType.MapConfig:
                            var mres = new CpfResourceMapConfig();
                            mres.FileName = filename;
                            mres.Raw = raw;
                            break;
                        default:
                            throw new ArgumentException("Not Valid Resource File");
                    }
                }
                else
                {
                    throw new ArgumentException("Not Valid Resource File");
                }
            }
            return new Cpf();
        }
    }
}
