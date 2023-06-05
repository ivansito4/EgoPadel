using Braintree;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgoPadel.Utilidades.BrainTree
{
	public class BrainTreeGate : IBrainTreeGate
	{
        public BrainTreeSetings _options { get; set; }
        private IBraintreeGateway brainTreeGateway { get; set; }

        public BrainTreeGate(IOptions<BrainTreeSetings> options)
        {
			_options = options.Value;
        }

        public IBraintreeGateway CreateGateWay()
		{
			return new BraintreeGateway(_options.Environment,_options.MerchanId,_options.PublicKey,_options.PrivateKey);
		}

		public IBraintreeGateway GetGateWay()
		{
			if(brainTreeGateway == null)
			{
				brainTreeGateway = CreateGateWay();
			}

            return brainTreeGateway;
		}

        
    }
}
