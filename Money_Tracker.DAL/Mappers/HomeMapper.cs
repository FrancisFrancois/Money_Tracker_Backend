using Money_Tracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Mappers
{
    // Classe HomeMapper : Utilisée pour convertir les données d'enregistrement de base de données (IDataRecord) en objets Home.
    public class HomeMapper
    {
        // Méthode pour mapper un enregistrement de base de données (IDataRecord) vers un objet Home.
        public static Home Mapper(IDataRecord record)
        {
            // Crée et renvoie un nouvel objet Home avec les données extraites de l'enregistrement IDataRecord.
            return new Home
            {
                // Extraction et affectation de l'identifiant du domicile.
                Id = (int)record["Home_Id"],

                // Extraction et affectation de l'identifiant de l'utilisateur principal associé au domicile.
                User_Id = (int)record["User_Id"],

                // Extraction et affectation du nom du domicile.
                Name_Home = (string)record["Name_Home"]
            };
        }
    }
}
