﻿using Cofoundry.Samples.SPASite.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Cofoundry.Samples.SPASite;

[AuthorizeUserArea(MemberUserArea.Code)]
[Route("api/members/current")]
public class CurrentMemberApiController : ControllerBase
{
    private readonly IContentRepository _contentRepository;
    private readonly IApiResponseHelper _apiResponseHelper;

    public CurrentMemberApiController(
        IContentRepository contentRepository,
        IApiResponseHelper apiResponseHelper
        )
    {
        _contentRepository = contentRepository;
        _apiResponseHelper = apiResponseHelper;
    }

    [HttpGet("cats/liked")]
    public async Task<JsonResult> GetLikedCats()
    {
        // Here we get the userId of the currently signed in member. We could have
        // done this in the query handler, but instead we've chosen to keep the query 
        // flexible so it can be re-used in a more generic fashion
        var userContext = await _contentRepository
            .Users()
            .Current()
            .Get()
            .AsUserContext()
            .ExecuteAsync();

        var query = new GetCatSummariesByMemberLikedQuery(userContext.UserId.Value);

        return await _apiResponseHelper.RunQueryAsync(query);
    }
}