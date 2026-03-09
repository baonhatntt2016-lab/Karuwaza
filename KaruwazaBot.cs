using Microsoft.Web.WebView2.WinForms;
using System.Threading.Tasks;

public class KaruwazaBot
{
    private WebView2 web;

    public KaruwazaBot(WebView2 webView)
    {
        web = webView;
    }

    public async Task RunQuery()
    {
        await web.ExecuteScriptAsync("fSubmit(11,true);");
    }

    public async Task<string> GetTable()
    {
        return await web.ExecuteScriptAsync(@"
            document.querySelector('table').outerHTML
        ");
    }
}
