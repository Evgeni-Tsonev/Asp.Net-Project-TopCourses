$(document).ready(function ShowAddTopicBlank() {
    $("#show").click(function () {
        $("#topics").css("display", "block");
        $("#show").hide();
    });
});

$(document).ready(function Cancel() {
    $("#cancelTopic").click(function () {
        $("#topics").hide();
        $("#show").show();
    });
});

$(document).ready(function AddVideo() {
    $("#addVideo").click(function () {
        let btnValue = $("#addVideo").text();
        console.log(btnValue);
        if (btnValue == "Add Video") {
            $("#addVideo").text("Cancel");
            $("#videoInputs").css("display", "block");
            $("#saveVideo").css("display", "block");
            $("#topicBtns").hide();
        } else {
            $("#addVideo").text("Add Video");
            $("#videoInputs").hide();
            $("#saveVideo").hide();
            $("#topicBtns").show();
        }
    });
});

$(document).ready(function SaveVideo() {
    $("#saveVideo").click(function () {
        let videoTitle = $("#videoTitle").val();
        console.log(videoTitle);

        let videoUrl = $("#videoUrl").val();
        console.log(videoUrl);

        let a = $("<a></a>").text(videoTitle).attr('href', videoUrl);
        console.log(a);

        let li = $("<li></li>").append(a);
        $("#videoList").append(li);

        $("#videoTitle").val("");
        $("#videoUrl").val("");
    });
});

$(document).ready(function () {
    $("#createTopic").click(function () {
        let topicTitle = $("#topicTitle").val();

        let topicDescription = $("#topicDescription").val();

        let videoList = [];

        $('#createdVideos ul li a').each(function (e) {
            var title = $(this).text();
            var href = $(this).attr('href');
            videoList.push({
                title: title,
                videoUrl: href
            });
        });

        let topic = {
            title: topicTitle,
            description: topicDescription,
            videos: videoList
        }

        $.ajax({
            type: "POST",
            url: 'https://localhost:7062/Topic/SectionTest',
            data: JSON.stringify(topic),
            contentType: "application/json; charset=utf-8",
/*            dataType: "json",*/
            success: function (data, state) {
                /*$("#topics-container").html(data);*/
                //console.log(data);
                //console.log(state);
                alert('ajax success');
            },
            error: function (err) {
                console.log(err.text);   // <-- printing error message to console  // <-- printing error message to console
                /*console.log("eror"); */  // <-- printing error message to console  // <-- printing error message to console
            }
        });
    });
});

