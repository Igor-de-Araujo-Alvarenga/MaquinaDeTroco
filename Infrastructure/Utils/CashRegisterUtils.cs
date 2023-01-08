using Domain;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    /*
     Classe para manipulações uteis durante o processamento
     */
    public class CashRegisterUtils : ICashRegisterUtils
    {
        private readonly IRepository<Money> _repo;
        public CashRegisterUtils(IRepository<Money> repo)
        {
            _repo = repo;     
        }

        public void Init()
        {
            var listMoney = _repo.GetAll();
            if(listMoney.Count == 0)
            {
                var oneReal = new Money(1.00, 0);
                var fiftyCent = new Money(0.50, 0);
                var twentyFiveCent = new Money(0.25, 0);
                var tenCent = new Money(0.10, 0);
                var fiveCent = new Money(0.05, 0);

                _repo.Create(oneReal);
                _repo.Create(fiftyCent);
                _repo.Create(twentyFiveCent);
                _repo.Create(tenCent);
                _repo.Create(fiveCent);
            }
        }

        public void Reset()
        {
            var allCoin = _repo.GetAll();
            if(allCoin.Count == 0)
            {
                Init();
            }
            var oneReal = _repo.GetById(1);
            var fiftyCent = _repo.GetById(2);
            var twentyFiveCent = _repo.GetById(3);
            var tenCent = _repo.GetById(4);
            var fiveCent = _repo.GetById(5);

            oneReal.Quantity = 0;
            fiftyCent.Quantity = 0;
            twentyFiveCent.Quantity = 0;
            tenCent.Quantity = 0;
            fiveCent.Quantity = 0;

            _repo.Update(oneReal);
            _repo.Update(fiftyCent);
            _repo.Update(twentyFiveCent);
            _repo.Update(tenCent);
            _repo.Update(fiveCent);
        }

        public bool IsValid(InputMoney model)
        {
            if (model.Price == model.Input || model.Price > model.Input)
            {
                return false;
            }

            return true;
        }
    
        public bool VerifyAndInsert(Money model)
        {
            if (model.MoneyValue != 1 &&
                model.MoneyValue != 0.5 &&
                model.MoneyValue != 0.25 &&
                model.MoneyValue != 0.1 &&
                model.MoneyValue != 0.05)
            {
                return false;
            }
            var dbMoney = _repo.GetById(model.Id);

            if(dbMoney.MoneyValue != model.MoneyValue)
            {
                return false;
            }


            dbMoney.Quantity += model.Quantity;

            _repo.Update(dbMoney);
            return true;
        }
    }
}
