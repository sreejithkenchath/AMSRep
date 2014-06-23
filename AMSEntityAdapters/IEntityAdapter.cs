using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace AMSEntityAdapters 
{
    interface IEntityAdapter<T,S> 
    {
        T ConvertDTOtoEntity(S s);
        S ConvertEntitytoDTO(T t);
    }
}
