using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestService.Models
{
    public enum AttributeType { String, Float, DateTime}

    [DataContract]
    public abstract class ClassItem
    {
        [DataMember]
        [JsonProperty(PropertyName = "Идентификатор", Order = 1)]
        public ulong Id { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "Класс", Order = 2)]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "Атрибуты", Order = 3)]
        public List<Attribute> Attributes = new List<Attribute>();

        protected ClassItem(ulong id, string name)
        {
            Id = id;
            Name = name; 
        }
    }

    [DataContract]
    public class Attribute
    {
        [DataMember]
        [JsonProperty(PropertyName = "Имя")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "Тип")]
        public AttributeType Type { get; set; }

        [DataMember]
        [JsonProperty(PropertyName = "Значение")]
        public object Value { get; set; }

        public Attribute(string name, AttributeType type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    [DataContract]
    public class ObjectItem : ClassItem
    {
        public ObjectItem(ulong id, string name) : base(id, name)
        {
        }
    }

    public class Pump : ClassItem
    {
        public Pump(ulong id, string mark, float? weight, DateTime? installationDate) : base(id, "Насос")
        {
            Attributes.Add(new Attribute("Марка", AttributeType.String, mark));
            Attributes.Add(new Attribute("Масса", AttributeType.Float, weight));
            Attributes.Add(new Attribute("Дата установки", AttributeType.DateTime, installationDate));
        }
    }
}
