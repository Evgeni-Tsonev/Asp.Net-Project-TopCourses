$(document).ready(function () {
    getSubCategoryListByCategoryId();
})
$("#categoryId").change(function () {
    getSubCategoryListByCategoryId();
});
var getSubCategoryListByCategoryId = function () {
    let origin = location.origin;
    let pathName = location.pathname;

    $.ajax({
        url: `${origin}/Course/GetSubCategoryByCategoryId`,
        type: 'GET',
        data: {
            categoryId: $('#categoryId').val(),
        },
        success: function (data) {
            $('#subCategoryId').find('option').remove()
            if (pathName === "/Course") {
                $('#subCategoryId').append('<option value="">All</option>')
            }
            $(data).each(
                function (index, item) {
                    $('#subCategoryId').append('<option value="' + item.id + '">' + item.title + '</option>')
                });
        },
        error: function () {
        }
    });
}