using Lykke.AzureStorage.Tables.Entity.Serializers;
using Lykke.Service.IcoCommon.Core.Settings.ServiceSettings;
using Newtonsoft.Json;

namespace Lykke.Service.IcoCommon.AzureRepositories.Campaign
{
    public class SmtpSettingsSerializer : IStorageValueSerializer
    {
        public object Deserialize(string serialized)
        {
            return JsonConvert.DeserializeObject<SmtpSettings>(serialized);
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
