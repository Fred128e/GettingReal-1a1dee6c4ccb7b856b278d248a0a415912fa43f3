using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GettingReal
{
    class Tildeling
    {

        private static string connectionsString =
        "Server=EALSQL1.eal.local; Database = DB2017_C03; User Id = user_C03; PassWord=SesamLukOp_03;";

        public string spuGivRNDKnummerOgLås(int Medarbejder_ID)
        {
            string knummer = "";
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))
            {
                try
                {
                    kNumberDB.Open();
                    SqlCommand tildeling = new SqlCommand("spuGivFørsteKNummer", kNumberDB);
                    tildeling.CommandType = CommandType.StoredProcedure;

                    tildeling.Parameters.Add(new SqlParameter("@MEDARBEJDER_ID", Medarbejder_ID));

                    SqlDataReader givKnummerIkkeIBrug = tildeling.ExecuteReader();

                    string kNummer = givKnummerIkkeIBrug["KNUMMER"].ToString(); ;
                    Console.WriteLine("Dit K-nummer for i dag er: " + kNummer);

                    knummer = givKnummerIkkeIBrug["@KNUMMER"].ToString();
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                }
            }
            return knummer;
        }
        public int spuØnskKNummer(string knummer, int medarbejder_ID)
        {
            int knummerOptaget = 0;
            using (SqlConnection kNumberDB = new SqlConnection(connectionsString))

            {
                try
                { 
                    kNumberDB.Open();
                    SqlCommand wishKNumber = new SqlCommand("spWishKNumber", kNumberDB);
                    wishKNumber.CommandType = CommandType.StoredProcedure;
                    wishKNumber.Parameters.Add(new SqlParameter("@MEDARBEJDER_ID_1", medarbejder_ID));
                    wishKNumber.Parameters.Add(new SqlParameter("@KNUMMER_1",knummer));

                    SqlDataReader receivedMedarbejder_IDAndKnummer = wishKNumber.ExecuteReader();
                    while (receivedMedarbejder_IDAndKnummer.Read())
                    {
                        medarbejder_ID = Convert.ToInt32(receivedMedarbejder_IDAndKnummer.GetString(0));
                        knummer = receivedMedarbejder_IDAndKnummer.GetString(1);
                        knummerOptaget = Convert.ToInt32(receivedMedarbejder_IDAndKnummer["@KNumberOccupied"]);
                    }
                    return knummerOptaget;
                }
                catch (SqlException error)
                {
                    Console.WriteLine("Fejl: " + error.Message);
                    return 0;
                }
                  
            }
        }
    }
}
