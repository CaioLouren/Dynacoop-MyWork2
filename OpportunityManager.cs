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
    public class OpportunityManager : IPlugin
    {
        public IOrganizationService Service { get; set; }
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory serviceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            Service = serviceFactory.CreateOrganizationService(context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity oportunidade = new Entity();
            bool? decrementOrIncrement = null;
            SetVariables(context, out oportunidade, out decrementOrIncrement);
            ExecuteOpportunity(context, oportunidade, decrementOrIncrement);
        }

        private void ExecuteOpportunity(IPluginExecutionContext context, Entity oportunidade, bool? decrementOrIncrement)
        {
            EntityReference accountReference = oportunidade.Contains("parentaccountid") ? (EntityReference)oportunidade["parentaccountid"] : null;

            if (accountReference != null)
            {

                if (context.MessageName == "Update")
                {
                    Entity oportunidadePostImage = (Entity)context.PostEntityImages["PostImage"];
                    EntityReference postAccountReference = (EntityReference)oportunidadePostImage["parentaccountid"];
                    UpdateAccount(true, postAccountReference);
                }
                else if (context.MessageName == "Delete")
                {
                    Entity oportunidadePreImage = (Entity)context.PreEntityImages["PreImage"];
                    EntityReference postAccountReference = (EntityReference)oportunidadePreImage["parentaccountid"];
                    UpdateAccount(false, postAccountReference);
                }
            }
        }

        private Entity UpdateAccount(bool? decrementOrIncrement, EntityReference accountReference)
        {
            ContaController contaController = new ContaController(this.Service);
            Entity oppAccount = contaController.GetContaById(accountReference.Id, "mywork_valortotaldeoportunidades");
            contaController.IncrementeOrDecrementNumber(oppAccount, decrementOrIncrement);
            return oppAccount;
        }

        private void SetVariables(IPluginExecutionContext context, out Entity oportunidade, out bool? decrementOrIncrement)
        {
            if (context.MessageName == "Update")
            {
                oportunidade = (Entity)context.PostEntityImages["PostImage"]; ;
                decrementOrIncrement = true;
            }
            else
            {
                oportunidade = (Entity)context.PreEntityImages["PreImage"];
                decrementOrIncrement = false;
            }
        }
    }
}
