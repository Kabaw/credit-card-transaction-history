using Microsoft.DotNet.PlatformAbstractions;
using PDF_Reader;

string creditCardExtract;
PdfFileReader pdfFileReader = new PdfFileReader();

if(args.Length == 0)
    creditCardExtract = pdfFileReader.ReadPDF("C:/Users/wagco/Downloads/Nubank_2022-09-12.pdf");
else
    creditCardExtract = pdfFileReader.ReadPDF(args[0]);

creditCardExtract = CreditCardExtratFormatter.FormatAsCSV(creditCardExtract);

File.WriteAllText(ApplicationEnvironment.ApplicationBasePath + "/ExtratoCartao.csv", creditCardExtract);

Console.WriteLine(creditCardExtract);