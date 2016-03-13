using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestService.Models
{
    public enum AttributeType { String, Float, DateTime}

    public abstract class ClassItem
    {
        [JsonProperty(PropertyName = "Идентификатор", Order = 1)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "Класс", Order = 2)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Атрибуты", Order = 3)]
        public List<Attribute> Attributes = new List<Attribute>();

        protected ClassItem(long id, string name)
        {
            Id = id;
            Name = name; 
        }
    }

    public class Attribute
    {
        [JsonProperty(PropertyName = "Имя")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "Тип")]
        public AttributeType Type { get; set; }

        [JsonProperty(PropertyName = "Значение")]
        public object Value { get; set; }

        public Attribute(string name, AttributeType type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    public class ObjectItem : ClassItem
    {
        public ObjectItem(long id, string name) : base(id, name)
        {
        }
    }

    public class Pump : ClassItem
    {
        public Pump(long id, string mark, float? weight, DateTime? installationDate) : base(id, "Насос")
        {
            Attributes.Add(new Attribute("Марка", AttributeType.String, mark));
            Attributes.Add(new Attribute("Масса", AttributeType.Float, weight));
            Attributes.Add(new Attribute("Дата установки", AttributeType.DateTime, installationDate));
        }
    }
    
    public class ObjectSubitems
    {
        public class ObjectInfo
        {
            [JsonProperty(PropertyName = "Идентикикатор")]
            public long Id { get; set; }

            [JsonProperty(PropertyName = "Класс")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "Уровень")]
            public short Level { get; set; }

            public ObjectInfo(long id, string name, short level)
            {
                Id = id;
                Name = name;
                Level = level;
            }
        }

        [JsonProperty(PropertyName = "Объекты")]
        public List<ObjectInfo> Items = new List<ObjectInfo>(); 
    }
}
