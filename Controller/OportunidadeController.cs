using Academia2023.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia2023.Controller
{
    public class OportunidadeController
    {
        #region PROPS
        public IOrganizationService ServiceClient { get; set; }
        public Oportunidade Oportunidade { get; set; }
        #endregion

        #region CONSTRUTOR
        public OportunidadeController(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.Oportunidade = new Oportunidade(ServiceClient);
        }
        public OportunidadeController(CrmServiceClient crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.Oportunidade = new Oportunidade(ServiceClient);
        }
        #endregion

        public Entity GetOppByLead(Guid leadId)
        {
            return Oportunidade.GetOppByLead(leadId);
        }
    }
}
