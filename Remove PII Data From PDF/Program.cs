//*******************************************************************************************//
//                                                                                           //
// Download Free Evaluation Version From: https://bytescout.com/download/web-installer       //
//                                                                                           //
// Also available as Web API! Get Your Free API Key: https://app.pdf.co/signup               //
//                                                                                           //
// Copyright © 2017-2020 ByteScout, Inc. All rights reserved.                                //
// https://www.bytescout.com                                                                 //
// https://pdf.co                                                                            //
//                                                                                           //
//*******************************************************************************************//


using System;
using System.Diagnostics;
using System.Drawing;
using Bytescout.PDFExtractor;

namespace RemovePIIDataFromPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Bytescout.PDFExtractor.Remover instance
            Remover remover = new Remover("demo", "demo");

            // Load sample PDF document
            remover.LoadDocumentFromFile(@"samplePDF_SSNNo.pdf");

            // Prepare TextExtractor
            using (TextExtractor textExtractor = new TextExtractor("demo", "demo"))
            {
                // Load document into TextExtractor
                textExtractor.LoadDocumentFromFile(@"samplePDF_SSNNo.pdf");

                // Search SSN in format 202-55-0130
                // See the complete regular expressions reference at https://msdn.microsoft.com/en-us/library/az24scfc(v=vs.110).aspx
                string regexPattern = "[0-9]{3}-[0-9]{2}-[0-9]{4}";

                // Enable RegexSearch
                textExtractor.RegexSearch = true;

                // Set word matching options
                textExtractor.WordMatchingMode = WordMatchingMode.None;

                ISearchResult[] searchResults = textExtractor.FindAll(0, regexPattern, caseSensitive: false);

                // Remove text objects find by SearchResults.
                // NOTE: The removed text might be larger than the specified rectangle. Currently the Remover is unable 
                // to split PDF text objects.
                remover.RemoveText(searchResults, @"result1.pdf");
            }

            // Open output file in default application
            ProcessStartInfo processStartInfo = new ProcessStartInfo("result1.pdf");
            processStartInfo.UseShellExecute = true;
            Process.Start(processStartInfo);

            // Clean up.
            remover.Dispose();
        }
    }
}
