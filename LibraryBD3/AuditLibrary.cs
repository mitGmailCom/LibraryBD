//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryBD3
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuditLibrary
    {
        public int Id { get; set; }
        public int IdPerson { get; set; }
        public int IdBook { get; set; }
        public System.DateTime DateIssuance { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Person Person { get; set; }
    }
}
