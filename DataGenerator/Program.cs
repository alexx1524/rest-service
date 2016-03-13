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
            const ulong count = 1000000;
            ulong processed = 0;

            ulong i = 0;
            var ot = new DataTable("objects");
            ot.Columns.Add(new DataColumn("Oid",  typeof (long)));
            ot.Columns.Add(new DataColumn("Hid",  typeof (SqlHierarchyId)));
            ot.Columns.Add(new DataColumn("Type", typeof (string)));

            ulong ai = 1;
            var at = new DataTable("attributes");
            at.Columns.Add(new DataColumn("Id",   typeof(long)));
            at.Columns.Add(new DataColumn("Oid", typeof(long)));
            //Type
            at.Columns.Add(new DataColumn("Type",    typeof(ushort)));
            at.Columns.Add(new DataColumn("Name",    typeof(string)));
            
            //Values
            at.Columns.Add(new DataColumn("FValue",  typeof(float)));
            at.Columns.Add(new DataColumn("SValue",  typeof(string)));
            at.Columns.Add(new DataColumn("DTValue", typeof(DateTime)));

            ulong dti = 1;
            var dtt = new DataTable("dtvalues");
            dtt.Columns.Add(new DataColumn("Id", typeof(long)));
            dtt.Columns.Add(new DataColumn("AttrId", typeof(long)));
            dtt.Columns.Add(new DataColumn("Value", typeof(DateTime)));

            ulong fi = 1;
            var ft = new DataTable("fvalues");
            ft.Columns.Add(new DataColumn("Id", typeof(long)));
            ft.Columns.Add(new DataColumn("AttrId", typeof(long)));
            ft.Columns.Add(new DataColumn("Value", typeof(float)));

            ulong si = 1;
            var st = new DataTable("svalues");
            st.Columns.Add(new DataColumn("Id", typeof(long)));
            st.Columns.Add(new DataColumn("AttrId", typeof(long)));
            st.Columns.Add(new DataColumn("Value", typeof(string)));


            ulong j = 0;

            while (i < count)
            {
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                j++;

                AddObject(ot, i, j, $"/{j}/", $"Помещение"); 
                AddStringAttribute(at,   ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at,    ref ai, "Плошадь", i, ft, ref fi, 12);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 23));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/", $"Помещение");
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 45);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/2/", $"Насос"); 
                AddStringAttribute(at, ref ai, "Марка", i, st, ref si, "МПТ-125");
                AddFloatAttribute(at, ref ai, "Масса", i, ft, ref fi, 25.5f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 08, 23));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/3/", $"Счетчик");
                AddStringAttribute(at, ref ai, "Марка", i, st, ref si, "ЭЦРЗ-125");
                AddStringAttribute(at, ref ai, "Единицы", i, st, ref si, "л");
                AddFloatAttribute(at, ref ai, "Масса", i, ft, ref fi, 0.2f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 06, 23));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/", $"Помещение"); 
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 4);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/1/", $"Насос"); 
                AddStringAttribute(at, ref ai, "Марка", i, st, ref si, "МТ-125");
                AddFloatAttribute(at, ref ai, "Масса", i, ft, ref fi, 31.5f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 10, 23));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/2/", $"Счетчик"); 
                AddStringAttribute(at, ref ai, "Марка", i, st, ref si, "ЭЦРЗ-125");
                AddStringAttribute(at, ref ai, "Единицы", i, st, ref si, "л");
                AddFloatAttribute(at, ref ai, "Масса", i, ft, ref fi, 0.2f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 11, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/", $"Помещение"); 
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 20);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/", $"Помещение");
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 14);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/",           $"Помещение"); 
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 18);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++; 
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/", $"Помещение"); 
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 13);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/1/", $"Помещение");
                AddStringAttribute(at, ref ai, "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai, "Плошадь", i, ft, ref fi, 4);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2015, 6, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/1/1/",     $"Помещение");
                AddStringAttribute(at, ref ai,  "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai,    "Плошадь", i, ft, ref fi, 28);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 10, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/1/1/1/",   $"Помещение"); 
                AddStringAttribute(at, ref ai,   "Наименование", i, st, ref si, "Помещение" + i);
                AddFloatAttribute(at, ref ai,    "Плошадь", i, ft, ref fi, 14);
                AddDateTimeAttribute(at, ref ai, "Дата постройки", i, dtt, ref dti, new DateTime(2016, 02, 10));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/1/1/1/1/", $"Насос"); 
                AddStringAttribute(at, ref ai,   "Марка", i, st, ref si, "МТ-125");
                AddFloatAttribute(at, ref ai,    "Масса", i, ft, ref fi, 31.5f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 10, 23));
                i++;
                if (i % 100000 == 0) WriteData(ref processed, count, ot, at, ft, st, dtt);

                AddObject(ot, i, j, $"/{j}/1/1/3/1/1/1/1/1/1/1/", $"Счетчик");
                AddStringAttribute(at, ref ai,   "Марка", i, st, ref si, "ЭЦРЗ-125");
                AddStringAttribute(at, ref ai,   "Единицы", i, st, ref si, "л");
                AddFloatAttribute(at, ref ai,    "Масса", i, ft, ref fi, 0.2f);
                AddDateTimeAttribute(at, ref ai, "Дата установки", i, dtt, ref dti, new DateTime(2015, 09, 10));
            }

            WriteData(ref processed, count, ot, at, ft, st, dtt);

            Console.WriteLine("Данные успешно загружены");
            Console.Read();
        }
        
        public static void BatchInsert(SqlConnection conn, DataTable dataTable, string tableName, int batchSize)
        {
            var blk = new SqlBulkCopy(conn) {DestinationTableName = tableName};
            blk.WriteToServer(dataTable);
            blk.Close();
        }

        public static void WriteData(ref ulong processed, ulong count, DataTable ot, DataTable at, DataTable ft, DataTable st, DataTable dtt)
        {
            const string connectionString =
                "Data Source=DESKTOP-VMBEBP6;Initial Catalog=restdb;Integrated Security=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                BatchInsert(connection, ot, "objects", int.MaxValue);
                BatchInsert(connection, at, "attrs", int.MaxValue);
                //BatchInsert(connection, at, "attributes", int.MaxValue);
                //BatchInsert(connection, ft, "fvalues", int.MaxValue);
                //BatchInsert(connection, st, "svalues", int.MaxValue);
                //BatchInsert(connection, dtt, "dtvalues", int.MaxValue);

                connection.Close();

                processed += Convert.ToUInt64(ot.Rows.Count);

                ot.Clear();
                at.Clear();
                ft.Clear();
                st.Clear();
                dtt.Clear();
            }
            
            Console.WriteLine($"Processed {Math.Round(Convert.ToDouble(processed*100)/count)}%");
        }

        public static void AddObject(DataTable ot, ulong i, ulong j, string hid, string type)
        {
            var row = ot.NewRow(); 
            row["Oid"] = i; 
            row["Hid"] = SqlHierarchyId.Parse(hid); 
            row["Type"] = type;

            ot.Rows.Add(row);
        }

        public static void AddFloatAttribute(DataTable at, ref ulong aid, string name, ulong oid, DataTable ft, ref ulong vid, float value)
        {
            var row = at.NewRow();
            row["Id"]   = aid;
            row["Type"] = 1;
            row["Name"] = name;
            row["Oid"]  = oid;
            row["FValue"]  = value;
            row["SValue"]  = DBNull.Value;
            row["DTValue"] = DBNull.Value; 
            at.Rows.Add(row);

            /*row = ft.NewRow();
            row["Id"]     = vid;
            row["AttrId"] = aid;
            row["Value"]  = value;
            ft.Rows.Add(row);
            vid++;*/

            aid++;
        }

        public static void AddStringAttribute(DataTable at, ref ulong aid, string name, ulong oid, DataTable st, ref ulong vid, string value)
        {
            var row = at.NewRow();
            row["Id"] = aid;
            row["Type"] = 0;
            row["Name"] = name;
            row["Oid"] = oid;
            row["FValue"] = DBNull.Value; 
            row["SValue"] = value;
            row["DTValue"] = DBNull.Value;
            at.Rows.Add(row);


            /*row = st.NewRow();
            row["Id"] = vid;
            row["AttrId"] = aid;
            row["Value"] = value;
            st.Rows.Add(row);
            vid++;*/

            aid++;
        }

        public static void AddDateTimeAttribute(DataTable at, ref ulong aid, string name, ulong oid, DataTable dtt, ref ulong vid, DateTime value)
        {
            var row = at.NewRow();
            row["Id"] = aid;
            row["Type"] = 2;
            row["Name"] = name;
            row["Oid"] = oid;
            row["FValue"] = DBNull.Value;
            row["SValue"] = DBNull.Value;
            row["DTValue"] = value; 
            at.Rows.Add(row);

            /*row = dtt.NewRow();
            row["Id"]     = vid;
            row["AttrId"] = aid;
            row["Value"]  = value;
            dtt.Rows.Add(row);
            vid++;*/

            aid++;
        }
    }
}
