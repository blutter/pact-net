namespace Provider.Api.Web.DotnetCore.Models
{
    public class HypermediaLink
    {
        public string Href { get; set; }

        public HypermediaLink(string href)
        {
            Href = href;
        }
    }
}