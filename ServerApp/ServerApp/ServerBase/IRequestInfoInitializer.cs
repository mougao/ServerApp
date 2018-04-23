using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp
{
    public interface IRequestInfoInitializer
    {
        void Initialize(string key, ArraySegment<byte> data, IRequestEntity entity);
    }
}
