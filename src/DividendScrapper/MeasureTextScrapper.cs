using HtmlAgilityPack;

namespace DividendScrapper
{
    internal class MeasureTextScrapper
    {
        private readonly string paramName;

        public MeasureTextScrapper(string paramName)
        {
            this.paramName = paramName;
        }

        public string GetScrappedText(HtmlDocument htmlDoc)
        {
            return htmlDoc.DocumentNode.SelectSingleNode($"//table/tr/td[text()='{paramName}']/following-sibling::td[1]").InnerText;
        }
    }
}
