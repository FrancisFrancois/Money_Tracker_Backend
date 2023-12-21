using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Money_Tracker.Tools.Utils
{
    // Classe statique contenant une méthode d'extension pour ajouter des paramètres à une commande de base de données.
    public static class AddSqlParameter
    {
        // Méthode d'extension pour ajouter un paramètre avec sa valeur à une commande de base de données.
        // - command : La commande de base de données à laquelle ajouter le paramètre.
        // - paramName : Le nom du paramètre.
        // - paramValue : La valeur du paramètre.
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
