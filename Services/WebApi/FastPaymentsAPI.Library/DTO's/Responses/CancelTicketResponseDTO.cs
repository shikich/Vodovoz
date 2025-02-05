﻿using Gamma.Utilities;

namespace FastPaymentsAPI.Library.DTO_s.Responses
{
	public class CancelTicketResponseDTO
	{
		public CancelTicketResponseDTO()
		{
		}

		public CancelTicketResponseDTO(ResponseStatus status)
		{
			ResponseStatus = status;
			if(status != ResponseStatus.Success)
			{
				ErrorMessage = status.GetEnumTitle();
			}
		}

		public CancelTicketResponseDTO(string errorMessage)
		{
			ResponseStatus = ResponseStatus.UnknownError;
			ErrorMessage = errorMessage;
		}

		public ResponseStatus ResponseStatus { get; }
		public string ErrorMessage { get; }
	}
}
