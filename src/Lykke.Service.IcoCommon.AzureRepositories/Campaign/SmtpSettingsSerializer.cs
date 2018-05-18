using System;
using Lykke.AzureStorage.Tables.Entity.Serializers;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Newtonsoft.Json;

namespace Lykke.Service.IcoCommon.AzureRepositories.Campaign
{
    public class SmtpSettingsSerializer : IStorageValueSerializer
    {
        public object Deserialize(string serialized)
        {
            return Deserialize(serialized, typeof(SmtpSettings));
        }

        public object Deserialize(string serialized, Type type)
        {
            return JsonConvert.DeserializeObject(serialized, type);
        }

        public string Serialize(object value)
        {
            return Serialize(value, typeof(SmtpSettings));
        }

        public string Serialize(object value, Type type)
        {
            return JsonConvert.SerializeObject(value, type, null);
        }
    }
}
