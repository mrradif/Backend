using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO.Common
{
    public class NotFoundEntityDto<TId, TDtoResult>
    {
        public TId Id { get; set; }
        public TDtoResult Entity { get; set; }
    }

}
