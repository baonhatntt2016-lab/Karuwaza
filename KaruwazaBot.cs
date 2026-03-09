using Microsoft.Web.WebView2.WinForms;
using System.Threading.Tasks;

namespace KaruwazaDriver
{
    public class KaruwazaBot
    {
        private WebView2 web;
        private bool _isStarted = false;

        public KaruwazaBot(WebView2 webView)
        {
            web = webView;
        }

        public async Task Run()
        {
            if (_isStarted) return;
            _isStarted = true;

            var url = web.Source.ToString();

            if (url.Contains("kwzweb"))
            {
                await WaitForElement("#txtLoginUserid");

                await web.ExecuteScriptAsync(@"
                    document.getElementById('txtLoginUserid').value = 'tnqthai@organvn.com.vn';
                    document.getElementById('txtLoginPassword').value = 'tnqthai0439';
                    document.forms[0].submit();
                ");
            }

            await WaitForElement("#pJDbTitle");

            await web.ExecuteScriptAsync(@"
                let ddl = document.getElementById('pJDbTitle');
                ddl.value = '14';
                ddl.dispatchEvent(new Event('change', { bubbles: true }));
            ");

            await WaitForElement("body");

            await web.ExecuteScriptAsync("fJyokenNew();");

            await Task.Delay(3000);

            await web.ExecuteScriptAsync("fTableSelect(1014);");

            await Task.Delay(3000);

            await web.ExecuteScriptAsync("fSubmit(11,true);");

            await Task.Delay(3000);
        }

        private async Task WaitForElement(string selector)
        {
            while (true)
            {
                var result = await web.ExecuteScriptAsync($@"
                    document.querySelector('{selector}') != null
                ");

                if (result.Contains("true"))
                    return;

                await Task.Delay(500);
            }
        }
    }
}
