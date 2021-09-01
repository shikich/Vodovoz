using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.Domain.Store;

namespace Vodovoz.ViewModels.ViewModels.PermissionNode
{
    public class PermissionTypeAllNodeViewModel : WarehousePermissionAllNodeViewModelBase
    {
        private WarehousePermissionsType _warehousePermissionsType;
        public WarehousePermissionsType WarehousePermissionsType
        {
            get => _warehousePermissionsType;
            set => SetField(ref _warehousePermissionsType, value);
        }

        public PermissionTypeAllNodeViewModel(WarehousePermissionsType warehousePermissionsType, IEnumerable<Warehouse> warehouses, WarehousePermissionModelBase warehousePermissionModelBase)
        {
            WarehousePermissionsType = warehousePermissionsType;
            Title = WarehousePermissionsType.GetEnumTitle();
            SubNodeViewModel = new List<WarehousePermissionNodeViewModel>();
            foreach (var warehouse in warehouses)
            {
                var permissionNode = new WarehousePermissionNodeViewModel(warehouse, warehousePermissionsType, warehousePermissionModelBase);
                permissionNode.ItemChangeValue += IsAllPermissionSeted;
                SubNodeViewModel.Add(permissionNode);
            }
        }
        
        public void IsAllPermissionSeted(object sender, EventArgs e)
        {
            var warehousePermissionNodeViewModel = sender as WarehousePermissionNodeViewModel;
            if (warehousePermissionNodeViewModel.UnSubscribe) return;
            UnSetAll = true;
            var collection = SubNodeViewModel.Where(x => x.Warehouse !=
                warehousePermissionNodeViewModel.Warehouse);
            if (collection.All(x => x.PermissionValue == true) && warehousePermissionNodeViewModel.PermissionValue == true)
            {
                PermissionValue = warehousePermissionNodeViewModel.PermissionValue;
            }
            else if (collection.All(x => x.PermissionValue == false) &&
                 warehousePermissionNodeViewModel.PermissionValue == false)
            {
                PermissionValue = warehousePermissionNodeViewModel.PermissionValue;
            }
            else PermissionValue = null;

            UnSetAll = false;
        }
    }
}