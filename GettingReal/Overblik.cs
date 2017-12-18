using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using GTO_PladsOversigt;

namespace GettingReal
{
    class Overblik
    {
        private static string connectionsString =
            "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public void SpShowKnubmerList()
        {
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {

                    kNumberDB.Open();
                    SqlCommand overblik = new SqlCommand("spuOverblikOverKnumre", kNumberDB);
                    overblik.CommandType = CommandType.StoredProcedure;

                    SqlDataReader visKnummer = overblik.ExecuteReader();

                    if (visKnummer.HasRows)
                    {
                        while (visKnummer.Read())
                        {
                            string kNummer = visKnummer["KNUMMER"].ToString();
                            string kNummer_i_Brug = visKnummer["KNUMMER_I_BRUG"].ToString();
                            string medarbejder_Navn = visKnummer["MEDARBEJDER_NAVN"].ToString();
                            Console.WriteLine(kNummer + " " + kNummer_i_Brug + " " + medarbejder_Navn);
                        }

                    }
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                }
            }
        }
        public List<DTOPladsOverblik> SpuSeatingList() //Frederik
        {
            List<DTOPladsOverblik> liste = new List<DTOPladsOverblik>();
            using(SqlConnection seatListDB = new SqlConnection(connectionsString))
            {
                try
                {
                    seatListDB.Open();
                    SqlCommand overblik = new SqlCommand("spuPladsOverblik",seatListDB);
                    overblik.CommandType = CommandType.StoredProcedure;

                    SqlDataReader showSeating = overblik.ExecuteReader();

                    if(showSeating.HasRows)
                    {

                        while(showSeating.Read())
                        {
                            string kNummer = showSeating["KNUMMER"].ToString();
                            string kNummer_i_Brug = showSeating["KNUMMER_I_BRUG"].ToString();
                            string plads = showSeating["PLADS"].ToString();
                            string plads_i_Brug = showSeating["PLADS_I_BRUG"].ToString();
                            string medarbejder_Navn = showSeating["MEDARBEJDER_NAVN"].ToString();

                            DTOPladsOverblik dto = new DTOPladsOverblik();
                            dto.kNummer = kNummer;
                            dto.kNummer_i_Brug = kNummer_i_Brug;
                            dto.medarbejder_Navn = medarbejder_Navn;
                            dto.plads = plads;
                            dto.plads_i_Brug = plads_i_Brug;

                            //tilføerj database felter fra DTOPladsOverblik i listen.
                            liste.Add(dto);
                        }
                    }
                }
                catch(SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                }
                return liste;
            }
        }
    }
}
