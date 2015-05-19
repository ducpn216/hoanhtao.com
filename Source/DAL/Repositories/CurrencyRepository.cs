using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CurrencyRepository
    {
        public List<Currency> GetList()
        {
            List<Currency> list = DBContext.Context.Currencies.ToList();
            return list;
        }

        public Currency GetByMoney(long money)
        {
            Currency _currency = DBContext.Context.Currencies.Where(x => x.Money == money).FirstOrDefault();
            if (_currency != null)
            {
                return _currency;
            }
            else
            {
                return null;
            }
        }

        public Currency GetById(int currencyId)
        {
            return DBContext.Context.Currencies.Where(x => x.Id == currencyId).FirstOrDefault();
        }

        public int Insert(Currency currency)
        {
            Currency _currency = DBContext.Context.Currencies.Where(x => x.Id == currency.Id && x.Money == currency.Money).FirstOrDefault();
            if (_currency != null)
            {
                DBContext.Context.Currencies.InsertOnSubmit(currency);
                DBContext.Context.SubmitChanges();
                return currency.Id;
            }
            else
            {
                return -1;
            }
        }

        public bool Update(Currency currency)
        {
            Currency item = GetById(currency.Id);
            if (item != null)
            {
                item.Money = currency.Money;
                item.KNB = currency.KNB;

                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int currencyId)
        {
            Currency item = GetById(currencyId);
            if (item != null)
            {
                DBContext.Context.Currencies.DeleteOnSubmit(item);
                DBContext.Context.SubmitChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateOrder(List<int> idList, List<int> moneyList)
        {
            if (idList != null && idList.Count > 0 && moneyList != null && moneyList.Count > 0)
            {
                List<Currency> currencyList = DBContext.Context.Currencies.ToList();

                for (int i = 0; i < idList.Count; i++)
                {
                    Currency currency = currencyList.Where(x => x.Id == idList[i]).FirstOrDefault();
                    if (currency != null)
                    {
                        currency.KNB = moneyList[i];
                    }
                }

                DBContext.Context.SubmitChanges();
                return true;
            }

            return false;
        }
    }
}
