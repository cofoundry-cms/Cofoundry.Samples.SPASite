"use strict";

// Set global namespace
var Helper = Helper || {};

Helper.prefilter = function(token) {
    console.log(token);

    $.ajaxPrefilter(function(options, originalOptions, jqXHR) {
        return jqXHR.setRequestHeader('X-XSRF-Token', token);
    });
};