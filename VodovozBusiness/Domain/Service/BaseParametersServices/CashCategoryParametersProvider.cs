using System;
using Vodovoz.Parameters;
using Vodovoz.Services;

namespace Vodovoz.Domain.Service.BaseParametersServices
{
	public class CashCategoryParametersProvider : ICashCategoryParametersProvider
	{
		private readonly IParametersProvider parametersProvider;

		public CashCategoryParametersProvider(IParametersProvider parametersProvider)
		{
			this.parametersProvider = parametersProvider ?? throw new ArgumentNullException(nameof(parametersProvider));
		}

		public int DefaultIncomeCategory {
			get {
				string parameterId = "default_income_category";
				if(!parametersProvider.ContainsParameter(parameterId)) {
					throw new InvalidProgramException("В параметрах базы не настроена категория " +
						$"прихода по умолчанию ({parameterId}).");
				}
				if(!int.TryParse(parametersProvider.GetParameterValue(parameterId), out int result)) {
					throw new InvalidProgramException("В параметрах базы не корректно настроена категория " +
						$"прихода по умолчанию ({parameterId}).");
				}

				return result;
			}
		}

		public int RouteListClosingIncomeCategory {
			get {
				string parameterId = "routelist_income_category_id";
				if(!parametersProvider.ContainsParameter(parameterId)) {
					throw new InvalidProgramException("В параметрах базы не настроена категория " +
						$"прихода по маршрутному листу по умолчанию ({parameterId}).");
				}
				if(!int.TryParse(parametersProvider.GetParameterValue(parameterId), out int result)) {
					throw new InvalidProgramException("В параметрах базы не корректно настроена категория " +
						$"прихода по маршрутному листу по умолчанию ({parameterId}).");
				}

				return result;
			}
		}
		public int RouteListClosingExpenseCategory {
			get {
				string parameterId = "routelist_expense_category_id";
				if(!parametersProvider.ContainsParameter(parameterId)) {
					throw new InvalidProgramException("В параметрах базы не настроена категория " +
						$"расхода по маршрутному листу по умолчанию ({parameterId}).");
				}
				if(!int.TryParse(parametersProvider.GetParameterValue(parameterId), out int result)) {
					throw new InvalidProgramException("В параметрах базы не корректно настроена категория " +
						$"расхода по маршрутному листу по умолчанию ({parameterId}).");
				}

				return result;
			}
		}


		public int FuelDocumentExpenseCategory {
			get {
				string parameterId = "fuel_expense";
				if(!parametersProvider.ContainsParameter(parameterId)) {
					throw new InvalidProgramException("В параметрах базы не настроена категория " +
						$"расхода для топлива ({parameterId}).");
				}
				if(!int.TryParse(parametersProvider.GetParameterValue(parameterId), out int result)) {
					throw new InvalidProgramException("В параметрах базы не корректно настроена категория " +
						$"расхода  для топлива ({parameterId}).");
				}

				return result;
			}
		}

		public int EmployeeSalaryExpenseCategory {
			get {
				string parameterId = "employee_salary";
				if(!parametersProvider.ContainsParameter(parameterId)) {
					throw new InvalidProgramException("В параметрах базы не настроена категория " +
						$"расхода для аванса ({parameterId}).");
				}
				if(!int.TryParse(parametersProvider.GetParameterValue(parameterId), out int result)) {
					throw new InvalidProgramException("В параметрах базы не корректно настроена категория " +
						$"расхода для аванса ({parameterId}).");
				}

				return result;
			}
		}
	}
}
