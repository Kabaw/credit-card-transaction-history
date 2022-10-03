using Microsoft.DotNet.PlatformAbstractions;
using PDF_Reader;

string creditCardExtract;
PdfFileReader pdfFileReader = new PdfFileReader();

creditCardExtract = pdfFileReader.ReadPDF(args[0]);

creditCardExtract = CreditCardExtratFormatter.FormatAsCSV(creditCardExtract);

File.WriteAllText(ApplicationEnvironment.ApplicationBasePath + "/ExtratoCartao.csv", creditCardExtract);

Console.WriteLine(creditCardExtract);