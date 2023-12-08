using Money_Tracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Mappers
{
    /// <summary>
    /// Classe utilisée pour mapper les données d'enregistrement (IDataRecord) vers l'objet Home.
    /// </summary>
    public class HomeMapper
    {
        /// <summary>
        /// Mappe les données de l'enregistrement vers un objet Home.
        /// </summary>
        /// <param name="record">L'enregistrement de données à mapper.</param>
        /// <returns>Un objet Home avec les données mappées.</returns>
        public static Home Mapper(IDataRecord record)
        {
            // Crée un nouvel objet Home en récupérant les valeurs de l'enregistrement IDataRecord.
            return new Home
            {
                Id = (int)record["Home_Id"],
                User_Id = (int)record["User_Id"],
                Name_Home = (string)record["Name_Home"]
            };
        }
    }
}