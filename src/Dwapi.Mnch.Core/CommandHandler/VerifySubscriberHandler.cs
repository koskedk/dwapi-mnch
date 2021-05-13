﻿using System.Threading;
using System.Threading.Tasks;
using Dwapi.Mnch.Core.Command;
using Dwapi.Mnch.Core.Interfaces.Repository;
using Dwapi.Mnch.SharedKernel.Exceptions;
using Dwapi.Mnch.SharedKernel.Model;
using MediatR;

namespace Dwapi.Mnch.Core.CommandHandler
{
    public class VerifySubscriberHandler : IRequestHandler<VerifySubscriber, VerificationResponse>
    {
        private readonly IDocketRepository _repository;

        public VerifySubscriberHandler(IDocketRepository repository)
        {
            _repository = repository;
        }


        public async Task<VerificationResponse> Handle(VerifySubscriber request, CancellationToken cancellationToken)
        {
            var docket = await _repository.FindAsync(request.DocketId);

            if (null == docket)
                throw new DocketNotFoundException(request.DocketId);

            if (!docket.SubscriberExists(request.SubscriberId))
                throw new SubscriberNotFoundException(request.SubscriberId);

            if (docket.SubscriberAuthorized(request.SubscriberId, request.AuthToken))
                    return new VerificationResponse(docket.Name,true);

            throw new SubscriberNotAuthorizedException(request.SubscriberId);
        }
    }
}