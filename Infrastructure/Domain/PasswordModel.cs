using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Domain
{
    public class PasswordModel
    {
        public virtual byte[] Password { get; set; }
        public virtual Guid Id { get; set; }
        public virtual Guid PlayerId { get; set; }
    }
}
