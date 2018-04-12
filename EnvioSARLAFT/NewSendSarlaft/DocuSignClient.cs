using System;
using System.Collections.Generic;
using System.Linq;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;

namespace EnvioSARLAFT.NewSendSarlaft
{
  public class DocuSignClient
  {
    public DocuSignCredentials Credentials { get; set; }

    public DocuSignTemplate Template { get; set; }

    public DocuSignClient()
    {
      Credentials = new DocuSignCredentials();
    }

    public EnvelopeSummary SendDocument(int personType, string clientName, string clientEmail, string brokerName, string brokerEmail, string recipientSubject, string recipientBodyMessage)
    {
      var accountId = Credentials.AccountId;
			Template = new DocuSignTemplate(accountId, personType, clientName, clientEmail, brokerName, brokerEmail);

			var templateId = Template.TemplateId;
			var templateRoles = Template.TemplateRoles;

			// create a new envelope which we will use to send the signature request
			var envelope = new EnvelopeDefinition
			{
				EmailSubject = recipientSubject,
				EmailBlurb = recipientBodyMessage,
				TemplateId = templateId,
				TemplateRoles = templateRoles,
				Status = "sent"
			};

			// |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
			var envelopesApi = new EnvelopesApi();
			return envelopesApi.CreateEnvelope(accountId, envelope);
    }

    public EnvelopeSummary SendDocumentB(int personType, string clientName, string clientEmail, string recipientSubject, string recipientBodyMessage)
    {
      var accountId = Credentials.AccountId;
      Template = new DocuSignTemplate(accountId, personType, clientName, clientEmail, "","");

      var templateId = Template.TemplateId;
      var templateRoles = Template.TemplateRoles;

      var envelopeRecipient = new Recipients()
      {
        Signers = new List<Signer>()
        {
          new Signer()
          {
            ClientUserId = "1",
            Name = "Broker",
            Email = "r_mcardoso@yahoo.com.br",
            RecipientId = "1",
            RoleName = "Broker",
            Tabs = templateRoles.FirstOrDefault().Tabs,
          }
        }
      };

      // create a new envelope which we will use to send the signature request
      var envelope = new EnvelopeDefinition
      {
        EmailSubject = recipientSubject,
        EmailBlurb = recipientBodyMessage,
        Recipients = envelopeRecipient,
        //TemplateId = templateId,
        TemplateRoles = templateRoles,
        Status = "sent"
      };

      // |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
      var envelopesApi = new EnvelopesApi();
      return envelopesApi.CreateEnvelope(accountId, envelope);
    }

    public EnvelopesInformation GetDocumentStatus(IList<string> envelopesIds)
    {
      var accountId = Credentials.AccountId;
      var envelopesApi = new EnvelopesApi();
      var nroDaysToVerifyStatusChanges = System.Configuration.ConfigurationManager.AppSettings["DocuSign_NroDaysToVerifyStatusChanges"];

      int.TryParse(nroDaysToVerifyStatusChanges, out var nroDays);

      EnvelopesApi.ListStatusChangesOptions options = new EnvelopesApi.ListStatusChangesOptions()
      {
        count = "100",
        fromDate = DateTime.UtcNow.AddDays(-nroDays).ToString("o"),
        status = "completed"
      };
      var processeddDocuments = envelopesApi.ListStatusChanges(accountId, options);

      return processeddDocuments;
    }
  }

}