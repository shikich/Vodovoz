using System;
using Autofac;
using QS.Navigation;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderMainViewModel<TEntity> : DialogViewModelBase, IAutofacScopeHolder
        where TEntity : OrderBase, new()
    {
        private TEntity entity;
        protected TEntity Entity
        {
            get => entity;
            set => SetField(ref entity, value);
        }
        
        public ILifetimeScope AutofacScope { get; set; }
        
        protected readonly ITdiCompatibilityNavigation tdiCompatibilityNavigation;
        
        protected OrderMainViewModel(
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) : base (tdiCompatibilityNavigation)
        {
            this.tdiCompatibilityNavigation = 
                tdiCompatibilityNavigation ?? throw new ArgumentNullException(nameof(tdiCompatibilityNavigation));
        }
    }
}