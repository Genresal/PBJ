using System.Reflection;
using PBJ.NotificationService.Domain.Abstract;
using PBJ.NotificationService.Domain.Constants;
using PBJ.NotificationService.Domain.Dtos;
using RazorLight;

namespace PBJ.NotificationService.Application.Generators
{
    public class MailBodyGenerator : IMailBodyGenerator
    {
        private readonly IRazorLightEngine _razorEngine;

        public MailBodyGenerator(IRazorLightEngine razorEngine)
        {
            _razorEngine = razorEngine;

            ReadDefaultTemplates();
        }

        public async Task<string> GenerateBodyAsync(MailTemplateDto mailTemplate)
        {
            try
            {
                if (!_razorEngine.Handler.Cache.RetrieveTemplate(mailTemplate.TemplateKey).Success)
                {
                    _ = CompileTemplate(mailTemplate);
                }

                var cacheResult = _razorEngine.Handler.Cache.RetrieveTemplate(Templates.BaseTemplate);

                return cacheResult.Success
                    ? await _razorEngine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), mailTemplate)
                    : await CompileTemplate(Templates.BaseTemplate, Templates.BaseTemplateFile, mailTemplate);
            }
            catch (Exception exception)
            {
                throw new Exception($@"Failed to create Email message body for ""{mailTemplate.TemplateFile}""", exception);
            }
        }

        private async Task<string> CompileTemplate<TModel>(string templateKey, string resourceName, TModel model)
        {
            var assembly = Assembly.GetEntryAssembly();

            var resourceFullName = $"{assembly!.GetName().Name}.Templates.{resourceName}";

            var resourceStream = assembly.GetManifestResourceStream(resourceFullName);

            if (resourceStream == null)
            {
                throw new Exception($@"Cannot find ""{resourceFullName}"" in the embedded resources.");
            }

            using var reader = new StreamReader(resourceStream);

            var template = reader.ReadToEnd();

            return await _razorEngine.CompileRenderStringAsync(templateKey, template, model);
        }

        private void ReadDefaultTemplates()
        {
            CompileTemplate(Templates.HeadTemplate, Templates.HeadTemplateFile, new { }).Wait();
        }

        private async Task<string> CompileTemplate(MailTemplateDto mailTemplateDto)
            => await CompileTemplate(mailTemplateDto.TemplateKey, mailTemplateDto.TemplateFile, mailTemplateDto);
    }
}
