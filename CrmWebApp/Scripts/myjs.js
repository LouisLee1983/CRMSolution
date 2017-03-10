$(document).ready(function () {
    $('#summernote').summernote({
        height: 400
    }).code($("#reportContentDiv").text());
});