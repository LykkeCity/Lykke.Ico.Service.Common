// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Lykke.Service.IcoCommon.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class DeletePayInAddressRequest
    {
        /// <summary>
        /// Initializes a new instance of the DeletePayInAddressRequest class.
        /// </summary>
        public DeletePayInAddressRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DeletePayInAddressRequest class.
        /// </summary>
        /// <param name="currency">Possible values include: 'BTC', 'ETH',
        /// 'USD'</param>
        public DeletePayInAddressRequest(CurrencyType currency, string address = default(string))
        {
            Address = address;
            Currency = currency;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'BTC', 'ETH', 'USD'
        /// </summary>
        [JsonProperty(PropertyName = "Currency")]
        public CurrencyType Currency { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
        }
    }
}