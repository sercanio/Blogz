public static class HtmlHelpers
{
    public static string TruncateHtml(string input, int maxLength)
    {
        if (string.IsNullOrEmpty(input) || maxLength <= 0)
            return string.Empty;

        var doc = new HtmlAgilityPack.HtmlDocument();
        doc.LoadHtml(input);
        return TruncateNode(doc.DocumentNode, maxLength);
    }

    private static string TruncateNode(HtmlAgilityPack.HtmlNode node, int maxLength)
    {
        if (node == null || maxLength <= 0)
            return string.Empty;

        if (node.NodeType == HtmlAgilityPack.HtmlNodeType.Text)
        {
            return node.InnerText.Length > maxLength ? node.InnerText.Substring(0, maxLength) + "..." : node.InnerText;
        }

        string truncated = string.Empty;

        foreach (var child in node.ChildNodes)
        {
            var childTruncated = TruncateNode(child, maxLength - truncated.Length);

            truncated += childTruncated;

            if (truncated.Length >= maxLength)
            {
                break;
            }
        }

        return truncated;
    }
}
