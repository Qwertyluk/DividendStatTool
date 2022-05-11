﻿using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class MeasureTextScrapper
    {
        private readonly HtmlDocument htmlDoc;
        private readonly string paramName;

        public MeasureTextScrapper(HtmlDocument htmlDoc, string paramName)
        {
            this.htmlDoc = htmlDoc;
            this.paramName = paramName;
        }

        public string GetScrappedText()
        {
            return htmlDoc.DocumentNode.SelectSingleNode($"//table/tr/td[text()='{paramName}']/following-sibling::td[1]").InnerText;
        }
    }
}
