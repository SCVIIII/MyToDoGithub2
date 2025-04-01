using MyToDo.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyToDo.ViewModels
{
    public class MT1_NavigationViewModel :BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        public readonly IEventAggregator aggregator;


        public MT1_NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
            aggregator=containerProvider.Resolve<IEventAggregator>();
        }
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            

        }

        public void UpdateLoading(bool IsOpen)
        {

            aggregator.MT1_UpdateLoading(new Common.Events.MT1_UpdateModel()
            {
                IsOpen = IsOpen
            });
        }
    }
}
