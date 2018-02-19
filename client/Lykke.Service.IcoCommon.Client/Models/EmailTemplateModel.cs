// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.IcoCommon.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class EmailTemplateModel
    {
        /// <summary>
        /// Initializes a new instance of the EmailTemplateModel class.
        /// </summary>
        public EmailTemplateModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the EmailTemplateModel class.
        /// </summary>
        public EmailTemplateModel(string campaignId = default(string), string templateId = default(string), string body = default(string), string subject = default(string))
        {
            CampaignId = campaignId;
            TemplateId = templateId;
            Body = body;
            Subject = subject;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "campaignId")]
        public string CampaignId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "templateId")]
        public string TemplateId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

    }
}
