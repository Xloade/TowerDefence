using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence_SharedContent.Soldiers
{
    public class SoldierConverter : JsonConverter
    {
        public override bool CanConvert(Type typeToConvert)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
