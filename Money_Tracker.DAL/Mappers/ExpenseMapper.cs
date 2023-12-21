using Money_Tracker.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Mappers
{
    // Classe ExpenseMapper : Contient une méthode pour mapper des enregistrements de base de données vers des objets 
    public class ExpenseMapper
    {
        // Méthode pour mapper un enregistrement de base de données (IDataRecord) vers un objet Expense.
        public static Expense Mapper(IDataRecord record)
        {
            // Crée et renvoie un nouvel objet Expense avec les données extraites de l'enregistrement IDataRecord.
            return new Expense
            {
                // Extraction et affectation de l'identifiant de la dépense.
                Id = (int)record["Expense_Id"],

                // Extraction et affectation de l'identifiant de la catégorie associée à la dépense.
                Category_Id = (int)record["Category_Id"],

                // Extraction et affectation de l'identifiant de l'utilisateur associé à la dépense.
                User_Id = (int)record["User_Id"],

                // Extraction et affectation de l'identifiant du domicile associé à la dépense.
                Home_Id = (int)record["Home_Id"],

                // Extraction et affectation du montant de la dépense.
                Amount = (double)record["Amount"],

                // Extraction et affectation de la description de la dépense.
                Description = (string)record["Description"],

                // Extraction et affectation de la date de la dépense.
                Date_Expense = (DateTime)record["Date_Expense"]
            };
        }
    }
}
