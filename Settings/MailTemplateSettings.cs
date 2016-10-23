using System.Linq;
using System.Reflection;

namespace DatacircleAPI.Settings
{
    public class MailTemplateSettings
    {
        public BaseMailTemplate accountRegistration { get; set; }
        public BaseMailTemplate accountVerification { get; set; }
        public BaseMailTemplate invitation { get; set; }

        public object GetTemplateByName(string propertyName)
        {
            return this.GetType().GetProperties()
                .Single(pi => pi.Name == propertyName)
                .GetValue(this, null);
        }
    }

    public class BaseMailTemplate 
    {
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
