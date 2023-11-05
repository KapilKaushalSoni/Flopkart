﻿using CartServices.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartServices.Queries
{
    public record ReadCartProductsQuery(string userId):IRequest<CartViewModelRead>
    {
    }
}
