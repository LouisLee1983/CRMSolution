
$(document).on('ready', function () {
    $('#imgUpload').fileinput({
        language: 'zh',
        uploadUrl: '#',
        allowedFileExtensions: ['jpg', 'png', 'gif'],
        showUpload: false,
        fileActionSettings: { showUpload: false }
    });
});