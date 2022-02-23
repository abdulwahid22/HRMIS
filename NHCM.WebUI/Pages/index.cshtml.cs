using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NHCM.Domain.Entities;
using NHCM.WebUI.Types;
using MediatR;
using NHCM.Application.Lookup.Queries;
using System.Threading.Tasks;

namespace NHCM.WebUI.Pages
{
    public class indexModel : BasePage
    {

        public string modultemplate = @"<div class='dash-header'>
                                       <h4 style = 'font-weight:bold;' >$module</h4>
                                       </div>";

        public string rowtemplate = @"<div class='row' style='margin-top:5px;'>$cols</div>";

        public string htmltemplate = @"
                                    <div class='col-md-3 pull-right'>
                                        <a href='$link?p=$id'>
                                            <img src='assets/icons/$icon' />
                                            <h5>$title</h5>
                                            <span>$des</span>
                                        </a>
                                    </div>
                                    ";

        public string HTML { get; set; } = "";
        List<Screens> slist = new List<Screens>();

        public async Task OnGetAsync([FromBody] GetScreens command)
        {
           
            try
            {
                List<Module> Modules = await Mediator.Send(new GetModuleQuery() { });
                string row = "";
                
                foreach (Module m in Modules)
                {
                    row = row + modultemplate.Replace("$module", m.Name);
                    string screens = await LoadscreensAsync(m.Id);
                    row = row + screens;
                }
                HTML = HTML + row;

            }
            catch (Exception ex)
            {

            }
        }

        private async Task<string> LoadscreensAsync(int ModuleID)
        {
            List<Screens> Screens = await Mediator.Send(new GetScreens() { ModuleID = ModuleID });
            int rownumber = (Screens.Count % 4) == 0 ? Screens.Count / 4 : (Screens.Count / 4) + 1;
            string row = "";
            List<Screens> templist = Screens;
            for (int i = 1; i <= rownumber; i++)
            {
                string cols = "";
                for (int j = 0; j <= 3; j++)
                {
                    if (Screens.Count > 0)
                    {
                        Screens s = templist[0];
                        String screenid = EncryptionHelper.Encrypt(s.Id.ToString());
                        cols = cols + htmltemplate.Replace("$id", screenid).Replace("$title", s.Title).Replace("$icon", s.Icon).Replace("$des", s.Description).Replace("$link", s.Path);
                        templist.RemoveAt(0);
                    }
                    // cols = cols + htmltemplate;
                }
                row = row + rowtemplate.Replace("$cols", cols);
            }
            return row;
        }
    }
}