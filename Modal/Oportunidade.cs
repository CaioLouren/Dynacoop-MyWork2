using Academia2023.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academia2023.Model
{
    public class Oportunidade
    {
        #region PROPS
        public IOrganizationService ServiceClient { get; set; }
        public String LogicalName { get; set; }
        #endregion

        #region CONSTRUTOR
        public Oportunidade(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "opportunity";
        }
        #endregion
        public Entity GetOppByLead(Guid leadId)
        {
            QueryExpression queryOppLead = new QueryExpression(this.LogicalName);
            queryOppLead.ColumnSet.AddColumns("parentaccountid");
            queryOppLead.Criteria.AddCondition("originatingleadid", ConditionOperator.Equal, leadId);
            return RetrieveOpportunity(queryOppLead);
        }

        public Entity RetrieveOpportunity(QueryExpression queryOpportunity)
        {
            EntityCollection opportunitys = this.ServiceClient.RetrieveMultiple(queryOpportunity);

            if (opportunitys.Entities.Count() > 0)
            {
                return opportunitys.Entities.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }

}
