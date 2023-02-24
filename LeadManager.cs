using Academia2023.Controller;
using Academia2023.Model;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Dynacoop2023.AlfaPeople.MyFirstPlugins
{
    public class LeadManager : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity lead = (Entity)context.InputParameters["Target"];
            ExecuteLead(context, lead);
        }

        private void ExecuteLead(IPluginExecutionContext context, Entity lead)
        {

            if (context.MessageName == "Update")
            {
                Entity leadPostImage = (Entity)context.PostEntityImages["PostImage"];
                Guid leadId = lead.Id;
                UpdateLead(context, leadPostImage, leadId);
            }

        }

        private void UpdateLead(IPluginExecutionContext context, Entity leadPostImage, Guid leadId)
        {
            OptionSetValue status = leadPostImage.Contains("statuscode") ? (OptionSetValue)leadPostImage["statuscode"] : null;
            if (status.Value == 3)
            {
                CheckOppAndAccount(leadId);

            }

        }

        private void CheckOppAndAccount(Guid leadId)
        {
            OportunidadeController oportunidadeController = new OportunidadeController(this.Service);
            Entity oportunidade = oportunidadeController.GetOppByLead(leadId);
            EntityReference conta = null;

            if (oportunidade != null)
            {
                conta = oportunidade.Attributes.Contains("parentaccountid") ? (EntityReference)oportunidade["parentaccountid"] : null;
            }

            if (conta != null)
            {
                ContaController contaController = new ContaController(this.Service);
                contaController.IncrementeOrDecrementNumberOfOpp(conta, true);
            }
        }
    }
}
