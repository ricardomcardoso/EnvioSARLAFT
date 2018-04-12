using System;
using System.Collections.Generic;
using System.Linq;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;

namespace EnvioSARLAFT.NewSendSarlaft
{
  public class DocuSignTemplate
  {
    public List<TemplateRole> TemplateRoles { get; set; }

    public string TemplateId { get; set; }

    public DocuSignTemplate(string accountId, int personType, string clientName, string clientEmail, string brokerName, string brokerEmail)
    {
      TemplateId = GetDocuSignTemplateId(personType);
      TemplateRoles = GetDocuSignTemplateRoles(accountId, clientName, clientEmail, brokerName, brokerEmail);
    }

    private string GetDocuSignTemplateId(int personType)
    {
      string templateId;
      if (personType != 1)
      {
        templateId = System.Configuration.ConfigurationManager.AppSettings["DocuSign_TemplatePersonJuridica"];
      }
      else
      {
        templateId = System.Configuration.ConfigurationManager.AppSettings["DocuSign_TemplatePersonNatural"];
      }

      if (string.IsNullOrWhiteSpace(templateId))
        throw new ArgumentException("No se pudo recuperar la plantilla solicitada");

      return templateId;
    }

    private List<TemplateRole> GetDocuSignTemplateRoles(string accountId, string clientName, string clientEmail, string brokerName, string brokerEmail)
    {
      //get TAB's (requiredFields) from document
      var templateInstanceApi = new TemplatesApi();
      EnvelopeTemplate templateBase = templateInstanceApi.Get(accountId, TemplateId);

      List<TemplateRole> templateRoles = new List<TemplateRole>();
      if (templateBase != null && templateBase.Recipients != null && templateBase.Recipients.Signers.Any())
      {
        foreach (var signer in templateBase.Recipients.Signers)
        {
          if (signer.RoleName.Contains("Broker"))
          {
            templateRoles.Add(new TemplateRole
            {
              Name = brokerName,
              Email = brokerEmail,
              RoleName = "Broker",
              Tabs = signer.Tabs,
              RoutingOrder = "99"
            });
          }
          else
          {
            templateRoles.Add(new TemplateRole
            {
              Name = clientName,
              Email = clientEmail,
              RoleName = "Cliente",
              Tabs = signer.Tabs,
              RoutingOrder = "1"
            });
          }
        }
      }

      return templateRoles;
    }
  }
}
