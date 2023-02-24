using Academia2023.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Academia2023.Model
{
    public class Conta
    {
        #region PROPS
        public IOrganizationService ServiceClient { get; set; }
        public String LogicalName { get; set; }
        #endregion

        #region CONSTRUTOR
        public Conta(IOrganizationService crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "account";
        }
        public Conta(CrmServiceClient crmServiceClient)
        {
            this.ServiceClient = crmServiceClient;
            this.LogicalName = "account";
        }
        #endregion
        public Entity GetContaById(Guid accountId, string collums)
        {
            return ServiceClient.Retrieve(this.LogicalName, accountId, new ColumnSet(collums));
        }

        private Entity RetrieveOneConta(QueryExpression queryAccount)
        {
            EntityCollection accounts = this.ServiceClient.RetrieveMultiple(queryAccount);

            if (accounts.Entities.Count() > 0)
                return accounts.Entities.FirstOrDefault();
            else
                Console.WriteLine("Nenhuma conta encontrada com esse cnpj");

            return null;
        }
        public void IncrementeOrDecrementNumberOfOpp(EntityReference conta, bool? decrementOrIncrement)
        {
            Entity oppAccount = ServiceClient.Retrieve("account", conta.Id, new ColumnSet("mywork_valortotaldeoportunidades"));
            int numeroOppAccount = oppAccount.Contains("mywork_valortotaldeoportunidades") ? (int)oppAccount["mywork_valortotaldeoportunidades"] : 0;

            if (Convert.ToBoolean(decrementOrIncrement))
            {
                numeroOppAccount += 1;
            }
            else
            {
                numeroOppAccount -= 1;
            }
            oppAccount["mywork_valortotaldeoportunidades"] = numeroOppAccount;
            ServiceClient.Update(oppAccount);
        }

        public void IncrementeOrDecrementNumber(Entity conta, bool? decrementOrIncrement)
        {
            int numeroOppAccount = conta.Contains("mywork_valortotaldeoportunidades") ? (int)conta["mywork_valortotaldeoportunidades"] : 0;

            if (Convert.ToBoolean(decrementOrIncrement))
            {
                numeroOppAccount += 1;
            }
            else
            {
                numeroOppAccount -= 1;
            }
            conta["mywork_valortotaldeoportunidades"] = numeroOppAccount;
            ServiceClient.Update(conta);
        }

    }
}
