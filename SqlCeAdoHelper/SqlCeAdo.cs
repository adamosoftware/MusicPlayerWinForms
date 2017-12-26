using System.Data;
using System.Data.SqlServerCe;

namespace SqlCeAdoHelper
{
    public static class SqlCeAdo
    {
        public static DataTable QueryDataTable(this SqlCeConnection connection, string query, object parameters)
        {
            using (var cmd = new SqlCeCommand(query, connection))
            {
                SetParameters(parameters, cmd);
                using (var adapter = new SqlCeDataAdapter(cmd))
                {
                    DataTable tbl = new DataTable();
                    adapter.Fill(tbl);
                    return tbl;
                }
            }
        }

        private static void SetParameters(object parameters, SqlCeCommand cmd)
        {
            var props = parameters.GetType().GetProperties();
            foreach (var pi in props)
            {
                cmd.Parameters.AddWithValue(pi.Name, pi.GetValue(parameters));
            }
        }
    }
}