"use strict";

// Set global namespace
var Helper = Helper || {};

Helper.prefilter = function() {
    $.ajaxPrefilter(function(options, originalOptions, jqXHR) {
        var token = SPACatsState.csrfToken;
        return jqXHR.setRequestHeader('X-XSRF-Token', token);
    });
};