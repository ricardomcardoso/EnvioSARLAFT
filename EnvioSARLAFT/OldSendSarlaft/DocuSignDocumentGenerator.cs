using System.Collections.Generic;
using System.Linq;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;

namespace EnvioSARLAFT.OldSendSarlaft
{
	public class DocuSignDocumentGenerator
	{
		public DocuSignDocumentGenerator(DocuSignCredentials credentials, EmailTemplate emailTemplate,
			DocuSignTemplate docuSignTemplate)
		{
			this.DocuSignCredentials = credentials;
			this.EmailTemplate = emailTemplate;
			this.DocuSignTemplate = docuSignTemplate;
		}

		public DocuSignCredentials DocuSignCredentials { get; set; }

		public EmailTemplate EmailTemplate { get; set; }

		public DocuSignTemplate DocuSignTemplate { get; set; }

		public EnvelopeSummary GenerateDocument(string name, string email)
		{
			var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
			var accountId = docuSignClient.AccountId;

			//get TAB's (requiredFields) from document
			TemplatesApi templateInstanceApi = new TemplatesApi();
			EnvelopeTemplate templateBase = templateInstanceApi.Get(accountId, this.DocuSignTemplate.TemplateId);

			Tabs requiredFields = new Tabs();
			if (templateBase != null && templateBase.Recipients != null && templateBase.Recipients.Signers.Any())
			{
				requiredFields = templateBase.Recipients.Signers.FirstOrDefault().Tabs;
			}

			// assign recipient to template role by setting name, email, and role name.  Note that the
			// template role name must match the placeholder role name saved in your account template.  
			List<TemplateRole> templateRoles = this.DocuSignTemplate.TemplateRoleNames.Select(m => new TemplateRole
			{
				Email = email,
				Name = name,
				RoleName = m,
				Tabs = requiredFields
			}).ToList();

			// create a new envelope which we will use to send the signature request
			EnvelopeDefinition envelope = new EnvelopeDefinition
			{
				EmailSubject = this.EmailTemplate.Subject,
				EmailBlurb = this.EmailTemplate.MessageBody,
				TemplateId = this.DocuSignTemplate.TemplateId,
				TemplateRoles = templateRoles,
				Status = "sent"
			};

			// |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
			var envelopesApi = new EnvelopesApi();

			return envelopesApi.CreateEnvelope(accountId, envelope);
		}
	}
}
