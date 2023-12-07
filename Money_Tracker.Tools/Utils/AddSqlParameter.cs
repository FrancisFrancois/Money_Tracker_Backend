using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.Tools.Utils
{
    /// <summary>
    /// Classe statique contenant une méthode d'extension pour ajouter des paramètres à une commande de base de données.
    /// </summary>
    public static class AddSqlParameter
    {
        /// <summary>
        /// Ajoute un paramètre avec sa valeur à une commande de base de données.
        /// </summary>
        /// <param name="command">La commande de base de données à laquelle ajouter le paramètre.</param>
        /// <param name="paramName">Le nom du paramètre.</param>
        /// <param name="paramValue">La valeur du paramètre.</param>
        public static void addParamWithValue(this DbCommand command, string paramName, Object? paramValue)
        {
            // Crée un nouveau paramètre de base de données
            DbParameter param = command.CreateParameter();

            // Définit le nom du paramètre
            param.ParameterName = paramName;

            // Définit la valeur du paramètre, ou DBNull.Value si la valeur est null
            param.Value = paramValue ?? DBNull.Value;

            // Ajoute le paramètre à la liste des paramètres de la commande
            command.Parameters.Add(param);
        }
    }
}