"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var rxjs_1 = require("rxjs");
/** Create async observable that emits-once and completes
 *  after a JS engine turn */
function asyncData(data) {
    return rxjs_1.defer(function () { return Promise.resolve(data); });
}
exports.asyncData = asyncData;
/** Create async observable error that errors
 *  after a JS engine turn */
function asyncError(errorObject) {
    return rxjs_1.defer(function () { return Promise.reject(errorObject); });
}
exports.asyncError = asyncError;
//# sourceMappingURL=test-utils.js.map