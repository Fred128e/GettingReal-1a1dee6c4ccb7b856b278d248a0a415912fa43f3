using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTO_PladsOversigt
{
    /// <summary>
    /// DTO: Data Transfer Object Overblik
    /// bruges til at skubbe data mellem data og UI'en
    /// </summary>
    public class DTOPladsOverblik
    {
        public string kNummer { get; set; }
        public string kNummer_i_Brug { get; set; }
        public string plads { get; set; }
        public string plads_i_Brug { get; set; }
        public string medarbejder_Navn { get; set; }

    }
}