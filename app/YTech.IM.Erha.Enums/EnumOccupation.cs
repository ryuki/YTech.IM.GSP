using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YTech.IM.GSP.Enums
{
    public enum EnumOccupation
    {
        [StringValue("Pegawai Swasta")]
        Pegawai_Swasta,
        [StringValue("Pegawai Negeri")]
        Pegawai_Negeri,
        [StringValue("Ibu Rumah Tangga")]
        IRT,
        [StringValue("Wiraswasta")]
        Wiraswasta,
        [StringValue("Pelajar")]
        Pelajar,
        [StringValue("Profesional")]
        Professional,
        [StringValue("Lainnya")]
        Others
    }
}
