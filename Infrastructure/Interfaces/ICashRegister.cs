using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICashRegister
    {
        IList<Money> GenerateCash(InputMoney input);
        bool VerifyTotalCash(InputMoney input);
    }
}
