using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Store;
using System;

namespace Vodovoz.Domain.Documents
{
	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Feminine,
		NominativePlural = "сырьё",
		Nominative = "сырьё")]
	public class IncomingWaterMaterial: PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		public virtual IncomingWater Document { get; set; }

		Nomenclature nomenclature;

		[Required (ErrorMessage = "Номенклатура должна быть заполнена.")]
		[Display (Name = "Номенклатура")]
		public virtual Nomenclature Nomenclature {
			get { return nomenclature; }
			set { SetField (ref nomenclature, value, () => Nomenclature);
				if (ConsumptionMaterialOperation != null && ConsumptionMaterialOperation.Nomenclature != nomenclature)
					ConsumptionMaterialOperation.Nomenclature = nomenclature;
			}
		}

		decimal? oneProductAmount;

		[Display (Name = "На один продукт")]
		public virtual decimal? OneProductAmount {
			get { return oneProductAmount; }
			set { SetField (ref oneProductAmount, value, () => OneProductAmount);
				if(oneProductAmount.HasValue && Document != null)
				{
					Amount = OneProductAmount.Value * Document.Amount;
				}
			}
		}

		//FIXME Костыль пока не научим Gtk.Binding работать с нулабле типами
		public virtual decimal OneProductAmountEdited {
			get { return OneProductAmount.HasValue ? OneProductAmount.Value : 0; }
			set { OneProductAmount = (value > 0) ? value : 0; }
		}

		decimal amount;

		[Min (1)]
		[Display (Name = "Количество")]
		public virtual decimal Amount {
			get { return amount; }
			set { SetField (ref amount, value, () => Amount);
				if (ConsumptionMaterialOperation != null && ConsumptionMaterialOperation.Amount != amount)
					ConsumptionMaterialOperation.Amount = amount;
			}
		}

		decimal amountOnSource = 10000000; //FIXME пока не реализуем способ загружать количество на складе на конкретный день

		[Display (Name = "Имеется на складе")]
		public decimal AmountOnSource {
			get { return amountOnSource; }
			set {
				SetField (ref amountOnSource, value, () => AmountOnSource);
			}
		}


		public virtual string Name {
			get { return Nomenclature != null ? Nomenclature.Name : ""; }
		}

		WarehouseMovementOperation consumptionMaterialOperation;

		public WarehouseMovementOperation ConsumptionMaterialOperation {
			get { return consumptionMaterialOperation; }
			set { SetField (ref consumptionMaterialOperation, value, () => ConsumptionMaterialOperation); }
		}

		public IncomingWaterMaterial() {}

		#region Функции

		public void CreateOperation(Warehouse warehouseSrc, DateTime time)
		{
			ConsumptionMaterialOperation = new WarehouseMovementOperation
				{
					WriteoffWarehouse = warehouseSrc,
					Amount = Amount,
					OperationTime = time,
					Nomenclature = Nomenclature
				};
		}

		#endregion
	}
}

