using PratiBus.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RefreshToken
{
    public class ConsumerIdentity : ValueObject
    {
        private ConsumerIdentity(){}
        public int? DriverId { get; set; }
        public int? CompanyId { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            return [DriverId, CompanyId];
        }
        private ConsumerIdentity(int? userId, int? companyId) 
        {
            DriverId = userId;
            CompanyId = companyId;
        }
        public static ConsumerIdentity CreateConsumerIdentity(int? userId, int? companyId) 
        {
            if(userId is null && companyId is null) throw new ArgumentNullException("Both parameters cannot be null");
            if (userId != null && companyId != null) throw new ArgumentNullException("one of the parameters must be different from null\r\n");
            return new ConsumerIdentity(userId, companyId);
        }
    }
}
