using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Galaxy.Gateway.Shared.Queries
{ 
    public class BlackListByIpQuery : IRequest<bool>
    {
        public readonly DateTime? CreationDate;
        public readonly string Ip;
        public BlackListByIpQuery(string ip)
        {
            Ip = ip;
            CreationDate = DateTime.Now;
        }
    }
}
