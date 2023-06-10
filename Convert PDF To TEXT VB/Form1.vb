Imports System.Text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser

Public Class Form1

    Public Function ConvertPdfToText(ByVal pdfPath As String) As String
        ' Read the PDF file
        Dim reader As New PdfReader(pdfPath)
        Dim RichTextBoxPDF As New RichTextBox
        ' Initialize a StringBuilder to store the extracted text
        Dim textBuilder As New StringBuilder()

        ' Iterate through each page of the PDF
        For page As Integer = 1 To reader.NumberOfPages
            ' Extract the text from the current page
            Dim strategy As New SimpleTextExtractionStrategy()
            Dim currentPageText As String = PdfTextExtractor.GetTextFromPage(reader, page, strategy)
            currentPageText = currentPageText.Replace(vbNullChar, "")
            Dim lines As String() = currentPageText.Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)
            For Each chunk As String In lines
                RichTextBoxPDF.AppendText(chunk & Environment.NewLine)
            Next
            'textBuilder.AppendLine(extractedText.ToString())
            RichTextBoxPDF.AppendText("-------------------------------------------- Page No. :" & page & "-------------------------------------------------------" & vbNewLine)
            ' Append the extracted text to the StringBuilder
            'textBuilder.AppendLine(currentPageText)
        Next

        ' Close the PDF reader
        reader.Close()

        ' Return the extracted text as a string
        Return RichTextBoxPDF.Text
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub BrowsePDF_Click(sender As Object, e As EventArgs) Handles BrowsePDF.Click
        Dim loadFile As New OpenFileDialog()
        loadFile.Filter = "PDF Files (*.pdf)|*.pdf"
        loadFile.Title = "Select a PDF File"
        TXT_PDF.Text = ""

        Try
            If loadFile.ShowDialog() = DialogResult.OK Then
                Dim selectedFilePath As String = loadFile.FileName
                RichTextBox1.Text = ConvertPdfToText(selectedFilePath)
                TXT_PDF.Text = selectedFilePath
            End If
        Catch ex As Exception

        End Try


    End Sub
End Class
