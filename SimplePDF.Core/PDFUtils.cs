using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout.Element;
using iText.Kernel.Geom;
using iText.Layout;

namespace SimplePDF.Core;

public class PDFUtils
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="access"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static PdfDocument CreatPDFDocument(string filePath, FileAccess access)
    {
        // check filepath absolute or relative
        var fullPath = System.IO.Path.GetFullPath(filePath);
        
        Guard.Against.FileNotExist(fullPath, nameof(fullPath));

        // TODO: add pdfDocumentProperties
        return access switch
        {
            FileAccess.Read => new PdfDocument(new PdfReader(fullPath)),

            FileAccess.Write => new PdfDocument(new PdfWriter(fullPath)),
            FileAccess.ReadWrite => new PdfDocument(new PdfReader(fullPath), new PdfWriter(fullPath)),
            _ => throw new InvalidOperationException($"FileAccess: [{access}] is invalid")
        };
    }

    public static void Merge(PdfDocument from, PdfDocument to, int FromStartPage = 1, int FromEndPage = 1)
    {
        var merger = new PdfMerger(to);
        merger.Merge(from, FromStartPage, FromEndPage);
        merger.Close(); // The resultant pdf doc will be closed implicitly.
    }

    private static void ConvertImg2PDF(Image image, PdfDocument dest)
    {
        var pageSize = new PageSize(image.GetImageWidth(), image.GetImageHeight());

        using var doc = new Document(dest, pageSize);
        dest.AddNewPage(pageSize);
        image.SetFixedPosition(pageNumber: 1, left: 0, bottom: 0);
        doc.Add(image);
    }
}


