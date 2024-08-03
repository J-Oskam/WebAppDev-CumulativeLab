function loadComments(projectId) {
    $.ajax({
        //route is [area]/[controller]/[action]
        url: '/ProjectManagement/ProjectComments/GetComments?projectId=' + projectId,
        method: 'GET',
        success: function (data) {
            var commentHTML = '';
            for (var i = 0; i < data.length; i++) {
                commentHTML += '<div class="comment">'
                commentHTML += '<p>' + data[i].content + '</p>'
                commentHTML += '<span>Posted on ' + new Date(data[i].CreatedDate).toLocaleString() + '</span>'
                commentHTML += '</div>'
            }
            $('#commentsList').html(commentHTML);
        }
    });
}

$(document).ready(function () {
    var projectId = $("#projectComments input[name='ProjectId']").val();
    loadComments(projectId);

    $('#addCommentForm').submit(function (e) {
        e.preventDefault(); //prevent the default so the backend code isn't called, that is the page isn't refreshed when button is pressed
        var formData = {
            ProjectId: projectId,
            Content: $("#projectComments textarea[name='Content']").val() //.val without data is to get
        };

        $.ajax({
            //route is [area]/[controller]/[action]
            url: '/ProjectManagement/ProjectComments/AddComment',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $("#projectComments textarea[name='Content']").val(''); //clear messag area. .val() with data is to set
                    loadComments(projectId);
                } else {
                    alert(response.messsage);
                }
            },
            error: function (xhr, status, error) {
                alert("Error " + error);
            }
        });
    });
});