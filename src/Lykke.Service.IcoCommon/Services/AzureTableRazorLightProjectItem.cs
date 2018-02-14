using System.IO;
using System.Text;
using Lykke.Service.IcoCommon.Core.Domain.Mail;
using RazorLight.Razor;

namespace Lykke.Service.IcoCommon.Services
{
    public class AzureTableRazorLightProjectItem : RazorLightProjectItem
    {
        private IEmailTemplate _template;

        public AzureTableRazorLightProjectItem(IEmailTemplate template)
        {
            _template = template ?? throw new System.ArgumentNullException(nameof(template));
        }

        public override string Key { get => _template.TemplateId; }
        public override bool Exists { get => true; }

        public override Stream Read()
        {
           return new MemoryStream(Encoding.UTF8.GetBytes(_template.Body));
        }
    }
}
