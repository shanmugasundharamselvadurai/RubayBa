using Walkland.Core.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Walkland.UI.DataTemplateSelectors
{
    public class TransactionTemplateSelector : DataTemplateSelector
    {
        public DataTemplate CreditTransaction { get; set; }

        public DataTemplate DebitTransaction { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var companyWalletTransaction = item as CompanyWalletTransaction;

            if (companyWalletTransaction.ToWalletId.ToString() == SecureStorage.GetAsync("CommpanyWalletId").Result)
            {
                return CreditTransaction;
            }

            return DebitTransaction;
        }
    }
}