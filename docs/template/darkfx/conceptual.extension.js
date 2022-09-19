/**
 * This method will be called at the end of exports.transform in conceptual.html.primary.js
 */
exports.postTransform = function (model) {
  console.log("Disable breadcrumb");
  model._disableBreadcrumb = true;
  return model;
}
