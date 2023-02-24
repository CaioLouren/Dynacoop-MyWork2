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
    public class ContaController
    {
        #region PROPS
        public IOrganizationService ServiceClient { get; set; }
        public Conta Conta { get; set; }
        #endregion

        #region CONSTRUTOR
        public ContaController(IOrganizationService crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.Conta = new Conta(ServiceClient);
        }
        public ContaController(CrmServiceClient crmServiceClient)
        {
            ServiceClient = crmServiceClient;
            this.Conta = new Conta(ServiceClient);
        }
        #endregion
        public Entity GetContaById(Guid accountId, string collums)
        {
            return Conta.GetContaById(accountId, collums);
        }
        public void IncrementeOrDecrementNumberOfOpp(EntityReference conta, bool? decrementOrIncrement)
        {
            Conta.IncrementeOrDecrementNumberOfOpp(conta, decrementOrIncrement);
        }

        public void IncrementeOrDecrementNumber(Entity conta, bool? decrementOrIncrement)
        {
            Conta.IncrementeOrDecrementNumber(conta, decrementOrIncrement);
        }

    }
}
