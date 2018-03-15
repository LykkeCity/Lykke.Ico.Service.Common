using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.IcoCommon.Models.Mail
{
    public class EmailTemplateAddOrUpdateRequest
    {
        [Required]
        public EmailTemplateModel EmailTemplate { get; set; }

        [Required]
        public string Username { get; set; }
    }
}
