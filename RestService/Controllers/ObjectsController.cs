using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using RestService.Models;
using WebGrease.Css.Extensions;
using Attribute = RestService.Models.Attribute;

namespace RestService.Controllers
{
    public class ObjectsController : ApiController
    {
        const string ConnString = "Data Source = localhost; Initial Catalog = restdb; User Id = sa; Password = pwd";

        //Получение объекта с атрибутами
        // GET: api/Objects/5
        public ClassItem Get(long id)
        {
            using (var connection = new SqlConnection(ConnString))
            {
                connection.Open();

                using (var command = new SqlCommand($"select * from [dbo].[GetObjectWithAttributes]({id})", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        ClassItem item = null;

                        reader.Cast<IDataRecord>().ForEach(rec =>
                        {
                            if (item == null)
                                item = new ObjectItem(Convert.ToInt64(rec["oid"].ToString()), rec["type"].ToString());

                            var atype =
                                (AttributeType)
                                    Enum.ToObject(typeof (AttributeType), Convert.ToInt16(rec["atype"].ToString()));

                            object value = null;

                            switch (atype)
                            {
                                case AttributeType.String:   value = rec["svalue"].ToString();  break;
                                case AttributeType.Float:    value = Convert.ToDouble(rec["fvalue"].ToString()); break;
                                case AttributeType.DateTime: value = DateTime.Parse(rec["dtvalue"].ToString()).ToString("yyyyMMdd HH:mm:ss");  break;
                            }

                            item.Attributes.Add(new Attribute(rec["name"].ToString(), atype, value));
                        });

                        return item;
                    }
                }
            }
        }

        //Добавление нового объекта, поле "Идентификатор" обозначает родителя
        // POST: api/Objects
        public ulong Post([FromBody] ObjectItem item)
        {
            if (item == null)
                return 0;

            var attrs = new DataTable("Attrs");
            attrs.Columns.Add("type", typeof(long));
            attrs.Columns.Add("name", typeof(string));
            attrs.Columns.Add("svalue", typeof(string));
            attrs.Columns.Add("fvalue", typeof(float));
            attrs.Columns.Add("dtvalue", typeof(DateTime));

            item.Attributes.ForEach(it =>
            {
                var row = attrs.NewRow();
                row["type"]    = (int)it.Type;
                row["name"]    = it.Name;
                row["svalue"]  = it.Type == AttributeType.String ?   it.Value : DBNull.Value;
                row["fvalue"]  = it.Type == AttributeType.Float ?    it.Value : DBNull.Value;
                row["dtvalue"] = it.Type == AttributeType.DateTime ? it.Value : DBNull.Value;
                attrs.Rows.Add(row);
            });

            using (var connection = new SqlConnection(ConnString))
            {
                connection.Open(); 
                
                using (var command = new SqlCommand("AddObjectWithAttributes", connection) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.AddWithValue("parentid", item.Id);
                    command.Parameters.AddWithValue("type_name", item.Name);

                    var tvpParam = command.Parameters.AddWithValue("@attrs", attrs);
                    tvpParam.SqlDbType = SqlDbType.Structured;
                    tvpParam.TypeName = "dbo.Attrs";

                    var result = command.ExecuteScalar();

                    return Convert.ToUInt64(result);
                }
            }
        }

        //Получение поддерева объектов по указанному
        [Route("api/Objects/Subitems/{id}")]
        [HttpGet]
        public ObjectSubitems.ObjectInfo[] GetObgectSubitems(long id)
        {
            using (var connection = new SqlConnection(ConnString))
            {
                connection.Open();

                using (var command = new SqlCommand($"select id, [type], [level] from [dbo].[GetObjectSubitems]({id}) ORDER BY [level]", connection))
                {
                    var oitems = new List<ObjectSubitems.ObjectInfo>();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Cast<IDataRecord>().ForEach(rec =>
                        {
                            oitems.Add(new ObjectSubitems.ObjectInfo(Convert.ToInt64(rec["id"].ToString()), 
                                                                      rec["type"].ToString(), 
                                                                      Convert.ToInt16(rec["level"].ToString())));
                        });
                    }

                    return oitems.ToArray();
                }
            }
        }

        //Получение списка насосов в поддереве объектов
        [Route("api/Objects/Pumps/{id}")]
        [HttpGet]
        public long[] GetPumps(long id)
        {
            using (var connection = new SqlConnection(ConnString))
            {
                connection.Open();

                using (var command = new SqlCommand($"select oid from [dbo].[GetPumps]({id})", connection))
                {
                    var ids = new List<long>();

                    using (var reader = command.ExecuteReader())
                    {
                        reader.Cast<IDataRecord>().ForEach(rec => { ids.Add(Convert.ToInt64(rec["oid"].ToString())); });
                    }

                    return ids.ToArray();
                }
            }
        }
    }
}
