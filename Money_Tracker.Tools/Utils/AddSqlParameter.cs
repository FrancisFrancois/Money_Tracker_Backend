using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.Tools.Utils
{
    public static class AddSqlParameter
    {
        public static void addParamWithValue(this DbCommand command, string paramName, Object? paramValue)
        {
            DbParameter param = command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue ?? DBNull.Value;
            command.Parameters.Add(param);
        }
    }
}
