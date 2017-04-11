using System;
using Microsoft.AspNetCore.Http;

namespace CustomerManagement.Api
{
	public static class Extensions
	{
		public static void AddPagination(this HttpResponse response, int totalItems)
		{
			response.Headers.Add("TotalItemsCount", totalItems.ToString());
			response.Headers.Add("access-control-expose-headers", "TotalItemsCount");
		}
	}
}
