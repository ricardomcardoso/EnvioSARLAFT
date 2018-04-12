using System.Collections.Generic;
using System.Threading;
using EnvioSARLAFT.NewSendSarlaft;

namespace EnvioSARLAFT
{
  class Program
  {
    public static void Main(string[] args)
    {
      NewSendSarlaft();

      //OldSendSarlaft();
    }

    private static void NewSendSarlaft()
    {
      var docuSignClient = new EnvioSARLAFT.NewSendSarlaft.DocuSignClient();
      var envelopeCreated = docuSignClient.SendDocument(2, "Ricardo FH", "ricardo.cardoso@fh.com.br", "Broker", "r_mcardoso@yahoo.com.br", "Olá Ricardo FH, Hay una pendiente para aceptar su solicitud número {1}!", "Sarlaft Inválido, por favor verifique y firmar la propuesta.");
      //var envelopeCreated = docuSignClient.SendDocumentB(1, "Ricardo FH", "ricardo.cardoso@fh.com.br", "Olá Ricardo FH, Hay una pendiente para aceptar su solicitud número {1}!", "Sarlaft Inválido, por favor verifique y firmar la propuesta.");


      //Thread newThread = new Thread(() => getStatus(docuSignCredentials));
      //newThread.Start();
    }

    //private static void OldSendSarlaft()
    //{
    //  var emailTemplate =
    //    new EmailTemplate("TESTES CAMPOS OBRIGATÓRIOS 666", "OLÁ Ricardo FH, VC TEM UMA PENDÊNCIA AQUI CONOSCO!");

    //  var docuSignTemplate = new DocuSignTemplate("4a8cb66c-6610-4cb9-994f-d23414ac31f4",
    //    new List<string> { "testes campos obrigatórios" });

    //  var docuSignCredentials =
    //    new DocuSignCredentials("r_mcardoso@yahoo.com.br", "fh123456", "f0c7884a-11c9-4987-a11f-e52e870b1662");

    //  var documentGenerator = new DocuSignDocumentGenerator(docuSignCredentials, emailTemplate, docuSignTemplate);

    //  var envelopeCreated = documentGenerator.GenerateDocument("Ricardo FH", "ricardo.cardoso@fh.com.br");


    //  Thread newThread = new Thread(() => getStatus(docuSignCredentials));
    //  newThread.Start();
    //}

    //private static void getStatus(DocuSignCredentials docuSignCredentials)
    //{
    //  var documentStatus = new DocuSignGetDocumentStatus(docuSignCredentials);

    //  var statusList = documentStatus.GetDocumentStatus(new List<string>());
    //}
  }
}
