using System;
using System.Collections.Generic;
using System.Linq;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;

namespace EnvioSARLAFT.OldSendSarlaft
{
	public class DocuSignGetDocumentStatus
	{
		public DocuSignCredentials DocuSignCredentials { get; set; }

		public DocuSignGetDocumentStatus(DocuSignCredentials credentials)
		{
			this.DocuSignCredentials = credentials;
		}

		public EnvelopesInformation GetDocumentStatus(IList<string> envelopesIds)
		{
			var docuSignClient = new DocuSignClient(this.DocuSignCredentials);
			var accountId = docuSignClient.AccountId;

			// |EnvelopesApi| contains methods related to creating and sending Envelopes (aka signature requests)
			var envelopesApi = new EnvelopesApi();

			// This example gets statuses of all envelopes in your account going back 1 full month...
			//DateTime fromDate = DateTime.UtcNow;
			//fromDate = fromDate.AddDays(-30);
			//string fromDateStr = fromDate.ToString("o");

			//// set a filter for the envelopes we want returned using the fromDate and count properties
			//EnvelopesApi.ListStatusChangesOptions options = new EnvelopesApi.ListStatusChangesOptions()
			//{
			//	count = "50",
			//	fromDate = fromDateStr,
			//	status = "completed"
			//};

			//// |EnvelopesApi| contains methods related to envelopes and envelope recipients
			//EnvelopesInformation envelopes = envelopesApi.ListStatusChanges(accountId, options);

			//EnvelopesApi.ListStatusOptions options2 = new EnvelopesApi.ListStatusOptions()
			//{
			//	fromDate = fromDateStr,
			//};

			return envelopesApi.ListStatus(accountId, new EnvelopeIdsRequest(envelopesIds.ToList()),
				new EnvelopesApi.ListStatusOptions() { fromDate = DateTime.UtcNow.AddHours(-1).ToString("o") });
		}
	}
}
