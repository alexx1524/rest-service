using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Types;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = new DataTable("myTable");
            dt.Columns.Add(new DataColumn("Oid", typeof(long)));
            dt.Columns.Add(new DataColumn("Hid", typeof(SqlHierarchyId)));
            dt.Columns.Add(new DataColumn("Type", typeof (string)));
            
            ulong i = 0;
            ulong j = 0;

            while(i < 10000000)
            {
                i++;
                j++;

                //*********************
                var row = dt.NewRow();row["Oid"]  = i;row["Hid"]  = SqlHierarchyId.Parse($"/{j}/");row["Type"] = $"Помещение";

                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/2/");row["Type"] = $"Насос";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/3/");row["Type"] = $"Счетчик";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/1/");row["Type"] = $"Насос";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow(); row["Oid"] = i; row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/2/"); row["Type"] = $"Счетчик"; 
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow(); row["Oid"] = i; row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/"); row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/1/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/1/1/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/1/1/1/1/");row["Type"] = $"Помещение";
                dt.Rows.Add(row);
                i++;

                //*********************
                row = dt.NewRow();row["Oid"] = i;row["Hid"] = SqlHierarchyId.Parse($"/{j}/1/1/3/1/1/1/1/1/1/");row["Type"] = $"Помещение";

                dt.Rows.Add(row);
            }
                
            BatchInsert(dt, int.MaxValue);

            Console.Read();
        }

        public static void BatchInsert(DataTable dataTable, int batchSize)
        {
            const string connectionString = "Data Source=agusta;Initial Catalog=restdb;Persist Security Info=True;User ID=sa;Password=uhb,jxrb3gbdf";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var blk = new SqlBulkCopy(connection) { DestinationTableName = "objects" };
                blk.WriteToServer(dataTable);
                blk.Close();
            }
        }
    }
}
