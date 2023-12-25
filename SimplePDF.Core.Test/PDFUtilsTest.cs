using iText.Kernel.Pdf;

namespace SimplePDF.Core.Test;

public class PDFUtilsTest
{
    [Fact]
    public void MergeIsOk()
    {
        // arrange
        var fromFilePath = string.Empty;
        using var from = new PdfDocument(new PdfWriter(fromFilePath));
        var fromNumberOfPages = from.GetNumberOfPages();
        var toFilePath = string.Empty;
        using var to = new PdfDocument(new PdfWriter(toFilePath));
        // act
        PDFUtils.Merge(from, to, FromStartPage: 1, FromEndPage: 1);

        Assert.Equal(fromNumberOfPages + 1 - 1 + 1, to.GetNumberOfPages());
    }
}